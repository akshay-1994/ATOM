using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurigo.Atom.Common.Attributes
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class TestCaseComponentGeneratorAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}