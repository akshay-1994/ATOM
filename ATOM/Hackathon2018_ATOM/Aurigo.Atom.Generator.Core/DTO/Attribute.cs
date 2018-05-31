using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Aurigo.Atom.Generator.Core.DTO
{
    /// <summary>
    ///
    /// </summary>
    public class Attribute
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the generator.
        /// </summary>
        /// <value>
        /// The name of the generator.
        /// </value>
        [XmlAttribute(AttributeName = "GeneratorName")]
        public string GeneratorName { get; set; }
    }
}