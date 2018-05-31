using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.Common.Dto;
using AurigoTest.Toolkit.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AurigoTest.Toolkit.MW
{
    public class GenericListPage : AbstractListPage<GenericListPage, GenericListPageVerifier, GenericFormPage>
    {

        public GenericListPage(IDriverLinker driverLinker, string listPageURL) : base(driverLinker, listPageURL)
        {
            ListPageUrl = listPageURL;
        }

        public T _GetParentObject_As<T>() where T : IDriverLinker
        {
            return (T)this.ParentObject;
        }

        public override GenericFormPage OpenCreateRecordForm(string optionalNewButtonId = null)
        {
            RibbonBar.Click_New_Button(optionalNewButtonId);

            DriverHelpers.WaitForIFrameContent(this.PrimaryDriver, ConfigData.IFrameID, "lnkCancel");

            return new GenericFormPage(this, ListPageUrl);
        }

        public GenericViewPage View_FirstRow(bool isTelerikGrid = true)
        {
            if (isTelerikGrid)
            {

                string gridID = (string)this.IFrameDriver.RunJavascript("return CONST_BrixListPage.ID.MWGrid;");

                var gridDiv = this.IFrameDriver.FindElement(By.Id(gridID));

                var gridTableContainingDiv = IFrameDriver.FindElement(By.Id(gridID + "_GridData"));

                var gridTable = gridTableContainingDiv.FindElement(By.Id(gridID + "_GridData")).FindElement(By.TagName("table"));

                string firstRowId = gridTable.GetAttribute("id") + "__0";//"ctl00_C1_MWGrid_ctl00";//
                var firstRow_tr = gridTable.FindElement(By.Id(firstRowId));

                //firstRow_tr.Click();
                new Actions(this.IFrameDriver).DoubleClick(firstRow_tr).Build().Perform();

            }

            DriverHelpers.WaitForIFrameContent(this.PrimaryDriver, ConfigData.IFrameID, "lnkCancel");

            return new GenericViewPage(this, ListPageUrl);
        }

      

        public GenericFormPage Edit_FirstRow(bool isTelerikGrid = true)
        {

            if (isTelerikGrid)
            {
                //IFrameDriver.FindElements(By.XPath("//*[@class='RadGrid RadGrid_Office2007 MWGrid' and @class='dact']"));
                string gridID = (string)this.IFrameDriver.RunJavascript("return CONST_BrixListPage.ID.MWGrid;");

                var gridDiv = this.IFrameDriver.FindElement(By.Id(gridID));

                var gridTableContainingDiv = IFrameDriver.FindElement(By.Id(gridID + "_GridData"));

                var gridTable = gridTableContainingDiv.FindElement(By.Id(gridID + "_GridData")).FindElement(By.TagName("table"));

                string firstRowId = gridTable.GetAttribute("id") + "__0";//"ctl00_C1_MWGrid_ctl00";//
                var firstRow_tr = gridTable.FindElement(By.Id(firstRowId));

                firstRow_tr.Click();

                RibbonBar.Click_Edit_Button();

                DriverHelpers.WaitForIFrameContent(this.PrimaryDriver, ConfigData.IFrameID, "lnkCancel");

                return new GenericFormPage(this, ListPageUrl);
            }
            else
            {
                //var gridDiv = IFrameDriver.FindElement(By.Id("RadGrid RadGrid_Office2007 MWGrid"));
            }

            DriverHelpers.WaitForIFrameContent(this.PrimaryDriver, ConfigData.IFrameID, "lnkCancel");

            return new GenericFormPage(this, ListPageUrl);
        }


        private string ManipulateURL_After_Hash(string url, Action<UrlGenerator> internalActions)
        {
            int i = url.IndexOf('#');

            string frontUrl = "";
            string proxyUrl = url;

            if (i > 0 && i != url.Length - 1)
            {
                frontUrl = url.Substring(0, i + 1);
                proxyUrl = url.Substring(i + 1);
            }

            UrlGenerator urlGen = new UrlGenerator(proxyUrl);

            internalActions.Invoke(urlGen);

            return frontUrl + urlGen.ToString();
        }

        private string Helper_Generate_ViewOrEdit_NavigationUrl(string primaryKeyValue, bool isView)
        {
            string newUrl = ManipulateURL_After_Hash(this.PrimaryDriver.Url, (urlGen) =>
            {
                string keyValue = urlGen.GetValueForKey("xcontext");

                if (!string.IsNullOrEmpty(keyValue))
                    urlGen.Remove("xcontext").Add("context", keyValue).Add("InstanceID", primaryKeyValue);

                if (isView)
                    urlGen.Add("Mode", "View");
                else
                    urlGen.Add("Mode", "Edit");

            });

            string urlToNavigateTo = newUrl.ToString().Replace("BrixListPage.aspx", "BrixForm.aspx");


            return urlToNavigateTo;
        }

        public GenericViewPage ViewRow_WithId_ByNavigationUrl(string primaryKeyValue)
        {
            string urlToNavigateTo = Helper_Generate_ViewOrEdit_NavigationUrl(primaryKeyValue, true);
            base.GoTo_URL(urlToNavigateTo);

            return new GenericViewPage(this, ListPageUrl);
        }

        public GenericFormPage EditRow_WithId_ByNavigationUrl(string primaryKeyValue)
        {
            string urlToNavigateTo = Helper_Generate_ViewOrEdit_NavigationUrl(primaryKeyValue, false);
            base.GoTo_URL(urlToNavigateTo);

            return new GenericFormPage(this, ListPageUrl);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnDisplayName"></param>
        /// <param name="value"></param>
        /// <param name="filterOptionText">Contains Equals</param>
        /// <returns></returns>
        public GenericListPage FilterColumn_Textbox(string columnDisplayName, string value, EnumGridFilterTypes filterOption)
        {
            throw new NotImplementedException();
            return this;
        }

        public GenericListPage FilterColumn_Numeric(string columnDisplayName, decimal value, EnumGridFilterTypes filterOption)
        {
            throw new NotImplementedException();
            return this;
        }

        public GenericListPage FilterColumn_Checkbox(string columnDisplayName, bool isChecked, EnumGridFilterTypes filterOption)
        {
            throw new NotImplementedException();
            return this;
        }


        public GenericFormPage EditRow_WithIndex(int index)
        {
            throw new NotImplementedException();
            return new GenericFormPage(this, ListPageUrl);
        }



        public GenericListPage SelectRow(int index)
        {
            return this;
        }

        public GenericListPage DeleteSelectedRow(int index)
        {
            return this;
        }


        public GenericFormPage CreateRecordForm_FullyAutomated(string moduleId, string optionalNewButtonId = null)
        {
            RibbonBar.Click_New_Button(optionalNewButtonId);

            GenericFormPage form = new GenericFormPage(this, ListPageUrl);

            ////form.Get
            //var xmlDoc = DBHelper.GetXML_Form(moduleId);

            //IEnumerable<XmlElement> xmlEleList = xmlDoc.GetElementsByTagName("Control").Cast<XmlElement>();//.FirstOrDefault(t => t.Attributes[attributeName]?.Value == fieldName);

            //if (xmlEleList == null)
            //    throw new InvalidDataException();

            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<XControl>));

            //using (TextReader textReader = new StringReader(xmlDoc.GetElementsByTagName("Control").))
            //{
            //    return (XControl)xmlSerializer.Deserialize(new NamespaceIgnorantXmlTextReader(textReader));
            //}

            return form;
        }
    }
}


