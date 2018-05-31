using Aurigo.Atom.Common.DTO;

namespace Aurigo.Atom.Generator.Core.Helpers
{
    public static class TestCaseTypeHelper
    {
        public static TestCaseType[] PositiveTestCaseTypes
        {
            get
            {
                return new TestCaseType[] { TestCaseType.POSITIVE, TestCaseType.DEFAULT };
            }
        }

        public static TestCaseType[] NegativeTestCaseTypes
        {
            get
            {
                return new TestCaseType[] { TestCaseType.NEGATIVE };
            }
        }
    }
}