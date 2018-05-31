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
    public class RibbonBarAccessor:AccessorBase
    {
        public RibbonBarAccessor(IDriverLinker driverLinker) : base(driverLinker)
        {
            this._refIDriverLinker = driverLinker;
        }

        public void Click_New_Button(string optionalButtonId = null)
        {
            
            _refIDriverLinker.IFrameDriver_Flush();
            var ribbonElement = _refIDriverLinker.IFrameDriver.FindElement(By.Id("MainToolBar_upToolbar"));
            var newBtn = ribbonElement.FindElement(By.Id(optionalButtonId = null ?? "lnkNew"));

            newBtn.Click();
        }

        public void Click_Save_Button(string optionalButtonId = null)
        {
            _refIDriverLinker.IFrameDriver_Flush();
            var ribbonElement = _refIDriverLinker.IFrameDriver.FindElement(By.Id("MainToolBar_upToolbar"));
            var newBtn = ribbonElement.FindElement(By.Id(optionalButtonId = null ?? "lnkSave"));

            newBtn.Click();
        }

        public void Click_CancelSave_Button(string optionalButtonId = null)
        {
            _refIDriverLinker.IFrameDriver_Flush();

            var ribbonElement = _refIDriverLinker.IFrameDriver.FindElement(By.Id("MainToolBar_upToolbar"));
            var newBtn = ribbonElement.FindElement(By.Id(optionalButtonId = null ?? "lnkCancel"));

            newBtn.Click();
        }

        public void Click_Edit_Button(string optionalButtonId = null)
        {
            _refIDriverLinker.IFrameDriver_Flush();

            var ribbonElement = _refIDriverLinker.IFrameDriver.FindElement(By.Id("MainToolBar_upToolbar"));
            var newBtn = ribbonElement.FindElement(By.Id(optionalButtonId = null ?? "lnkEdit"));

            newBtn.Click();
        }
    }
}
