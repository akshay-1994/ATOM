using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Aurigo.Atom.Common.Helpers
{
    public class DbTypeParser
    {
        public static int GetColumnLength(string dbType)
        {
            int columnLength = 1;

            if (string.IsNullOrEmpty(dbType))
                return columnLength;

            var result = Regex.Match(dbType, @"\(([^)]*)\)").Groups[1].Value;

            if (!int.TryParse(result, out columnLength))
            {
                if (result.Equals("max", StringComparison.InvariantCultureIgnoreCase))
                    columnLength = 4000;
            }

            return columnLength;
        }
    }
}