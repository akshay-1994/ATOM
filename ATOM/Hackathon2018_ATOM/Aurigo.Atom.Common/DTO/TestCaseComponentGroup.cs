using System.Collections.Generic;

namespace Aurigo.Atom.Common.DTO
{
    public class TestCaseComponentGroup
    {
        public string ControlName { get; set; }

        public List<TestCaseComponent> TestCaseComponents { get; set; } = new List<TestCaseComponent>();
    }
}