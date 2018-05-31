using Aurigo.Atom.Common.DTO;
using Aurigo.Atom.Generator.Core.CodeGenObjects.BasicTemplate;
using Aurigo.Atom.Generator.Core.CodeGenTemplates.BasicTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurigo.Atom.Generator.Core.CodeSolutionBuilder
{
    public class TestCaseScenarioCSharpFileGenerator : AbstractFileGeneratorBase
    {
        public ScenarioDTO TemplateDataObject { get; private set; }

        public TestModuleConfig TestCaseMainClassObjectRef { get; private set; }

        public TestCaseScenarioCSharpFileGenerator(TestModuleConfig tcMainClassObjectRef, ScenarioDTO scenarioObj)
        {
            
            this.TemplateDataObject = scenarioObj;
            this.TestCaseMainClassObjectRef = tcMainClassObjectRef;
        }
        
        protected override string FileName { get { return $"TC.{this.TemplateDataObject.Name}.cs"; } }

        protected override string GetFileContent()
        {
            var template = new TCMainClassScenarioTemplate();

            template.Session = new Dictionary<string, object>()
            {
                { "ScenarioDTO", this.TemplateDataObject },
                { "TestModuleConfig", this.TestCaseMainClassObjectRef }
            };

            template.Initialize();

            return template.TransformText();
        }
    }
}