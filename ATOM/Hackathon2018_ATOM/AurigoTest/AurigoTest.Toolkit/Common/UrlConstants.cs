using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Common
{
    public class UrlConstants
    {
        public static string SiteUrl { get; set; }

        public static string ModuleUploaderUrl
        {
            get
            {
                return UrlConstants.SiteUrl + "/Default.aspx#/Modules/MODMGMT/NewModule.aspx";
            }
        }

        public static string LoginUrl
        {
            get
            {
                return UrlConstants.SiteUrl + "/Modules/USRMGMT/Login.aspx";
            }
        }
        public static string SuccessLoginURL
        {
            get
            {
                return UrlConstants.SiteUrl + "/Default.aspx";
            }
        }


    }
}
