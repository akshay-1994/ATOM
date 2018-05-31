using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit
{
    /// <summary>
    /// Apply this attribute to run only during debug time
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RunDebugAttribute : Attribute
    {

    }
}
