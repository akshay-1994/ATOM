using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.Core;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW
{
    public abstract class AbstractFormPage<TSelf, TVerifier, TList> : AutomationBase<TSelf, TVerifier>
        where TSelf : AbstractFormPage<TSelf, TVerifier, TList>//AutomationBase<TSelf, TVerifier>
        where TVerifier : VerifierBase<TVerifier, TSelf>
        where TList : IListPage
    {
        protected string _listPageURL;
        protected string _formContext;

        #region Properties


        protected TList ListPageReference { get { return (TList)this.ParentObject; } }

        public List<TVerifier> VerificationBlockList { get; private set; } = new List<TVerifier>();

        protected TVerifier LastKnownVerificationBlock
        {
            get
            {
                if (VerificationBlockList.Count == 0) return null;
                return VerificationBlockList.Last();
            }
        }

        public bool IsAnyVerificationBlockHavingException { get; set; } = false;

        protected RibbonBarAccessor _ribbonBar = null;
        protected RibbonBarAccessor RibbonBar
        {
            get
            {
                if (_ribbonBar == null)
                {
                    _ribbonBar = new RibbonBarAccessor(this);
                }

                return _ribbonBar;
            }
        }
        #endregion Properties

        #region Abstract Methods
        public abstract TList GoToListPage();
        //public abstract A SetUsing_Data(OrderedDictionary keyValue);
        //public abstract A SetUsing_JsonData(JObject jsonObjectKeyValue);

        /// <summary>
        /// This is used by SetData() method to decode the field and value setting
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        protected virtual TSelf DecodeAndSetValue(string propertyName, object value)
        {
            return Helpers.ObjectHelper_InjectValueToProperty(this as TSelf, propertyName, value);
        }

        #endregion Abstract Methods

        #region Constructor And Inits
        public AbstractFormPage(TList listPage, string listPageURL, string formContext) : base(listPage)
        {
            this._listPageURL = listPageURL;
            this._formContext = formContext;
        }

        #endregion Constructor And Inits

        #region OVerride Methods
        public override TSelf Wait(int seconds)
        {
            return base.Wait(seconds, EnumSearchLocation.IFrame);
        }

        #endregion OVerride Methods

        #region Virtual Methods

        #region General Form Operations
        public virtual TSelf SaveForm_ExpectValidationError(string optionalButtonId = null)
        {
            //TODO: check if this is required
            //if (isStopOnVerificationException && this.IsAnyVerificationBlockHavingException)
            //    throw new AurigoTestException(this, EnumExceptionType.MethodCallAverted, "Save averted due to one or more verificaiton issues.");

            string oldUrl = base.PrimaryDriver.Url;

            this.RibbonBar.Click_Save_Button(optionalButtonId);
            try
            {
                string linkFound = DriverHelpers.WaitForAnyOfTheIFrameContents(base.PrimaryDriver, ConfigData.IFrameID, new List<string> { "lnkCancel", "lnkNew" }, 10);
                if (string.IsNullOrEmpty(linkFound) || linkFound == "lnkNew")
                    throw new AurigoTestException(this, EnumExceptionType.UrlChanged, "Expecting error on save, but it got saved.");
            }
            catch { }

            return this as TSelf;
        }

        public virtual TList SaveForm_Successfully(bool isStopOnVerificationException = true, string optionalButtonId = null)
        {
            if (isStopOnVerificationException && this.IsAnyVerificationBlockHavingException)
                throw new AurigoTestException(this, EnumExceptionType.MethodCallAverted, "Save averted due to one or more verificaiton issues.");

            string oldUrl = base.PrimaryDriver.Url;

            this.RibbonBar.Click_Save_Button(optionalButtonId);
            DriverHelpers.WaitForIFrameContent(base.PrimaryDriver, ConfigData.IFrameID, "lnkNew"); //means list page //C1_divRadGrid

            string newUrl = base.PrimaryDriver.Url;

            if (oldUrl == newUrl)
                throw new AurigoTestException(this, EnumExceptionType.NoUrlChange, "Something went wrong. Page did not navigate correctly");

            return ListPageReference;
        }

        public virtual TList CancelForm(string optionalButtonId = null)
        {
            string oldUrl = base.PrimaryDriver.Url;
            this.RibbonBar.Click_CancelSave_Button(optionalButtonId);

            string newUrl = base.PrimaryDriver.Url;

            if (oldUrl == newUrl)
                throw new AurigoTestException(this, EnumExceptionType.NoUrlChange, "Something went wrong. Page did not navigate correctly");

            return ListPageReference;// new L(this, _listPageURL);
        }

        #endregion General Form Operations

        #region Verification Code
        public virtual TSelf BeginVerification(string verificationIdentification, Action<IWebDriver, TVerifier> verificationBlock)
        {
            string testName = Helper_GetActualTestName(verificationIdentification + " (Form Verifier)");
            var testObj = Helpers.Report.StartTest(testName);

            TVerifier formVerifier = (TVerifier)Activator.CreateInstance(typeof(TVerifier), this, _formContext);

            try
            {   
                VerificationBlockList.Add(formVerifier);

                verificationBlock.Invoke(this.IFrameDriver, formVerifier);//TODO: asheesh (Optional either use one verifier or multiple verifier and save as List internally)

                this.IsAnyVerificationBlockHavingException |= formVerifier.HasException;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                base.LogTestSteps(testObj, testName, formVerifier.VerificationStepsTrackerList);

                Helpers.Report.EndTest(testObj);
            }

            return this as TSelf;
        }

        public virtual TSelf BeginVerification(string verificationIdentification, Action<TVerifier> verificationBlock)
        {
            string testName = Helper_GetActualTestName(verificationIdentification + " (Form Verifier)");

            var testObj = Helpers.Report.StartTest(testName);
            TVerifier formVerifier = (TVerifier)Activator.CreateInstance(typeof(TVerifier), this, _formContext);

            try
            {
                VerificationBlockList.Add(formVerifier);

                verificationBlock.Invoke(formVerifier);//TODO: asheesh (Optional either use one verifier or multiple verifier and save as List internally)

                this.IsAnyVerificationBlockHavingException |= formVerifier.HasException;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                base.LogTestSteps(testObj, testName, formVerifier.VerificationStepsTrackerList);
                Helpers.Report.EndTest(testObj);
            }

            return this as TSelf;
        }

        public virtual TSelf BeginVerification(Action<IWebDriver, TVerifier> verificationBlock)
        {
            return BeginVerification("Unamed_Step", verificationBlock);
        }

        public virtual TSelf BeginVerification(Action<TVerifier> verificationBlock)
        {
            return BeginVerification(verificationBlock);
        }
        #endregion Verification Code

        #region Data Setting Code
        /// <summary>
        /// Set using properties of the class
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="propertyLambda"></param>
        /// <param name="valueToSet"></param>
        /// <returns></returns>
        public virtual TSelf Set<TField>(Expression<Func<TSelf, TField>> propertyLambda, TField valueToSet)
        {
            string fieldName = Helpers.ObjectHelper_GetPropertyNameString(this as TSelf, propertyLambda);

            //Type type = typeof(A);

            //MemberExpression memberExpr = propertyLambda.Body as MemberExpression;

            //if (memberExpr == null)
            //    throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", propertyLambda.ToString()));

            //PropertyInfo propInfo = memberExpr.Member as PropertyInfo;
            //if (propInfo == null)
            //    throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", propertyLambda.ToString()));

            ////This condition may not be required
            //if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            //    throw new ArgumentException(string.Format("Expresion '{0}' refers to a property that is not from type {1}.", propertyLambda.ToString(), type));

            return DecodeAndSetValue(fieldName, valueToSet);
        }

        public virtual TSelf Set(OrderedDictionary keyValue)
        {
            foreach (DictionaryEntry de in keyValue)
            {
                DecodeAndSetValue(de.Key.ToString(), de.Value);
            }

            return this as TSelf;
        }

        /// <summary>
        /// set using Json object
        /// </summary>
        /// <param name="jsonObjectKeyValue"></param>
        /// <returns></returns>
        public virtual TSelf Set(JObject jsonObjectKeyValue)
        {
            foreach (var prop in jsonObjectKeyValue)
            {
                DecodeAndSetValue(prop.Key, prop.Value);
            }

            return this as TSelf;
        }

        /// <summary>
        /// Set using json string
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public virtual TSelf Set(string jsonString)
        {
            JObject jsonObjectKeyValue = JObject.Parse(jsonString);

            return Set(jsonObjectKeyValue);
        }

        #endregion Data Setting Code

        #endregion Virtual Methods
    }
}
