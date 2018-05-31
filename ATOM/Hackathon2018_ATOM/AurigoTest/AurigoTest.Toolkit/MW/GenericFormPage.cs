using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Xml;
using AurigoTest.Toolkit.Common.Dto;
using AurigoTest.Toolkit.MW.Controls;

namespace AurigoTest.Toolkit.MW
{
    /// <summary>
    /// This class is to be used when dealing with generic form pages
    /// </summary>
    public class GenericFormPage : AbstractFormPage<GenericFormPage, GenericFormPageVerifier, GenericListPage>
    {
        public GenericFormPage(GenericListPage listPage, string listPageURL) : base(listPage, listPageURL, GetFormContext_UsingURL(listPageURL))
        {

        }

        #region Private Method : Static
        private static string GetFormContext_UsingURL(string listPageURL)
        {
            if (string.IsNullOrEmpty(listPageURL))
                return string.Empty;

            var lowerCaseURL = listPageURL.ToLower();

            if (!lowerCaseURL.Contains("xcontext") && !lowerCaseURL.Contains("context"))
                return listPageURL;

            string formContext = string.Empty;
            var urlGen = new UrlGenerator(listPageURL);

            if (urlGen.ContainsKey("xcontext"))
                formContext = urlGen.GetValueForKey("xcontext");
            else if (urlGen.ContainsKey("context"))
                formContext = urlGen.GetValueForKey("context");

            if (string.IsNullOrEmpty(formContext))
                formContext = listPageURL;

            return formContext;
        }
        #endregion Private Method : Static

        #region Generic Control Value setting

        #region For Text and numeric
        /// <summary>
        /// Use this method to set text value to any textbox on screen
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public GenericFormPage SetTextbox(string fieldName, string value)
        {
            this.IFrameDriver.RunJavascript(string.Format("xmlForm.setControlValue('{0}','', '{1}'); return '0';", fieldName, value));
            return this;
        }

        public GenericFormPage SetTextbox(string fieldName, double value)
        {
            this.IFrameDriver.RunJavascript(string.Format("xmlForm.setControlValue('{0}','', {1}); return '0';", fieldName, value));
            return this;
        }

        public GenericFormPage SetTextbox(string fieldName, int value)
        {
            this.IFrameDriver.RunJavascript(string.Format("xmlForm.setControlValue('{0}','', {1}); return '0';", fieldName, value));
            return this;
        }
        #endregion For Text and numeric

        #region Combobox

        /// <summary>
        /// Set combobox selection based on the value(interal value)
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="valueId"></param>
        /// <returns></returns>
        public GenericFormPage SetComobobox_ByValue(string fieldName, string valueId)
        {
            this.IFrameDriver.RunJavascript(string.Format("xmlForm.setControlValue('{0}','', '{1}'); return '0';", fieldName, valueId));
            return this;
        }

        /// <summary>
        /// Set combobox selection based on the text available in combobox
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public GenericFormPage SetComobobox_ByText(string fieldName, string text)
        {
            string injectedCode = @"
xmlForm.setComboboxText = function (controlName, ContainerName, text) {

    var control = xmlForm.getControl(controlName, ContainerName);
    if (!control)
        return;

    var controlType = $(control).attr('ControlType');
    var controlId = $(control).attr('id');

    if (controlType != 'DropDownList')
        return;

    var objSelect = $(control);
    $('option', $(objSelect)).each(function() {
                if ($(this).text() == text) {
                    $(this).attr('selected', 'true');
                    return;
                }
    });
}
";
            this.IFrameDriver.RunJavascript(injectedCode);
            this.IFrameDriver.RunJavascript(string.Format("xmlForm.setComboboxText('{0}','', '{1}');", fieldName, text));

            return this;
        }

        #endregion Combobox

        /// <summary>
        /// Set the checkbox status based on value provided
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public GenericFormPage SetCheckbox(string fieldName, bool value)
        {
            this.IFrameDriver.RunJavascript(string.Format("xmlForm.setControlValue('{0}','', {1}); return '0';", fieldName, value));
            return this;
        }

        public string GetTextbox(string fieldName)
        {
           return this.IFrameDriver.RunJavascript(string.Format("return xmlForm.getControlValue('{0}','');", fieldName)).ToString();
            
        }

        #region Datetime
        /// <summary>
        /// Sets Datetime control value
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public GenericFormPage SetDateTime(string fieldName, DateTime dt)
        {
            string dateValueObj = Helpers.GetJavascript_DateObject(dt);

            this.IFrameDriver.RunJavascript(string.Format("xmlForm.setControlValue('{0}','', {1});", fieldName, dateValueObj));
            return this;
        }
        /// <summary>
        /// Set the date control value
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public GenericFormPage SetDate(string fieldName, int year, int month, int day)
        {
            return SetDateTime(fieldName, new DateTime(year, month, day));
        }
        /// <summary>
        /// set the date control value based on date object as input
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public GenericFormPage SetDate(string fieldName, DateTime dt)
        {
            return SetDateTime(fieldName, new DateTime(dt.Year, dt.Month, dt.Day));
        }
        #endregion Datetime

        #region Dynamic Grid

        public DynamicGrid DynamicGrid_AddRecord(string dynamicGridName)
        {
            return DynamicGrid.AddRecord(this.IFrameDriver, dynamicGridName, this);
        }

        public DynamicGrid DynamicGrid_EditRecord_ByRowIndex(string dynamicGridName, int rowIndex)
        {
            return DynamicGrid.EditRecord_ByRowIndex(this.IFrameDriver, dynamicGridName, this);
        }

        public DynamicGrid DynamicGrid_DeleteRecord_ByRowIndex(string dynamicGridName, int rowIndex)
        {
            return DynamicGrid.DeleteRecord_ByRowIndex(this.IFrameDriver, dynamicGridName, this);
        }

        #endregion Dynamic Grid

        #endregion Generic Control Value setting

        #region Override Methods
        /// <summary>
        /// This method is use to navigate to the list page from this form page
        /// </summary>
        /// <returns></returns>
        public override GenericListPage GoToListPage()
        {
            return new GenericListPage(this, _listPageURL);
        }

        //protected override GenericFormPage DecodeAndSetValue(string propertyName, object value)
        //{
        //    if (this._formContext == this._listPageURL)
        //        throw new Exception("Context of the page cannot be determined to auto compute the control type based on propertyName alone.");

        //    var controlObj = DBHelper.GetXML_FromControlObject_ByName(this._formContext, propertyName);

        //    switch (controlObj.Type)
        //    {
        //        case ControlType.TextBox:
        //        case ControlType.Hidden:
        //            return SetTextbox(propertyName, value?.ToString());
        //        case ControlType.CheckBox:
        //            return SetValue_Checkbox(propertyName, (bool)value);
        //        case ControlType.DropDownList:
        //            return SetComobobox_ByText(propertyName, value?.ToString());
        //            //TODO: support other controls
        //    }

        //    return this;
        //}

        #endregion Override Methods

    }

}
