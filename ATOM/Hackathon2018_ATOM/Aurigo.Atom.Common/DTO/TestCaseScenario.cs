using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurigo.Atom.Common.DTO
{
    public class TestCaseScenario
    {
        public TestModuleConfig ModuleConfig { get; set; }
        public List<ScenarioDTO> Scenarios { get; set; }
    }
}
