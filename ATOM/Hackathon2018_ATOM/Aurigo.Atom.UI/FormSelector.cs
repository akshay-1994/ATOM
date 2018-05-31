using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Aurigo.Atom.Common;
using Aurigo.Atom.Common.DTO;
using Aurigo.Atom.Common.Interfaces;
using Aurigo.Atom.Generator.Core.Builder;
using Aurigo.Atom.Generator.Core.Builders;
using Aurigo.Atom.Generator.Core.Config;
using Aurigo.Atom.UI.DTO;
using Aurigo.Atom.UI.Managers;
using Aurigo.Atom.UI.UserControls;
using Aurigo.Brix.Platform.BusinessLayer.AbstractModels;
using Aurigo.Brix.Platform.BusinessLayer.XMLForm;
using Unity;
using Aurigo.Atom.Generator.Core.Helpers;

namespace Aurigo.Atom.UI
{
    public partial class FormSelector : System.Windows.Forms.Form
    {
        private ModuleManager _moduleManager;
        private Dictionary<string, xControl> _formControls;
        private BrixFormModel _formModel;
        private AtomUITemplates _savedTemplates;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormSelector"/> class.
        /// </summary>
        public FormSelector()
        {
            InitializeComponent();
        }

        #region FORM METHODS

        private void ResetForm()
        {
            txtConnectionString.Text = string.Empty;
            txtMasterworksUrl.Text = string.Empty;
            txtMasterworksUserName.Text = string.Empty;
            txtMasterworksPassword.Text = string.Empty;
        }

        private void btnFetchModules_Click(object sender, EventArgs e)
        {
            _moduleManager = new ModuleManager(txtConnectionString.Text);
            var allModules = _moduleManager.GetAllModules();

            if (allModules == null)
            {
                return;
            }

            allModules.Add(new Module { ModuleId = string.Empty, ModuleName = string.Empty });

            allModules = allModules.OrderBy(x => x.ModuleName).ToList();

            cmbModules.ValueMember = "ModuleId";
            cmbModules.DisplayMember = "ModuleName";
            cmbModules.DataSource = allModules;

            //tacMain.Enabled = true;
            tacMain.SelectedIndex = 2;
        }

        private void LoadControls()
        {
            var uniqueControls = new Dictionary<string, xControl>();

            if (cmbModules.SelectedIndex == 0)
            {
                ClearTestCaseConfigurations();
                ClearSolutionConfigurations();
                return;
            }

            _formModel = _moduleManager.GetXmlForm(cmbModules.SelectedValue.ToString());

            _formModel.form.ProcessAllControlsDeeply(control =>
            {
                if (!string.IsNullOrEmpty(control.Caption) && !uniqueControls.ContainsKey(control.Caption))
                {
                    ValidateControl(control);
                    uniqueControls.Add(control.Caption, control);
                }
            });

            _formControls = uniqueControls;
            BuildControlsTree(uniqueControls);
            FillAutomationGuidFieldCombo();
        }

        private void btnGenerateTests_Click(object sender, EventArgs e)
        {
            UpdateStatusStrip("Bulding test case components.");
            var testComponents = BuildTestComponents();

            var testModuleConfig = BuildTestModuleConfig();

            UpdateStatusStrip("Building test case fragments.");
            var builder = new TestCaseComponentBuilder(testComponents, testModuleConfig);
            var testCaseComponentGroups = builder.Build();

            UpdateStatusStrip("Building test scenarios");
            var scenarioBuilder = new ScenarioBuilder(testCaseComponentGroups, testModuleConfig);
            List<ScenarioDTO> scenarios = scenarioBuilder.Build();

            UpdateStatusStrip("Building test project");
            TestCaseScenario finalObject = new TestCaseScenario() { ModuleConfig = testModuleConfig, Scenarios = scenarios };
            CodeSolutionBuilder solutionBuilder = new CodeSolutionBuilder();

            SolutionGenerationConfig solConfig = new SolutionGenerationConfig()
            {
                IsGeneratedDistinctDLL = chkGenerateDistinctDLL.Checked,
                IsGeneratedDistinctSuiteXmlConfig = chkGenerateDistinctSuiteXmlConfig.Checked,
                IsAutoGenerateSolutionName = chkAutoGenerateSolutionName.Checked, //if this is false then we must specify SolutionNameExplicitly
                SolutionTargetPath = txtSolutionTargetPath.Text,
                //SolutionName
                //LastKnownSolutionFolderDirPath
                IsCompileAndGenerateLibrary = chkCompileAndGenerateLibrary.Checked,
                SolutionName = txtSolutionName.Text,
                IsAutoDeploy = chkAutoDeploy.Checked,
                AutoDeployPath = txtDeploymentPath.Text,
            };

            tacMain.SelectedIndex = 3;

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    solutionBuilder.Generate(finalObject, solConfig, writer);
                    writer.Flush();

                    txtCompilerWarnings.Text = Encoding.ASCII.GetString(stream.ToArray());

                }
            }



