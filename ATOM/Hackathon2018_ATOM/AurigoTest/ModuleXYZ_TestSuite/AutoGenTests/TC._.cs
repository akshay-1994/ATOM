using AurigoTest.Toolkit;
using AurigoTest.Toolkit.Core;

namespace ModuleXYZ_TestSuite.AutoGenTests
{
    public partial class TC : TestClassBase
    {
        #region Dynamic Global Configuration
        public string ModuleTableName { get { return "{ModuleTableName}"; } }//{ModuleTableName}
        public string ModuleTablePrimaryKeyName { get { return "{ModuleTablePrimaryKeyName}"; } }

        public string AutomationGUID_FieldName { get { return "{AutomationGUID_FieldName}"; } }

        public string IsEnableDatabaseVerification { get { return "{IsEnableDatabaseVerification}"; } }//{ModuleTableName}

        #endregion Dynamic Global Configuration

        #region Fixed Global Configuration

        public HintSetting GetTableRecordHintObject(string hintValue)
        {
            return new HintSetting(this.ModuleTableName, this.ModuleTablePrimaryKeyName, this.AutomationGUID_FieldName, hintValue, EnumsHintFieldDataType.Text, EnumHintFieldSearchTechnique.Contains);
        }

        #endregion Fixed Global Configuration
    }
}
