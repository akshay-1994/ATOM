using AurigoTest.Toolkit;
using AurigoTest.Toolkit.Core;
using AurigoTest.Toolkit.MW;
using AurigoTest.Toolkit.MW.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInConsole.MwInit
{
    public class TimeZoneFormTester
    {
        const string DEFAULT_USER_NAME = "administrator";
        const string DEFAULT_USER_PWD = "aurigo";

        public void TimezoneAutomation_DateField(string testId, string testSummary)
        {
            MasterworksScreen
                .Begin(testId, testSummary, BrowserType.Chrome, false)
                .Login(DEFAULT_USER_NAME, DEFAULT_USER_PWD)
                .OpenEnterprise_Form_ByDisplayName("Timezone Testing")
                .OpenCreateRecordForm()
                    .SetDate("Date000", 2002, 10, 20)
                    .SetDateTime("Date012", new DateTime(2016, 1, 29, 5, 23, 20))
                .SaveForm_Successfully()
                .ExecuteCustom_Using_LastId(CONST_TableNames.TimezoneTesting, "ID", (id, listPageRef) =>
                {
                    var form = listPageRef.EditRow_WithId_ByNavigationUrl(id);

                    form.BeginVerification((driver, formVerifier) =>
                    {
                        formVerifier
                            .AssertDate("Date000", 2002, 10, 20)
                            .AssertDateTime("Date012", new DateTime(2016, 1, 29, 0, 0, 0))
                        ;
                    });
                })
                .End_Automation();

        }

        public void TimezoneAutomation_DateField_VerifyingInForm(string testId, string testSummary)
        {
            MasterworksScreen
                .Begin(testId, testSummary, BrowserType.Chrome, false).Login(DEFAULT_USER_NAME, DEFAULT_USER_PWD)
                .OpenEnterprise_Form_ByDisplayName("Timezone Testing")
                .OpenCreateRecordForm()
                    .SetDate("Date000", 2002, 10, 20)
                    .SetDateTime("Date012", new DateTime(2016, 1, 29, 5, 23, 20))
                //.BeginVerification((driver, formVerifier) =>
                //{

                //})
                .SaveForm_Successfully()
                .ExecuteCustom_Using_LastId("TimezoneTesting", "ID", (id, listPageRef) =>
                {
                    var form = listPageRef.EditRow_WithId_ByNavigationUrl(id);

                    form.BeginVerification((driver, formVerifier) =>
                    {
                        formVerifier
                            .AssertDate("Date000", 2002, 10, 20)
                            .AssertDateTime("Date012", new DateTime(2016, 1, 29, 0, 0, 0))
                        ;
                    });
                })
                
                .End_Automation();

        }

        public void TimezoneAutomation_DateField_Final(string testId, string testSummary)
        {
            MasterworksScreen ts = new MasterworksScreen(testId, testSummary, BrowserType.Chrome, false);

            ts.Login(DEFAULT_USER_NAME, DEFAULT_USER_PWD)
              .OpenEnterprise_Form_ByDisplayName("Timezone Testing")
              .OpenCreateRecordForm()
                  .SetDate("Date000", 2002, 10, 20)
                  .SetDateTime("Date012", new DateTime(2016, 1, 29, 5, 23, 20))
              .SaveForm_Successfully()
              .ExecuteCustom_Using_LastId("TimezoneTesting", "ID", (id, listPageRef) =>
              {
                  listPageRef
                    .EditRow_WithId_ByNavigationUrl(id)
                    .BeginVerification("Verificaiton Date000 is corret", (driver, formVerifier) =>
                    {
                        //Verify Step 1
                        formVerifier
                            .AssertDate("Date000", 2002, 10, 20)
                            .AssertDateTime("Date012", new DateTime(2016, 1, 29, 10, 10, 10));
                    })
                    .BeginVerification((driver, formVerifier) =>
                    {
                        //Unnamed Verify Step 2
                        formVerifier
                            .AssertDateTime("Date012", new DateTime(2016, 1, 29, 0, 0, 0));
                    })
                    .BeginVerification("My DateOnly Verification", (driver, formVerifier) =>
                    {
                        //Custom Verify Step 3
                        formVerifier
                            .AssertDateTime("Date012", new DateTime(2016, 1, 29, 0, 0, 0))
                            .AssertDate("Date000", 2002, 10, 20);
                    })
                    .CancelForm()//closed form and goes to list page
                    ;
              })
              .End_Automation();
        }

        [IgnoreRun]
        public  void TimezoneTest_Test_CreateAndValidate(string testId, string testSummary)
        {
            MasterworksScreen ts = new MasterworksScreen(testId, testSummary, BrowserType.Chrome, false);
            
            ts.Login(DEFAULT_USER_NAME, DEFAULT_USER_PWD)
              .OpenEnterprise_Form_ByDisplayName("Timezone Testing")
              .OpenCreateRecordForm()
              .SetDate("Date000", 2002, 10, 20)
              .SetDateTime("Date012", new DateTime(2016, 1, 29, 5, 23, 20))
              .SaveForm_Successfully()
              .ExecuteCustom_Using_LastId("TimezoneTesting", "ID", (id, listPageRef) =>
              {
                  listPageRef
                    .EditRow_WithId_ByNavigationUrl(id)
                    .BeginVerification("Verificaiton Date000 is corret", (driver, formVerifier) =>
                    {
                        //Verify Step 1
                        formVerifier
                            .AssertDate("Date000", 2002, 10, 20)
                            .AssertDateTime("Date012", new DateTime(2016, 1, 29, 10, 10, 10));
                    })
                    .BeginVerification((driver, formVerifier) =>
                    {
                        //Unnamed Verify Step 2
                        formVerifier
                            .AssertDateTime("Date012", new DateTime(2016, 1, 29, 0, 0, 0));
                    })
                    .BeginVerification("My DateOnly Verification", (driver, formVerifier) =>
                    {
                        //Custom Verify Step 3
                        formVerifier
                            .AssertDateTime("Date012", new DateTime(2016, 1, 29, 0, 0, 0))
                            .AssertDate("Date000", 2002, 10, 20);
                    })
                    .CancelForm()//closed form and goes to list page
                    ;
              });
        }
    }
}
