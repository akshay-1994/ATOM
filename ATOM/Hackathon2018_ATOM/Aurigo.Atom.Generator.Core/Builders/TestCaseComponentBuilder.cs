using System.Collections.Generic;
using System.Linq;
using Aurigo.Atom.Common;
using Aurigo.Atom.Common.DTO;
using Aurigo.Atom.Common.Interfaces;
using Aurigo.Atom.Generator.Core.Config;
using Unity;

namespace Aurigo.Atom.Generator.Core.Builders
{
    /// <summary>
    ///
    /// </summary>
    public class TestCaseComponentBuilder
    {
        private List<TestComponent> _testCaseComponents;
        private TestModuleConfig _testModuleConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseComponentBuilder"/> class.
        /// </summary>
        /// <param name="testComponents">The test components.</param>
        public TestCaseComponentBuilder(List<TestComponent> testComponents, TestModuleConfig testModuleConfig)
        {
            _testCaseComponents = testComponents;
            _testModuleConfig = testModuleConfig;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public List<TestCaseComponentGroup> Build()
        {
            var testCaseComponentGroups = new List<TestCaseComponentGroup>();

            foreach (var testComponent in _testCaseComponents)
            {
                var controlName = testComponent.Name;
                var controlType = testComponent.Control.Type;

                if (controlName == _testModuleConfig.AutomationGuidFieldName)
                    continue;

                var testCaseComponentGroup = new TestCaseComponentGroup
                {
                    ControlName = controlName,
                    TestCaseComponents = new List<TestCaseComponent>()
                };

                var controlConfig = Configurator.TestCaseComponentConfig.Controls
                    .FirstOrDefault(c => c.Type == controlType);

                if (controlConfig == null)
                    continue;

                foreach (var attribute in testComponent.Attribues)
                {
                    var attributeConfig = controlConfig.Attributes.Find(a => a.Name == attribute);

                    if (attributeConfig == null ||
                        !Configurator.Container.IsRegistered<ITestCaseComponentGenerator>(attributeConfig.GeneratorName))
                        continue;

                    var eventArgs = new TestCaseComponentGeneratorOptions
                    {
                        Control = testComponent.Control,
                        TestModuleConfig = _testModuleConfig
                    };

                    var testCaseComponentGenerator = Configurator.Container.Resolve<ITestCaseComponentGenerator>(attributeConfig.GeneratorName);
                    var results = testCaseComponentGenerator?.Generate(eventArgs);

                    if (results != null && results.Count > 0)
                        testCaseComponentGroup.TestCaseComponents.AddRange(results);
                }

                if (testComponent.Attribues.Count == 0)
                {
                    if (Configurator.Container.IsRegistered<ITestCaseComponentGenerator>(controlConfig.DefaultValueGeneratorName))
                    {
                        var defaultValueEventArgs = new TestCaseComponentGeneratorOptions
                        {
                            Control = testComponent.Control,
                            TestModuleConfig = _testModuleConfig
                        };

                        var testCaseDefaultValueGenerator = Configurator.Container.Resolve<ITestCaseComponentGenerator>(controlConfig.DefaultValueGeneratorName);
                        var defaultValueResult = testCaseDefaultValueGenerator?.Generate(defaultValueEventArgs);

                        if (defaultValueResult != null && defaultValueResult.Count > 0)
                            testCaseComponentGroup.TestCaseComponents.AddRange(defaultValueResult);
                    }
                }

                if (_testModuleConfig.IncludeSecurityTestCase)
                {
                    if (Configurator.Container.IsRegistered<ITestCaseComponentGenerator>(controlConfig.SecurityTestCaseGeneratorName))
                    {
                        var securityTestEventArgs = new TestCaseComponentGeneratorOptions
                        {
                            Control = testComponent.Control,
                            TestModuleConfig = _testModuleConfig
                        };

                        var securityTestCaseGenerator = Configurator.Container.Resolve<ITestCaseComponentGenerator>(controlConfig.SecurityTestCaseGeneratorName);
                        var securityTestResult = securityTestCaseGenerator?.Generate(securityTestEventArgs);

                        if (securityTestResult != null && securityTestResult.Count > 0)
                            testCaseComponentGroup.TestCaseComponents.AddRange(securityTestResult);
                    }
                }

                if (testCaseComponentGroup.TestCaseComponents.Count > 0)
                    testCaseComponentGroups.Add(testCaseComponentGroup);
            }

            return testCaseComponentGroups;
        }
    }
}