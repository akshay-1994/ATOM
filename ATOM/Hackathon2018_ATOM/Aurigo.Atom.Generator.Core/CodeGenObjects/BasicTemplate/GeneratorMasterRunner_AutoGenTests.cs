﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurigo.Atom.Generator.Core.CodeGenObjects.BasicTemplate
{
    public class GeneratorMasterRunner_AutoGenTests
    {
        public string AutoGeneratedProjectNamespace { get { return ModuleId + "_TestSuite"; } }

        public string ModuleId { get; set; }

        public string SuiteName { get; set; }

        public List<string> ComputedList_For_ScenarioMethod { get; set; } = new List<string>();
    }
}
