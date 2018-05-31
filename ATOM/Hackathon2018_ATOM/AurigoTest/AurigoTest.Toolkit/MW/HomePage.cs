using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW
{
    public class HomePage : AutomationBase<HomePage, HomePageVerifier>
    {
        public HomePage(IDriverLinker driverLinker) : base(driverLinker)
        {

        }

        public AdminScreen GoTo_AdminScreen()
        {
            return new AdminScreen();
        }

        public ModuleUploaderScreen GoTo_ModuleUploader()
        {
            return new ModuleUploaderScreen();
        }

        public GenericListPage OpenEnterprise_Form_ByDisplayName(string formDisplayName)
        {
            //base.GoTo_URL(UrlConstants.SiteUrl + string.Format(URL_TEMPLATE_ProjectDetails, pid));
            base.MainMenu_Select(EnumMainMenuItem.Home);

            TreePanelHelper.Tree_SelectDirectNode(this.PrimaryDriver, formDisplayName);

            DriverHelpers.WaitForIFrameContent(this.PrimaryDriver, ConfigData.IFrameID, "MainToolBar_upToolbar");

            return new GenericListPage(this, this.PrimaryDriver.Url);
        }


    }
}
