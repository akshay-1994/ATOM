using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Core
{
    public class AurigoConfigException : Exception
    {
        public AurigoConfigException(string msg) : base(msg) { }
    }
}
