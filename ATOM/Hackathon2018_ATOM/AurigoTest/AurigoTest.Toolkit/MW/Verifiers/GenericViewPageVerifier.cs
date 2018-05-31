using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AurigoTest.Toolkit.Core;
using System.Runtime.CompilerServices;

namespace AurigoTest.Toolkit.MW
{
    public class GenericViewPageVerifier : AbstractViewPageVerifier<GenericViewPageVerifier, GenericViewPage>
    {
        public GenericViewPageVerifier(GenericViewPage viewRef, string formContext) : base(viewRef, formContext)
        {
        }

        public GenericViewPageVerifier AssertTextbox(string fieldName, string expectedValue, bool ignoreCase = false, bool isByLabel = true)
        {
            string valueOnScreen = this.ExecuteScriptWithReturnedValue(string.Format("return xmlForm.getControlValue('{0}','');", fieldName));
            return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, valueOnScreen, ignoreCase));
        }

        public GenericViewPageVerifier AssertTextbox(string fieldName, int expectedValue, bool ignoreCase = false)
        {
            string valueInForm = this.ExecuteScriptWithReturnedValue(string.Format("return xmlForm.getControlValue('{0}','');", fieldName));
            
            int out_val;
            if (int.TryParse(valueInForm, out out_val))
                return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, valueInForm));

            throw new AurigoTestException(this.PageRef, EnumExceptionType.AssertException, $"expected integer value ({expectedValue}) but got an invalid value ({valueInForm})");
        }

        public GenericViewPageVerifier AssertTextbox(string fieldName, double expectedValue, bool ignoreCase = false)
        {
            string valueInForm = this.ExecuteScriptWithReturnedValue(string.Format("return xmlForm.getControlValue('{0}','');", fieldName));
            
            double out_val;
            if (double.TryParse(valueInForm, out out_val))
                return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, valueInForm));

            throw new AurigoTestException(this.PageRef, EnumExceptionType.AssertException, $"expected double value ({expectedValue}) but got an invalid value ({valueInForm})");
        }

        public GenericViewPageVerifier AssertCheckbox(string fieldName, bool expectedValue, bool isByLabel = true)
        {
            //TODO:
            string valueOnScreen = "";// this.ExecuteScriptWithReturnedValue(string.Format("return xmlForm.getControlValue('{0}','');", fieldName));
            return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, valueOnScreen));
        }

        #region Datetime

        private void AssertDateTime_Main(string fieldName, DateTime expectedDateTime, bool isByLabel = true)
        {
            AssertDateTime_Main(fieldName, expectedDateTime, null, isByLabel);
        }

        private void AssertDateTime_Main(string fieldName, DateTime expectedDateTime, TimeSpan? deltaTimeSpan, bool isByLabel = true)
        {
            //TODO:
            throw new NotImplementedException();
            //FormRef.IFrameDriver.RunJavascript(CodeInjectionConstants.DateFormatMethod);

            //string valueInForm = ExecuteScriptWithReturnedValue(string.Format("return getFormattedDate(xmlForm.getControlValue('{0}',''));", fieldName));

            //if (string.IsNullOrEmpty(valueInForm))
            //    throw AurigoTestException.AsAssertException(this.FormRef, expectedDateTime.ToString(), " Empty date");

            //DateTime dateValueObj = DateTime.ParseExact(valueInForm, "yyyy-MM-dd hh:mm:ss", null);

            //if (deltaTimeSpan == null)
            //    deltaTimeSpan = new TimeSpan(0, 1, 0);

            //string errorMsg = string.Format("DateTime offset is skewed [Expected: {0}] | [Original : {1}] ", expectedDateTime.ToString("yyyy-MM-dd hh:mm:ss"), dateValueObj.ToString("yyyy-MM-dd hh:mm:ss"));

            //if (dateValueObj.Ticks > expectedDateTime.Ticks)
            //    Assert.IsTrue((dateValueObj - expectedDateTime) < deltaTimeSpan.Value, errorMsg);
            //else
            //    Assert.IsTrue((expectedDateTime - dateValueObj) < deltaTimeSpan.Value, errorMsg);
        }

        public GenericViewPageVerifier AssertDateTime(string fieldName, DateTime expectedDateTime, TimeSpan? deltaTimeSpan = null, bool isByLabel = true)
        {
            return TRACK(this, t => AssertDateTime_Main(fieldName, expectedDateTime, deltaTimeSpan, isByLabel));
        }

        public GenericViewPageVerifier AssertDate(string fieldName, int year, int month, int day, bool isByLabel = true)
        {
            return TRACK(this, t => AssertDateTime_Main(fieldName, new DateTime(year, month, day), isByLabel));
        }

        public GenericViewPageVerifier AssertDate(string fieldName, DateTime dt, bool isByLabel = true)
        {
            return TRACK(this, t => AssertDateTime_Main(fieldName, new DateTime(dt.Year, dt.Month, dt.Day), isByLabel));
        }

        #endregion Datetime
    }
}