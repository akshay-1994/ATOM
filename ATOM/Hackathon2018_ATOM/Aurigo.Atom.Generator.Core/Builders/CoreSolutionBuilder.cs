using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Aurigo.Atom.Common.DTO;
using Aurigo.Atom.Common.GeneratorObjects;
using Aurigo.Atom.Generator.Core.CodeGenObjects.VSTemplates;
using Aurigo.Atom.Generator.Core.CodeSolutionBuilder;
using Aurigo.Atom.Generator.Core.Helpers;

namespace Aurigo.Atom.Generator.Core.Builder
{
    public class CodeSolutionBuilder
    {
        #region Constants

        public const string CONST_FOLDER_AutoGenTests = "AutoGenTests";
        public const string CONST_FOLDER_Properties = "Properties";
        public const string CONST_FOLDER_UserTests = "UserTests";

        public const string CONST_FOLDER_SOURCE_CODE_CONTAINER = "SourceCode";
        public const string CONST_FOLDER_COMPILED_CODE_CONTAINER = "CompiledCode";

        #endregion Constants

        #region Static Methods

        /// <summary>
        /// Returns last known Solution Folder  dirPath
        /// </summary>
        /// <param name="oldSolutionPath"></param>
        /// <param name="solutionName"></param>
        /// <returns></returns>
        private static string TryGettingLastGeneratedFolderPath(string oldSolutionPath, string solutionName)
        {
            var folderName = FileHelper.GetValidFileName(solutionName);

            if (!string.IsNullOrEmpty(oldSolutionPath))
            {
                //List<Tuple<string, DateTime>> viableDirectoryNames = new List<Tuple<string, DateTime>>();

                Tuple<string, DateTime> latestFolder = null;

                foreach (var dirPath in Directory.EnumerateDirectories(oldSolutionPath))
                {
                    string directoryName = Path.GetFileName(dirPath);
                    if (directoryName.StartsWith(solutionName + "_", StringComparison.InvariantCultureIgnoreCase))
                    {
                        DateTime dt = Directory.GetCreationTime(dirPath);

                        var curDirItem = Tuple.Create(dirPath, dt);

                        //viableDirectoryNames.Add(curDirItem);

                        if (latestFolder == null || curDirItem.Item2 > latestFolder.Item2)
                            latestFolder = curDirItem;
                    }
                }

                if (latestFolder != null)
                    return latestFolder.Item1;
            }

            return null;
        }

        #endregion Static Methods

        #region Public Get properties

        public SolutionGenerationConfig SolutionGenConfig { get; private set; }

        public VsSolutionFileGenerator SolutionGenerator { get; private set; }

        public List<CsProjectFileGenerator> CsProjectList { get; private set; } = new List<CsProjectFileGenerator>();

        #endregion Public Get properties

        #region Private  properties /class

        private string _Final_SolutionFolderName { get; set; }

        private string _DateTimeStamp { get; set; }

        private string _SolutionSourceCodeFolderFullPath { get; set; }

        private string _SolutionCompiledCodeFolderFullPath { get; set; }

        /// <summary>
        /// this stores the full path of the generated solution code (ie the folder (with datetimestam and module id) where solution code goes into
        /// </summary>
        private string _SolutionContentFolderFullPath { get; set; }

