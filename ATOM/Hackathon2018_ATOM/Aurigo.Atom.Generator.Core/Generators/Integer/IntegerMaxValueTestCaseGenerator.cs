using System;
using System.Collections.Generic;
using Aurigo.Atom.Common.Attributes;
using Aurigo.Atom.Common.DTO;
using Aurigo.Atom.Common.Helpers;
using Aurigo.Atom.Common.Interfaces;
using Aurigo.Brix.Platform.BusinessLayer.XMLForm;

namespace Aurigo.Atom.Generator.Core.Generators.Integer
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Aurigo.Atom.Common.Interfaces.ITestCaseComponentGenerator" />
    [TestCaseComponentGenerator(Name = "Integer_MaxValue")]
    public class IntegerMaxValueTestCaseGenerator : ITestCaseComponentGenerator
    {
        /// <summary>
        /// Generates the specified arguments.
        /// </summary>
        /// <param name="args">The <see cref="T:Aurigo.Atom.Common.EventArgs.TestCaseComponentGeneratorEventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<TestCaseComponent> Generate(TestCaseComponentGeneratorOptions args)
        {
            var testCaseComponents = new List<TestCaseComponent>();

            if (!string.IsNullOrEmpty(args.Control.MaxValue))
            {
                if (args.TestModuleConfig.IncludeNegativeTestCase)
                    testCaseComponents.Add(GenerateGreaterThanMaxValueTestCase(args.Control));
                testCaseComponents.Add(GenerateEqualToMaxValueTestCase(args.Control));
                testCaseComponents.Add(GenerateLessThanMaxValueTestCase(args.Control));
            }

            return testCaseComponents;
        }

        /// <summary>
        /// Generates the less than maximum value test case.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        private TestCaseComponent GenerateLessThanMaxValueTestCase(xControl control)
        {
            int maxValue;
            int testValue = int.MaxValue;

            if (int.TryParse(control.MaxValue, out maxValue))
                testValue = maxValue - 1;

            return new TestCaseComponent
            {
                Name = $"{control.Name}_MaxValue_Positive",
                Type = TestCaseType.POSITIVE,
                TestCaseSetter = string.Format("SetTextbox(\"{0}\", {1});", control.Name, testValue),
                TestCaseDBValidator = string.Format("Assert_Data(\"{0}\", {1});", control.Name, testValue),
                TestCaseEditModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                TestCaseViewModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                Description = string.Format("TestCase for MaxValue of Integer control ({0}). Value, is less than the MaxValue specified.", control.Caption),
                WillSaveSucceed = true
            };
        }

        /// <summary>
        /// Generates the equal to maximum value test case.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        private TestCaseComponent GenerateEqualToMaxValueTestCase(xControl control)
        {
            int maxValue;
            int testValue = int.MaxValue;

            if (int.TryParse(control.MaxValue, out maxValue))
                testValue = maxValue;

            return new TestCaseComponent
            {
                Name = $"{control.Name}_MaxValue_Positive",
                Type = TestCaseType.POSITIVE,
                TestCaseSetter = string.Format("SetTextbox(\"{0}\", {1});", control.Name, testValue),
                TestCaseDBValidator = string.Format("Assert_Data(\"{0}\", {1});", control.Name, testValue),
                TestCaseEditModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                TestCaseViewModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                Description = string.Format("TestCase for MaxValue of Integer control ({0}). Value is equal to the MaxValue specified.", control.Caption),
                WillSaveSucceed = true
            };
        }

        /// <summary>
        /// Generates the greater than maximum value test case.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        private TestCaseComponent GenerateGreaterThanMaxValueTestCase(xControl control)
        {
            int maxValue;
            int testValue = int.MaxValue;

            if (int.TryParse(control.MaxValue, out maxValue))
                testValue = maxValue + 1;

            return new TestCaseComponent
            {
                Name = $"{control.Name}_MaxValue_Negative",
                Type = TestCaseType.NEGATIVE,
                TestCaseSetter = string.Format("SetTextbox(\"{0}\", {1});", control.Name, testValue),
                OnScreenValidator = string.Format("AssertControlSpanErrorMessage(\"{0}\");", control.Name),
                Description = string.Format("TestCase for MaxValue of Integer control ({0}). Value is greater than the MaxValue specified.", control.Caption),
                WillSaveSucceed = false
            };
        }
    }
}