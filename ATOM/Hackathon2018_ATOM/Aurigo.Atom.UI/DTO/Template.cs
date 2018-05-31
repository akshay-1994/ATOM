using System.Collections.Generic;
using System.Xml.Serialization;

namespace Aurigo.Atom.UI.DTO
{
    public class Template
    {
        [XmlAttribute("TemplateId")]
        public int TemplateId { get; set; }

        [XmlAttribute("TemplateName")]
        public string TemplateName { get; set; }

        [XmlElement("ConnectionString")]
        public string ConnectionString { get; set; }

        [XmlElement("MasterworksUrl")]
        public string MasterworksUrl { get; set; }

        [XmlElement("UserName")]
        public string UserName { get; set; }

        [XmlElement("Password")]
        public string Password { get; set; }

        [XmlElement("Module")]
        public List<ModuleConfig> Modules { get; set; }

        [XmlElement("DeploymentTargetPath")]
        public string DeploymentPath { get; set; }

        [XmlElement("SolutionTargetPath")]
        public string SolutionTargetPath { get; set; }

        [XmlElement("AtomReactorExecutableFilePath")]
        public string AtomReactorExecutableFilePath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [compile and generate library].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [compile and generate library]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("CompileAndGenerateLibrary")]
        public bool CompileAndGenerateLibrary { get; set; }

        [XmlElement("IsAutoDeploy")]
        public bool IsAutoDeploy { get; set; }
    }
}