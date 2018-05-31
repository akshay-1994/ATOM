using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.Common.Dto;
using AurigoTest.Toolkit.Core;
using AurigoTest.Toolkit.Core.Verifiers;
using AurigoTest.Toolkit.MW.Customizations;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW
{
    public class MasterworksScreen : AutomationBase<MasterworksScreen, GeneralVerifier<MasterworksScreen>>
    {

        private bool _isAutoLoginDone = false;
        public string URL_TEMPLATE_ProjectDetails { get; set; } = "/Default.aspx#/Modules/PROJECT/ProjectDetails.aspx?pid={0}&Context=PROJECT&InstanceID=0&Mode=View";

        public MasterworksScreen(string testID, string testSummary, BrowserType browserType = BrowserType.Chrome, bool isAutoLogin = false) : base(null)//parent object can be null in this call only
        {
            UrlConstants.SiteUrl = RuntimeAppConfig.Instance.URL;
            AurigoAppSettings.ConnectionStringSource = DBHelper.TryGetDatabaseConnectionString();

            base.TestID = testID;
            base.TestSummary = testSummary;

            base.PrimaryDriver = DriverHelpers.CreateWindow(browserType);

            if (isAutoLogin && base.PrimaryDriver != null)
            {
                base.LoginInternal(RuntimeAppConfig.Instance.Username, RuntimeAppConfig.Instance.Password);
                _isAutoLoginDone = true;
            }
        }

        public static MasterworksScreen Begin(string testID, string testSummary, BrowserType browserType = BrowserType.Chrome, bool isAutoLogin = false)
        {
            return new MasterworksScreen(testID, testSummary, browserType, isAutoLogin);
        }

        public HomePage Login(string userName, string pwd)
        {
            base.LoginInternal(userName, pwd);
            return new HomePage(this);
        }

        public ProjectContent OpenProject_ById(int pid)
        {
            base.GoTo_URL(UrlConstants.SiteUrl + string.Format(URL_TEMPLATE_ProjectDetails, pid));

            return new ProjectContent(this, pid);
        }

        public ProjectFormPage CreateProjectFromPlanning()
        {
            string currentUrl = this.PrimaryDriver.Url;
            base.GoTo_URL(UrlConstants.SiteUrl + "/Default.aspx#/Modules/PROJECT/CreateProjects.aspx?PP=1");
            return new ProjectFormPage(new GenericListPage(this, currentUrl), currentUrl);
        }

        public ProjectFormPage CreateProject()
        {
            string currentUrl = this.PrimaryDriver.Url;
            base.GoTo_URL(UrlConstants.SiteUrl + "/Default.aspx#/Modules/PROJECT/CreateProjects.aspx");
            return new ProjectFormPage(new GenericListPage(this, currentUrl), currentUrl);
        }


        public HomePage Home()
        {
            if (!_isAutoLoginDone)
            {

                new WebDriverWait(PrimaryDriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists((By.Id("PageTabs_tabBar"))));

                //PrimaryDriver.FindElement(By.Id("PageTabs_tabBar"), 10);

            }
            //DriverHelpers.WaitForSometime(PrimaryDriver);

            return new HomePage(this);
        }

        //~AutomateScreen()
        //{

        //}



    }
}
