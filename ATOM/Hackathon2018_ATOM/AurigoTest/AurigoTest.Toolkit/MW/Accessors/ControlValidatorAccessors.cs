using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AurigoTest.Toolkit.Core;
using OpenQA.Selenium;
using AurigoTest.Toolkit.Common;

namespace AurigoTest.Toolkit.MW.Accessors
{
    public class ControlValidatorAccessor : AccessorBase
    {
        public ControlValidatorAccessor(IDriverLinker driverLinker) : base(driverLinker)
        {
        }

        private IWebElement GetSpannerObjectInstance(string controlName)
        {
            try
            {
                By spanMessage = By.XPath("//table[@formdesignername='" + controlName + "']/tbody/tr/td[3]/span");

                var spannerObject = _refIDriverLinker.IFrameDriver.FindElement(spanMessage, 30);
                return spannerObject;
            }
            catch {
                return null;
            }
        }

        public string GetSpannerMessages(string controlName)
        {
            var spannerObject = GetSpannerObjectInstance(controlName);
            if (spannerObject == null)
                return "";

            return spannerObject.Text;
        }

        public bool CheckIfSpanExists(string controlName)
        {
            var spannerObject = GetSpannerObjectInstance(controlName);
            return (spannerObject != null);
        }
    }
}
