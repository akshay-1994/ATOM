using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW
{
    public class FormToasterAccessor : AccessorBase
    {
        public FormToasterAccessor(IDriverLinker driverLinker) : base(driverLinker)
        {
        }

        private IWebElement ToasterObjectInstance
        {
            get
            {
                try
                {
                    By toastMessageClass = By.ClassName("toast-message");
                    //DriverHelpers.WaitForIFrameContent(_refIDriverLinker.IFrameDriver, ConfigData.IFrameID, toastMessageClass, 30);
                    var toasterObject = _refIDriverLinker.IFrameDriver.FindElement(toastMessageClass, 30);
                    return toasterObject;
                }
                catch {
                    return null;
                }
            }
        }

        public string GetToasterMessages()
        {
            var toasterObject = ToasterObjectInstance;
            if (toasterObject == null)
                return "";

            return toasterObject.Text;
        }

        public bool CheckToasterObjectExists()
        {
            return (ToasterObjectInstance != null);
        }

    }
}
