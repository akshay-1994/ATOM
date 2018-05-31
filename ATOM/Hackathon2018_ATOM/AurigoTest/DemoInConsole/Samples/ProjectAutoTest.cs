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
    public static class ProjectAutoTest
    {
        public static void Add_NewProject(string testId, string testSummary)
        {
            string currentAutomationId = Helpers.GetUniqueData("Project_Auto");

            HashSet<string> ignoreListControls = new HashSet<string>();//get this list from some settings location

            MasterworksScreen
                .Begin(testId, testSummary, BrowserType.Chrome, true)
                .CreateProjectFromPlanning()
                    .Set(t => t.ProjectName, currentAutomationId)
                    .Set(t => t.ProjectCode, "PCODE_" + currentAutomationId)
                    .Set(t => t.ProjectOwner, "User-Automator")
                    .Set(t => t.ProjectStatus, "Advertisement")
            //.Set_ProjectName(currentAutomationId).Wait(5)
            //.Set_ProjectCode("PCODE_" + currentAutomationId)
            //.Set_ProjectOwner("User-Automator")
            //.Set_ProjectStatus("Advertisement")//combobox
            //.SaveForm()

            .End_Automation()
            ;
        }

        public static void Add_NewProject_UsingData(string testId, string testSummary)
        {
            var dataGroup = GetProjectCreation_ConfigData_FromSomeWhere();

            MasterworksScreen
                .Begin(testId, testSummary, BrowserType.Chrome, true)
                .ExecuteCustomMethod(t =>
                {
                    //can put all prerequsite here

                })
                .CreateProjectFromPlanning()

                    .Set(dataGroup)

                    .Set(t => t.EndDate, new DateTime(2017, 2, 21)) //.Set_EndDate(new DateTime(2016, 2, 21))

                    .Set(t=> t.ProjectCategory, "Central")

                    .Set(t => t.BusinessUnit_Text, "test4352")

                .BeginVerification("Verify ProjectForm",
                    (driver, formVerifier) =>
                    {
                        formVerifier
                            .Assert_ContractDays(176)//this way also can be coded
                                                     //.Assert_ContractDays(1)//this way also can be coded
                            .Assert_CustomSettingsAreCorrect(dataGroup["ProjectName"].ToString())

                            .AssertCondition("Multiple verificaiton", t => t.ContractDays > 100 && t.ContractDays < 200)

                            .Assert_EndDate(new DateTime(2017, 2, 21))

                            .AssertCondition("ContractDays verificaiton", t => t.EndDate == new DateTime(2017, 2, 21))

                            .AssertCondition("ProjectName verificaiton", t => t.ProjectName == "someName")//or use generic way

                        //.AssertBlock(t => t.ProjectOwner, "temp")
                        ;
                    })
                    .ExecuteCustomMethod(t=>
                    {
                        //t.VerificationBlockList // user has access to this object here

                        
                    })
            .SaveForm_Successfully(isStopOnVerificationException: true)
            .End_Automation()
            ;
        }

        public static OrderedDictionary GetProjectCreation_ConfigData_FromSomeWhere()
        {
            string prefixData = Helpers.GetUniqueData("Project_From_Excel");

            OrderedDictionary projConfig = new OrderedDictionary();

            projConfig.Add("ProjectName", prefixData);
            projConfig.Add("ProjectCode", "PCODE_" + prefixData);
            projConfig.Add("ProjectOwner", "User-" + 1);
            projConfig.Add("Status", "Advertisement");

            return projConfig;
        }

    }
}
