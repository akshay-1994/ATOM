using System.Collections.Generic;
using System.Xml.Serialization;

namespace Aurigo.Atom.UI.DTO
{
    /// <summary>
    ///
    /// </summary>
    public class ModuleConfig
    {
        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        [XmlAttribute("Id")]
        public string ModuleId { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        [XmlAttribute("Name")]
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include negative test case].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include negative test case]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("IncludeNegativeTestCase")]
        public bool IncludeNegativeTestCase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include security test case].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include security test case]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("IncludeSecurityTestCase")]
        public bool IncludeSecurityTestCase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include edit mode validation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include edit mode validation]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("IncludeEditModeValidation")]
        public bool IncludeEditModeValidation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include view mode validation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include view mode validation]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("IncludeViewModeValidation")]
        public bool IncludeViewModeValidation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include database validation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include database validation]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("IncludeDBValidation")]
        public bool IncludeDBValidation { get; set; }

        /// <summary>
        /// Gets or sets the controls.
        /// </summary>
        /// <value>
        /// The controls.
        /// </value>
        [XmlElement("Control")]
        public List<ControlConfig> Controls { get; set; }

        /// <summary>
        /// Gets or sets the name of the automation unique identifier field.
        /// </summary>
        /// <value>
        /// The name of the automation unique identifier field.
        /// </value>
        [XmlElement("AutomationGUIDFieldName")]
        public string AutomationGuidFieldName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [generate distinct dl ls].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [generate distinct dl ls]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("GenerateDistinctDLLs")]
        public bool GenerateDistinctDLLs { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [generate distinct suite XML configuration].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [generate distinct suite XML configuration]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("GenerateDistinctSuiteXMLConfig")]
        public bool GenerateDistinctSuiteXMLConfig { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic generate solution name].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic generate solution name]; otherwise, <c>false</c>.
        /// </value>
        [XmlElement("AutoGenerateSolutionName")]
        public bool AutoGenerateSolutionName { get; set; }

       
        /// <summary>
        /// Gets or sets the name of the solution.
        /// </summary>
        /// <value>
        /// The name of the solution.
        /// </value>
        [XmlElement("SolutionName")]
        public string SolutionName { get; set; }

        ///// <summary>
        ///// Gets or sets the solution target path.
        ///// </summary>
        ///// <value>
        ///// The solution target path.
        ///// </value>
        //[XmlElement("SolutionTargetPath")]
        //public string SolutionTargetPath { get; set; }
    }
}