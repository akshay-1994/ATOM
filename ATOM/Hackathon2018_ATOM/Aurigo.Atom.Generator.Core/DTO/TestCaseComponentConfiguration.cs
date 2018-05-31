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
    [XmlRoot("TestCaseComponentConfiguration")]
    public class TestCaseComponentConfiguration
    {
        /// <summary>
        /// Gets or sets the controls.
        /// </summary>
        /// <value>
        /// The controls.
        /// </value>
        //[XmlElement("Control")]
        [XmlElement("Control")]
        public List<Control> Controls { get; set; }
    }
}