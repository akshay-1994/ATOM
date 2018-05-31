using Aurigo.Brix.Platform.BusinessLayer.XMLForm;

/// <summary>
///
/// </summary>
namespace Aurigo.Atom.Common.DTO
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class TestCaseComponentGeneratorOptions
    {
        /// <summary>
        /// Gets or sets the control.
        /// </summary>
        /// <value>
        /// The control.
        /// </value>
        public xControl Control { get; set; }

        /// <summary>
        /// Gets or sets the test module configuration.
        /// </summary>
        /// <value>
        /// The test module configuration.
        /// </value>
        public TestModuleConfig TestModuleConfig { get; set; }
    }
}