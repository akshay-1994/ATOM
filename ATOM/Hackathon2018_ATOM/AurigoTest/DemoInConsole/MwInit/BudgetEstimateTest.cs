using AurigoTest.Toolkit;
using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.Core;
using AurigoTest.Toolkit.MW;
using AurigoTest.Toolkit.MW.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BgtEst = AurigoTest.Toolkit.MW.Constants.CONST_BudgetEstimate.Form;
using DB = AurigoTest.Toolkit.Core.DBHelper;

namespace DemoInConsole.MwInit
{
    public class BudgetEstimateTest
    {
        public string Current_BudgetEstimateName { get; private set; }

        [RunCustom]
        [Run]
        public void DemoData_CreateAndVerify_BudgetEstimate(string testId, string testSummary)
        {
            const string projectCodeValue = PlanningProjectManager.CONST_TEST_PROJECT_CODE;

            int currentProjectId = Convert.ToInt32(DB.GetLastCreatedIdForTable(HintFieldLookup.Project_By_ProjectCode(projectCodeValue)));

            Current_BudgetEstimateName = "AutomatedBudget"; // Helpers.GetUniqueData("AutomatedBudget");

            string currentAutomationId = Current_BudgetEstimateName;

            var hintObject_BudgetEstimate = HintFieldLookup.BudgetEstimate(currentAutomationId);

            MasterworksScreen
                .Begin(testId, testSummary, BrowserType.Chrome, true)
                .OpenProject_ById(currentProjectId)
                .OpenListPage_By_Path(TreePath_UnderProject.BudgetEstimate)
                .OpenCreateRecordForm()
                    .SetTextbox(BgtEst.BudgetEstimateName, currentAutomationId)//.Wait(5)
                    .SetComobobox_ByText(BgtEst.BudgetEstimateType, "Construction")//.Wait(5)//.Wait_Till_TextToBePresentInElement(By.Id("xyz"), "xyz")
                    .SetComobobox_ByText(BgtEst.MeasurementSystem, "IS System")
                    
                .SaveForm_Successfully()
                .ExecuteCustom_Using_LastId(hintObject_BudgetEstimate, (id, listPageRef) =>
                {
                    var form = listPageRef.EditRow_WithId_ByNavigationUrl(id);

                    form.BeginVerification("Verify BudgetEstimateType",
                    (driver, formVerifier) =>
                    {
                        formVerifier
                            .AssertTextbox(BgtEst.BudgetEstimateName, currentAutomationId)
                            .AssertComobobox_ByText(BgtEst.BudgetEstimateType, "Construction1")
                        ;
                    });
                })
                .VerifyInDB_Using_LastId("Verify BudgetEstimate in DB", hintObject_BudgetEstimate, (rowVerifier, listPageRef) =>
                //.VerifyInDB_Using_LastId("Verify BudgetEstimate in DB", CONST_BudgetEstimate.TableName, "ID", CONST_BudgetEstimate.Form.BudgetEstimateName, currentAutomationId, (rowVerifier, listPageRef) =>
                {
                    rowVerifier
                        .Assert_Data(CONST_BudgetEstimate.Form.BudgetEstimateName, currentAutomationId)
                        .Assert_Data(CONST_BudgetEstimate.Form.BudgetEstimateType, 1)
                    ;
                })
                //.DB_Run_SelectStatement("Select * from Table1", (dataSet, listPageRef)=>
                //{
                //    //dataSet.Tables[0].Rows
                //})
                .End_Automation()
            ;
        }

