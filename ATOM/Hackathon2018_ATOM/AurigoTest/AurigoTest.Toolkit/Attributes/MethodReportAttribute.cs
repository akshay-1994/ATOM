using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodReportAttribute: Attribute
    {
        public string Description { get; set; }
        public string Id { get; set; }
        
    }
}
