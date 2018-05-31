using AurigoTest.Toolkit.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW.Customizations
{
    public class ProjectFormPageVerifier : AbstractFormPageVerifier<ProjectFormPageVerifier, ProjectFormPage>
    {
        public ProjectFormPageVerifier(ProjectFormPage formPage, string formContext) : base(formPage, formContext)
        {
            //this.FormRef

        }

        public ProjectFormPageVerifier Assert_ContractDays(int expectedValue)
        {
            int valueInForm = this.FormRef.ContractDays;

            return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, valueInForm));
        }

        public ProjectFormPageVerifier Assert_ProjectName(string expectedValue, bool ignoreCase = false)
        {
            string valueInForm = this.FormRef.ProjectName;

            return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, valueInForm, ignoreCase));
        }

        public ProjectFormPageVerifier Assert_CustomSettingsAreCorrect(string expectedValue)
        {
            string valueInForm = this.FormRef.ProjectName;

            return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, valueInForm, true));
        }

        public ProjectFormPageVerifier Assert_EndDate(DateTime? expectedValue)
        {
            var valueInForm = this.FormRef.EndDate;

            return TRACK(this, t => Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedValue, valueInForm));
        }
    }
}
