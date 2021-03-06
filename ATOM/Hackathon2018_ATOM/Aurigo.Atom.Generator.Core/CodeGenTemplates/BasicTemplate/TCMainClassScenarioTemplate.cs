﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 14.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Aurigo.Atom.Generator.Core.CodeGenTemplates.BasicTemplate
{
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class TCMainClassScenarioTemplate : TCMainClassScenarioTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\nusing AurigoTest.Toolkit;\r\nusing AurigoTest.Toolkit.Common;\r\nusing AurigoTest.T" +
                    "oolkit.Common.Dto;\r\nusing AurigoTest.Toolkit.Core;\r\nusing AurigoTest.Toolkit.MW;" +
                    "\r\nusing System;\r\nusing System.Configuration;\r\n\r\nnamespace ");
            
            #line 17 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TestModuleConfig.AutoGeneratedProjectNamespace));
            
            #line default
            #line hidden
            this.Write(".AutoGenTests\r\n{\r\n    public partial class TC\r\n    {\r\n\t\t[MethodReport(Id = \"");
            
            #line 21 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ScenarioDTO.ID));
            
            #line default
            #line hidden
            this.Write("\", Description = @\"");
            
            #line 21 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ScenarioDTO.ScenarioDescriptionToString));
            
            #line default
            #line hidden
            this.Write("\")] \r\n        public void ");
            
            #line 22 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ScenarioDTO.Name));
            
            #line default
            #line hidden
            this.Write("(string testId = \"");
            
            #line 22 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ScenarioDTO.ID));
            
            #line default
            #line hidden
            this.Write("\", \r\n\t\t\t\tstring testSummary = @\"");
            
            #line 23 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(Environment.NewLine, ScenarioDTO.ScenarioDescriptionToString)));
            
            #line default
            #line hidden
            this.Write(@""")
        {
            TestScenarioConfig config = null;

            //-------------------------------------------------------------------------------
            #region AutoGenerate Configurations

            config = new TestScenarioConfig()
            {
                IsSaveWillSucceed = ");
            
            #line 32 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ScenarioDTO.IsSaveWillSucceed.ToString().ToLower()));
            
            #line default
            #line hidden
            this.Write(",\r\n                AutomationGUID_FieldName = \"");
            
            #line 33 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ScenarioDTO.AutomationGuidFieldName));
            
            #line default
            #line hidden
            this.Write("\",\r\n                IsVerificationRequired_InDatabase = ");
            
            #line 34 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ScenarioDTO.IsDatabaseVerificationRequired.ToString().ToLower()));
            
            #line default
            #line hidden
            this.Write(", \r\n                VerificationDescriptionText = \"");
            
            #line 35 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ScenarioDTO.Name));
            
            #line default
            #line hidden
            this.Write("\",\r\n\t\t\t\tIsVerificationRequired_InEditMode = ");
            
            #line 36 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TestModuleConfig.VerifyInEditMode.ToString().ToLower()));
            
            #line default
            #line hidden
            this.Write(",\r\n\t\t\t\tIsVerificationRequired_InViewMode = ");
            
            #line 37 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TestModuleConfig.VerifyInViewMode.ToString().ToLower()));
            
            #line default
            #line hidden
            this.Write(",\r\n            };\r\n\r\n\t\t\tSystem.Configuration.Configuration configFile = Configura" +
                    "tionManager.OpenExeConfiguration(ConfigurationUserLevel.None);\r\n            conf" +
                    "igFile.AppSettings.File = AppDomain.CurrentDomain.BaseDirectory + \"TestSuites\" +" +
                    " \"\\\\\" + \"");
            
            #line 41 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TestModuleConfig.AssemblyName));
            
            #line default
            #line hidden
            this.Write(@""" +""_App.config"";
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(""appSettings"");
			
            #endregion AutoGenerate Configurations
            //-------------------------------------------------------------------------------

            var listPage = MasterworksScreen
                                .Begin(testId, testSummary, BrowserType.Chrome, false)
                                .Login(RuntimeAppConfig.Instance.Username, RuntimeAppConfig.Instance.Password)
                                .OpenEnterprise_Form_ByDisplayName(""");
            
            #line 51 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(TestModuleConfig.ModuleName));
            
            #line default
            #line hidden
            this.Write(@""");

            try
            {

				var formPage = listPage.OpenCreateRecordForm();

				//This code will help in running parallel tests in different machine on same database
				if (config.IsAutomationGUID_Field_Defined) {
					formPage.SetTextbox(config.AutomationGUID_FieldName, config.AutomationGUID_FieldValue);

					base.AdditionalRunInfo = $"" [AutomationGUID : {config.AutomationGUID_FieldValue}]"";
				}

				#region AutoGenerated Values to be set

				");
            
            #line 67 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
 foreach( var item in ScenarioDTO.Setters) {  
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\tformPage.");
            
            #line 68 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(item));
            
            #line default
            #line hidden
            this.Write("\r\n\t\t\t\t");
            
            #line 69 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
  }  
            
            #line default
            #line hidden
            this.Write(@"            
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

						");
            
            #line 86 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
 foreach( var item in ScenarioDTO.DBValidators) {  
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\tv.");
            
            #line 87 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(item));
            
            #line default
            #line hidden
            this.Write("\r\n\t\t\t\t\t\t");
            
            #line 88 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
  }  
            
            #line default
            #line hidden
            this.Write(@"
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

						");
            
            #line 114 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
 foreach( var item in ScenarioDTO.EditModeValidators) {  
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\tv.");
            
            #line 115 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(item));
            
            #line default
            #line hidden
            this.Write("\r\n\t\t\t\t\t\t");
            
            #line 116 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
  }  
            
            #line default
            #line hidden
            this.Write(@"
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

						");
            
            #line 146 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
 foreach( var item in ScenarioDTO.ViewModeValidators) {  
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\tv.");
            
            #line 147 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(item));
            
            #line default
            #line hidden
            this.Write("\r\n\t\t\t\t\t\t");
            
            #line 148 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
  }  
            
            #line default
            #line hidden
            this.Write(@"
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

						");
            
            #line 180 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
 foreach( var item in ScenarioDTO.OnScreenValidators) {  
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\tv.");
            
            #line 181 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(item));
            
            #line default
            #line hidden
            this.Write("\r\n\t\t\t\t\t\t");
            
            #line 182 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"
  }  
            
            #line default
            #line hidden
            this.Write(@"
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
");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "D:\TFS_REPO\Hackathon2018_ATOM\Aurigo.Atom.Generator.Core\CodeGenTemplates\BasicTemplate\TCMainClassScenarioTemplate.tt"

private global::Aurigo.Atom.Common.DTO.ScenarioDTO _ScenarioDTOField;

/// <summary>
/// Access the ScenarioDTO parameter of the template.
/// </summary>
private global::Aurigo.Atom.Common.DTO.ScenarioDTO ScenarioDTO
{
    get
    {
        return this._ScenarioDTOField;
    }
}

private global::Aurigo.Atom.Common.DTO.TestModuleConfig _TestModuleConfigField;

/// <summary>
/// Access the TestModuleConfig parameter of the template.
/// </summary>
private global::Aurigo.Atom.Common.DTO.TestModuleConfig TestModuleConfig
{
    get
    {
        return this._TestModuleConfigField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool ScenarioDTOValueAcquired = false;
if (this.Session.ContainsKey("ScenarioDTO"))
{
    this._ScenarioDTOField = ((global::Aurigo.Atom.Common.DTO.ScenarioDTO)(this.Session["ScenarioDTO"]));
    ScenarioDTOValueAcquired = true;
}
if ((ScenarioDTOValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("ScenarioDTO");
    if ((data != null))
    {
        this._ScenarioDTOField = ((global::Aurigo.Atom.Common.DTO.ScenarioDTO)(data));
    }
}
bool TestModuleConfigValueAcquired = false;
if (this.Session.ContainsKey("TestModuleConfig"))
{
    this._TestModuleConfigField = ((global::Aurigo.Atom.Common.DTO.TestModuleConfig)(this.Session["TestModuleConfig"]));
    TestModuleConfigValueAcquired = true;
}
if ((TestModuleConfigValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("TestModuleConfig");
    if ((data != null))
    {
        this._TestModuleConfigField = ((global::Aurigo.Atom.Common.DTO.TestModuleConfig)(data));
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public class TCMainClassScenarioTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
