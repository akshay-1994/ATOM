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
    public class VsSolutionFileGenerator : AbstractFileGeneratorBase
    {
        public GeneratorSolutionFile TemplateDataObject { get; private set; }

        private string _solutionFileNameWithoutFileExtension = string.Empty;

        public VsSolutionFileGenerator(string solutionFileNameWithoutFileExtension, GeneratorSolutionFile slnFile)
        {
            this._solutionFileNameWithoutFileExtension = solutionFileNameWithoutFileExtension;
            this.TemplateDataObject = slnFile;
        }

        protected override string FileName { get { return $"{this._solutionFileNameWithoutFileExtension}.sln"; } }

        protected override string GetFileContent()
        {
            var template = new SolutionFileTemplate();

            template.Session = new Dictionary<string, object>()
            {
                { "GeneratorSolutionFile", this.TemplateDataObject }
            };

            template.Initialize();

            return template.TransformText();
        }
    }
}
