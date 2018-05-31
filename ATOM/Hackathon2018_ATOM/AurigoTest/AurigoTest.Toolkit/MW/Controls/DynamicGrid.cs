using AurigoTest.Toolkit.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW.Controls
{
    public class DynamicGrid
    {
        public static DynamicGrid AddRecord(IWebDriver iFrameDriver, string dynamicGridName, GenericFormPage parentFormReference)
        {
            //Get div tab with property   dynamicgridreference="TableDummy"
            //var css = $"div[dynamicgridreference='{dynamicGridName}'] [class='RadGrid']";
            var css = $"div[id $= '_{dynamicGridName}_id']";

            var ele_DynamicGridRef = iFrameDriver.FindElement(By.CssSelector(css));

            var ele_tableOFButtons = ele_DynamicGridRef.FindElement(By.XPath("following-sibling::table")); //("following-sibling::table[@class='some-class']")

            var btnAdd = ele_tableOFButtons.FindElement(By.XPath("//input[@type='submit'][@value='Add']"));

            btnAdd.Click();

            //must wait till dialog box opens

            //when popup is visible
            //class="ui-dialog"

            return new DynamicGrid(iFrameDriver, dynamicGridName, parentFormReference);
        }

        public static DynamicGrid EditRecord_ByRowIndex(IWebDriver iFrameDriver, string dynamicGridName, GenericFormPage parentFormReference)
        {
            throw new NotImplementedException();
            return new DynamicGrid(iFrameDriver, dynamicGridName, parentFormReference);
        }

        public static DynamicGrid DeleteRecord_ByRowIndex(IWebDriver iFrameDriver, string dynamicGridName, GenericFormPage parentFormReference)
        {
            throw new NotImplementedException();
            return new DynamicGrid(iFrameDriver, dynamicGridName, parentFormReference);
        }

        public IWebDriver IFrameDriver { get; private set; }
        public string DynamicGridName { get; private set; }
        public GenericFormPage ParentFormReference { get; private set; }

        public DynamicGrid(IWebDriver iFrameDriver, string dynamicGridName, GenericFormPage parentFormReference)
        {
            this.IFrameDriver = iFrameDriver;
            this.DynamicGridName = dynamicGridName;
            this.ParentFormReference = parentFormReference;
        }

        public DynamicGrid SetTextbox(string fieldName, string value)
        {
            this.IFrameDriver.RunJavascript($"xmlForm.setControlValue('{fieldName}','{DynamicGridName}', '{value}'); return '0';");
            return this;
        }

        public GenericFormPage Save_DynamicGridData()
        {
            var ele_popup_EditDynaGrid = this.IFrameDriver.FindElement(By.Id($"{DynamicGridName}_EditTemplate")); //format : TableDummy_EditTemplate

            var btnSave = ele_popup_EditDynaGrid.FindElement(By.CssSelector("input[type='submit'][value='Save']"));

            btnSave.Click();

            DriverHelpers.WaitForSometime(this.IFrameDriver, 2);
            
            return this.ParentFormReference;
        }
        public GenericFormPage Cancel_DynamicGridData(string fieldName, string value)
        {
            //this.IFrameDriver.RunJavascript($"xmlForm.setControlValue('{fieldName}','{DynamicGridName}', '{value}'); return '0';");
            return this.ParentFormReference;
        }
    }
}
