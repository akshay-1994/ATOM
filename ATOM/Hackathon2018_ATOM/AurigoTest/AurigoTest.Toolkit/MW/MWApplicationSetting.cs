using AurigoTest.Toolkit.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW
{
    public class MWApplicationSettingSingleton
    {
        private static object _lockObj = new object();

        private static MWApplicationSettingSingleton _instance = null;

        private MWApplicationSettingSingleton()
        {
            ReloadSettingFromDatabase();
        }

        public static MWApplicationSettingSingleton Instance
        {
            get
            {

                if (_instance == null)
                    lock (_lockObj)
                        if (_instance == null)
                            _instance = new MWApplicationSettingSingleton();

                return _instance;
            }
        }

        public void ReloadSettingFromDatabase()
        {

            DataTable dt = DBHelper.Execute_SelectStatement("SELECT * FROM AMP3AppSettings");

            foreach (DataRow dr in dt.Rows)
            {
                string settingName = dr["SettingName"].ToString();
                string settingValue = dr["SettingValue"].ToString();
                switch (settingName)
                {

                    case "FORMAT_AMOUNT": FORMAT_AMOUNT = settingValue; break;
                    case "FORMAT_UNIT_PRICE": FORMAT_UNIT_PRICE = settingValue; break;
                    case "FORMAT_QUANTITY": FORMAT_QUANTITY = settingValue; break;
                    case "FORMAT_DATE": FORMAT_DATE = settingValue; break;
                    case "FORMAT_TIME": FORMAT_TIME = settingValue; break;
                    case "FORMAT_DATETIME": FORMAT_DATETIME = settingValue; break;
                    case "CurrentTimeZone": CurrentTimeZone = settingValue; break;
                    case "LoginMode": LoginMode = settingValue; break;
                    case "Culture": LoginMode = Culture; break;
                }
            }
        }

        public string DateTimeFormat { get; private set; }

        public string FORMAT_AMOUNT { get; private set; }
        public string FORMAT_UNIT_PRICE { get; private set; }
        public string FORMAT_QUANTITY { get; private set; }
        public string FORMAT_DATE { get; private set; }
        public string FORMAT_TIME { get; private set; }
        public string FORMAT_DATETIME { get; private set; }
        public string CurrentTimeZone { get; private set; }
        public string LoginMode { get; private set; }

        public string Culture { get; private set; }
    }
}
