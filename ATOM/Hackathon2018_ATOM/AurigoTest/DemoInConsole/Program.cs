using AurigoTest.Toolkit;
using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.Core;
using AurigoTest.Toolkit.MW;
using AurigoTest.Toolkit.MW.Constants;
using DemoInConsole.MwInit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BgtEst = AurigoTest.Toolkit.MW.Constants.CONST_BudgetEstimate.Form;

namespace DemoInConsole
{
    class Program
    {
        ~Program()
        {

        }

        static void Main(string[] args)
        {
            try
            {
                Samples.SampleFormTest sf = new Samples.SampleFormTest();

                List<Action<string, string>> testMethodChain = new List<Action<string, string>>();

                testMethodChain.Add(sf.Open);

                TestRunner.RunWithDesc("SecurityForm", "Test Open", testMethodChain, sf);
                
                //------------------------------------------------------------------------------------------
                //Settings
                //------------------------------------------------------------------------------------------
                //TestRunner.InitRunOnlyAttributes(typeof(RunAttribute), typeof(RunCustomAttribute));

                //TestRunner.InitRunOnlyAttributes(typeof(RunCustomAttribute));//optional

                //------------------------------------------------------------------------------------------

                //TestRunner.Run("TC_PRJ_ALL", "All Project Methods", new PlanningProjectManager().DemoData_CreateAndVerify_NewProject_IfRequired);

                //TestRunner.Run("TC_PRJ_ALL", "All Project Methods", new PlanningProjectManager());

                //TestRunner.Run("TC_BGTEST_ALL", "All Project Methods", new BudgetEstimateTest());


                // TimeZoneFormTester timezoneForm = new TimeZoneFormTester();

                //TestRunner.Run("TIMEZONE_0001", "Test DateField", timezoneForm.TimezoneAutomation_DateField);

                //TestRunner.Run("TIMEZONE_0100", "Test DateField", timezoneForm.TimezoneAutomation_DateField_Final);


                //------------------------------------------------------------------------------------------

                //TestRunner.Run("TEST_0001", "Add New Budget Estimate", BudgetEstimateTest.EditProject_Add_CustomFormScreen);
                //TestRunner.Run("TEST_0003", "Test DateField", timezoneForm.TimezoneAutomation_DateField_Final);

                //TestRunner.Run("TEST_0003", "Test DateField", timezoneForm.TimezoneAutomation_DateField, Sample_View_FirstRow);
                //TestRunner.Run(
                //    "TEST_0002", "Test DateField",
                //         timezoneForm.TimezoneAutomation_DateField,
                //         timezoneForm.TimezoneAutomation_DateField_VerifyingInForm
                //);

                //Samples.ContractorInformationSecurity cis = new Samples.ContractorInformationSecurity();

                //TestRunner.Run("SecurityForm", "Test Open", cis.Open);

                //TestRunner.Run("TIMEZONE_0100", "Test DateField", timezoneForm.TimezoneAutomation_DateField_Final);

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Helpers.Report.Close(); //THIS HAS TO BE DONE so report is created

                if (System.IO.File.Exists(Helpers.ReportFileFullPath))
                    System.Diagnostics.Process.Start(Helpers.ReportFileFullPath);
            }
        }



      
    }
}
