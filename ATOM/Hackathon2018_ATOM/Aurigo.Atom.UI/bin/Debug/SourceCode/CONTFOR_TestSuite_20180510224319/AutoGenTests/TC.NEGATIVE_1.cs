
using AurigoTest.Toolkit;
using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.Common.Dto;
using AurigoTest.Toolkit.Core;
using AurigoTest.Toolkit.MW;
using System;
using System.Configuration;

namespace CONTFOR_TestSuite.AutoGenTests
{
    public partial class TC
    {
		[MethodReport(Id = "0", Description = @"TestCase for testing data tampering in form TextBox (Created By). Server side validation implemented!")] 
        public void NEGATIVE_1(string testId = "0", 
				string testSummary = @"TestCase for testing data tampering in form TextBox (Created By). Server side validation implemented!")
        {
            TestScenarioConfig config = null;

            //-------------------------------------------------------------------------------
            #region AutoGenerate Configurations

            config = new TestScenarioConfig()
            {
                IsSaveWillSucceed = false,
                AutomationGUID_FieldName = "AutomationGuid",
                IsVerificationRequired_InDatabase = true, 
                VerificationDescriptionText = "NEGATIVE_1",
				IsVerificationRequired_InEditMode = true,
				IsVerificationRequired_InViewMode = false,
            };

			System.Configuration.Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configFile.AppSettings.File = AppDomain.CurrentDomain.BaseDirectory + "TestSuites" + "\\" + "CONTFOR_TestSuite_20180510224319" +"_App.config";
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
			
            #endregion AutoGenerate Configurations
            //-------------------------------------------------------------------------------

            var listPage = MasterworksScreen
                                .Begin(testId, testSummary, BrowserType.Chrome, false)
                                .Login(RuntimeAppConfig.Instance.Username, RuntimeAppConfig.Instance.Password)
                                .OpenEnterprise_Form_ByDisplayName("Contractor Information");

            try
            {

				var formPage = listPage.OpenCreateRecordForm();

				//This code will help in running parallel tests in different machine on same database
				if (config.IsAutomationGUID_Field_Defined) {
					formPage.SetTextbox(config.AutomationGUID_FieldName, config.AutomationGUID_FieldValue);

					base.AdditionalRunInfo = $" [AutomationGUID : {config.AutomationGUID_FieldValue}]";
				}

				#region AutoGenerated Values to be set

									formPage.SetTextbox("ContractorName", "");
									formPage.SetTextbox("CreatedBy", "WAYIJVAF409QUNQH7N4J09TJ1AZG8IJM60");
				            
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

													v.Assert_Data("ContractorName", "");
						
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

													v.AssertTextbox("ContractorName", "");
													v.AssertTextbox("CreatedBy", "WAYIJVAF409QUNQH7N4J09TJ1AZG8IJM60");
						
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

													v.AssertTextbox("ContractorName", "");
						
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
				if (!config.IsSaveWillSucceed)
				{
					Action<GenericFormPageVerifier> formPageVerifier = (v) =>
					{
					   #region AutoGenerated Assert In viewVerifier

													v.AssertIfToasterExist();
						
						#endregion AutoGenerated Assert In viewVerifier
					};
					formPage.BeginVerification(config.VerificationDescriptionText, formPageVerifier);
				}
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
