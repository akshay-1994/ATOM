using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Common
{
    public static class AurigoAppSettings
    {
        public static bool IsReportTrackingAtMethod
        {
            get
            {
                return RuntimeAppConfig.Instance.IsReportTrackingAtMethod;
            }
        }

        public static string DatabaseSnapshotPath
        {
            get
            {
                return RuntimeAppConfig.Instance.DatabaseSnapshotPath;
            }
        }

        public static string ConnectionStringSource { get;set; }
    }
}
