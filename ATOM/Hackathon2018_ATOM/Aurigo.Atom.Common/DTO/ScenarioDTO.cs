using System.Collections.Generic;

namespace Aurigo.Atom.Common.DTO
{
    /// <summary>
    ///
    /// </summary>
    public class ScenarioDTO
    {
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance save will succeed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is save will succeed; otherwise, <c>false</c>.
        /// </value>
        public bool IsSaveWillSucceed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether DB verification is required
        /// </summary>
        public bool IsDatabaseVerificationRequired { get; set; } = true;

        /// <summary>
        /// Gets or sets the setters.
        /// </summary>
        /// <value>
        /// The setters.
        /// </value>
        public List<string> Setters { get; set; }

        /// <summary>
        /// Gets or sets the database validators.
        /// </summary>
        /// <value>
        /// The database validators.
        /// </value>
        public List<string> DBValidators { get; set; }

        /// <summary>
        /// Gets or sets the view mode validators.
        /// </summary>
        /// <value>
        /// The view mode validators.
        /// </value>
        public List<string> ViewModeValidators { get; set; }

        /// <summary>
        /// Gets or sets the edit mode vallidators.
        /// </summary>
        /// <value>
        /// The edit mode vallidators.
        /// </value>
        public List<string> EditModeValidators { get; set; }

        /// <summary>
        /// Gets or sets the name of the automation unique identifier field.
        /// </summary>
        /// <value>
        /// The name of the automation unique identifier field.
        /// </value>
        public string AutomationGuidFieldName { get; set; }

        /// <summary>
        /// Gets or sets the scenario description.
        /// </summary>
        /// <value>
        /// The scenario description.
        /// </value>
        public List<string> ScenarioDescription { get; set; }

        /// <summary>
        /// Gets or sets the edit mode vallidators.
        /// </summary>
        /// <value>
        /// The edit mode vallidators.
        /// </value>
        public List<string> OnScreenValidators { get; set; }

        public string ScenarioDescriptionToString { get { return string.Join(System.Environment.NewLine, this.ScenarioDescription); } }
    }
}