using System;
using Aurigo.Brix.Platform.BusinessLayer.XMLForm;

namespace Aurigo.Atom.Common.Attributes
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class ControlVerifierAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public ControlType Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlVerifierAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ControlVerifierAttribute(ControlType name)
        {
            Name = name;
        }
    }
}