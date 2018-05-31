using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Aurigo.Atom.UI.DTO
{
    [XmlRoot("AtomUITemplates")]
    public class AtomUITemplates
    {
        [XmlElement("Template")]
        public List<Template> Templates { get; set; }
    }
}