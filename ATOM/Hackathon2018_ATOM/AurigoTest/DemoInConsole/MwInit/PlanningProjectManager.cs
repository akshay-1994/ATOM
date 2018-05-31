using AurigoTest.Toolkit;
using AurigoTest.Toolkit.Core;
using AurigoTest.Toolkit.MW;
using AurigoTest.Toolkit.MW.Constants;
using AurigoTest.Toolkit.MW.Customizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DB = AurigoTest.Toolkit.Core.DBHelper;

namespace DemoInConsole.MwInit
{
    public class PlanningProjectManager //: MasterworksScreen
    {
        public const string CONST_TEST_PROJECT_CODE = "PRJAUT1";

        [RunCustom]
        [Run]
        public void DemoData_CreateAndVerify_NewProject_IfRequired(string testId, string testSummary)
        {
            string currentAutomationId = Helpers.GetUniqueData("PlanProject_Auto1");
            DateTime projectEndDate = DateTime.Today.AddYears(1);

            const string projectCode = CONST_TEST_PROJECT_CODE;
            if (DB.Check_DataExist(HintFieldLookup.Project_By_ProjectCode(projectCode)))
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
        
        [Run]
        public void Add_NewProject(string testId, string testSummary)
        {
            string currentAutomationId = Helpers.GetUniqueData("PlanProject_Auto");
            DateTime projectEndDate = DateTime.Today.AddYears(1);

            string projectCode = Helpers.GetUniqueData(CONST_TEST_PROJECT_CODE);

            if (DB.Check_DataExist(HintFieldLookup.Project_By_ProjectCode(projectCode)))
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
                        .Assert(t => t.EndDate, projectEndDate)

                        .Assert(t => t.ContractDays, 365)
                        ;
                    })

            //.End_Automation()
            ;
        }


        //[Run]
        //public void Add_NewProject_Using_RemoteData(string testId, string testSummary)
        //{
        //    string currentAutomationId = Helpers.GetUniqueData("PlanProject_Auto");
        //    DateTime projectEndDate = DateTime.Today.AddYears(1);

        //    string projectCode = Helpers.GetUniqueData(CONST_TEST_PROJECT_CODE);

        //    if (DB.Check_DataExist(HintFieldLookup.Project_By_ProjectCode(projectCode)))
        //        return;

        //    MasterworksScreen
        //        .Begin(testId, testSummary, BrowserType.Chrome, true)
        //            .CreateProjectFromPlanning()
        //                .Set(t => t.ProjectName, currentAutomationId)
        //                .Set(t => t.ProjectCode, projectCode)
        //                .Set(t => t.ProjectOwner, "User-Automator")
        //                .Set(t => t.ProjectStatus, "Advertisement")
        //                .Set(t => t.ProjectCategory, "Central")
        //                .Set(t => t.BusinessUnit_Text, "HeadOffice")
        //                .Set(t => t.ProgramYear, "2017")
        //                //.Set(t => t.Calendar, "Calendar By Vinay")
        //                .Set(t => t.EndDate, projectEndDate)
        //            .SaveForm()
        //        .View_FirstRow_CustomViewer<ProjectViewPage>()
        //        .BeginVerification("PRJ_VER_TC_0001", (driver, viewVerifier) =>
        //        {
        //            viewVerifier
        //            .Assert(t => t.ProjectName, currentAutomationId)
        //            .Assert(t => t.ProjectCode, projectCode)
        //            .Assert(t => t.ProjectOwner, "User-Automator")
        //            .Assert(t => t.ProjectStatus, "Advertisement")
        //            .Assert(t => t.ProjectCategory, "Central")
        //            .Assert(t => t.BusinessUnit_Text, "HeadOffice")
        //            .Assert(t => t.ProgramYear, "2017")
        //            //.Set(t => t.Calendar, "Calendar By Vinay")
        //            .Assert(t => t.EndDate, projectEndDate)

        //            .Assert(t => t.ContractDays, 365)
        //            ;
        //        })

        //    //.End_Automation()
        //    ;
        //}
    }
}
