using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurigo.Atom.Common.DTO
{
    /// <summary>
    ///
    /// </summary>
    public class TestCaseComponent
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public TestCaseType Type { get; set; }

        /// <summary>
        /// Gets or sets the test case.
        /// </summary>
        /// <value>
        /// The test case.
        /// </value>
        public string TestCaseSetter { get; set; }

        /// <summary>
        /// Gets or sets the test case validator.
        /// </summary>
        /// <value>
        /// The test case validator.
        /// </value>
        public string TestCaseDBValidator { get; set; }

        /// <summary>
        /// Gets or sets the test case edit mode validator.
        /// </summary>
        /// <value>
        /// The test case edit mode validator.
        /// </value>
        public string TestCaseEditModeValidator { get; set; }

        /// <summary>
        /// Gets or sets the test case view mode validator.
        /// </summary>
        /// <value>
        /// The test case view mode validator.
        /// </value>
        public string TestCaseViewModeValidator { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [will save succeed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [will save succeed]; otherwise, <c>false</c>.
        /// </value>
        public bool WillSaveSucceed { get; set; }

        /// <summary>
        /// Order in which the test cases will be picked
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        public string OnScreenValidator { get; set; }
    }
}