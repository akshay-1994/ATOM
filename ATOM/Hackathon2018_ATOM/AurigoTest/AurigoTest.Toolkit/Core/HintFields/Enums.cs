using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Core
{

    public enum EnumsHintFieldDataType
    {
        Text,
        Numeric,
        DateTime,
    }

    public enum EnumHintFieldSearchTechnique
    {
        Contains,
        BeginWith,
        EndsWith,
        NotContains,
        ExactMatch,
    }
}
