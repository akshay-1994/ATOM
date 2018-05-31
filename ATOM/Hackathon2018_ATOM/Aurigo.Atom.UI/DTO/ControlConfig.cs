using System.Collections.Generic;
using System.Xml.Serialization;

namespace Aurigo.Atom.UI.DTO
{
    public class ControlConfig
    {
        [XmlAttribute("Type")]
        public string Type { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("Attribute")]
        public List<string> Attributes { get; set; }
    }
}