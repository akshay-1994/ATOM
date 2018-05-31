using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurigo.Atom.Generator.Core.Helpers
{
    public static class FileHelper
    {
        public static string GetFolderPathWithCurrentTimeStamp(string solutionName, out string dateTimeStampUsed)
        {
            dateTimeStampUsed = DateTime.Now.ToString("yyyyMMddHHmmss");
            return solutionName + "_" + dateTimeStampUsed;
        }

        public static string GetValidFileName(string currentName)
        {
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                currentName = currentName.Replace(c, '_');
            }

            return currentName;
        }
    }
}