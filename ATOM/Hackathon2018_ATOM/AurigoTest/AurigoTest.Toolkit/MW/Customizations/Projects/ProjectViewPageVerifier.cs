using AurigoTest.Toolkit.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW.Customizations
{
    public class ProjectViewPageVerifier : AbstractViewPageVerifier<ProjectViewPageVerifier, ProjectViewPage>
    {
        protected ProjectViewPage ViewRef { get { return base.PageRef; } }

        public ProjectViewPageVerifier(ProjectViewPage viewRef, string formContext) : base(viewRef, formContext)
        {
            //this.AssertCondition("")
        }


        //public ProjectViewPageVerifier Assert_ContractDays(int expectedValue)
        //{
        //    int valueInForm = this.ViewRef.ContractDays;

        //    return TRACK(this, t => Assert.AreEqual(expectedValue, valueInForm));
        //}

        //public ProjectViewPageVerifier Assert_ProjectName(string expectedValue, bool ignoreCase = false)
        //{
        //    string valueInForm = this.ViewRef.ProjectName;

        //    return TRACK(this, t => Assert.AreEqual(expectedValue, valueInForm, ignoreCase));
        //}

        //public ProjectViewPageVerifier Assert_CustomSettingsAreCorrect(string expectedValue)
        //{
        //    string valueInForm = this.ViewRef.ProjectName;

        //    return TRACK(this, t => Assert.AreEqual(expectedValue, valueInForm, true));
        //}

        //public ProjectViewPageVerifier Assert_EndDate(DateTime? expectedValue)
        //{
        //    var valueInForm = this.ViewRef.EndDate;

        //    return TRACK(this, t => Assert.AreEqual(expectedValue, valueInForm));
        //}
    }
}