            MessageBox.Show("Operation completed successfully.", "Save successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            UpdateStatusStrip("Finished");
        }

        private void FormSelector_Load(object sender, EventArgs e)
        {
            FillTemplateComboBox();
            btnGenerateTests.Enabled = false;
        }

        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            SaveFormConfigChanges();
        }

        private void cmbTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((int)cmbTemplate.SelectedIndex == 0)
            {
                tacMain.SelectedIndex = 0;
                btnGenerateTests.Enabled = false;
                //tacMain.Enabled = false;

                //ResetForm();

                return;
            }
            
            LoadFormConfigurations((int)cmbTemplate.SelectedValue);
        }

        private void cmbModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadControls();

            var selectedTemplateId = cmbTemplate.SelectedIndex == -1 ? 0 : (int)cmbTemplate.SelectedValue;

            if (selectedTemplateId == 0)
            {
                btnGenerateTests.Enabled = false;
                return;
            }

            var template = _savedTemplates.Templates.Find(t => t.TemplateId == selectedTemplateId);

            if (template != null)
            {
                LoadControlConfigurations(template, (string)cmbModules.SelectedValue);
                btnGenerateTests.Enabled = true;
            }

        }

        private void btnNext2_Click(object sender, EventArgs e)
        {
            tacMain.SelectedIndex = 3;
        }

        #endregion

        /// <summary>
        /// Tests the module configuration.
        /// </summary>
        private TestModuleConfig BuildTestModuleConfig()
        {
            var testModuleConfig = new TestModuleConfig();
            testModuleConfig.ModuleId = _formModel.form.Name;
            testModuleConfig.ModuleName = _formModel.form.Header;
            testModuleConfig.PrimaryKeyColumnName = _formModel.form.PrimaryKeyName;
            testModuleConfig.TableName = _formModel.form.TableName;
            testModuleConfig.TemplatePath = AppConfig.TemplatePath;
            testModuleConfig.IncludeNegativeTestCase = chkIncludeNegativeTestCase.Checked;
            testModuleConfig.IncludeSecurityTestCase = chkIncludeSecurityTestCase.Checked;
            testModuleConfig.VerifyInEditMode = chkIncludeEditModeValidation.Checked;
            testModuleConfig.VerifyInViewMode = chkIncludeViewModeValidation.Checked;
            testModuleConfig.AutomationGuidFieldName = cmbAutomationGuidField.SelectedValue.ToString();

            testModuleConfig.Credentials = new TestBuildCredentials();
            testModuleConfig.Credentials.ConnectionString = txtConnectionString.Text;
            testModuleConfig.Credentials.Username = txtMasterworksUserName.Text;
            testModuleConfig.Credentials.Password = txtMasterworksPassword.Text;
            testModuleConfig.Credentials.URL = txtMasterworksUrl.Text;

            testModuleConfig.SolutionConfig = new SolutionGenerationConfig();
            testModuleConfig.SolutionConfig.IsAutoGenerateSolutionName = chkAutoGenerateSolutionName.Checked;
            testModuleConfig.SolutionConfig.IsGeneratedDistinctSuiteXmlConfig = chkGenerateDistinctSuiteXmlConfig.Checked;
            testModuleConfig.SolutionConfig.IsGeneratedDistinctDLL = chkGenerateDistinctDLL.Checked;
            testModuleConfig.SolutionConfig.SolutionName = txtSolutionName.Text;
            //testModuleConfig.SolutionConfig.SolutionTargetPath = txtSolutionTargetPath.Text;
            //testModuleConfig.SolutionConfig.IsCompileAndGenerateLibrary = chkCompileAndGenerateLibrary.Checked;

            return testModuleConfig;
        }

        /// <summary>
        /// Validates the control.
        /// </summary>
        /// <param name="control">The control.</param>
        private void ValidateControl(xControl control)
        {
            if (!Configurator.Container.IsRegistered<IControlVerifier>(control.Type.ToString()))
                return;

            var verifier = Configurator.Container.Resolve<IControlVerifier>(control.Type.ToString());

            if (verifier == null)
                return;

            var result = verifier.Verify(control);
            if (result != null && result.Count() > 0)
                txtWarnings.AppendText(string.Join(Environment.NewLine, result));
        }

        /// <summary>
        /// Builds the controls tree.
        /// </summary>
        /// <param name="uniqueControls">The unique controls.</param>
        private void BuildControlsTree(Dictionary<string, xControl> uniqueControls)
        {
            tvControlsTree.Nodes.Clear();
            tvControlsTree.CheckBoxes = true;
            foreach (var control in uniqueControls.OrderBy(c => c.Key.ToString()))
            {
                var node = new TreeNode(control.Key.ToString()) { Name = control.Key.ToString() };
                BuildAttributesForControl(control.Value, node);
                tvControlsTree.Nodes.Add(node);
            }
        }

        /// <summary>
        /// Builds the attributes for control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="node">The node.</param>
        private void BuildAttributesForControl(xControl control, TreeNode node)
        {
            var attributes = control.GetType().GetFields()
                            .Where(a =>
                            {
                                if (a.IsDefined(typeof(FormDesignerAttributeAttribute), true))
                                {
                                    var formDesignerAttribute = a.GetCustomAttributes(typeof(FormDesignerAttributeAttribute), true) as FormDesignerAttributeAttribute[];
                                    if (formDesignerAttribute.Length > 0 &&
                                        formDesignerAttribute[0].HideAttribute == false &&
                                        //(formDesignerAttribute[0].ReleventControls.Contains(control.Type) || formDesignerAttribute[0].ReleventControls.Contains(ControlType.All)) &&
                                        formDesignerAttribute[0].AttributeType == AttributeType.Common
                                        )
                                        return true;
                                }

                                return false;
                            });

            node.Nodes.AddRange(attributes.Select(x => { return new TreeNode(x.Name) { Name = x.Name }; }).OrderBy(a => a.Text).ToArray());
        }

        /// <summary>
        /// Builds the test components.
        /// </summary>
        /// <returns></returns>
        private List<TestComponent> BuildTestComponents()
        {
            var testComponents = new List<TestComponent>();

            for (int i = 0; i < tvControlsTree.Nodes.Count; i++)
            {
                var node = tvControlsTree.Nodes[i];
                var testComponent = new TestComponent
                {
                    Name = node.Text,
                    Control = _formControls[node.Text],
                    Attribues = new List<string>()
                };

                for (int j = 0; j < node.Nodes.Count; j++)
                {
                    var subNode = node.Nodes[j];

                    if (subNode.Checked)
                    {
                        testComponent.Attribues.Add(subNode.Text);
                    }
                }

                testComponents.Add(testComponent);
            }

            return testComponents;
        }

        /// <summary>
        /// Fills the template ComboBox.
        /// </summary>
        private void FillTemplateComboBox()
        {
            _savedTemplates = LoadSavedTemplates();

            if (_savedTemplates.Templates == null)
                return;

            var templateList = new List<TemplateListItem>();

            cmbTemplate.ValueMember = "TemplateId";
            cmbTemplate.DisplayMember = "TemplateName";

            templateList.Add(new TemplateListItem { TemplateId = 0, TemplateName = "" });

            foreach (var item in _savedTemplates.Templates)
            {
                templateList.Add(new TemplateListItem { TemplateId = item.TemplateId, TemplateName = item.TemplateName });
            }

            cmbTemplate.DataSource = templateList;
        }

        /// <summary>
        /// Loads the saved templates.
        /// </summary>
        /// <returns></returns>
        private AtomUITemplates LoadSavedTemplates()
        {
            if (!File.Exists(AppConfig.AtomUITemplate))
                return new AtomUITemplates();

            try
            {
                using (var fs = new FileStream(AppConfig.AtomUITemplate, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(AtomUITemplates));
                    return serializer.Deserialize(fs) as AtomUITemplates;
                }
            }
            catch (Exception e)
            {
                return new AtomUITemplates();
            }
        }

        /// <summary>
        /// Saves the form configuration changes.
        /// </summary>
        private void SaveFormConfigChanges()
        {
            var serializer = new XmlSerializer(typeof(AtomUITemplates));

            if (_savedTemplates == null)
                _savedTemplates = new AtomUITemplates
                {
                    Templates = new List<Template>()
                };
            else if (_savedTemplates.Templates == null)
                _savedTemplates.Templates = new List<Template>();

            bool isNewTemplate = cmbTemplate.SelectedIndex == -1 || (int)cmbTemplate.SelectedValue == 0;
            string templateName = string.Empty;

            if (isNewTemplate)
            {
                templateName = Prompt.ShowDialog("Enter template name", "Template Name");

                if (string.IsNullOrEmpty(templateName))
                {
                    MessageBox.Show("Template was not saved.", "Save failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            Template template;

            if (isNewTemplate)
            {
                var templateId = _savedTemplates.Templates?.Count > 0
                                   ? _savedTemplates.Templates[_savedTemplates.Templates.Count - 1].TemplateId + 1
                                   : 1;
                template = new Template
                {
                    TemplateId = templateId,
                    TemplateName = templateName,
                    ConnectionString = txtConnectionString.Text,
                    MasterworksUrl = txtMasterworksUrl.Text,
                    UserName = txtMasterworksUserName.Text,
                    Password = txtMasterworksPassword.Text,

                    Modules = new List<ModuleConfig>()
                };
                _savedTemplates.Templates.Add(template);
            }
            else
            {
                template = _savedTemplates.Templates.Find(t => t.TemplateId == (int)cmbTemplate.SelectedValue);
                template.ConnectionString = txtConnectionString.Text;
                template.MasterworksUrl = txtMasterworksUrl.Text;
                template.UserName = txtMasterworksUserName.Text;
                template.Password = txtMasterworksPassword.Text;
            }

            template.SolutionTargetPath = txtSolutionTargetPath.Text;
            template.DeploymentPath = txtDeploymentPath.Text;
            template.AtomReactorExecutableFilePath = txtAtomReactorPath.Text;
            template.CompileAndGenerateLibrary = chkCompileAndGenerateLibrary.Checked;
            template.IsAutoDeploy = chkAutoDeploy.Checked;

            ModuleConfig moduleConfig = template.Modules.Find(m => m.ModuleId == cmbModules.SelectedValue.ToString());

            if (moduleConfig == null)
            {
                moduleConfig = new ModuleConfig
                {
                    ModuleId = cmbModules.SelectedValue.ToString(),
                    ModuleName = cmbModules.SelectedText,
                    Controls = new List<ControlConfig>()
                };
                template.Modules.Add(moduleConfig);
            }

            moduleConfig.IncludeNegativeTestCase = chkIncludeNegativeTestCase.Checked;
            moduleConfig.IncludeSecurityTestCase = chkIncludeSecurityTestCase.Checked;
            moduleConfig.IncludeEditModeValidation = chkIncludeEditModeValidation.Checked;
            moduleConfig.IncludeViewModeValidation = chkIncludeViewModeValidation.Checked;
            moduleConfig.AutomationGuidFieldName = cmbAutomationGuidField.SelectedValue?.ToString();
            moduleConfig.IncludeDBValidation = chkIncludeDbValidation.Checked;

            moduleConfig.GenerateDistinctDLLs = chkGenerateDistinctDLL.Checked;
            moduleConfig.GenerateDistinctSuiteXMLConfig = chkGenerateDistinctSuiteXmlConfig.Checked;
            moduleConfig.AutoGenerateSolutionName = chkAutoGenerateSolutionName.Checked;
            //moduleConfig.CompileAndGenerateLibrary = chkCompileAndGenerateLibrary.Checked;
            //moduleConfig.SolutionName = txtSolutionName.Text;
            //moduleConfig.SolutionTargetPath = txtSolutionTargetPath.Text;

            for (var i = 0; i < tvControlsTree.Nodes.Count; i++)
            {
                var node = tvControlsTree.Nodes[i];
                var control = _formControls[node.Text];
                var controlConfig = moduleConfig.Controls.Find(c => c.Name == node.Text);
                bool isNewControl = false, hasAttributes = false;

                if (controlConfig == null)
                {
                    isNewControl = true;
                    controlConfig = new ControlConfig
                    {
                        Name = node.Text,
                        Type = control.Type.ToString(),
                        Attributes = new List<string>()
                    };
                }

                for (var j = 0; j < node.Nodes.Count; j++)
                {
                    var subNode = node.Nodes[j];
                    var attribute = controlConfig.Attributes.Find(a => a == subNode.Text);

                    if (!subNode.Checked && attribute != null)
                    {
                        controlConfig.Attributes.Remove(attribute);
                        continue;
                    }
                    else if (subNode.Checked && attribute == null)
                    {
                        controlConfig.Attributes.Add(subNode.Text);
                        hasAttributes = true;
                    }
                }

                if (isNewControl || hasAttributes)
                    moduleConfig.Controls.Add(controlConfig);
            }

            using (var fs = new FileStream(AppConfig.AtomUITemplate, FileMode.Truncate))
            {
                serializer.Serialize(fs, _savedTemplates);
            }

            FillTemplateComboBox();

            cmbTemplate.SelectedValue = template.TemplateId;

            MessageBox.Show("Template saved successfully.", "Save successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Loads the form configurations.
        /// </summary>
        /// <param name="selectedValue">The selected value.</param>
        private void LoadFormConfigurations(int selectedValue)
        {
            var template = _savedTemplates.Templates.Find(t => t.TemplateId == selectedValue);

            if (template == null)
                return;

            txtConnectionString.Text = template.ConnectionString;
            txtMasterworksUrl.Text = template.MasterworksUrl;
            txtMasterworksUserName.Text = template.UserName;
            txtMasterworksPassword.Text = template.Password;

            txtSolutionTargetPath.Text = template.SolutionTargetPath;
            txtDeploymentPath.Text = template.DeploymentPath;
            txtAtomReactorPath.Text = template.AtomReactorExecutableFilePath;

            chkCompileAndGenerateLibrary.Checked = template.CompileAndGenerateLibrary;
            chkAutoDeploy.Checked = template.IsAutoDeploy;

            btnFetchModules_Click(this, new EventArgs());

            var module = template.Modules.Find(m => m.ModuleId == cmbModules.SelectedValue.ToString());

            if (module != null)
            {
                cmbAutomationGuidField.SelectedValue = module.AutomationGuidFieldName;
            }

            LoadControlConfigurations(template, (string)cmbModules.SelectedValue);
        }

        /// <summary>
        /// Loads the test case configurations.
        /// </summary>
        /// <param name="module">The module.</param>
        private void LoadTestCaseConfigurations(ModuleConfig module)
        {
            chkIncludeNegativeTestCase.Checked = module.IncludeNegativeTestCase;
            chkIncludeSecurityTestCase.Checked = module.IncludeSecurityTestCase;
            chkIncludeEditModeValidation.Checked = module.IncludeEditModeValidation;
            chkIncludeViewModeValidation.Checked = module.IncludeViewModeValidation;
            chkIncludeDbValidation.Checked = module.IncludeDBValidation;
        }

        /// <summary>
        /// Loads the solution configurations.
        /// </summary>
        /// <param name="module">The module.</param>
        private void LoadSolutionConfigurations(ModuleConfig module)
        {
            chkGenerateDistinctDLL.Checked = module.GenerateDistinctDLLs;
            chkGenerateDistinctSuiteXmlConfig.Checked = module.GenerateDistinctSuiteXMLConfig;
            chkAutoGenerateSolutionName.Checked = module.AutoGenerateSolutionName;
            //chkCompileAndGenerateLibrary.Checked = module.CompileAndGenerateLibrary;

            //txtSolutionName.Text = module.SolutionName;
            //txtSolutionTargetPath.Text = module.SolutionTargetPath;
        }

        /// <summary>
        /// Loads the control configurations.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="moduleId">The module identifier.</param>
        private void LoadControlConfigurations(Template template, string moduleId)
        {
            var module = template.Modules.Find(m => m.ModuleId == moduleId);

            if (module == null)
                return;

            foreach (var control in module.Controls)
            {
                var node = tvControlsTree.Nodes.Find(control.Name, false);

                if (node.Length == 0)
                    continue;

                foreach (var attribute in control.Attributes)
                {
                    var subNode = node[0].Nodes.Find(attribute, false);

                    if (subNode.Length == 0)
                        continue;

                    subNode[0].Checked = true;
                    node[0].Expand();
                }
            }

            LoadTestCaseConfigurations(module);
            LoadSolutionConfigurations(module);
        }

        /// <summary>
        /// Fills the automation unique identifier field combo.
        /// </summary>
        private void FillAutomationGuidFieldCombo()
        {
            if (_formControls == null || _formControls.Count == 0)
                return;

            var controls = _formControls.Keys
                     .Select<string, ControlsListItem>(x =>
                     {
                         return new ControlsListItem
                         {
                             ControlName = x,
                             ControlCaption = _formControls[x].Caption
                         };
                     })
                     .ToList();

            cmbAutomationGuidField.ValueMember = "ControlCaption";
            cmbAutomationGuidField.DisplayMember = "ControlName";
            cmbAutomationGuidField.DataSource = controls.OrderBy(f => f.ControlCaption).ToList();
        }

        /// <summary>
        /// Updates the status strip.
        /// </summary>
        /// <param name="message">The message.</param>
        private void UpdateStatusStrip(string message)
        {
            stsCurrentOperation.Text = message;
            ssStatusStrip.Refresh();
        }

        /// <summary>
        /// Clears the module configuration.
        /// </summary>
        private void ClearTestCaseConfigurations()
        {
            if (cmbAutomationGuidField.Items.Count > 0)
            {
                cmbAutomationGuidField.DataSource = null;
                cmbAutomationGuidField.Items.Clear();
            }
            tvControlsTree.Nodes.Clear();
            chkIncludeNegativeTestCase.Checked = false;
            chkIncludeEditModeValidation.Checked = false;
            chkIncludeSecurityTestCase.Checked = false;
            chkIncludeViewModeValidation.Checked = false;
            chkIncludeDbValidation.Checked = false;
        }

        /// <summary>
        /// Clears the solution configurations.
        /// </summary>
        private void ClearSolutionConfigurations()
        {
            chkGenerateDistinctDLL.Checked = false;
            chkGenerateDistinctSuiteXmlConfig.Checked = false;
            chkAutoGenerateSolutionName.Checked = false;
        }

        #region Private Class

        /// <summary>
        ///
        /// </summary>
        private class TemplateListItem
        {
            /// <summary>
            /// Gets or sets the name of the template.
            /// </summary>
            /// <value>
            /// The name of the template.
            /// </value>
            public string TemplateName { get; set; }

            /// <summary>
            /// Gets or sets the template identifier.
            /// </summary>
            /// <value>
            /// The template identifier.
            /// </value>
            public int TemplateId { get; set; }
        }

        /// <summary>
        ///
        /// </summary>
        private class ControlsListItem
        {
            /// <summary>
            /// Gets or sets the name of the control.
            /// </summary>
            /// <value>
            /// The name of the control.
            /// </value>
            public string ControlName { get; set; }

            /// <summary>
            /// Gets or sets the control caption.
            /// </summary>
            /// <value>
            /// The control caption.
            /// </value>
            public string ControlCaption { get; set; }
        }

        #endregion

        #region Solution Config Stuff
        private void txtSolutionName_Leave(object sender, EventArgs e)
        {
            txtSolutionName.Text = FileHelper.GetValidFileName(txtSolutionName.Text.Trim());
        }

        private void btnSelectSolutionTargetPath_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSolutionTargetPath.Text))
                pickFolderDialog.SelectedPath = txtSolutionTargetPath.Text;

            DialogResult dr = pickFolderDialog.ShowDialog();

            if (dr == DialogResult.OK)
                txtSolutionTargetPath.Text = pickFolderDialog.SelectedPath;

        }

        #endregion Solution Config Stuff

        private void btnDeploymentPathSelect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDeploymentPath.Text))
                pickFolderDialog.SelectedPath = txtDeploymentPath.Text;

            DialogResult dr = pickFolderDialog.ShowDialog();

            if (dr == DialogResult.OK)
                txtDeploymentPath.Text = pickFolderDialog.SelectedPath;
        }

        private void btnSelectReactorFile_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAtomReactorPath.Text))
                openFileDialog.InitialDirectory = Path.GetDirectoryName(txtAtomReactorPath.Text);

            openFileDialog.Filter = "txt files (*.exe)|*.exe";

            DialogResult dr = openFileDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                txtAtomReactorPath.Text = openFileDialog.FileName;
            }
        }

        private void btnLaunchAtomReactor_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAtomReactorPath.Text))
            {
                MessageBox.Show("Reactor Executable Path is not selected. Please select it under Master Setting page.");
                return;
            }

            Aurigo.Atom.UI.Program.ExecuteCommand(string.Format("\"{0}\"", txtAtomReactorPath.Text));
        }

        private void chkAutoDeploy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoDeploy.Checked)
            {
                if (String.IsNullOrWhiteSpace(txtDeploymentPath.Text))
                {
                    MessageBox.Show("Deployment path is still missing. Cannot be checked");
                    chkAutoDeploy.Checked = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tacMain.SelectedIndex = 2;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}