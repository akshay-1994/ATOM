using AurigoTest.Toolkit.Core;
using AurigoTest.Toolkit.MW;
using AurigoTest.Toolkit.MW.Customizations;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInConsole
{
    public static class PlanningProjectTest
    {
        public static void Add_NewProject(string testId, string testSummary)
        {
            string currentAutomationId = Helpers.GetUniqueData("PlanProject_Auto");
            
            MasterworksScreen
                .Begin(testId, testSummary, BrowserType.Chrome, true)
                .DB_Snapshot_Create("MySnapshot", continueIfExist: true)
                    .CreateProjectFromPlanning()
                        .Set(t => t.ProjectName, currentAutomationId)
                        .Set(t => t.ProjectCode, "PCODE_" + currentAutomationId)
                        .Set(t => t.ProjectOwner, "User-Automator")
                        .Set(t => t.ProjectStatus, "Advertisement")
                        .Set(t => t.ProjectCategory, "Central")
                        .Set(t => t.BusinessUnit_Text, "HeadOffice")
                        .Set(t => t.ProgramYear, "2017")
                        //.Set(t => t.Calendar, "Calendar By Vinay")
                        .Set(t => t.EndDate, DateTime.Today.AddYears(1))
                    .SaveForm_Goto_MasterworksScreen()
                    //.DB_Snapshot_Restore("MySnapshot")
                    .CreateProjectFromPlanning()
                    .Set("{ProjectName:\"asheesh\",  }")
                        .Set(t => t.ProjectName, currentAutomationId)
                        .Set(t => t.ProjectCode, "New_PCODE_" + currentAutomationId)
                        .Set(t => t.ProjectOwner, "User-Automator")
                        .Set(t => t.ProjectStatus, "Advertisement")
                        .Set(t => t.ProjectCategory, "Central")
                        .Set(t => t.BusinessUnit_Text, "HeadOffice")
                        .Set(t => t.ProgramYear, "2017")
                        //.Set(t => t.Calendar, "Calendar By Vinay")
                        .Set(t => t.EndDate, DateTime.Today.AddYears(1))
                    .SaveForm_Successfully()

               // .DB_Snapshot_Restore("MySnapshot")

                .View_FirstRow_CustomViewer<ProjectViewPage>()
                    .CheckSomeThingCustomHere()
                    //.
                    //.ContinueIf("Basic ProjectCreation Validation", t => t.ProjectName == currentAutomationId && t.ProjectOwner == "User-Automator")
                    .BeginVerification("aaa", (driver, viewVerifier) =>
                    {
                        viewVerifier
                            .Assert(t => t.ContractDays, 366)
                            .Assert(t => t.ProjectName, currentAutomationId)
                            .AssertCondition("TestCase9999", t => t.ProjectName == currentAutomationId)
                        //viewVerifier
                        //.Assert_ContractDays(176)//this way also can be coded
                        //.Assert_ContractDays(1)//this way also can be coded
                        //.Assert_CustomSettingsAreCorrect(dataGroup["ProjectName"].ToString())
                        ;
                    })
                //.DB_Snapshot_Delete("MySnapshot")

                ;

            //.AssertFormValue("")

            //.End_Automation()
            ;
        }

        //public static void Add_NewProject_UsingData(string testID)
        //{
        //    var dataGroup = GetProjectCreation_ConfigData_FromSomeWhere();

        //    MasterworksScreen
        //        .Begin(testID, BrowserType.Chrome, true)
        //        .ExecuteCustomMethod(t =>
        //        {
        //            //can put all prerequsite here

        //        })
        //        .CreateProjectFromPlanning()

        //            .Set(dataGroup)

        //            .Set(t => t.EndDate, new DateTime(2017, 2, 21)) //.Set_EndDate(new DateTime(2016, 2, 21))

        //            .Set(t=> t.ProjectCategory, "Central")

        //            .Set(t => t.BusinessUnit, "test4352")

        //        .BeginVerification("Verify ProjectForm",
        //            (driver, formVerifier) =>
        //            {
        //                formVerifier
        //                    .Assert_ContractDays(176)//this way also can be coded
        //                                             //.Assert_ContractDays(1)//this way also can be coded
        //                    .Assert_CustomSettingsAreCorrect(dataGroup["ProjectName"].ToString())

        //                    .AssertCondition("Multiple verificaiton", t => t.ContractDays > 100 && t.ContractDays < 200)

        //                    .Assert_EndDate(new DateTime(2017, 2, 21))

        //                    .AssertCondition("ContractDays verificaiton", t => t.EndDate == new DateTime(2017, 2, 21))

        //                    .AssertCondition("ProjectName verificaiton", t => t.ProjectName == "someName")//or use generic way

        //                //.AssertBlock(t => t.ProjectOwner, "temp")
        //                ;
        //            })
        //            .ExecuteCustomMethod(t=>
        //            {
        //                //t.VerificationBlockList // user has access to this object here


        //            })
        //    .SaveForm(isStopOnVerificationException: true)
        //    .End_Automation()
        //    ;
        //}

        //public static OrderedDictionary GetProjectCreation_ConfigData_FromSomeWhere()
        //{
        //    string prefixData = Helpers.GetUniqueData("Project_From_Excel");

        //    OrderedDictionary projConfig = new OrderedDictionary();

        //    projConfig.Add("ProjectName", prefixData);
        //    projConfig.Add("ProjectCode", "PCODE_" + prefixData);
        //    projConfig.Add("ProjectOwner", "User-" + 1);
        //    projConfig.Add("Status", "Advertisement");

        //    return projConfig;
        //}

    }
}
