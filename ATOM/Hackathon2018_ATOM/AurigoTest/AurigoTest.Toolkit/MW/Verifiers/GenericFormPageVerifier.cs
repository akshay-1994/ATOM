using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AurigoTest.Toolkit.Core;
using System.Runtime.CompilerServices;
using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.MW.Accessors;

namespace AurigoTest.Toolkit.MW
{
    public class GenericFormPageVerifier : AbstractFormPageVerifier<GenericFormPageVerifier, GenericFormPage>
    {
        #region Properties

        protected FormToasterAccessor _toasterReference = null;

        protected FormToasterAccessor ToasterReference
        {
            get
            {
                if (_toasterReference == null)
                    _toasterReference = new FormToasterAccessor(base.FormRef);

                return _toasterReference;
            }
        }

        protected ControlValidatorAccessor _controlValidatorReference = null;

        protected ControlValidatorAccessor ControlValidatorReference
        {
            get
            {
                if (_controlValidatorReference == null)
                    _controlValidatorReference = new ControlValidatorAccessor(base.FormRef);

                return _controlValidatorReference;
            }
        }

        public GenericFormPageVerifier(GenericFormPage formPage, string formContext) : base(formPage, formContext)
        {
        }

        #endregion Properties

        public GenericFormPageVerifier AssertTextbox(string fieldName, string expectedValue, bool ignoreCase = false)
        {
            string valueInForm = this.ExecuteScriptWithReturnedValue(string.Format("return xmlForm.getControlValue('{0}','');", fieldName));

            return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, valueInForm, ignoreCase));
        }

        public GenericFormPageVerifier AssertTextbox(string fieldName, int expectedValue, bool ignoreCase = false)
        {
            string valueInForm = this.ExecuteScriptWithReturnedValue(string.Format("return xmlForm.getControlValue('{0}','');", fieldName));

            int out_val;
            if (int.TryParse(valueInForm, out out_val))
                return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, valueInForm));

            throw new AurigoTestException(this.FormRef, EnumExceptionType.AssertException, $"expected integer value ({expectedValue}) but got an invalid value ({valueInForm})");
        }

        public GenericFormPageVerifier AssertTextbox(string fieldName, double expectedValue, bool ignoreCase = false)
        {
            string valueInForm = this.ExecuteScriptWithReturnedValue(string.Format("return xmlForm.getControlValue('{0}','');", fieldName));

            double out_val;
            if (double.TryParse(valueInForm, out out_val))
                return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, valueInForm));

            throw new AurigoTestException(this.FormRef, EnumExceptionType.AssertException, $"expected double value ({expectedValue}) but got an invalid value ({valueInForm})");
        }

        #region Combobox

        public GenericFormPageVerifier AssertComobobox_ByValue(string fieldName, int expectedValueId)
        {
            string valueInForm = this.ExecuteScriptWithReturnedValue(string.Format("return xmlForm.getControlValue('{0}','');", fieldName));

            return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValueId, valueInForm));
        }

        public GenericFormPageVerifier AssertComobobox_ByText(string fieldName, string expectedText)
        {
            string injectedCode = @"
xmlForm.getComboboxText = function (controlName, ContainerName) {
    var ctrlClientId = xmlForm.getControlClientId(controlName, ContainerName);
    return $('[ClntId = ' + ctrlClientId + ']').find('option:selected').text();
}
";
            FormRef.IFrameDriver.RunJavascript(injectedCode);
            string valueInForm = ExecuteScriptWithReturnedValue(string.Format("return xmlForm.getComboboxText('{0}','');", fieldName));

            return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedText, valueInForm));
        }

        #endregion Combobox

        public GenericFormPageVerifier AssertCheckbox(string fieldName, bool expectedValue)
        {
            string valueInForm = this.ExecuteScriptWithReturnedValue(string.Format("return xmlForm.getControlValue('{0}','');", fieldName));


            bool out_val;
            if (bool.TryParse(valueInForm, out out_val))
                return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, valueInForm));

            throw new AurigoTestException(this.FormRef, EnumExceptionType.AssertException, $"expected boolean value ({expectedValue}) but got an invalid value ({valueInForm})");
        }

        #region Datetime

        private void AssertDateTime_Main(string fieldName, DateTime expectedDateTime)
        {
            AssertDateTime_Main(fieldName, expectedDateTime, null);
        }

        private void AssertDateTime_Main(string fieldName, DateTime expectedDateTime, TimeSpan? deltaTimeSpan)
        {
            FormRef.IFrameDriver.RunJavascript(CodeInjectionConstants.DateFormatMethod);

            string valueInForm = ExecuteScriptWithReturnedValue(string.Format("return getFormattedDate(xmlForm.getControlValue('{0}',''));", fieldName));

            if (string.IsNullOrEmpty(valueInForm))
                throw AurigoTestException.AsAssertException(this.FormRef, expectedDateTime.ToString(), " Empty date");

            DateTime dateValueObj = DateTime.ParseExact(valueInForm, "yyyy-MM-dd hh:mm:ss", null);

            if (deltaTimeSpan == null)
                deltaTimeSpan = new TimeSpan(0, 1, 0);

            string errorMsg = string.Format("DateTime offset is skewed [Expected: {0}] | [Original : {1}] ", expectedDateTime.ToString("yyyy-MM-dd hh:mm:ss"), dateValueObj.ToString("yyyy-MM-dd hh:mm:ss"));

            if (dateValueObj.Ticks > expectedDateTime.Ticks)
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue((dateValueObj - expectedDateTime) < deltaTimeSpan.Value, errorMsg);
            else
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue((expectedDateTime - dateValueObj) < deltaTimeSpan.Value, errorMsg);
        }

        public GenericFormPageVerifier AssertDateTime(string fieldName, DateTime expectedDateTime, TimeSpan? deltaTimeSpan = null)
        {
            return TRACK(this, t => AssertDateTime_Main(fieldName, expectedDateTime, deltaTimeSpan));
        }

        public GenericFormPageVerifier AssertDate(string fieldName, int year, int month, int day)
        {
            return TRACK(this, t => AssertDateTime_Main(fieldName, new DateTime(year, month, day)));
        }

        public GenericFormPageVerifier AssertDate(string fieldName, DateTime dt)
        {
            return TRACK(this, t => AssertDateTime_Main(fieldName, new DateTime(dt.Year, dt.Month, dt.Day)));
        }

        #endregion Datetime

        public GenericFormPageVerifier AssertIfToasterExist()
        {
            var refThis = TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(true, this.ToasterReference.CheckToasterObjectExists()));
            if (this.VerificationStepsTrackerList.Any() && this.VerificationStepsTrackerList.Last().StepStatus == EnumStepStatus.Failed)
                throw new AurigoTestException(this.FormRef, EnumExceptionType.AssertException, "Control does not have expected control validation");

            return this;
        }

        public GenericFormPageVerifier AssertToasterMessage(string expectedMessage)
        {
            return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedMessage, this.ToasterReference.GetToasterMessages()));
        }

        public GenericFormPageVerifier AssertControlSpanErrorMessage(string controlName)
        {
            var refThis = TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(true, this.ControlValidatorReference.CheckIfSpanExists(controlName)));

            if (this.VerificationStepsTrackerList.Any() && this.VerificationStepsTrackerList.Last().StepStatus == EnumStepStatus.Failed)
                throw new AurigoTestException(this.FormRef, EnumExceptionType.AssertException, "Control does not have expected control validation");

            return this;
        }
    }
}