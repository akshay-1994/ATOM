using System.Collections.Generic;
using Aurigo.Brix.Platform.BusinessLayer.XMLForm;

namespace Aurigo.Atom.Common
{
    /// <summary>
    ///
    /// </summary>
    public class TestComponent
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the control.
        /// </summary>
        /// <value>
        /// The control.
        /// </value>
        public xControl Control { get; set; }

        /// <summary>
        /// Gets or sets the attribues.
        /// </summary>
        /// <value>
        /// The attribues.
        /// </value>
        public List<string> Attribues { get; set; }
    }
}