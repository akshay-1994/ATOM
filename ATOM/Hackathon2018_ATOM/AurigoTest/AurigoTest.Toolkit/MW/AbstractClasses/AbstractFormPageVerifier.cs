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
    /// <typeparam name="TForm"></typeparam>
    public abstract class AbstractFormPageVerifier<VSelf, TForm> : VerifierBase<VSelf, TForm>
        where TForm : IDriverLinker
        where VSelf : VerifierBase<VSelf, TForm>
    {
        protected TForm FormRef { get { return base.PageRef; } }

        /// <summary>
        /// Takes reference of the form equivalent of this verifier
        /// </summary>
        /// <param name="formPage"></param>
        /// <param name="formContext"></param>
        public AbstractFormPageVerifier(TForm formPage, string formContext) : base(formPage)
        {

        }

        #region Private Helper Methods
        /// <summary>
        /// Execute Javascript which may returned a value
        /// </summary>
        /// <param name="javaScript"></param>
        /// <returns></returns>
        protected string ExecuteScriptWithReturnedValue(string javaScript)
        {
            FormRef.IFrameDriver_Flush();
            var returnedObject= FormRef.IFrameDriver.RunJavascript(javaScript);
            return returnedObject.ToString();

        }

        #endregion Private Helper Methods

        ///// <summary>
        ///// This is a generic assert where values can be tested based on the condition provided
        ///// </summary>
        ///// <param name="conditionLabel"></param>
        ///// <param name="checkMethod"></param>
        ///// <returns></returns>
        //public VSelf AssertCondition(string conditionLabel, Func<TForm, bool> checkMethod)
        //{
        //    bool isSuccess = checkMethod(this.FormRef);
        //    return TRACK(this as VSelf, t => Assert.IsTrue(isSuccess, conditionLabel));
        //}
    }
}