        private string PathForSharedLibraryResource
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PathForSharedLibraryResource"]))
                    return ConfigurationManager.AppSettings["PathForSharedLibraryResource"];
                else
                    throw new Exception("Please define the PathForSharedLibraryResource");
            }
        }

        protected string _ProjectFileNameWithExt { get; set; }

        protected class GlobalGeneratedFileNames
        {
            public string CsFile_AutoGenTest { get; set; }

            public string CsFile_UserTest { get; set; }

            public string TestSuiteXmlFile { get; set; }

            public string CsFile_TestConfigFile { get; set; }

            public string ConfigFile_AppSettingConfigFile { get; set; }
        }

        #endregion Private  properties /class

        #region Constructor

        public CodeSolutionBuilder()
        {
        }

        #endregion Constructor

        #region Public Generator Method

        public void Generate(TestCaseScenario scenarioConfig, SolutionGenerationConfig solutionConfig, StreamWriter sWrite = null)
        {
            if (!Directory.Exists(solutionConfig.SolutionTargetPath))
                Directory.CreateDirectory(solutionConfig.SolutionTargetPath);

            GlobalGeneratedFileNames fileNameListing = Generate_Core(scenarioConfig, solutionConfig);

            sWrite.WriteLine("Code generation started");

            if (this.SolutionGenConfig.IsCompileAndGenerateLibrary)
            {
                this._SolutionCompiledCodeFolderFullPath = Path.Combine(this.SolutionGenConfig.SolutionTargetPath, CONST_FOLDER_COMPILED_CODE_CONTAINER);
                //string targetCompiledFolderPath = Path.Combine(this.SolutionGenConfig.SolutionTargetPath, "CompiledCode");

                string dllFileNameWithFullPath;
                bool isCOmpileSuccess = this.BuildRunLibrary(sWrite, this._SolutionCompiledCodeFolderFullPath, out dllFileNameWithFullPath);

                if (isCOmpileSuccess)
                {
                    //copy TestSuiteXmlFile  To TargetCompiledPath
                    string xmlFileNameWithFullPath = this.CopyFile_ToTargetCompiledPath(fileNameListing.TestSuiteXmlFile, this._SolutionContentFolderFullPath, this._SolutionCompiledCodeFolderFullPath);

                    //Copy AppConfigFile To TargetCompiledPath
                    string appSettingFilePath = this.CopyFile_ToTargetCompiledPath(fileNameListing.ConfigFile_AppSettingConfigFile, this._SolutionContentFolderFullPath, this._SolutionCompiledCodeFolderFullPath);

                    if (this.SolutionGenConfig.IsAutoDeploy)
                    {
                        string dllfileNameWithoutExtension = Path.GetFileNameWithoutExtension(dllFileNameWithFullPath);
                        string src_dllLocationPath = Path.GetDirectoryName(dllFileNameWithFullPath);

                        string dllFileName = dllfileNameWithoutExtension + ".dll";
                        string pdbFileName = dllfileNameWithoutExtension + ".pdb";

                        string xmlFileName = Path.GetFileName(xmlFileNameWithFullPath);
                        string appSettingFileName = Path.GetFileName(appSettingFilePath);

                        //Copy dll and pdb file
                        if (File.Exists(Path.Combine(src_dllLocationPath, dllFileName)))
                            File.Copy(Path.Combine(src_dllLocationPath, dllFileName), Path.Combine(this.SolutionGenConfig.AutoDeployPath, dllFileName), true);
                        if (File.Exists(Path.Combine(src_dllLocationPath, pdbFileName)))
                            File.Copy(Path.Combine(src_dllLocationPath, pdbFileName), Path.Combine(this.SolutionGenConfig.AutoDeployPath, pdbFileName), true);

                        //copy xml file
                        if (File.Exists(xmlFileNameWithFullPath))
                            File.Copy(xmlFileNameWithFullPath, Path.Combine(this.SolutionGenConfig.AutoDeployPath, xmlFileName), true);

                        //copy app.Config file
                        if (File.Exists(appSettingFilePath))
                            File.Copy(appSettingFilePath, Path.Combine(this.SolutionGenConfig.AutoDeployPath, appSettingFileName), true);
                    }
                }
            }

            sWrite.WriteLine("Code generation completed");
        }

        protected GlobalGeneratedFileNames Generate_Core(TestCaseScenario scenarioConfig, SolutionGenerationConfig solutionConfig)
        {
            this.SolutionGenConfig = solutionConfig;

            string suiteName = scenarioConfig.ModuleConfig.ModuleId + "_TestSuite";

            //Step 1: Create Solution Folder
            this.Create_SolutionFolder_WithDateTimeStamp(suiteName);

            string assemblyName = scenarioConfig.ModuleConfig.AutoGeneratedProjectNamespace + (this.SolutionGenConfig.IsGeneratedDistinctSuiteXmlConfig ? ("_" + this._DateTimeStamp) : string.Empty);
            scenarioConfig.ModuleConfig.AssemblyName = assemblyName;

            //Step 2: create necessary folders : create top level folders : AutoGenTests & UserTests
            this.Create_RequiredFolders_InsideSolutionFolder();

            List<string> scenarioMethodNames = new List<string>();
            scenarioConfig.Scenarios.ForEach((s) =>
            {
                scenarioMethodNames.Add(s.Name);
            });

            //Step 3: create top level files: MasterRunner_AutoGenTests.cs, MasterRunner_UserTests.cs, ModuleXYZ_TestSuite.xml,TestConfig.cs
            GlobalGeneratedFileNames globalGeneratedFileNames = this.Create_RequiredFiles_InsideSolutionFolder(scenarioConfig.ModuleConfig.ModuleId, suiteName, scenarioMethodNames, scenarioConfig);

            //Step 4: create files inside AutoGenTests folder
            //TC._.cs
            this.Create_TestCaseMainFile_InsideAutoGenTests(scenarioConfig.ModuleConfig);
            //TC.Scenario.cs
            List<GeneratorScenarioFileObject> scenarioFileList = Create_All_TestCaseScenarioFile_InsideAutoGenTests(scenarioConfig); //TODO: substitue null with object

            //Step 5: copy old UserTests contents
            //TODO: based on time constraint

            //Step 6: Create only one project for now :  project 1
            GeneratorProjectXmlFile csprojXmlObj = this.Create_ProjectFile(
                    scenarioConfig.ModuleConfig.ModuleId,
                    scenarioFileList,
                    globalGeneratedFileNames.TestSuiteXmlFile,
                    assemblyName
                );

            //Step 7: Create Assembly file name under properties using GeneratorProjectXmlFile
            Create_AssemblyInfoCsFile(csprojXmlObj);

            //Step 8: Create Solution File (yipee!!!)
            Create_SolutionFile(new List<GeneratorProjectXmlFile>() { csprojXmlObj });

            return globalGeneratedFileNames;
        }

        /// <summary>
        /// Return if it is success
        /// </summary>
        /// <param name="sWrite"></param>
        /// <param name="targetCompiledPath"></param>
        /// <param name="dllFileNameWithFullPath"></param>
        /// <returns></returns>
        protected bool BuildRunLibrary(TextWriter sWrite, string targetCompiledPath, out string dllFileNameWithFullPath)
        {
            bool hasError = true;

            if (sWrite == null)
                sWrite = new StringWriter();

            try
            {
                string projectFullPath = Path.Combine(this._SolutionContentFolderFullPath, this._ProjectFileNameWithExt);
                hasError = CompilerHelper.CompilerProject(this._SolutionContentFolderFullPath, projectFullPath, targetCompiledPath, sWrite, out dllFileNameWithFullPath);
            }
            catch (Exception e)
            {
                throw;
            }

            return !hasError;

        }

        /// <summary>
        /// Return the target filename with full path (File that got created)
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="srcFilePath"></param>
        /// <param name="targetCompiledPath"></param>
        /// <returns></returns>
        protected string CopyFile_ToTargetCompiledPath(string fileName, string srcFilePath, string targetCompiledPath)
        {
            string fullFilePath_appConfigFile = Path.Combine(this._SolutionContentFolderFullPath, this._ProjectFileNameWithExt);

            string fullSrcPathWithFileName = Path.Combine(srcFilePath, fileName);
            string fulltargetPathWithFileName = Path.Combine(targetCompiledPath, fileName);
            File.Copy(fullSrcPathWithFileName, fulltargetPathWithFileName, true);

            return fulltargetPathWithFileName;
        }

        #endregion Public Generator Method

        #region Sequential Private Methods

        private void Create_SolutionFolder_WithDateTimeStamp(string defaultSuiteName)
        {
            string dateTimeStampUsed = "";
            string autoGeneratedSolutionName = FileHelper.GetFolderPathWithCurrentTimeStamp(defaultSuiteName, out dateTimeStampUsed);
            this._DateTimeStamp = dateTimeStampUsed;

            if (this.SolutionGenConfig.IsAutoGenerateSolutionName || string.IsNullOrEmpty(this.SolutionGenConfig.SolutionName))
                this._Final_SolutionFolderName = autoGeneratedSolutionName;
            else
                this._Final_SolutionFolderName = FileHelper.GetValidFileName(this.SolutionGenConfig.SolutionName);

            this._SolutionSourceCodeFolderFullPath = Path.Combine(this.SolutionGenConfig.SolutionTargetPath, CONST_FOLDER_SOURCE_CODE_CONTAINER);
            this._SolutionCompiledCodeFolderFullPath = Path.Combine(this.SolutionGenConfig.SolutionTargetPath, CONST_FOLDER_COMPILED_CODE_CONTAINER);

            if (!Directory.Exists(this._SolutionSourceCodeFolderFullPath))
                Directory.CreateDirectory(this._SolutionSourceCodeFolderFullPath);

            if (!Directory.Exists(this._SolutionCompiledCodeFolderFullPath))
                Directory.CreateDirectory(this._SolutionCompiledCodeFolderFullPath);

            this._SolutionContentFolderFullPath = Path.Combine(this._SolutionSourceCodeFolderFullPath, this._Final_SolutionFolderName);

            if (!Directory.Exists(this._SolutionContentFolderFullPath))
                Directory.CreateDirectory(this._SolutionContentFolderFullPath);
        }

        private void Create_RequiredFolders_InsideSolutionFolder()
        {
            Directory.CreateDirectory(Path.Combine(this._SolutionContentFolderFullPath, CONST_FOLDER_Properties));
            Directory.CreateDirectory(Path.Combine(this._SolutionContentFolderFullPath, CONST_FOLDER_AutoGenTests));
            Directory.CreateDirectory(Path.Combine(this._SolutionContentFolderFullPath, CONST_FOLDER_UserTests));
        }

        private GlobalGeneratedFileNames Create_RequiredFiles_InsideSolutionFolder(string moduleId, string suiteName, List<string> scenarioMethodNames, TestCaseScenario scenarioConfig)
        {
            //Step 3: create top level files: MasterRunner_AutoGenTests.cs, MasterRunner_UserTests.cs, ModuleXYZ_TestSuite.xml,TestConfig.cs

            GlobalGeneratedFileNames fileNameListing = new GlobalGeneratedFileNames();

            MasterRunnerAutoGenCSharpFileGenerator csFile_autoGen = new MasterRunnerAutoGenCSharpFileGenerator(moduleId, suiteName);
            csFile_autoGen.TemplateDataObject.ComputedList_For_ScenarioMethod = scenarioMethodNames;
            fileNameListing.CsFile_AutoGenTest = csFile_autoGen.GenerateFile(this._SolutionContentFolderFullPath);

            MasterRunnerUserCSharpFileGenerator csFile_user = new MasterRunnerUserCSharpFileGenerator(moduleId, suiteName);
            fileNameListing.CsFile_UserTest = csFile_user.GenerateFile(this._SolutionContentFolderFullPath);

            string fileNameModifier = this.SolutionGenConfig.IsGeneratedDistinctSuiteXmlConfig ? ("_" + this._DateTimeStamp) : string.Empty;
            TestSuiteXmlFileGenerator testSuiteXmlFile = new TestSuiteXmlFileGenerator(moduleId, fileNameModifier, Get_TestSuiteConfigFile_Object(scenarioMethodNames, scenarioConfig));
            fileNameListing.TestSuiteXmlFile = testSuiteXmlFile.GenerateFile(this._SolutionContentFolderFullPath);

            TestSuiteAppConfigGenerator testSuiteAppConfig = new TestSuiteAppConfigGenerator(moduleId, fileNameModifier, Get_TestSuiteAppConfig(scenarioConfig));
            fileNameListing.ConfigFile_AppSettingConfigFile = testSuiteAppConfig.GenerateFile(this._SolutionContentFolderFullPath);

            TestConfigCSharpFileGenerator csFile_TestConfigFile = new Core.CodeSolutionBuilder.TestConfigCSharpFileGenerator(moduleId);
            fileNameListing.CsFile_TestConfigFile = csFile_TestConfigFile.GenerateFile(this._SolutionContentFolderFullPath);

            return fileNameListing;
        }

        private AppSettings Get_TestSuiteAppConfig(TestCaseScenario scenarioConfig)
        {
            AppSettings settings = new AppSettings();
            settings.Keys.Add(new AppSettingsKey() { Key = "Username", Value = scenarioConfig.ModuleConfig.Credentials.Username });
            settings.Keys.Add(new AppSettingsKey() { Key = "Password", Value = scenarioConfig.ModuleConfig.Credentials.Password });
            settings.Keys.Add(new AppSettingsKey() { Key = "Url", Value = scenarioConfig.ModuleConfig.Credentials.URL });
            settings.Keys.Add(new AppSettingsKey() { Key = "SiteConnectionString", Value = scenarioConfig.ModuleConfig.Credentials.ConnectionString });

            return settings;
        }

        private TestSuiteConfigFile Get_TestSuiteConfigFile_Object(List<string> scenarioMethodNames, TestCaseScenario scenarioConfig)
        {
            TestSuiteConfigFile config = new TestSuiteConfigFile();
            config.LibraryName = scenarioConfig.ModuleConfig.AutoGeneratedProjectNamespace + "." + "MasterRunner_AutoGenTests";
            config.LibraryAssemblyName = scenarioConfig.ModuleConfig.AssemblyName;

            RunConfigObject runObject = new RunConfigObject();
            runObject.Run = "1-" + scenarioMethodNames.Count;
            runObject.RunUser = true;
            config.RunConfig = runObject;

            List<TestDefinition> tests = new List<TestDefinition>();
            config.TestDescriptions = new List<TestDescription>();

            int scenarioId = 1;
            scenarioMethodNames.ForEach((scenario) =>
            {
                TestDefinition test = new TestDefinition();
                test.Method = scenario;
                test.Id = scenarioId++;
                tests.Add(test);

                var description = new TestDescription
                {
                    Id = test.Id,
                    Description = string.Join(Environment.NewLine, scenarioConfig.Scenarios.Find(s => s.Name == scenario).ScenarioDescription)
                };

                config.TestDescriptions.Add(description);
            });

            config.TestDefinitionList = tests;

            return config;
        }

        private void Create_TestCaseMainFile_InsideAutoGenTests(TestModuleConfig moduleConfig)
        {
            TestCaseMainCSharpFileGenerator csFile = new TestCaseMainCSharpFileGenerator(moduleConfig);
            csFile.GenerateFile(Path.Combine(this._SolutionContentFolderFullPath, CONST_FOLDER_AutoGenTests));
        }

        private List<GeneratorScenarioFileObject> Create_All_TestCaseScenarioFile_InsideAutoGenTests(TestCaseScenario someConfigObject)
        {
            List<GeneratorScenarioFileObject> returnObj = new List<GeneratorScenarioFileObject>();

            for (var i = 0; i < someConfigObject.Scenarios.Count; i++)
            {
                TestCaseScenarioCSharpFileGenerator csFile = new TestCaseScenarioCSharpFileGenerator(someConfigObject.ModuleConfig, someConfigObject.Scenarios[i]);

                string fileName = csFile.GenerateFile(Path.Combine(this._SolutionContentFolderFullPath, CONST_FOLDER_AutoGenTests));

                returnObj.Add(new CodeGenObjects.VSTemplates.GeneratorScenarioFileObject() { ScenarioFunctionName = someConfigObject.Scenarios[i].Name, FileName = fileName });
            }

            return returnObj;
        }

        private GeneratorProjectXmlFile Create_ProjectFile(string moduleId, List<GeneratorScenarioFileObject> scenarioFileObjectList,
            string moduleConfigXmlFileNameWithExtension, string assemblyName)
        {
            GeneratorProjectXmlFile prjXmlFileForTemplate = new CodeGenObjects.VSTemplates.GeneratorProjectXmlFile(moduleId, moduleConfigXmlFileNameWithExtension, assemblyName);
            prjXmlFileForTemplate.ScenarioFileObjectList.AddRange(scenarioFileObjectList);

            CsProjectFileGenerator csProjFileGen = new CsProjectFileGenerator(prjXmlFileForTemplate);
            this._ProjectFileNameWithExt = csProjFileGen.GenerateFile(this._SolutionContentFolderFullPath);

            //Create DLL reference folder
            string dllPath = Path.Combine(this._SolutionContentFolderFullPath, prjXmlFileForTemplate.SharedLibraryPath);
            Directory.CreateDirectory(dllPath);

            List<string> arrayOfDllTCopy = new List<string>() { "AurigoTest.Toolkit.dll", "Newtonsoft.Json.dll", "RelevantCodes.ExtentReports.dll", "WebDriver.dll", "WebDriver.Support.dll" };

            foreach (var fileName in arrayOfDllTCopy)
            {
                File.Copy(Path.Combine(PathForSharedLibraryResource, fileName), Path.Combine(dllPath, fileName));
            }

            return prjXmlFileForTemplate;
        }

        private string Create_AssemblyInfoCsFile(GeneratorProjectXmlFile prjXmlFileForTemplate)
        {
            AssemblyInfoCsFileGenerator asmInfoCsFileGen = new AssemblyInfoCsFileGenerator(prjXmlFileForTemplate);
            return asmInfoCsFileGen.GenerateFile(Path.Combine(this._SolutionContentFolderFullPath, CONST_FOLDER_Properties));
        }

        private string Create_SolutionFile(List<GeneratorProjectXmlFile> projectList)
        {
            GeneratorSolutionFile slnFile = new CodeGenObjects.VSTemplates.GeneratorSolutionFile();
            slnFile.ProjectFileList.AddRange(projectList);

            VsSolutionFileGenerator vsSlnFileGen = new VsSolutionFileGenerator(this._Final_SolutionFolderName, slnFile);
            return vsSlnFileGen.GenerateFile(this._SolutionContentFolderFullPath); //returns csproj Filename
        }

        #endregion Sequential Private Methods
    }
}