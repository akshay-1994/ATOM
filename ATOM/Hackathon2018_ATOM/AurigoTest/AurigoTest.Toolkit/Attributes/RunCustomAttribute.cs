using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit
{
    /// <summary>
    /// Apply this attribute to run base on your preference
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RunCustomAttribute : Attribute
    {

    }
}
