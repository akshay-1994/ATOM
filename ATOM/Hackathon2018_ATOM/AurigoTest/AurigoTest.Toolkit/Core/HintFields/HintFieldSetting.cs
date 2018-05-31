using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Core
{
    public class HintSetting
    {
        public string TableName { get; private set; }
        public string IdField { get; private set; }

        public string HintField { get; set; }
        public string HintFieldValue { get; set; }

        public EnumsHintFieldDataType HintFieldColumnDataType { get; private set; }

        public EnumHintFieldSearchTechnique HintFieldSearchTechnique { get; private set; }

        public HintSetting(string tableName, string idField, string hintField, string hintFieldValue, EnumsHintFieldDataType hintFieldColumnDataType, EnumHintFieldSearchTechnique hintFieldSearchTechnique)
        {
            TableName = tableName;
            IdField = idField;
            HintField = hintField;
            HintFieldValue = hintFieldValue;
            HintFieldColumnDataType = hintFieldColumnDataType;
            HintFieldSearchTechnique = hintFieldSearchTechnique;
        }
    }
}
