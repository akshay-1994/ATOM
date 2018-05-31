using AurigoTest.Toolkit.Core;
using AurigoTest.Toolkit.MW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInConsole.Samples
{
    class testDemo
    {


        public static void ErrorGenerate(string testId, string testSummary)
        {
            throw new Exception("test eerrrror");
        }




        public static void Sample_View_FirstRow(string testId, string testSummary)
        {
            MasterworksScreen ts = new MasterworksScreen(testId, testSummary, BrowserType.Chrome, true);

            ts.Home()
                .OpenEnterprise_Form_ByDisplayName("Timezone Testing")
                .View_FirstRow();
        }

        public static void Sample_EditFirstRow(string testId, string testSummary)
        {
            //MasterworksScreen ts = new MasterworksScreen(testId, testSummary, BrowserType.Chrome, false);

            //ts.Home()
            //    .OpenEnterprise_Form_ByDisplayName("Timezone Testing")
            //    .Edit_FirstRow()
            //    .DoVerification(
            //        (driver, form, vScreen) =>
            //        {
            //            //vScreen
            //        })
            //        ;





            //Form editFrm = ts.OpenProject(872).OpenListPage_By_ModuleName("Pay Estimates")
            //    .FilterColumn_Numeric("Pay Estimate Number", 2323, EnumGridFilterTypes.Contains)
            //    .FilterColumn_Textbox("text", "aaa", EnumGridFilterTypes.Contains)
            //    //.FilterColumn_Checkbox("CheckFieldName", true, EnumGridFilterTypes.Contains)
            //    .EditFirstRow();


            //editFrm.SetTextbox("aaaa", "aaaaa");
        }


        public static void AddNewProject(string testID)
        {
            string projectId = DBHelper.GetLastCreatedIdForTable("PROJECTProjectMain", "ProjectId", "ProjectName", "TemplateProject", EnumHintFieldSearchTechnique.ExactMatch);


            //return Convert.ToInt32(projectId);


            //AutomateScreen ts = new AutomateScreen(BrowserType.Chrome, true);

            //ts.CreateProject()
            //  .OpenListPage_By_Path("Budget Management\\Budget Estimates")
            //  .OpenCreateRecordForm()
            //  .SetTextbox("BudgetEstimateName", "AutomatedBudget")
            //  .SetComobobox_ByText("BudgetEstimateType", "Construction")
            //  .SetComobobox_ByText("MeasurementSystem", "IS System").ExecuteCustomMethod((driver) =>
            //  {

            //  })
            //  .SaveForm()
            //  ;

        }

    }
}

//var ttt = DBHelper.GetXML_FromControlObject_ByName("XPROJCT", "ProjectCode");

//XmlDocument xmlDoc = DBHelper.GetXML_Form("XPROJCT");

//var list = xmlDoc.GetElementsByTagName("Control").Cast<XmlElement>();

//XmlElement xmlEle = list.FirstOrDefault(t => t.Attributes["Name"]?.Value == "ProjectCode");

//list.AsQueryable().Attributes["Name"] == 




//DBHelper.Shapshot_Create("asheesh");
//DBHelper.Snapshot_Delete("asheesh");
//DBHelper.Snapshot_Restore("asheesh");

//DBHelper.GetNewConnection_As_MasterDatabase();

//TestRunner.Run("TC_PRJ_0001", "Add New Project", PlanningProjectTest.Add_NewProject);

//PlanningProjectManager mgr = new PlanningProjectManager();

//TestRunner.Run("TC_PRJ_0001", "Add New Project", mgr.CreateAndVerify_NewProject_IfNotExists);