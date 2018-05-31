using AurigoTest.Toolkit.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Core
{
    public class ElementManipulator
    {
        public IWebDriver Driver { get; set; }
        public ElementManipulator(IWebDriver driver)
        {
            Driver = driver;
        }

        #region Get elements

        public IWebElement GetEle_HiddenInput_IdEndsWith(string prefixIdSignature)
        {
            IWebElement targetElement = Driver.FindElement(By.XPath("//input[@type='hidden'][substring(@id, string-length(@id) - string-length('" + prefixIdSignature + "') +1) = '" + prefixIdSignature + "']"));
            //IWebElement targetElement = Driver.FindElement(By.XPath("//input[contains(@id, \"" + prefixIdSignature + "\")]"));
            return targetElement;
        }

        public IWebElement GetEle_Textarea_IdEndsWith(string prefixIdSignature)
        {
            IWebElement targetElement = Driver.FindElement(By.XPath("//textarea[substring(@id, string-length(@id) - string-length('" + prefixIdSignature + "') +1) = '" + prefixIdSignature + "']"));
            //To get the ends with we need to use substring as chrome does not support endswith //substring(@id, string - length(@id) - string - length('register') + 1) = 'register'

            //IWebElement targetElement = Driver.FindElement(By.XPath("//input[@type=\"text\"][contains(@id, \"" + prefixIdSignature + "\")]"));
            return targetElement;
        }

        public IWebElement GetEle_Textbox_IdEndsWith(string prefixIdSignature)
        {
            IWebElement targetElement = Driver.FindElement(By.XPath("//input[@type='text'][substring(@id, string-length(@id) - string-length('" + prefixIdSignature + "') +1) = '" + prefixIdSignature + "']"));
            //To get the ends with we need to use substring as chrome does not support endswith //substring(@id, string - length(@id) - string - length('register') + 1) = 'register'

            //IWebElement targetElement = Driver.FindElement(By.XPath("//input[@type=\"text\"][contains(@id, \"" + prefixIdSignature + "\")]"));
            return targetElement;
        }
        /// <summary>
        /// Get by exact id
        /// </summary>
        /// <param name="exactId"></param>
        /// <returns></returns>
        public IWebElement GetEle_Textbox(string exactId)
        {
            //IWebElement targetElement = Driver.FindElement(By.XPath("//input[@type='text'][substring(@id, string-length(@id) - string-length('" + prefixIdSignature + "') +1) = '" + prefixIdSignature + "']"));
            //substring(@id, string - length(@id) - string - length('register') + 1) = 'register'

            IWebElement targetElement = Driver.FindElement(By.XPath("//input[@type=\"text\"][@id=\"" + exactId + "\"]"));//
            return targetElement;
        }

        public SelectElement GetEle_Combobox_IdEndsWith(string prefixIdSignature)
        {
            IWebElement targetElement = Driver.FindElement(By.XPath("//select[contains(@id, \"" + prefixIdSignature + "\")]"));

            var selectElement = new SelectElement(targetElement);

            return selectElement;

            //selectElement.
            ////select by value
            //selectElement.SelectByValue("Jr.High");
            //// select by text
            //selectElement.SelectByText("HighSchool");
        }

        public string GetText_Textarea_IdEndsWith(string prefixIdSignature)
        {
            return GetEle_Textarea_IdEndsWith(prefixIdSignature).Text;// = .GetAttribute("value");
        }

        public string GetText_Textbox(string exactId)
        {
            return GetEle_Textbox(exactId).GetAttribute("value");
        }

        public string GetText_Textbox_IdEndsWith(string prefixIdSignature)
        {
            return GetEle_Textbox_IdEndsWith(prefixIdSignature).GetAttribute("value");
        }

        public string GetText_Combobox_IdEndsWith(string prefixIdSignature)
        {
            return GetEle_Combobox_IdEndsWith(prefixIdSignature).SelectedOption.Text;
        }

        #endregion Get elements

        #region Get Elements : Non input
        public IWebElement GetEle_SpanTag_IdEndsWith(string prefixIdSignature)
        {
            IWebElement targetElement = Driver.FindElement(By.XPath("//span[substring(@id, string-length(@id) - string-length('" + prefixIdSignature + "') +1) = '" + prefixIdSignature + "']"));
            //IWebElement targetElement = Driver.FindElement(By.XPath("//input[contains(@id, \"" + prefixIdSignature + "\")]"));
            return targetElement;
        }

        #endregion Get Elements : Non input


        #region Set elements data
        public void SetText_Textbox(string exactId, string text)
        {
            IWebElement targetElement = GetEle_Textbox(exactId);
            targetElement.SendKeys(text);
        }


        public void SetText_Textbox_IdEndsWith(string prefixIdSignature, string text)
        {
            IWebElement targetElement = GetEle_Textbox_IdEndsWith(prefixIdSignature);
            targetElement.SendKeys(text);
        }

        public void SetText_Textarea_IdEndsWith(string prefixIdSignature, string text)
        {
            IWebElement targetElement = GetEle_Textarea_IdEndsWith(prefixIdSignature);
            targetElement.SendKeys(text);
        }

        public void SetText_Combobox_IdEndsWith(string prefixIdSignature, string text)
        {
            SelectElement targetElement = GetEle_Combobox_IdEndsWith(prefixIdSignature);
            if (text == null)
                targetElement.DeselectAll();
            else
                targetElement.SelectByText(text);
        }

        public void SetValue_Combobox_IdEndsWith(string prefixIdSignature, string value)
        {
            SelectElement targetElement = GetEle_Combobox_IdEndsWith(prefixIdSignature);
            if (value == null)
                targetElement.DeselectAll();
            else
                targetElement.SelectByValue(value);
        }

        public void SetIndex_Combobox_IdEndsWith(string prefixIdSignature, int index)
        {
            SelectElement targetElement = GetEle_Combobox_IdEndsWith(prefixIdSignature);
            if (index < 0)
                targetElement.DeselectAll();
            else
                targetElement.SelectByIndex(index);
        }

        #endregion Set elements data



        #region INfrangistic control helper

        public  DateTime? GetDate_For_WebDateChooser_Using_IdEndsWith(string ctrl_id)
        {
            const string postfix_String = "_input";
            string generatedId = ctrl_id + postfix_String;
            var webEle = this.GetEle_Textbox_IdEndsWith(generatedId);

            string fullId = webEle.GetAttribute("id");//this id will be used to get control reference for WebDateChooser

            string requiredControlId = fullId.Remove(fullId.Length - postfix_String.Length);


            this.Driver.RunJavascript(CodeInjectionConstants.DateFormatMethod);

            //this.Driver.IFrameDriver_Flush();

            /*
             * $find('C1_ERP_CC_BODY_wdcDateCreated')
             * var date = new Date(dateText); dp.set_value(date);
             */
            string valueFromDateTimeObject = (string)this.Driver.RunJavascript(string.Format("return getFormattedDate(igdrp_getComboById('{0}').getValue());", requiredControlId));

            if (string.IsNullOrEmpty(valueFromDateTimeObject))
                return null;
            
            DateTime dateValueObj = DateTime.ParseExact(valueFromDateTimeObject, "yyyy-MM-dd hh:mm:ss", null);
            
            return dateValueObj;
        }

        public  void SetDate_For_WebDateChooser_Using_IdEndsWith(string ctrl_id, DateTime? dt)
        {//wdcDateCreated

            const string postfix_String = "_input";
            string generatedId = ctrl_id + postfix_String;
            var webEle = this.GetEle_Textbox_IdEndsWith(generatedId);

            string fullId = webEle.GetAttribute("id");//this id will be used to get control reference for WebDateChooser

            string requiredControlId = fullId.Remove(fullId.Length - postfix_String.Length);

            if (dt.HasValue)
            {
                string dateValueObj = Helpers.GetJavascript_DateObject(dt.Value);
                /*
                 * igdrp_getComboById('C1_ERP_CC_BODY_wdcDateCreated')
                 * var date = new Date(dateText); dp.set_value(date);
                 */
                this.Driver.RunJavascript(string.Format("igdrp_getComboById('{0}').setValue({1});", requiredControlId, dateValueObj));
            }
            else
            {
                this.Driver.RunJavascript(string.Format("igdrp_getComboById('{0}').set_value(null)", requiredControlId));
            }
            DriverHelpers.Blur(webEle, this.Driver);
        }
        #endregion INfrangistic control helper

        #region Telerik Controls
        
        public string GetSelectionTextOrValue_For_TreeCombobox_Using_IdEndsWith(string ctrl_id, bool isByText)
        {
            const string postfix_String = "_ClientState";
            string generatedId = ctrl_id + postfix_String;
            var webEle = this.GetEle_HiddenInput_IdEndsWith(generatedId);

            string fullId = webEle.GetAttribute("id");
            string requiredControlId = fullId.Remove(fullId.Length - postfix_String.Length); //this id will be used to get control reference for WebDateChooser

            string injectScriptExecuted = string.Empty;

            if (isByText)
                injectScriptExecuted = string.Format(@"$find('{0}').get_embeddedTree().get_selectedNode().get_text();", requiredControlId);
            else
                injectScriptExecuted = string.Format(@"$find('{0}').get_embeddedTree().get_selectedNode().get_value();", requiredControlId);

            string valueOrText = (string)this.Driver.RunJavascript(injectScriptExecuted);

            return valueOrText;

            //$find('ctl00_ctl00_C1_ERP_CC_BODY_radddlBusinessUnitTree').get_embeddedTree().get_selectedNode().get_value()
            //$find('ctl00_ctl00_C1_ERP_CC_BODY_radddlBusinessUnitTree').get_embeddedTree().get_selectedNode().get_text()
        }

        public void SetSelection_For_TreeCombobox_Using_IdEndsWith(string ctrl_id , string nodeText, bool isByText)
        {
            const string postfix_String = "_ClientState";
            string generatedId = ctrl_id + postfix_String;
            var webEle = this.GetEle_HiddenInput_IdEndsWith(generatedId);

            string fullId = webEle.GetAttribute("id");//this id will be used to get control reference for WebDateChooser

            string requiredControlId = fullId.Remove(fullId.Length - postfix_String.Length);

            if (!string.IsNullOrEmpty(nodeText))
            {
                
                string injectScriptExecuted = string.Format(@" $find('{0}').openDropDown(); var ctrlNode = $find('{0}').get_embeddedTree().findNodeByText('{1}'); ctrlNode.select(); $telerik.$(ctrlNode.get_element()).find('span.rtIn')[0].click(); ", requiredControlId, nodeText);

                if (!isByText)
                    injectScriptExecuted = string.Format(@" $find('{0}').openDropDown(); var ctrlNode = $find('{0}').get_embeddedTree().findNodeByValue('{1}'); ctrlNode.select(); $telerik.$(ctrlNode.get_element()).find('span.rtIn')[0].click(); ", requiredControlId, nodeText);

                //DriverHelpers.WaitForSometime(this.Driver, 2);

                this.Driver.RunJavascript(injectScriptExecuted);
            }
            else
            {
                this.Driver.RunJavascript(string.Format("document.getElementById('{0}').control.get_entries().clear();", requiredControlId));
            }

            //src: http://www.telerik.com/forums/set-selected-value-client-side

            //            var treeCtrl = $find('ctl00_ctl00_C1_ERP_CC_BODY_radddlBusinessUnitTree');
            //            var ctrlNode = treeCtrl.get_embeddedTree().findNodeByText("test4352");
            //            ctrlNode.select();
            //            var nodeElement = ctrlNode.get_element();
            //$telerik.$(nodeElement).find("span.rtIn")[0].click();

        }

        public object GetTreeItems_For_TreeCombobox_Using_IdEndsWith(string ctrl_id)
        {
            //$find('ctl00_ctl00_C1_ERP_CC_BODY_radddlBusinessUnitTree').get_embeddedTree().get_nodes()

            throw new NotImplementedException();
        }
        #endregion Telerik Controls
    }
}
