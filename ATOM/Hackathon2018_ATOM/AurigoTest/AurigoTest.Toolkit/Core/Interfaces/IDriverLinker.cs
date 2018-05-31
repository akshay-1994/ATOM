using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Core
{
    public interface IDriverLinker
    {
        //IWebDriver CurrentDriver { get; set; }


        string TestID { get; set; }
        string TestSummary { get; set; }

        IWebDriver PrimaryDriver { get; set; }

        IWebDriver IFrameDriver { get; }

        void IFrameDriver_Flush();
    }
}
