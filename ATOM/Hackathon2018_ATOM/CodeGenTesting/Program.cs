using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aurigo.Atom.Generator.Core.CodeGenObjects.BasicTemplate;
using Aurigo.Atom.Generator.Core.CodeGenTemplates.BasicTemplate;
using Aurigo.Atom.Common.DTO;

namespace CodeGenTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            TestModuleConfig tc = new TestModuleConfig()
            {
                ModuleId = "CONTDWR",
                ModuleName = "Daily Progress Report",
                TableName = "DPRTable",
                PrimaryKeyColumnName = "ID",
            };

            var template = new TCMainClassTemplate();

            template.Session = new Dictionary<string, object>()
            {
                { "TestModuleConfig", tc },
            };

            template.Initialize();

            Console.WriteLine(template.TransformText());
        }

    }
}
