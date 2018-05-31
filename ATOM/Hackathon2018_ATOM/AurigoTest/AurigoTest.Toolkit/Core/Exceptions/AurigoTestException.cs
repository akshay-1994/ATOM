using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Core
{
    public class AurigoTestException : Exception
    {
        public IDriverLinker DriverReference { get; private set; }
        public string ScreenshotPath { get; private set; }

        public AurigoTestException(IDriverLinker driverRef) { }
        public AurigoTestException(IDriverLinker driverRef, EnumExceptionType exceptionType, string msg) : base(msg)
        {
            DriverReference = driverRef;

            if (DriverReference != null)
            {
                ScreenshotPath = Helpers.GetImageLogFileWithFullPath();
                //Take the screenshot
                Screenshot image = ((ITakesScreenshot)DriverReference.PrimaryDriver).GetScreenshot();
                //Save the screenshot
                image.SaveAsFile(ScreenshotPath, ScreenshotImageFormat.Png);
            }
        }

        public AurigoTestException(IDriverLinker driverRef, EnumExceptionType exceptionType, string msg, Exception innerException) : base(msg, innerException)
        {
            DriverReference = driverRef;
        }

        public static AurigoTestException AsAssertException(IDriverLinker driverRef, string expectedValue, string actualValue, Exception ex = null)
        {
            return new AurigoTestException(driverRef, EnumExceptionType.AssertException,
                string.Format("Assert failed expected '{0}' (actual '{1}')", expectedValue, actualValue), ex);
        }
    }
}
