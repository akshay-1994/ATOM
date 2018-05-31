using Aurigo.Atom.Common.DTO;
using Aurigo.Atom.Generator.Core.CodeGenObjects.BasicTemplate;
using Aurigo.Atom.Generator.Core.CodeGenTemplates.BasicTemplate;
using System;
using System.Collections.Generic;

namespace Aurigo.Atom.Generator.Core.CodeSolutionBuilder
{
    public class TestCaseMainCSharpFileGenerator : AbstractFileGeneratorBase
    {
        public TestModuleConfig TemplateDataObject { get; private set; }

        public TestCaseMainCSharpFileGenerator(TestModuleConfig tcMainClassObject)
        {
            this.TemplateDataObject = tcMainClassObject;
        }

        protected override string FileName { get { return "TC._.cs"; } }

        protected override string GetFileContent()
        {
            var template = new TCMainClassTemplate();

            template.Session = new Dictionary<string, object>()
            {
                { "TestModuleConfig", this.TemplateDataObject }
            };

            template.Initialize();

            return template.TransformText();
        }
    }
}