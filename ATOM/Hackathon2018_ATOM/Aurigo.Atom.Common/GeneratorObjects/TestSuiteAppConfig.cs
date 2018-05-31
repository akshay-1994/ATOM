using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Aurigo.Atom.Common.GeneratorObjects
{
    [XmlRoot("appSettings")]
    public class AppSettings
    {
        [XmlElement("add")]
        public List<AppSettingsKey> Keys { get; set; } = new List<AppSettingsKey>();
    }

    public class AppSettingsKey
    {
        [XmlAttribute("key")]
        public string Key { get; set; }
        [XmlAttribute("value")]
        public string Value { get; set; }
    }

}

