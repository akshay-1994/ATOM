using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurigo.Atom.UI.DTO
{
    internal class AppConfig
    {
        /// <summary>
        /// Gets the test case component configuration.
        /// </summary>
        /// <value>
        /// The test case component configuration.
        /// </value>
        public static string TestCaseComponentConfiguration { get; private set; }

        /// <summary>
        /// Gets the template path.
        /// </summary>
        /// <value>
        /// The template path.
        /// </value>
        public static string TemplatePath { get; private set; }

        /// <summary>
        /// Gets the atom UI template.
        /// </summary>
        /// <value>
        /// The atom UI template.
        /// </value>
        public static string AtomUITemplate { get; private set; }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        public static void Build()
        {
            TestCaseComponentConfiguration = ConfigurationManager.AppSettings["TestCaseComponentConfiguration"];
            AtomUITemplate = ConfigurationManager.AppSettings["AtomUITemplate"];
            TemplatePath = ConfigurationManager.AppSettings["TemplatePath"];
        }
    }
}