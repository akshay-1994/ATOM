using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurigo.Atom.Generator.Core.CodeGenObjects.VSTemplates
{
    public class GeneratorSolutionFile
    {
        public Guid SolutionGuid { get; private set; } = Guid.NewGuid();
      
        public List<GeneratorProjectXmlFile> ProjectFileList { get; private set; } = new List<GeneratorProjectXmlFile>();
    }
}
