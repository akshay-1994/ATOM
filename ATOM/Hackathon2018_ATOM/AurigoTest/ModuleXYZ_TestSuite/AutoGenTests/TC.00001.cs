﻿using AurigoTest.Toolkit;
using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.Common.Dto;
using AurigoTest.Toolkit.Core;
using AurigoTest.Toolkit.MW;
using System;
using System.Configuration;

namespace ModuleXYZ_TestSuite.AutoGenTests
{
    //TC.00001
    public partial class TC
    {
        public void Senario_00001(string testId = "Senario_00001", string testSummary = "Formxyz_Senario_00001")
        {

            TestScenarioConfig config = null;

            //-------------------------------------------------------------------------------
            #region AutoGenerate Configurations

            config = new TestScenarioConfig()
            {
                IsSaveWillSucceed = true, // { isSaveWillSucceed };
                AutomationGUID_FieldName = this.AutomationGUID_FieldName,// { AutomationGUID_FieldName };
                IsVerificationRequired_InDatabase = true, // { IsDatabaseVerificationRequired };
                VerificationDescriptionText = "Verify ModuleXYZ in DB",// { VerificationDescriptionText };
            };




            #endregion AutoGenerate Configurations
            //-------------------------------------------------------------------------------

            var listPage = MasterworksScreen
                                .Begin(testId, testSummary, BrowserType.Chrome, false)
                                .Login(RuntimeAppConfig.Instance.Username, RuntimeAppConfig.Instance.Password)
                                .OpenEnterprise_Form_ByDisplayName("Sample Form");

            try
            {        

                var formPage = listPage.OpenCreateRecordForm();

                //This code will help in running parallel tests in different machine on same database
                if (config.IsAutomationGUID_Field_Defined)
                {
                    formPage.SetTextbox(config.AutomationGUID_FieldName, config.AutomationGUID_FieldValue);

                    base.AdditionalRunInfo = $"AutomationGUID [{config.AutomationGUID_FieldValue}]";
                }

                #region AutoGenerated Values to be set

                formPage.SetTextbox("Name", "asheesh");
                formPage.SetTextbox("Name", "asheesh");
                formPage.SetTextbox("Name", "asheesh");

                #endregion  AutoGenerated Values to be set

                if (config.IsSaveWillSucceed)
                    listPage = formPage.SaveForm_Successfully();
                else
                    formPage = formPage.SaveForm_ExpectValidationError();

                #region If Save will succeeed
                #region If Database validation is required
                if (config.IsSaveWillSucceed && config.IsVerificationRequired_InDatabase)
                {
                    Action<DataRowVerifier<GenericListPage>, GenericListPage> dbVerifierFunction = (v, listPageRef) =>
                    {
                        #region AutoGenerated Assert In DB rowVerifier

                        v.Assert_Data("Name", "asheesh");
                        v.Assert_Data("Name", "asheesh");

                        #endregion AutoGenerated Assert In DB rowVerifier
                    };

                    if (config.IsAutomationGUID_Field_Defined)
                    {
                        var hintObject = this.GetTableRecordHintObject(config.AutomationGUID_FieldValue);
                        listPage = listPage.VerifyInDB_Using_LastId(config.VerificationDescriptionText, hintObject, dbVerifierFunction);
                    }
                    else
                    {
                        //if no automation id field then use primary key: ButTestcases cannot be distributed and run in parallel
                        listPage = listPage.VerifyInDB_Using_LastId(config.VerificationDescriptionText, this.ModuleTableName, this.ModuleTablePrimaryKeyName, dbVerifierFunction);
                    }

                }
                #endregion If Database validation is required

                #region If verification required by editing the record
                if (config.IsSaveWillSucceed && config.IsVerificationRequired_InEditMode)
                {
                    Action<GenericFormPageVerifier> formPageVerifier = (v) =>
                    {
                        #region AutoGenerated Assert In formVerifier

                        v.AssertTextbox("Name", "asheesh");
                        v.AssertTextbox("Name", "asheesh");

                        #endregion AutoGenerated Assert In formVerifier
                    };

                    Action<string, GenericListPage> formVerificationHandler = (id, listPageRef) =>
                    {
                        var form = listPageRef.EditRow_WithId_ByNavigationUrl(id);

                        form.BeginVerification(config.VerificationDescriptionText, formPageVerifier);
                    };

                    if (!config.IsAutomationGUID_Field_Defined)
                        listPage = listPage.ExecuteCustom_Using_LastId(this.ModuleTableName, this.ModuleTablePrimaryKeyName, formVerificationHandler);
                    else
                    {
                        var hintObject = this.GetTableRecordHintObject(config.AutomationGUID_FieldValue);
                        listPage = listPage.ExecuteCustom_Using_LastId(hintObject, formVerificationHandler);
                    }
                }

                #endregion If verification required by editing the record

                #region If verification required by viewing the record
                if (config.IsSaveWillSucceed && config.IsVerificationRequired_InViewMode)
                {
                    Action<GenericViewPageVerifier> viewPageVerifier = (v) =>
                    {
                        #region AutoGenerated Assert In viewVerifier

                        v.AssertTextbox("Name", "asheesh");
                        v.AssertTextbox("Name", "asheesh");

                        #endregion AutoGenerated Assert In viewVerifier
                    };

                    Action<string, GenericListPage> viewVerificationHandler = (id, listPageRef) =>
                    {
                        var view = listPageRef.ViewRow_WithId_ByNavigationUrl(id);

                        view.BeginVerification(config.VerificationDescriptionText, viewPageVerifier);
                    };

                    if (!config.IsAutomationGUID_Field_Defined)
                        listPage = listPage.ExecuteCustom_Using_LastId(this.ModuleTableName, this.ModuleTablePrimaryKeyName, viewVerificationHandler);
                    else
                    {
                        var hintObject = this.GetTableRecordHintObject(config.AutomationGUID_FieldValue);
                        listPage = listPage.ExecuteCustom_Using_LastId(hintObject, viewVerificationHandler);
                    }
                }

                #endregion If verification required by viewing the record

                #endregion If Save will succeeed

                #region If Save will throw validation error
                //Then we must verify if we are still on the same page and validation error was thrown
                //TODO:asheesh
                #endregion If Save will throw validation error

            }
            catch
            {
                throw;
            }
            finally
            {
                listPage.End_Automation();
            }

        }
    }
}
