using Aurigo.Atom.Generator.Core.CodeGenObjects.VSTemplates;
using Aurigo.Atom.Generator.Core.CodeGenTemplates.VSTemplates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurigo.Atom.Generator.Core.CodeSolutionBuilder
{
    public class AssemblyInfoCsFileGenerator : AbstractFileGeneratorBase
    {
        public GeneratorProjectXmlFile TemplateDataObject { get; private set; }

        public AssemblyInfoCsFileGenerator(GeneratorProjectXmlFile templateDataObject)
        {
            this.TemplateDataObject = templateDataObject;
        }

        protected override string FileName { get { return "AssemblyInfo.cs"; } }

        protected override string GetFileContent()
        {
            var template = new AssemblyInfoCsFileTemplate();

            template.Session = new Dictionary<string, object>()
            {
                { "GeneratorProjectXmlFile", this.TemplateDataObject }
            };

            template.Initialize();

            return template.TransformText();
        }
    }
}