using System.Collections.Generic;
using Aurigo.Atom.Common.DTO;

namespace Aurigo.Atom.Common.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface ITestCaseComponentGenerator
    {
        /// <summary>
        /// Generates the specified arguments.
        /// </summary>
        /// <param name="args">The <see cref="TestCaseComponentGeneratorOptions" /> instance containing the event data.</param>
        /// <returns></returns>
        List<TestCaseComponent> Generate(TestCaseComponentGeneratorOptions args);
    }
}