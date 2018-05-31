using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Core
{
    public abstract class SeleniumBase<TSelf> : IDriverLinker where TSelf : IDriverLinker
    {
        bool isIFrameUsed = false;
        public string TestID { get; set; }

        public string TestSummary { get; set; }

        protected IDriverLinker ParentObject { get; private set; }

        private IWebDriver _primaryDriver;
        public IWebDriver PrimaryDriver
        {
            get
            {
                if (isIFrameUsed)
                {
                    _primaryDriver = _primaryDriver.SwitchTo().ParentFrame();
                    isIFrameUsed = false;
                }

                return _primaryDriver;
            }
            set
            {
                _primaryDriver = value;
            }
        }

        private IWebDriver _iFrameDriver = null;
        public IWebDriver IFrameDriver
        {
            get
            {
                if (_iFrameDriver == null && PrimaryDriver != null)
                {
                    _iFrameDriver = PrimaryDriver.SwitchTo().Frame(ConfigData.IFrameID);
                }

                isIFrameUsed = true;
                return _iFrameDriver;
            }
            set { _iFrameDriver = value; }
        }


        public SeleniumBase(IDriverLinker parentObject)
        {
            this.ParentObject = parentObject;
            if (parentObject != null)
                this.Sync_IDriverLinker_ToThis_Class(parentObject);
        }


        public void IFrameDriver_Flush()
        {
            _iFrameDriver = null;
        }
        private void Sync_IDriverLinker_ToThis_Class(IDriverLinker src)
        {
            this.TestID = src.TestID;
            this.PrimaryDriver = src.PrimaryDriver;
            this.IFrameDriver = null; //this will make sure when it is accessed next the latest iframe ref is available
        }


        //http://stackoverflow.com/questions/11961178/finding-an-element-by-partial-id-with-selenium-in-c-sharp
        protected static string GetXpathStringForIdEndsWith(string tagName, string endStringOfControlId)
        {
            return "//" + tagName + "[substring(@id, string-length(@id)- string-length(\"" + endStringOfControlId + "\") + 1 )=\"" + endStringOfControlId + "\"]";
        }

        //public void BackUpAndSwitchDriverTo(IWebDriver)

        protected string Helper_GetActualTestName(string str)
        {
            return this.TestID + " - " + str;
        }


        #region Methods : Public

        public void GoTo_URL(string url)
        {
            this.PrimaryDriver.Url = url;
            this.PrimaryDriver.Navigate().Refresh();

            //this.PrimaryDriver.Navigate().GoToUrl(url);//.Refresh();
        }

        public void IFrame_GoTo_URL(string url)
        {
            this.IFrameDriver.Url = url;
            this.IFrameDriver.Navigate().Refresh();
        }



        #endregion Methods : Public

    }
}
