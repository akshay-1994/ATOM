//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using AurigoTest.Toolkit.MW;
//using AurigoTest.Toolkit.Core;

//namespace AurigoTest.UT.GenericFormControls
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        [TestMethod]
//        public void UsingExistingData()
//        {
//            AutomateScreen ts = new AutomateScreen(BrowserType.Chrome, true);

//            var project = ts.OpenProject(872);


//            //Form editFrm = ts.OpenProject(872).OpenListPage_By_ModuleName("Pay Estimates")
//            //    .FilterColumn_Numeric("Pay Estimate Number", 2323, EnumGridFilterTypes.Contains)
//            //    .FilterColumn_Textbox("text", "aaa", EnumGridFilterTypes.Contains)
//            //    //.FilterColumn_Checkbox("CheckFieldName", true, EnumGridFilterTypes.Contains)
//            //    .EditFirstRow();


//            //editFrm.SetTextbox("aaaa", "aaaaa");
//        }

//        //[TestMethod]
//        public void UsingNewForm()
//        {
//            AutomateScreen ts = new AutomateScreen();

//            Form newFrm = ts.CreateProject().OpenListPage_By_ModuleName("Pay Estimates")
//                .CreateNewForm();

//            newFrm.SetTextbox("aaaa", "aaaaa");

//            newFrm.ExecuteCustomMethod((driver) =>
//            {
//                //driver.FindElement()
//            });

//        }
//    }
//}
