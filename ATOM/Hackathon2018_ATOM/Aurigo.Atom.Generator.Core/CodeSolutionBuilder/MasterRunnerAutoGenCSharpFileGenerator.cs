using Aurigo.Atom.Generator.Core.CodeGenObjects.BasicTemplate;
using Aurigo.Atom.Generator.Core.CodeGenTemplates.BasicTemplate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurigo.Atom.Generator.Core.CodeSolutionBuilder
{
    public class MasterRunnerAutoGenCSharpFileGenerator : AbstractFileGeneratorBase
    {
        public GeneratorMasterRunner_AutoGenTests TemplateDataObject { get; private set; }

        public MasterRunnerAutoGenCSharpFileGenerator(string moduleId, string suiteName)
        {
            this.TemplateDataObject = new GeneratorMasterRunner_AutoGenTests()
            {
                ModuleId = moduleId,
                SuiteName = suiteName,
            };
        }

        public void AddScenarioMethodName(string scenarioMethodName)
        {
            this.TemplateDataObject.ComputedList_For_ScenarioMethod.Add(scenarioMethodName);
        }
        
        protected override string FileName { get { return "MasterRunner_AutoGenTests.cs"; } }

        protected override string GetFileContent()
        {
            var template = new MasterRunner_AutoGenTestsTemplate();

            template.Session = new Dictionary<string, object>()
            {
                { "GeneratorMasterRunner_AutoGenTests", this.TemplateDataObject }
            };

            template.Initialize();

            return template.TransformText();
        }
    }
}
