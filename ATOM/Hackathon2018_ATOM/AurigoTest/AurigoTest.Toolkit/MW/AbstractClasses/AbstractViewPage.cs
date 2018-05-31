using AurigoTest.Toolkit.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW
{
    public abstract class AbstractViewPage<TSelf, TVerifier, TList> : AutomationBase<TSelf, TVerifier>, IViewPage<TSelf>
        where TSelf : AbstractViewPage<TSelf, TVerifier, TList>
        where TVerifier : VerifierBase<TVerifier, TSelf> // VerifierBase<GenericViewPageVerifier, GenericViewPage>
        where TList : IListPage
    {
        protected TList ListPageReference { get { return (TList)this.ParentObject; } }

        protected string _listPageURL;
        //protected string _formContext;


        protected RibbonBarAccessor _ribbonBar = null;
        public RibbonBarAccessor RibbonBar
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


        public AbstractViewPage(TList listPageReference, string listPageURL) : base(listPageReference)
        {
            _listPageURL = listPageURL;
        }

        //public TSelf AssertFormValue(string fieldName, string expectedValue)
        //{
        //    return this as TSelf;
        //}

        //public virtual TSelf ContinueIf_FieldPropertyValueMatches<TField>(Expression<Func<TSelf, TField>> propertyLambda, TField expectedValue)
        //{
        //    string fieldName = Helpers.ObjectHelper_GetPropertyNameString(this as A, propertyLambda);

        //    object actualValue = Helpers.ObjectHelper_ExtractValueFromProperty(this as TSelf, fieldName);

        //    if (!actualValue.Equals(expectedValue))
        //        throw new AurigoException(string.Format("ContinueIf failed Expected: [{0}]  Actual:[{1}]", expectedValue, actualValue));

        //    return this as TSelf;
        //}

        public virtual TSelf ContinueIf(string conditionIdentifier, Func<TSelf, bool> checkMethod)
        {
            TSelf self = this as TSelf;

            try
            {
                bool isSuccess = checkMethod(self);

                if (isSuccess)
                    throw new AurigoTestException(this, EnumExceptionType.ContinueIfFailed, string.Format("ContinueIf failed for : ", conditionIdentifier));

            }
            catch (Exception ex)
            {
                if (ex is AurigoTestException)
                    throw;
                else
                    throw new AurigoTestException(this, EnumExceptionType.Unknown, ex.Message, ex);
            }
            return self;
        }

        #region Verification Code
        public virtual TSelf BeginVerification(string verificationIdentification, Action<IWebDriver, TVerifier> verificationBlock)
        {
            string testName = Helper_GetActualTestName(verificationIdentification + " (View Verifier)");
            var testObj = Helpers.Report.StartTest(testName);

            TVerifier formVerifier = (TVerifier)Activator.CreateInstance(typeof(TVerifier), this, _listPageURL);
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
            string testName = Helper_GetActualTestName(verificationIdentification + " (View Verifier)");
            var testObj = Helpers.Report.StartTest(testName);

            TVerifier formVerifier = (TVerifier)Activator.CreateInstance(typeof(TVerifier), this, _listPageURL);
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
            return BeginVerification("Unamed_Step", verificationBlock);
        }
        #endregion Verification Code

        public virtual TList CancelForm(string optionalButtonId = null)
        {
            string oldUrl = base.PrimaryDriver.Url;
            this.RibbonBar.Click_CancelSave_Button(optionalButtonId);

            string newUrl = base.PrimaryDriver.Url;

            if (oldUrl == newUrl)
                throw new AurigoTestException(this, EnumExceptionType.NoUrlChange, "Something went wrong. Page did not navigate correctly");

            return ListPageReference;// new L(this, _listPageURL);
        }
    }

}
