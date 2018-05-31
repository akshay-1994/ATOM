using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Core
{
    public abstract class VerifierBase<VSelf, TPageForVerification>
        where VSelf : VerifierBase<VSelf, TPageForVerification>
        //where A: IDriverLinker
    {
        public TPageForVerification PageRef { get; private set; }

        public List<TestStepsDto> VerificationStepsTrackerList { get; private set; } = new List<TestStepsDto>();

        public bool HasException { get; private set; }

        public VerifierBase(TPageForVerification pageRef)
        {
            this.PageRef = pageRef;
        }

        //public void LogStep_Success([CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        //{
        //    VerificationStepsTracker.Add(string.Format("OK : {0}", memberName));
        //}
        
        public VSelf TRACK(VSelf currentInstance, Expression<Action<VSelf>> wrapperMethod, [CallerMemberName] string methodName = "")
        {
            try
            {
                string bodyDetails = string.Empty;

                if (AurigoAppSettings.IsReportTrackingAtMethod)
                    bodyDetails = Helpers.GetPropertyAndObject<VSelf>(wrapperMethod);

                if (!HasException)
                {
                    wrapperMethod.Compile().Invoke(currentInstance);
                    VerificationStepsTrackerList.Add(TestStepsDto.Success(methodName, bodyDetails));
                }
                else
                {
                    VerificationStepsTrackerList.Add(TestStepsDto.Skipped(methodName, bodyDetails));
                }
            }
            catch (AssertFailedException ex)
            {
                VerificationStepsTrackerList.Add(TestStepsDto.Failed(methodName, ex.Message));
                HasException = true;
            }
            catch (Exception ex)
            {
                VerificationStepsTrackerList.Add(TestStepsDto.Failed(methodName, ex));
                HasException = true;
            }

            return currentInstance;
        }


        /// <summary>
        /// This is a generic assert where values can be tested based on the condition provided
        /// </summary>
        /// <param name="conditionLabel"></param>
        /// <param name="checkMethod"></param>
        /// <returns></returns>
        public VSelf AssertCondition(string conditionLabel, Func<TPageForVerification, bool> checkMethod)
        {
            bool isSuccess = checkMethod(this.PageRef);
            return TRACK(this as VSelf, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(isSuccess, conditionLabel));
        }

        /// <summary>
        /// Assets value for specific property in the form/view class
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="propertyLambda"></param>
        /// <param name="expectedValue"></param>
        /// <returns></returns>
        public virtual VSelf Assert<TField>(Expression<Func<TPageForVerification, TField>> propertyLambda, TField expectedValue)
        {
            string propertyName = Helpers.ObjectHelper_GetPropertyNameString(this.PageRef, propertyLambda);

            var val = Helpers.ObjectHelper_ExtractValueFromProperty(this.PageRef, propertyName);

            TField valueInForm = (TField)val;

            return TRACK(this as VSelf, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, valueInForm));
        }

    }
}
