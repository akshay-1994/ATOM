using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Common
{
    public static class ConfigData
    {
        public static string DriverPathOfChrome { get { return @"D:\DO_NOT_DELETE\CoolApps\POC\chromedriver_win32"; } }
        public static string DriverPathOfIE_64Bit { get { return @"D:\DO_NOT_DELETE\CoolApps\POC\IEDriverServer_x64_2.48.0"; } }
        public static string DriverPathOfIE_32Bit { get { return @"D:\DO_NOT_DELETE\CoolApps\POC\IEDriverServer_Win32_2.48.0"; } }

        public static string IFrameID { get { return @"contentFrame"; } }

    }
}
