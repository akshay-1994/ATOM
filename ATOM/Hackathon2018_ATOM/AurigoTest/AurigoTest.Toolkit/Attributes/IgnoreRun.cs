using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit
{
    /// <summary>
    /// This attribubte will be used to run the methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class IgnoreRun : Attribute
    {

    }
}
