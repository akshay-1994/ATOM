using AurigoTest.Toolkit;
using AurigoTest.Toolkit.Core;
using AurigoTest.Toolkit.MW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInConsole.Samples
{
    public class SampleFormTest : TestClassBase
    {
        const string DEFAULT_USER_NAME = "administrator";
        const string DEFAULT_USER_PWD = "aurigo";

        [MethodReport(Id = "ASHEESH123456789", Description = @" 
this is a kadsj;flkjasl ;d
sdlkjf;aslkdf
slkdjf;lkjsdf
sadkfhlkasjdhfk ajsdh ksajdhflkjha2
sdfasdfsadfasdfasdfasdf
this is a kadsj;flkjasl ;d
sdlkjf;aslkdf
slkdjf;lkjsdf
sadkfhlkasjdhfk ajsdh ksajdhflkjha
sdfasdfsadfasdfasdfasdfthis is a kadsj;flkjasl ;d4
sdlkjf;aslkdf3
slkdjf;lkjsdf
sadkfhlkasjdhfk ajsdh ksajdhflkjha
sdfasdfsadfasdfasdfasdfthis is a kadsj;flkjasl ;d5
sdlkjf;aslkdf
slkdjf;lkjsdf
sadkfhlkasjdhfk ajsdh ksajdhflkjha
sdfasdfsadfasdfasdfasdfthis is a kadsj;flkjasl ;d6
sdlkjf;aslkdf
slkdjf;lkjsdf
sadkfhlkasjdhfk ajsdh ksajdhflkjha  7
sdfasdfsadfasdfasdfasdf8
")]
        public void Open(string testId, string testSummary)
        {
            MasterworksScreen
             .Begin(testId, testSummary, BrowserType.Chrome, false)
             .Login(DEFAULT_USER_NAME, DEFAULT_USER_PWD)
             .OpenEnterprise_Form_ByDisplayName("Sample Form")

             .OpenCreateRecordForm()
             .SetTextbox("Name", "asheesh")

             //{Contols_HERE}

             //Add one dynamic grid record
             .DynamicGrid_AddRecord("TableDummy")
             //.SetTextbox()
             .Save_DynamicGridData()

             ////Add another dynamic grid record (2)
             // .DynamicGrid_AddRecord("TableDummy")
             ////.SetTextbox()
             //.Save_DynamicGridData()

             ////Add another dynamic grid record (3)
             // .DynamicGrid_AddRecord("TableDummy")
             ////.SetTextbox()
             //.Save_DynamicGridData()

             //Set the form text field
             .SetTextbox("Name", "bajracharya")


             .SaveForm_Successfully()
             //{ SAVE_CODE}

             //.SaveForm_ExpectValidationError()


             //  .DynamicGrid_AddRecord("TableDummy")
             ////.SetTextbox()
             //.Save_DynamicGridData()

             //.ExecuteCustom_Using_LastId("SAMPLEFSampleForm", "ID", (id, listPageRef) =>
             //{
             //    var form = listPageRef.EditRow_WithId_ByNavigationUrl(id);

             //    form.BeginVerification((driver, formVerifier) =>
             //    {
             //        //formVerifier
             //        //    .AssertDate("Date000", 2002, 10, 20)
             //        //    .AssertDateTime("Date012", new DateTime(2016, 1, 29, 0, 0, 0))
             //        //;
             //    });
             //})
             .End_Automation()
             ;

        }
        //{END TEST_0001}
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