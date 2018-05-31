using System;

namespace DemoInConsole
{
    public class TestScenarioConfig
    {
        /// <summary>
        /// Indicates if save will succeed or will stop for validation
        /// </summary>
        public bool IsSaveWillSucceed { get; set; }

        /// <summary>
        /// Optional: will help in distributed running of test and verification by mutual exclusion
        /// </summary>
        public string AutomationGUID_FieldName { get; set; } = null;

        public bool IsAutomationGUID_Field_Defined { get { return !string.IsNullOrEmpty(this.AutomationGUID_FieldName); } }

        /// <summary>
        /// This will be auto set 
        /// </summary>
        public string AutomationGUID_FieldValue { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Indicates if save will succeed and DB verification will get done when save succeeds
        /// </summary>
        public bool IsVerificationRequired_InDatabase { get; set; }

        /// <summary>
        /// indicates if verification required in View Mode
        /// </summary>
        public bool IsVerificationRequired_InViewMode { get; set; }

        /// <summary>
        /// indicates if verification required in Edit Mode
        /// </summary>
        public bool IsVerificationRequired_InEditMode { get; set; }

        public string VerificationDescriptionText { get; set; }
    }
}
