using AurigoTest.Toolkit.Core;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW.Customizations
{
    public class ProjectFormPage : AbstractFormPage<ProjectFormPage, ProjectFormPageVerifier, GenericListPage>
    {
        private ElementManipulator _elementHelper = null;
        private ElementManipulator EM
        {
            get
            {
                if (_elementHelper == null)
                    _elementHelper = new ElementManipulator(this.IFrameDriver);

                //base.IFrameDriver_Flush();

                return _elementHelper;
            }
        }


        public ProjectFormPage(GenericListPage driverLinker, string listPageURL) : base(driverLinker, listPageURL, "PROJECT")
        {
            _listPageURL = listPageURL;

            //var urlGen = new UrlGenerator(listPageURL);

            //if (urlGen.ContainsKey("xcontext"))
            //    _formContext = urlGen.GetValueForKey("xcontext");
            //else if (urlGen.ContainsKey("context"))
            //    _formContext = urlGen.GetValueForKey("context");

            //if (string.IsNullOrEmpty(_formContext))
            //    _formContext = listPageURL;

        }

        #region Project Form Element Accessors

        public string ProjectName { get { return EM.GetText_Textarea_IdEndsWith("txtProjectName"); } set { EM.SetText_Textarea_IdEndsWith("txtProjectName", value); } }
        public string ProjectCode { get { return EM.GetText_Textbox_IdEndsWith("txtProjectCode"); } set { EM.SetText_Textbox_IdEndsWith("txtProjectCode", value); } }
        public string ProjectOwner { get { return EM.GetText_Textbox_IdEndsWith("txtProjectOwner"); } set { EM.SetText_Textbox_IdEndsWith("txtProjectOwner", value); } }
        public string ProjectStatus { get { return EM.GetText_Combobox_IdEndsWith("ddlStatus"); } set { EM.SetText_Combobox_IdEndsWith("ddlStatus", value); } }

        public string Calendar { get { return EM.GetText_Combobox_IdEndsWith("ddlExpenditureCurve"); } set { EM.SetText_Combobox_IdEndsWith("ddlExpenditureCurve", value); } }

        public int ContractDays { get { return Convert.ToInt32(EM.GetText_Textbox_IdEndsWith("wneContractDays") ?? "0"); } private set { EM.SetText_Textbox_IdEndsWith("wneContractDays", value.ToString()); } }
        public DateTime? StartDate { get { return EM.GetDate_For_WebDateChooser_Using_IdEndsWith("wdcStartDate"); } set { EM.SetDate_For_WebDateChooser_Using_IdEndsWith("wdcStartDate", value); } }
        public DateTime? EndDate { get { return EM.GetDate_For_WebDateChooser_Using_IdEndsWith("wdcEndDate"); } set { EM.SetDate_For_WebDateChooser_Using_IdEndsWith("wdcEndDate", value); } }
        public string ProjectCategory { get { return EM.GetText_Combobox_IdEndsWith("ddlProjectClass"); } set { EM.SetText_Combobox_IdEndsWith("ddlProjectClass", value); } }
        public string BusinessUnit_Text { get { return EM.GetSelectionTextOrValue_For_TreeCombobox_Using_IdEndsWith("radddlBusinessUnitTree", isByText: true); } set { EM.SetSelection_For_TreeCombobox_Using_IdEndsWith("radddlBusinessUnitTree", value, isByText: true); } }
        public string BusinessUnit_Value { get { return EM.GetSelectionTextOrValue_For_TreeCombobox_Using_IdEndsWith("radddlBusinessUnitTree", isByText: false); } set { EM.SetSelection_For_TreeCombobox_Using_IdEndsWith("radddlBusinessUnitTree", value, isByText: false); } }
        public string ProgramYear { get { return EM.GetText_Combobox_IdEndsWith("ddlProgramYear"); } set { EM.SetText_Combobox_IdEndsWith("ddlProgramYear", value); } }

        #endregion Project Form Element Accessors

        //protected override ProjectFormPage DecodeAndSetValue(string propertyName, object value)
        //{
        //    var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    PropertyInfo propertyInfo = properties.FirstOrDefault(t => t.Name == propertyName);

        //    if (propertyInfo == null)
        //        throw new NotImplementedException(string.Format("Property '{0}' not found in class.", propertyName));

        //    propertyInfo.SetValue(this, value);

        //    //if (propertyInfo.GetIndexParameters().Length == 0)
        //    //{
        //    //    var propertyValue = propertyInfo.GetValue(value);
        //    //    dynamicObject[propertyInfo.Name] = propertyValue;
        //    //}


        //    //foreach (var propertyInfo in properties)
        //    //{
        //    //    if (propertyInfo.GetIndexParameters().Length == 0)
        //    //    {
        //    //        var propertyValue = propertyInfo.GetValue(value);
        //    //        dynamicObject[propertyInfo.Name] = propertyValue;
        //    //    }
        //    //}

        //    //switch (propertyName)
        //    //{
        //    //    case "ProjectName": this.ProjectName = value as string; break;
        //    //    case "ProjectCode": this.ProjectCode = value as string; break;
        //    //    case "ProjectOwner": this.ProjectOwner = value as string; break;
        //    //    //case "ContractDays": this.ContractDays = (int)value; break;//this should not be set
        //    //    case "ProjectStatus": this.ProjectStatus = value as string; break;
        //    //    case "Calendar": this.Calendar = value as string; break;
        //    //    case "StartDate": this.StartDate = value as DateTime?; break;
        //    //    case "EndDate": this.EndDate = value as DateTime?; break;
        //    //    case "ProjectCategory": this.ProjectCategory = value as string; break;
        //    //    case "BusinessUnit":
        //    //    case "BusinessUnit_Text": this.BusinessUnit_Text = value as string; break;
        //    //    case "BusinessUnit_Value": this.BusinessUnit_Text = value as string; break;


        //    //    default: throw new NotImplementedException(propertyName);
        //    //}

        //    return this;
        //}

        public override GenericListPage GoToListPage()
        {
            throw new NotImplementedException();
        }

        public override GenericListPage SaveForm_Successfully(bool isStopOnVerificationException = true, string optionalButtonId = null)
        {
            return base.SaveForm_Successfully(isStopOnVerificationException, optionalButtonId);
        }

        public ProjectViewPage SaveForm_Goto_ViewMode(bool isStopOnVerificationException = true, string optionalButtonId = null)
        {
            GenericListPage listPage = base.SaveForm_Successfully(isStopOnVerificationException, optionalButtonId);

            //http://p1.dev.aurigoblr.com/Default.aspx#/Modules/PROJECT/ProjectInfo.aspx?pid=1014&Context=PROJECT&InstanceID=0&Mode=View&PP=1

            throw new NotImplementedException();
        }

        public MasterworksScreen SaveForm_Goto_MasterworksScreen(bool isStopOnVerificationException = true, string optionalButtonId = null)
        {
            GenericListPage listPage = base.SaveForm_Successfully(isStopOnVerificationException, optionalButtonId);

            return listPage._GetParentObject_As<MasterworksScreen>();
        }
    }
}


