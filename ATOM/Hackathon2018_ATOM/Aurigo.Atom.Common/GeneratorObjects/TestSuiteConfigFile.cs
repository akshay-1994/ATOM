using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Aurigo.Atom.Common.GeneratorObjects
{
    /// <summary>
    ///
    /// </summary>
    [XmlType("TestRunConfig")]
    public class TestSuiteConfigFile
    {
        /// <summary>
        /// Gets or sets the name of the library.
        /// </summary>
        /// <value>
        /// The name of the library.
        /// </value>
        [XmlAttribute("Name")]
        public string LibraryName { get; set; }

        /// <summary>
        /// Gets or sets the name of the library assembly.
        /// </summary>
        /// <value>
        /// The name of the library assembly.
        /// </value>
        [XmlAttribute("AssemblyName")]
        public string LibraryAssemblyName { get; set; }

        /// <summary>
        /// Gets or sets the run configuration.
        /// </summary>
        /// <value>
        /// The run configuration.
        /// </value>
        [XmlElement("Config")]
        public RunConfigObject RunConfig { get; set; }

        /// <summary>
        /// Gets or sets the test definition list.
        /// </summary>
        /// <value>
        /// The test definition list.
        /// </value>
        [XmlArray("Tests")]
        [XmlArrayItem("Test")]
        public List<TestDefinition> TestDefinitionList { get; set; }

        /// <summary>
        /// Gets or sets the test descriptions.
        /// </summary>
        /// <value>
        /// The test descriptions.
        /// </value>
        [XmlArray("TestDescriptions")]
        [XmlArrayItem("TestDescription")]
        public List<TestDescription> TestDescriptions { get; set; }

        //[XmlElement("appSettings")]
        //public AppSettings AppSettings { get; set; }
    }

    //public class AppSettings
    //{
    //    [XmlElement("add")]
    //    public List<AppSettingsKey> Keys { get; set; } = new List<AppSettingsKey>();
    //}

    //public class AppSettingsKey
    //{
    //    [XmlAttribute("key")]
    //    public string Key { get; set; }
    //    [XmlAttribute("value")]
    //    public string Value { get; set; }
    //}

    [XmlType("Config")]
    public class RunConfigObject
    {
        [XmlAttribute("Run")]
        public string Run { get; set; }

        [XmlAttribute("NoRun")]
        public string NoRun { get; set; }

        [XmlAttribute("RunUser")]
        public bool RunUser { get; set; }
    }

    [XmlType("Test")]
    public class TestDefinition
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Method")]
        public string Method { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    [XmlType("TestDescription")]
    public class TestDescription
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [XmlAttribute("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [XmlIgnore]
        public string Description
        {
            get
            {
                return CDataDescription.InnerText;
            }
            set
            {
                var text = string.Format("{0}{1}", value, Environment.NewLine);
                CDataDescription = (new XmlDocument()).CreateCDataSection(text);
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [XmlAnyElement]
        public XmlNode CDataDescription { get; set; }
    }
}