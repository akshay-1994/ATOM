using System;
using System.Linq;
using OpenQA.Selenium;
using AurigoTest.Toolkit.Common;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using RelevantCodes.ExtentReports;
using System.Data;

namespace AurigoTest.Toolkit.Core
{
    public  abstract partial class AutomationBase<TSelf, V> : SeleniumBase<TSelf>, IDisposable
        where TSelf : AutomationBase<TSelf, V>
        where V : VerifierBase<V, TSelf>
    {
        private bool _isDisposed = false;

        /// <summary>
        /// This can be used to persist some temp data for references
        /// </summary>
        public Dictionary<string, object> AutomationSharedData { get; private set; } = new Dictionary<string, object>();


        public AutomationBase(IDriverLinker parentObject) : base(parentObject)
        {

        }

        public void Dispose()
        {
            if (!_isDisposed)
                this.End_Automation();
        }


        #region Wait code
        //protected T Wait_WithCallback(T refObj, bool isPrimary, By by, int timeoutInSeconds, Action<IWebElement> callback = null)
        //{
        //    var driver = isPrimary ? base.PrimaryDriver : base.IFrameDriver;
        //    DriverHelpers.Wait_WithCallback(driver, by, timeoutInSeconds, callback);

        //    return this as T;
        //}
        public virtual TSelf Wait(int seconds)
        {
            DriverHelpers.WaitForSometime(base.PrimaryDriver, seconds);
            return this as TSelf;
        }

        public TSelf Wait(int seconds, EnumSearchLocation location = EnumSearchLocation.IFrame)
        {
            if (location == EnumSearchLocation.MainWindow)
                DriverHelpers.WaitForSometime(base.PrimaryDriver, seconds);
            else
                DriverHelpers.WaitForSometime(base.IFrameDriver, seconds);
            return this as TSelf;
        }

        private TSelf WaitTill_ElementNotExists_OnMain(By by, int timeoutInSeconds, Action<IWebElement> callback = null)
        {
            DriverHelpers.Wait_WithCallback(base.PrimaryDriver, by, timeoutInSeconds, callback);
            return this as TSelf;
            //return this.Wait_WithCallback(this as T, true, by, timeoutInSeconds, callback);
        }

        private TSelf WaitTill_ElementNotExists_OnIFrame(By by, int timeoutInSeconds, Action<IWebElement> callback = null)
        {
            DriverHelpers.Wait_WithCallback(base.IFrameDriver, by, timeoutInSeconds, callback);
            return this as TSelf;
        }

        public TSelf WaitTill_ElementNotExists(By by, EnumSearchLocation location = EnumSearchLocation.IFrame, int timeoutInSeconds = 2, Action<IWebElement> callback = null)
        {
            if (location == EnumSearchLocation.MainWindow)
                return WaitTill_ElementNotExists_OnMain(by, timeoutInSeconds, callback);
            else
                return WaitTill_ElementNotExists_OnIFrame(by, timeoutInSeconds, callback);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <param name="textToCheck"></param>
        /// <param name="handlerMethod">Reference to the main class, element found, whether success or failed</param>
        /// <returns></returns>
        public TSelf WaitTill_TextToBePresentInElement(By by, string textToCheck, EnumSearchLocation location = EnumSearchLocation.IFrame, int timeoutInSeconds = 2, Action<TSelf, IWebElement, EnumElementCallStatus> handlerMethod = null)
        {
            var driver = base.PrimaryDriver;
            if (location == EnumSearchLocation.IFrame)
                driver = base.IFrameDriver;

            if (timeoutInSeconds <= 0)
            {
                #region when no timeout required
                IWebElement ele = driver.FindElement(by);
                if (ele == null)
                {
                    if (handlerMethod == null)
                        throw new AurigoTestException(this, EnumExceptionType.ElementNotFound, by.ToString());
                    else
                        handlerMethod(this as TSelf, ele, EnumElementCallStatus.ElementNotFound);
                }
                else if (ele.Text == textToCheck)
                {
                    if (handlerMethod != null)
                        handlerMethod(this as TSelf, ele, EnumElementCallStatus.Success);
                }
                else
                {
                    if (handlerMethod == null)
                        throw new AurigoTestException(this, EnumExceptionType.WaitOnElementNotMatched, "values not matching when waiting for TextToBePresentInElement");

                    handlerMethod(this as TSelf, ele, EnumElementCallStatus.TimeOutError);
                }
                #endregion when no timeout required
            }
            else
            {
                #region when time is provided
                IWebElement ele = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds)).Until(ExpectedConditions.ElementExists(by));
                if (ele == null)
                {
                    if (handlerMethod == null)
                        throw new AurigoTestException(this, EnumExceptionType.ElementNotFound, by.ToString());
                    else
                        handlerMethod(this as TSelf, ele, EnumElementCallStatus.ElementNotFound);
                }
                else
                {
                    bool isPresent = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds)).Until(ExpectedConditions.TextToBePresentInElement(ele, textToCheck));

                    if (isPresent)
                        handlerMethod(this as TSelf, ele, EnumElementCallStatus.Success);
                    else
                    {
                        if (handlerMethod == null)
                            throw new AurigoTestException(this, EnumExceptionType.WaitOnElementNotMatched, "values not matching when waiting for TextToBePresentInElement");
                        else
                            handlerMethod(this as TSelf, ele, EnumElementCallStatus.TimeOutError);
                    }
                }

