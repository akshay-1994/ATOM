using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AurigoTest.Toolkit.Core;
using AurigoTest.Toolkit.MW;

using AurigoTest.Toolkit.Core;
using AurigoTest.Toolkit.MW.Constants;
using AurigoTest.Toolkit.MW.Customizations;

namespace AurigoTest.UT.GenericFormControls
{
    [TestClass]
    public class ProjectCreationTesting
    {
        public const string CONST_TEST_PROJECT_CODE = "PRJAUTO";

        public ProjectCreationTesting()
        {

        }

        [TestMethod]
        public void TestMethod1()
        {

            this.TestProject("TC_PRJ_ALL", "All Project Methods");
            //TestRunner.Run("TC_PRJ_ALL", "All Project Methods",  this.TestProject);
        }

        public void TestProject(string testId, string testSummary)
        {
            string currentAutomationId = Helpers.GetUniqueData("PlanProject_Auto");
            DateTime projectEndDate = DateTime.Today.AddYears(1);

            const string projectCode = CONST_TEST_PROJECT_CODE;
            if (DBHelper.Check_DataExist(HintFieldLookup.Project_By_ProjectCode(projectCode)))
                return;

            MasterworksScreen
                .Begin(testId, testSummary, BrowserType.Chrome, true)
                    .CreateProjectFromPlanning()
                        .Set(t => t.ProjectName, currentAutomationId)
                        .Set(t => t.ProjectCode, projectCode)
                        .Set(t => t.ProjectOwner, "User-Automator")
                        .Set(t => t.ProjectStatus, "Advertisement")
                        .Set(t => t.ProjectCategory, "Central")
                        .Set(t => t.BusinessUnit_Text, "HeadOffice")
                        .Set(t => t.ProgramYear, "2017")
                        //.Set(t => t.Calendar, "Calendar By Vinay")
                        .Set(t => t.EndDate, projectEndDate)
                    .SaveForm_Successfully()
                .View_FirstRow_CustomViewer<ProjectViewPage>()
                    .BeginVerification("PRJ_VER_TC_0001", (driver, viewVerifier) =>
                    {
                        viewVerifier
                        .Assert(t => t.ProjectName, currentAutomationId)
                        .Assert(t => t.ProjectCode, projectCode)
                        .Assert(t => t.ProjectOwner, "User-Automator")
                        .Assert(t => t.ProjectStatus, "Advertisement")
                        .Assert(t => t.ProjectCategory, "Central")
                        .Assert(t => t.BusinessUnit_Text, "HeadOffice")
                        .Assert(t => t.ProgramYear, "2017")
                        //.Set(t => t.Calendar, "Calendar By Vinay")
                        //.Assert(t => t.EndDate, projectEndDate)

                        .Assert(t => t.ContractDays, 365)
                        ;
                    })

            //.End_Automation()
            ;
        }
    }
}
