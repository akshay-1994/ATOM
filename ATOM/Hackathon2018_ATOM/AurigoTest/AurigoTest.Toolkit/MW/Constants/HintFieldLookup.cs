using AurigoTest.Toolkit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW.Constants
{
    public static partial class HintFieldLookup
    {
        public static HintSetting BudgetEstimate(string hintValue)
        {
            return new HintSetting(CONST_TableNames.BudgetEstimate, "ID", "BudgetEstimateName", hintValue, EnumsHintFieldDataType.Text, EnumHintFieldSearchTechnique.Contains);
        }

        public static HintSetting SomeOther(string hintValue)
        {
            return new HintSetting(CONST_TableNames.BudgetEstimate, "ID", "BudgetEstimateName", hintValue, EnumsHintFieldDataType.Text, EnumHintFieldSearchTechnique.Contains);
        }

        public static HintSetting Project_By_ProjectCode(string hintValue)
        {
            return new HintSetting(CONST_TableNames.ProjectMain, "ProjectId", "ProjectCode", hintValue, EnumsHintFieldDataType.Text, EnumHintFieldSearchTechnique.ExactMatch);
        }
    }
}
