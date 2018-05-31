

using AurigoTest.Toolkit;
using AurigoTest.Toolkit.Core;

namespace CONTFOR_TestSuite.AutoGenTests
{
    public partial class TC : TestClassBase
    {
        #region Dynamic Global Configuration

        public string ModuleTableName { get { return "CONTFORMContractorInformation"; } }

        public string ModuleTablePrimaryKeyName { get { return "ID"; } }

        public string AutomationGUID_FieldName { get { return  "AutomationGuid";  } }

        public string IsEnableDatabaseVerification { get { return "true"; } }

        #endregion Dynamic Global Configuration

        #region Fixed Global Configuration

        public HintSetting GetTableRecordHintObject(string hintValue)
        {
            return new HintSetting(this.ModuleTableName, this.ModuleTablePrimaryKeyName, this.AutomationGUID_FieldName, hintValue, EnumsHintFieldDataType.Text, EnumHintFieldSearchTechnique.Contains);
        }

        #endregion Fixed Global Configuration
    }
}