        public void CreateAndVerify_BudgetEstimate_ViewPage(string testId, string testSummary)
        {
            const string projectCodeValue = PlanningProjectManager.CONST_TEST_PROJECT_CODE;

            int currentProjectId = Convert.ToInt32(DB.GetLastCreatedIdForTable(HintFieldLookup.Project_By_ProjectCode(projectCodeValue)));

            Current_BudgetEstimateName = Helpers.GetUniqueData("AutomatedBudget");

            string currentAutomationId = Current_BudgetEstimateName;

            var hintObject_BudgetEstimate = HintFieldLookup.BudgetEstimate(currentAutomationId);

            MasterworksScreen
                .Begin(testId, testSummary, BrowserType.Chrome, true)
                .OpenProject_ById(currentProjectId)
                .OpenListPage_By_Path(TreePath_UnderProject.BudgetEstimate)
                .OpenCreateRecordForm()
                    .SetTextbox(BgtEst.BudgetEstimateName, currentAutomationId)//.Wait(5)
                    .SetComobobox_ByText(BgtEst.BudgetEstimateType, "Construction")//.Wait(5)//.Wait_Till_TextToBePresentInElement(By.Id("xyz"), "xyz")
                    .SetComobobox_ByText(BgtEst.MeasurementSystem, "IS System")
                .SaveForm_Successfully()
                .ExecuteCustom_Using_LastId(CONST_TableNames.BudgetEstimate, hintObject_BudgetEstimate.IdField, (id, listPageRef) =>
                {
                    var viewPage = listPageRef.ViewRow_WithId_ByNavigationUrl(id);

                    viewPage.BeginVerification((driver, viewVerifier) =>
                    {
                        viewVerifier
                            .AssertTextbox(BgtEst.BudgetEstimateName, currentAutomationId)
                            .AssertDate("Date000", 2002, 10, 20)
                            .AssertDateTime("Date012", new DateTime(2016, 1, 29, 0, 0, 0))

                        ;
                    });
                })
                .VerifyInDB_Using_LastId("Verify BudgetEstimate in DB", hintObject_BudgetEstimate, (rowVerifier, listPageRef) =>
                //.VerifyInDB_Using_LastId("Verify BudgetEstimate in DB", CONST_BudgetEstimate.TableName, "ID", CONST_BudgetEstimate.Form.BudgetEstimateName, currentAutomationId, (rowVerifier, listPageRef) =>
                {
                    rowVerifier
                        .Assert_Data(CONST_BudgetEstimate.Form.BudgetEstimateName, currentAutomationId)
                        .Assert_Data(CONST_BudgetEstimate.Form.BudgetEstimateType, 1)
                    ;
                })
                //.DB_Run_SelectStatement("Select * from Table1", (dataSet, listPageRef)=>
                //{
                //    //dataSet.Tables[0].Rows
                //})
                .End_Automation()
            ;
        }



        [IgnoreRun]
        public static void EditProject_Add_CustomFormScreen(string testId, string testSummary)
        {
            int currentProjectId = 872;
            string currentAutomationId = Helpers.GetUniqueData("AutomatedBudget");
            var hintObject_BudgetEstimate = HintFieldLookup.BudgetEstimate(currentAutomationId);

            MasterworksScreen
                .Begin(testId, testSummary, BrowserType.Chrome, true)
                .OpenProject_ById(currentProjectId)
                .OpenListPage_By_Path(TreePath_UnderProject.BudgetEstimate)
                .OpenCreateRecordForm()
                .SetTextbox(BgtEst.BudgetEstimateName, currentAutomationId)//.Wait(5)
                .SetComobobox_ByText(BgtEst.BudgetEstimateType, "Construction")//.Wait(5)//.Wait_Till_TextToBePresentInElement(By.Id("xyz"), "xyz")
                .SetComobobox_ByText(BgtEst.MeasurementSystem, "IS System")
                .SaveForm_Successfully()
                .ExecuteCustom_Using_LastId(hintObject_BudgetEstimate, (id, listPageRef) =>
                {
                    var form = listPageRef.EditRow_WithId_ByNavigationUrl(id);

                    form.BeginVerification("Verify BudgetEstimateType",
                    (driver, formVerifier) =>
                    {
                        formVerifier
                            .AssertTextbox(BgtEst.BudgetEstimateName, currentAutomationId)
                            .AssertComobobox_ByText(BgtEst.BudgetEstimateType, "Construction1")
                        ;
                    });
                })
                .VerifyInDB_Using_LastId("Verify BudgetEstimate in DB", hintObject_BudgetEstimate, (rowVerifier, listPageRef) =>
                //.VerifyInDB_Using_LastId("Verify BudgetEstimate in DB", CONST_BudgetEstimate.TableName, "ID", CONST_BudgetEstimate.Form.BudgetEstimateName, currentAutomationId, (rowVerifier, listPageRef) =>
                {
                    rowVerifier
                        .Assert_Data(CONST_BudgetEstimate.Form.BudgetEstimateName, currentAutomationId)
                        .Assert_Data(CONST_BudgetEstimate.Form.BudgetEstimateType, 1)
                    ;
                })
                //.DB_Run_SelectStatement("Select * from Table1", (dataSet, listPageRef)=>
                //{
                //    //dataSet.Tables[0].Rows
                //})
                .End_Automation()
            ;
        }
        
    }
}
