using AurigoTest.Toolkit.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AurigoTest.Toolkit.Core
{
    public static class DriverHelpers
    {
        public static string _DriverPathOfChrome = null;
        public static string DriverPathOfChrome
        {
            get
            {
                if (_DriverPathOfChrome == null)
                {
                    var expectedPath = Path.Combine(AppContext.BaseDirectory, "DriverRepository/chromedriver.exe");
                    if (File.Exists(expectedPath))
                        _DriverPathOfChrome = Path.Combine(AppContext.BaseDirectory, "DriverRepository");
                    else
                        _DriverPathOfChrome = ConfigurationManager.AppSettings.Get("driverPathChrome");
                }

                return _DriverPathOfChrome ?? @"chromedriver_win32";
            }
        }
        public static string DriverPathOfIE_64Bit { get { return @"D:\DO_NOT_DELETE\CoolApps\POC\IEDriverServer_x64_2.48.0"; } }
        public static string DriverPathOfIE_32Bit { get { return @"D:\DO_NOT_DELETE\CoolApps\POC\IEDriverServer_Win32_2.48.0"; } }

        public static IWebDriver CreateWindow(BrowserType browserType = BrowserType.Chrome, bool isAutoLogin = false)
        {
            IWebDriver driver = null;
            switch (browserType)
            {
                case BrowserType.Chrome:
                    {
                        var driverService = ChromeDriverService.CreateDefaultService(DriverPathOfChrome);

                        var chromeOptions = new ChromeOptions();
                        driverService.Port = 8777;
                        driver = new ChromeDriver(driverService, chromeOptions, TimeSpan.FromMinutes(5));
                    }
                    break;
                case BrowserType.IE:
                    {
                        var driverService = InternetExplorerDriverService.CreateDefaultService(DriverPathOfIE_32Bit);

                        //DesiredCapabilities capabilities = DesiredCapabilities.internetExplorer();
                        //capabilities.set(InternetExplorerDriver.INTRODUCE_FLAKINESS_BY_IGNORING_SECURITY_DOMAINS,
                        //true);
                        //WebDriver driver = new InternetExplorerDriver(capabilities);

                        var ieOptions = new InternetExplorerOptions();
                        ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                        driver = new InternetExplorerDriver(driverService, ieOptions);

                    }
                    break;
            }


            return driver;
        }

        public static void WaitForPageLoaded(IWebDriver driver)
        {
            Func<IWebDriver, Boolean> expectation = dd =>
            {
                return ((IJavaScriptExecutor)dd).ExecuteScript("return document.readyState").Equals("complete");
            };

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            try
            {
                wait.Until(expectation);
            }
            catch (Exception error)
            {
                throw new Exception("Timeout waiting for Page Load Request to complete.");
            }
        }

        //private static Func<IWebDriver, bool> WaitUntilFrameLoadedAndSwitchToIt()
        //{
        //    string iFrameName = ''
        //    return (driver) =>
        //    {
        //        try
        //        {
        //            driver.SwitchTo().Frame(iFrameName);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }

        //        return true;
        //    };
        //}

        public static void WaitForIFrameContent(IWebDriver driver, string iFrameName, string optionalElementToWaitInsideFrame = "", int timeoutPeriodInSeconds = 60)
        {
            Func<IWebDriver, bool> expectation = dd =>
            {

                try
                {
                    //Step 1:
                    var driverFrame = dd.SwitchTo().Frame(iFrameName);

                    //Step 2:

                    WebDriverWait waitInside = new WebDriverWait(driverFrame, new TimeSpan(0, 0, timeoutPeriodInSeconds));
                    waitInside.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id(optionalElementToWaitInsideFrame)));

                }
                catch (Exception)
                {
                    return false;
                }


                return true;
            };

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutPeriodInSeconds));
            try
            {
                wait.Until(expectation);
            }
            catch (Exception error)
            {
                throw new Exception("Timeout waiting for Page Load Request to complete.");
            }

            driver.SwitchTo().DefaultContent();
        }

        public static void WaitForIFrameContent(IWebDriver driver, string iFrameName, By searchElement, int timeoutPeriodInSeconds = 60)
        {
            Func<IWebDriver, bool> expectation = dd =>
            {

                try
                {
                    //Step 1:
                    var driverFrame = dd.SwitchTo().Frame(iFrameName);

                    //Step 2:

                    WebDriverWait waitInside = new WebDriverWait(driverFrame, new TimeSpan(0, 0, timeoutPeriodInSeconds));
                    waitInside.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(searchElement));

                }
                catch (Exception)
                {
                    return false;
                }


                return true;
            };

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutPeriodInSeconds));
            try
            {
                wait.Until(expectation);
            }
            catch (Exception error)
            {
                throw new Exception("Timeout waiting for Page Load Request to complete.");
            }

            driver.SwitchTo().DefaultContent();
        }

        public static string WaitForAnyOfTheIFrameContents(IWebDriver driver, string iFrameName, List<string> controlsToFind, int timeoutPeriodInSeconds = 60)
        {
            string controlThatWasFound = string.Empty;
            Func<IWebDriver, string> expectation = dd =>
            {

                try
                {
                    //Step 1:
                    var driverFrame = dd.SwitchTo().Frame(iFrameName);

                    //Step 2:

                    foreach (string controlToFind in controlsToFind)
                    {
                        try
                        {
                            WebDriverWait waitInside = new WebDriverWait(driverFrame, new TimeSpan(0, 0, timeoutPeriodInSeconds));
                            waitInside.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id(controlToFind)));
                            return controlToFind;
                        }
                        catch
                        {
                            //Find next element
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }


                return null;
            };

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutPeriodInSeconds));
            try
            {
                controlThatWasFound = wait.Until(expectation);
            }
            catch (Exception error)
            {
                throw new Exception("Timeout waiting for Page Load Request to complete.");
            }

            //driver.SwitchTo().DefaultContent();
            return controlThatWasFound;
        }

        public static void WaitForSometime(IWebDriver driver, int seconds = 1)
        {
            WaitWithInterval(driver, seconds * 1000, 2);
            //driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, seconds));
        }

        public static void WaitWithInterval(IWebDriver driver, double delayInMilliSeconds, double interval)
        {
            // Causes the WebDriver to wait for at least a fixed delay
            var now = DateTime.Now;
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(delayInMilliSeconds));
            wait.PollingInterval = TimeSpan.FromMilliseconds(interval);
            wait.Until(wd => (DateTime.Now - now) - TimeSpan.FromMilliseconds(delayInMilliSeconds) > TimeSpan.Zero);
        }

        public static object RunJavascript(this IWebDriver driver, string script, params object[] args)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(script, args);
        }

        public static object RunJavascript_Async(this IWebDriver driver, string script, params object[] args)
        {
            return ((IJavaScriptExecutor)driver).ExecuteAsyncScript(script, args);
        }

        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }

        public static void Wait_WithCallback(IWebDriver driver, By by, int timeoutInSeconds, Action<IWebElement> callback = null)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                if (callback != null)
                    callback.Invoke(wait.Until(drv => drv.FindElement(by)));
            }

            if (callback != null)
                callback.Invoke(driver.FindElement(by));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="driver"></param>
        /// <param name="condition">use ExpectedConditions...</param>
        /// <param name="timeoutInSeconds"></param>
        /// <param name="callback"></param>
        public static void Wait_Till<TResult>(IWebDriver driver, Func<IWebDriver, TResult> condition, int timeoutInSeconds, Action<TResult> callback = null)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));//.Until(ExpectedConditions.ElementExists((By.Id(login))));
                //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                if (callback != null)
                    callback.Invoke(wait.Until(condition));
            }

            if (callback != null)
                callback.Invoke(condition(driver));
        }

        public static void Blur(this IWebElement element, IWebDriver driver)
        {
            //Src: http://stackoverflow.com/questions/7795084/selenium-rc-fireevent-not-working-in-c-sharp
            try
            {
                driver.RunJavascript(
                    @"var fireOnThis = arguments[0];
             fireOnThis.scrollIntoView(false);
             var evt = 'blur';      
             if( document.createEvent ) {
             var evObj = document.createEvent('MouseEvents');
             evObj.initEvent(evt, true, false);
             fireOnThis.dispatchEvent(evObj);
             } else if (document.createEventObject) {
            fireOnThis.fireEvent('on'+evt);
        }", element);
            }
            catch
            { }
        }

    }
}
