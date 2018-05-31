using AurigoTest.Toolkit.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit
{
    public class RuntimeAppConfig
    {
        private static object _lockObj = new object();

        private static RuntimeAppConfig _instance = null;

        private RuntimeAppConfig()
        {

        }

        public static RuntimeAppConfig Instance
        {
            get
            {

                if (_instance == null)
                    lock (_lockObj)
                        if (_instance == null)
                            _instance = new RuntimeAppConfig();

                return _instance;
            }
        }
        
        public string URL { get { return ConfigurationManager.AppSettings["URL"].Trim(); } }
        public string Username { get { return ConfigurationManager.AppSettings["Username"].Trim(); } }
        public string Password { get { return ConfigurationManager.AppSettings["Password"].Trim(); } }
        public string ConnectionString { get { return ConfigurationManager.AppSettings["SiteConnectionString"].Trim(); } }
        public string DatabaseSnapshotPath
        {
            get
            {
                if (ConfigurationManager.AppSettings["DatabaseSnapshotPath"] != null)
                    return ConfigurationManager.AppSettings["DatabaseSnapshotPath"].Trim();
                else
                    return string.Empty;
            }
        }

        public bool IsReportTrackingAtMethod
        {
            get
            {
                if (ConfigurationManager.AppSettings["IsReportTrackingAtMethod"] != null)
                    return ConfigurationManager.AppSettings["IsReportTrackingAtMethod"].ToLower().Trim() == "false";
                else
                    return true;
            }
        }
    }
}
