using System;
using OpenQA.Selenium;
using AurigoTest.Toolkit.Core;

namespace AurigoTest.Toolkit.Common
{
    public class CtrlAppLogin //: AutomationBase
    {
        public void LoginWithSuccess(IWebDriver driver, string userName, string password)
        {
            driver.Url = UrlConstants.LoginUrl;
            DriverHelpers.WaitForPageLoaded(driver);

            //driver.Navigate().GoToUrl(AurigoFramework.SuperHelperObject.MasterWorksURL);//.SwitchTo().DefaultContent();

            driver.FindElement(By.Id("txtUserID")).SendKeys(userName);

            driver.FindElement(By.Id("txtPassword")).SendKeys(password);

            driver.FindElement(By.Id("btnLogin")).Click();


            if (!driver.Url.StartsWith(UrlConstants.SuccessLoginURL))
                throw new Exception("Login Failed");

        }
    }
}