//#region Private methods

//private DateTime? GetDate_For_WebDateChooser_Using_IdEndsWith(string ctrl_id)
//{
//    const string postfix_String = "_input";
//    string generatedId = ctrl_id + postfix_String;
//    var webEle = ElementHelper.GetEle_Textbox_IdEndsWith(generatedId);

//    string fullId = webEle.GetAttribute("id");//this id will be used to get control reference for WebDateChooser

//    string requiredControlId = fullId.Remove(fullId.Length - postfix_String.Length);

//    /*
//     * $find('C1_ERP_CC_BODY_wdcDateCreated')
//     * var date = new Date(dateText); dp.set_value(date);
//     */
//    string dateValue = (string)this.IFrameDriver.RunJavascript(string.Format("igdrp_getComboById('{0}').getValue()", requiredControlId));

//    return null;
//}

//private void SetDate_For_WebDateChooser_Using_IdEndsWith(string ctrl_id, DateTime? dt)
//{//wdcDateCreated

//    const string postfix_String = "_input";
//    string generatedId = ctrl_id + postfix_String;
//    var webEle = ElementHelper.GetEle_Textbox_IdEndsWith(generatedId);

//    string fullId = webEle.GetAttribute("id");//this id will be used to get control reference for WebDateChooser

//    string requiredControlId = fullId.Remove(fullId.Length - postfix_String.Length);

//    if (dt.HasValue)
//    {
//        string dateValueObj = Helpers.GetJavascript_DateObject(dt.Value);
//        /*
//         * igdrp_getComboById('C1_ERP_CC_BODY_wdcDateCreated')
//         * var date = new Date(dateText); dp.set_value(date);
//         */
//        this.IFrameDriver.RunJavascript(string.Format("igdrp_getComboById('{0}').setValue({1});", requiredControlId, dateValueObj));
//    }
//    else
//    {
//        this.IFrameDriver.RunJavascript(string.Format("igdrp_getComboById('{0}').set_value(null)", requiredControlId));
//    }
//    DriverHelpers.Blur(webEle, this.IFrameDriver);
//}
//#endregion Private methods