///// <summary>
///// 
///// </summary>
///// <param name="actionIfIdExists">string id, reference to list page</param>
///// <returns></returns>
//public GenericListPage ExecuteCustom_Using_LastId(string tableName, string idFieldName, string hintFieldName, string hintFieldValue, Action<string, GenericListPage> actionIfIdExists)
//{
//    string id = DBHelper.GetLastCreatedIdForTable(tableName, idFieldName, hintFieldName, hintFieldValue);

//    if (!string.IsNullOrEmpty(id))
//        actionIfIdExists.Invoke(id, this);
//    else
//        throw new Exception(string.Format("Last ID not available for {0}.{1}", tableName, idFieldName));

//    return this;
//}

//public GenericListPage ExecuteCustom_Using_LastId(HintFieldSetting hintFieldSetting, Action<string, GenericListPage> actionIfIdExists)
//{
//    return ExecuteCustom_Using_LastId(hintFieldSetting.TableName, hintFieldSetting.IdField, hintFieldSetting.HintField, hintFieldSetting.HintFieldValue, actionIfIdExists);
//}

//public GenericListPage VerifyInDB_Using_LastId(string verificationIdentification, string tableName, string idFieldName, string hintFieldName, string hintFieldValue, Action<DataRowVerifier, GenericListPage> actionIfIdExists)
//{
//    string testName = Helper_GetActualTestName(verificationIdentification);
//    var extentTest = Helpers.Report.StartTest(testName);

