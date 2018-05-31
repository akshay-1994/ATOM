using System.Collections.Generic;
using System.Xml.Serialization;
using Aurigo.Brix.Platform.BusinessLayer.XMLForm;

namespace Aurigo.Atom.Generator.Core.DTO
{
    /// <summary>
    ///
    /// </summary>
    public class Control
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [XmlAttribute("Type")]
        public ControlType Type { get; set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        [XmlElement("Attribute")]
        public List<Attribute> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the default name of the value generator.
        /// </summary>
        /// <value>
        /// The default name of the value generator.
        /// </value>
        [XmlAttribute("DefaultValueGeneratorName")]
        public string DefaultValueGeneratorName { get; set; }

        /// <summary>
        /// Gets or sets the name of the security test case generator.
        /// </summary>
        /// <value>
        /// The name of the security test case generator.
        /// </value>
        [XmlAttribute("SecurityTestCaseGeneratorName")]
        public string SecurityTestCaseGeneratorName { get; set; }
    }
}