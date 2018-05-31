using AurigoTest.Toolkit.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW
{
    public class ProjectContent : AutomationBase<ProjectContent, ProjectContentVerifier>
    {
        protected IDriverLinker DriverLinkerReference { get; set; }

        public ProjectContent(IDriverLinker driverLinker, int pid) : base(driverLinker)
        {

        }

        public GenericListPage OpenListPage_By_ModuleName(string moduleName)
        {
            throw new NotImplementedException();
            return new GenericListPage(this, "/Common/BrixListPage.aspx?xContext=FNDPRJT&PID=681&ParentID=681");
        }

        /// <summary>
        /// Path must be separated by \\
        /// </summary>
        /// <param name="fullPathUnderProjectFolder"></param>
        /// <returns></returns>
        public GenericListPage OpenListPage_By_Path(string fullPathUnderProjectFolder)
        {
            Tree_SelectLeafUnderProjectByPath(fullPathUnderProjectFolder);

            return new GenericListPage(this, base.PrimaryDriver.Url);// "/Common/BrixListPage.aspx?xContext=FNDPRJT&PID=681&ParentID=681");
        }

        public T OpenListPage_By_Path<T, V>(T obj, V vObj, string fullPathUnderProjectFolder) //where T : AutomationBase<T,>
        {
            Tree_SelectLeafUnderProjectByPath(fullPathUnderProjectFolder);

            return obj;
            //return new T(this, base.PrimaryDriver.Url);// "/Common/BrixListPage.aspx?xContext=FNDPRJT&PID=681&ParentID=681");
        }

        public void SimulateDataByModule(string fullPathUnderProjectFolder)
        {
            Tree_SelectLeafUnderProjectByPath(fullPathUnderProjectFolder);
        }
    }
}