//    try
//    {
//        var dt = base.BeginDatabaseVerification_Using_LastId(tableName, idFieldName, hintFieldName, hintFieldValue);

//        var dataVerifier = new DataRowVerifier(dt.Rows[0], string.Empty);

//        actionIfIdExists.Invoke(dataVerifier, this);

//        base.LogTestSteps(extentTest, testName, dataVerifier.VerificationStepsTrackerList);
//    }
//    catch (Exception ex)
//    {
//        extentTest.Log(LogStatus.Error, ex);
//    }

//    Helpers.Report.EndTest(extentTest);

//    return this;
//}

//public GenericListPage VerifyInDB_Using_LastId(string verificationIdentification, HintFieldSetting hintFieldSetting, Action<DataRowVerifier, GenericListPage> actionIfIdExists)
//{
//    return VerifyInDB_Using_LastId(verificationIdentification, hintFieldSetting.TableName, hintFieldSetting.IdField, hintFieldSetting.HintField, hintFieldSetting.HintFieldValue, actionIfIdExists);
//}


///// <summary>
///// 
///// </summary>
///// <param name="action">string id, reference to list page</param>
///// <returns></returns>
//public GenericListPage ExecuteCustom_Using_LastId(string tableName, string idFieldName, Action<string, GenericListPage> actionIfIdExists)
//{
//    string id = DBHelper.GetLastCreatedIdForTable(tableName, idFieldName);

//    if (!string.IsNullOrEmpty(id))
//        actionIfIdExists.Invoke(id, this);
//    else
//        throw new Exception(string.Format("Last ID not available for {0}.{1}", tableName, idFieldName));
//    //else if (actionIf_NO_Id != null)
//    //    actionIf_NO_Id.Invoke(this);

//    return this;
//}
/*
        public FormPage EditRow_WithId_ByNavigationUrl_Temp(string primaryKeyValue)
        {
            
            //UrlGenerator urlGen = new UrlGenerator("http://p1.dev.aurigoblr.com/Common/BrixListPage.aspx?xcontext=XF00044&PID=0&parentid=0")
           
            
            //                Console.WriteLine(this.IFrameDriver.Url);

            //                //IFrameDriver.FindElements(By.XPath("//*[@class='RadGrid RadGrid_Office2007 MWGrid' and @class='dact']"));
            //                this.IFrameDriver.Scripts().ExecuteScript(
            //                    @"
            //var backup_lnkValidation = lnkValidation;

            //window.lnkValidation = function(ValidateSelect, gridName, queryStringName) {    

            //    document.getElementById('hdnMulti').value = '" + primaryKeyValue + @"';


            //    return true;
            //}
            //");

            //alert(document.getElementById('hdnMulti').value);

            //var gridDiv = this.IFrameDriver.FindElement(By.Id(gridID));

            //var gridTableContainingDiv = IFrameDriver.FindElement(By.Id(gridID + "_GridData"));

            //var gridTable = gridTableContainingDiv.FindElement(By.Id(gridID + "_GridData")).FindElement(By.TagName("table"));

            //string firstRowId = gridTable.GetAttribute("id") + "__0";//"ctl00_C1_MWGrid_ctl00";//
            //var firstRow_tr = gridTable.FindElement(By.Id(firstRowId));

            //firstRow_tr.Click();

            //RibbonBar.Click_Edit_Button();

            //    return new FormPage(this, ListPageUrl);
            //}
            //else
            //{
            //    //var gridDiv = IFrameDriver.FindElement(By.Id("RadGrid RadGrid_Office2007 MWGrid"));
            //}


            return new FormPage(this, ListPageUrl);
        }

 * */
