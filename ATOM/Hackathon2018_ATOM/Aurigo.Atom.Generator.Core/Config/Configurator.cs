using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Aurigo.Atom.Common.Attributes;
using Aurigo.Atom.Common.Interfaces;
using Aurigo.Atom.Generator.Core.DTO;
using Unity;
using Unity.RegistrationByConvention;

namespace Aurigo.Atom.Generator.Core.Config
{
    public class Configurator
    {
        private static TestCaseComponentConfiguration _testCaseComponentConfiguration;

        static Configurator()
        {
            Build();
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public static UnityContainer Container { get; private set; }

        /// <summary>
        /// Gets the test case component configuration.
        /// </summary>
        /// <value>
        /// The test case component configuration.
        /// </value>
        public static TestCaseComponentConfiguration TestCaseComponentConfig
        {
            get
            {
                return _testCaseComponentConfiguration;
            }
        }

        /// <summary>
        /// Adds the test case component configuration.
        /// </summary>
        /// <param name="configXml">The configuration XML.</param>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        public static void AddTestCaseComponentConfig(string configXml)
        {
            if (!File.Exists(configXml))
                throw new FileNotFoundException();

            using (var fileStream = new FileStream(configXml, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TestCaseComponentConfiguration));
                _testCaseComponentConfiguration = serializer.Deserialize(fileStream) as TestCaseComponentConfiguration;
            }
        }

        public static void Build()
        {
            Container = new UnityContainer();

            Container.RegisterTypes(
                AllClasses.FromAssemblies(typeof(Configurator).Assembly),
                WithMappings.FromAllInterfaces,
                tn =>
                {
                    if (tn.IsDefined(typeof(TestCaseComponentGeneratorAttribute), true))
                    {
                        var customAttributes = tn.GetCustomAttributes(typeof(TestCaseComponentGeneratorAttribute), true);

                        if (customAttributes != null && customAttributes.Length > 0)
                        {
                            var customAttribute = customAttributes[0] as TestCaseComponentGeneratorAttribute;
                            return customAttribute.Name;
                        }
                    }
                    else if (tn.IsDefined(typeof(ControlVerifierAttribute), true))
                    {
                        var customAttributes = tn.GetCustomAttributes(typeof(ControlVerifierAttribute), true);

                        if (customAttributes != null && customAttributes.Length > 0)
                        {
                            var customAttribute = customAttributes[0] as ControlVerifierAttribute;
                            return customAttribute.Name.ToString();
                        }
                    }
                    return "NoName";
                },
                WithLifetime.Transient);
        }
    }
}