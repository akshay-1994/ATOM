using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurigo.Atom.Generator.Core.CodeSolutionBuilder
{
    public abstract class AbstractFileGeneratorBase
    {
        protected abstract string GetFileContent();

        protected abstract string FileName { get; }

        /// <summary>
        /// Return fileName
        /// </summary>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public virtual string GenerateFile(string savePath)
        {
            StreamWriter stream = File.CreateText(Path.Combine(savePath, this.FileName));

            stream.Write(this.GetFileContent());

            stream.Flush();

            stream.Close();

            return this.FileName;
        }
    }
}
