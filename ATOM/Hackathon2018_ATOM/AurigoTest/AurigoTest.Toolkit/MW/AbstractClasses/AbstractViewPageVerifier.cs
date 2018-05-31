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
    /// <summary>
    /// This is the bases class for all form verifier.
    /// </summary>
    /// <typeparam name="VSelf"></typeparam>
    /// <typeparam name="TViewPage"></typeparam>
    public abstract class AbstractViewPageVerifier<VSelf, TViewPage> : VerifierBase<VSelf, TViewPage>
        where TViewPage : IDriverLinker
        where VSelf : VerifierBase<VSelf, TViewPage>
    {
        //protected TViewPage ViewRef { get; set; }

        /// <summary>
        /// Takes reference of the form equivalent of this verifier
        /// </summary>
        /// <param name="viewRef"></param>
        /// <param name="formContext"></param>
        public AbstractViewPageVerifier(TViewPage viewRef, string formContext): base(viewRef)
        {
            //ViewRef = viewRef;
        }

        #region Private Helper Methods
        /// <summary>
        /// Execute Javascript which may returned a value
        /// </summary>
        /// <param name="javaScript"></param>
        /// <returns></returns>
        protected string ExecuteScriptWithReturnedValue(string javaScript)
        {
            base.PageRef.IFrameDriver_Flush();
            var returnedObject = base.PageRef.IFrameDriver.RunJavascript(javaScript);
            return returnedObject.ToString();
        }

        #endregion Private Helper Methods

        ///// <summary>
        ///// This is a generic assert where values can be tested based on the condition provided
        ///// </summary>
        ///// <param name="conditionLabel"></param>
        ///// <param name="checkMethod"></param>
        ///// <returns></returns>
        //public VSelf AssertCondition(string conditionLabel, Func<TViewPage, bool> checkMethod)
        //{
        //    bool isSuccess = checkMethod(base.PageRef);
        //    return TRACK(this as VSelf, t => Assert.IsTrue(isSuccess, conditionLabel));
        //}
    }
}
