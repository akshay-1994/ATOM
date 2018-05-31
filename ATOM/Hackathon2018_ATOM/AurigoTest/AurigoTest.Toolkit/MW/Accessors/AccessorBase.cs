using AurigoTest.Toolkit.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW
{
    /// <summary>
    /// This class mustbe inherited by all accessor class
    /// </summary>
    public abstract class AccessorBase
    {
        protected IDriverLinker _refIDriverLinker { get; set; } = null;

        public AccessorBase(IDriverLinker driverLinker)
        {
            this._refIDriverLinker = driverLinker;
        }
    }
}
