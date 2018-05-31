using System;
using System.Collections.Generic;
using Aurigo.Atom.Common.Attributes;
using Aurigo.Atom.Common.DTO;
using Aurigo.Atom.Common.Interfaces;
using Aurigo.Brix.Platform.BusinessLayer.XMLForm;

namespace Aurigo.Atom.Generator.Core.Generators.Integer
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Aurigo.Atom.Common.Interfaces.ITestCaseComponentGenerator" />
    [TestCaseComponentGenerator(Name = "Integer_MinValue")]
    public class IntegerMinValueTestCaseGenerator : ITestCaseComponentGenerator
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

            if (!string.IsNullOrEmpty(args.Control.MinValue))
            {
                if (args.TestModuleConfig.IncludeNegativeTestCase)
                    testCaseComponents.Add(GenerateGreaterThanMinValueTestCase(args.Control));
                testCaseComponents.Add(GenerateEqualToMinValueTestCase(args.Control));
                testCaseComponents.Add(GenerateLessThanMinValueTestCase(args.Control));
            }

            return testCaseComponents;
        }

        /// <summary>
        /// Generates the less than minimum value test case.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        private TestCaseComponent GenerateLessThanMinValueTestCase(xControl control)
        {
            int minValue;
            int testValue = int.MinValue;

            if (int.TryParse(control.MinValue, out minValue))
                testValue = minValue - 1;

            return new TestCaseComponent
            {
                Name = $"{control.Name}_MinValue_Negative",
                Type = TestCaseType.NEGATIVE,
                TestCaseSetter = string.Format("SetTextbox(\"{0}\", {1});", control.Name, testValue),
                OnScreenValidator = string.Format("AssertControlSpanErrorMessage(\"{0}\");", control.Name),
                Description = string.Format("TestCase for MinValue of Integer control ({0}). Value is less than the MinValue specified.", control.Caption),
                WillSaveSucceed = false
            };
        }

        /// <summary>
        /// Generates the equal to minimum value test case.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        private TestCaseComponent GenerateEqualToMinValueTestCase(xControl control)
        {
            int minValue;
            int testValue = int.MinValue;

            if (int.TryParse(control.MinValue, out minValue))
                testValue = minValue;

            return new TestCaseComponent
            {
                Name = $"{control.Name}_MinValue_Positive",
                Type = TestCaseType.POSITIVE,
                TestCaseSetter = string.Format("SetTextbox(\"{0}\", {1});", control.Name, testValue),
                TestCaseDBValidator = string.Format("Assert_Data(\"{0}\", {1});", control.Name, testValue),
                TestCaseEditModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                TestCaseViewModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                Description = string.Format("TestCase for MinValue of Integer control ({0}). Value is equal to the MinValue specified.", control.Caption),
                WillSaveSucceed = true
            };
        }

        /// <summary>
        /// Generates the greater than minimum value test case.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        private TestCaseComponent GenerateGreaterThanMinValueTestCase(xControl control)
        {
            int minValue;
            int testValue = int.MinValue;

            if (int.TryParse(control.MinValue, out minValue))
                testValue = minValue + 1;

            return new TestCaseComponent
            {
                Name = $"{control.Name}_MinValue_Positive",
                Type = TestCaseType.POSITIVE,
                TestCaseSetter = string.Format("SetTextbox(\"{0}\", {1});", control.Name, testValue),
                TestCaseDBValidator = string.Format("Assert_Data(\"{0}\", {1});", control.Name, testValue),
                TestCaseEditModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                TestCaseViewModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                Description = string.Format("TestCase for MinValue of Integer control ({0}). Value is greater than the MinValue specified.", control.Caption),
                WillSaveSucceed = true
            };
        }
    }
}