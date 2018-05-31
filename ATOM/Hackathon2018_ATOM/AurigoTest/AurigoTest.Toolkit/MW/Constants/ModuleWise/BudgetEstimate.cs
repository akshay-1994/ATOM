using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW.Constants
{
    public abstract class CONST_BudgetEstimate 
    {
        public const string TableName = CONST_TableNames.BudgetEstimate;

        public abstract class Form
        {
            public const string BudgetEstimateName = "BudgetEstimateName";
            public const string BudgetEstimateType = "BudgetEstimateType";
            public const string MeasurementSystem = "MeasurementSystem";
            
        }
        
    }
}
