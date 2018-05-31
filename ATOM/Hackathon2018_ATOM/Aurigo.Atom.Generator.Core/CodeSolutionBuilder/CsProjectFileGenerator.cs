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
    public class CsProjectFileGenerator : AbstractFileGeneratorBase
    {
        public GeneratorProjectXmlFile TemplateDataObject { get; private set; }

        public CsProjectFileGenerator(GeneratorProjectXmlFile templateDataObject)
        {
            this.TemplateDataObject = templateDataObject;
        }

        protected override string FileName { get { return  this.TemplateDataObject.ProjectFileName ; } }

        protected override string GetFileContent()
        {
            var template = new ProjectXmlFileTemplate();

            template.Session = new Dictionary<string, object>()
            {
                { "GeneratorProjectXmlFile", this.TemplateDataObject }
            };

            template.Initialize();

            return template.TransformText();
        }
    }
}