                #endregion when time is provided
            }

            return this as TSelf;
        }

        #endregion Wait code

        public void End_Automation()
        {
            if (!_isDisposed)
            {
                //base.IFrameDriver.Close();
                //base.PrimaryDriver.Close();
                _isDisposed = true;
                try
                {
                    base.IFrameDriver.Quit();
                    base.PrimaryDriver.Quit();
                }
                catch
                {

                }
            }
        }

        public virtual TSelf ExecuteCustomMethod(Action<TSelf> customFunction)
        {
            TSelf obj = this as TSelf;
            customFunction(obj);
            return obj;
        }

        protected virtual void LoginInternal(string userName, string pwd)
        {
            CtrlAppLogin la = new CtrlAppLogin();
            la.LoginWithSuccess(this.PrimaryDriver, userName, pwd);
        }

        /// <summary>
        /// Path must be separated by \
        /// </summary>
        /// <param name="fullPathUnderProjectFolder"></param>
        /// <returns></returns>
        protected void Tree_SelectLeafUnderProjectByPath(string fullPathUnderProjectFolder)
        {
            string[] pathArr = fullPathUnderProjectFolder.Split('\\');

            IWebDriver driver = this.PrimaryDriver;

            var parentTreeNode = TreePanelHelper.Tree_Select_ProjectFolder(driver);

            int ctr = 0;
            while (ctr < pathArr.Length - 1)
            {
                parentTreeNode = TreePanelHelper.Tree_SubFolder_Toggle(driver, parentTreeNode, pathArr[ctr], true);
                ctr++;
            }

            TreePanelHelper.Tree_SelectLeafUnderFolder(driver, parentTreeNode, pathArr[ctr]);

            DriverHelpers.WaitForSometime(driver);

        }

        public void MainMenu_Select(EnumMainMenuItem mainMenuItem)
        {
            IWebDriver driver = this.PrimaryDriver;

            string pageName;

            switch (mainMenuItem)
            {
                default:
                case EnumMainMenuItem.Home: pageName = "Home"; break;
                case EnumMainMenuItem.Project: pageName = "Projects"; break;
                case EnumMainMenuItem.Library: pageName = "Library"; break;
                case EnumMainMenuItem.Administration: pageName = "Administration"; break;
            }

            IWebElement ulist = driver.FindElement(By.Id("MenuTabsUL"));

            string xPath = string.Format("./li/a/span[text()='{0}']", pageName);

            IWebElement spanTag_ForGroup = ulist.FindElement(By.XPath(xPath));

            IWebElement hyperLink_ToGroup = spanTag_ForGroup.FindElement(By.XPath(".."));

            hyperLink_ToGroup.Click();
        }

        protected void LogTestSteps(ExtentTest extentTest, string testStepId, List<TestStepsDto> verificationStepsList)
        {
            if (!verificationStepsList.Any())
                return;

            foreach (var step in verificationStepsList)
            {
                var typeStep = step.GetTranslatedStatusForReports();
                if (typeStep == LogStatus.Fail)
                {
                    if (step.ExceptionRef != null)
                        extentTest.Log(typeStep, step.ExceptionRef);//, step.ExceptionRef);
                    else
                        extentTest.Log(typeStep, step.GetStepNameAndDescription() + step.ErrorMsg);//, step.ExceptionRef);
                }
                else
                    extentTest.Log(typeStep, step.GetStepNameAndDescription());
            }
        }


    }
}
