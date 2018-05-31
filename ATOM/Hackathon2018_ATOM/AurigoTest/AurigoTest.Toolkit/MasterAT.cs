using AurigoTest.Toolkit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit
{
    public abstract class MasterAT
    {
        public IDriverLinker DriverLinker { get; private set; }
        public MasterAT(IDriverLinker driverLinker)
        {
            DriverLinker = driverLinker;
        }

        public abstract void Start();
    }
}
