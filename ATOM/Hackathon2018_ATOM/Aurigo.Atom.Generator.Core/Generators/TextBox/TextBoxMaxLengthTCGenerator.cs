using System.Collections.Generic;
using Aurigo.Atom.Common.Attributes;
using Aurigo.Atom.Common.DTO;
using Aurigo.Atom.Common.Interfaces;
using Aurigo.Atom.Generator.Core.Helpers;
using Aurigo.Brix.Platform.BusinessLayer.XMLForm;

namespace Aurigo.Atom.Generator.Core.Generators.TextBox
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Aurigo.Atom.Common.Interfaces.ITestCaseComponentGenerator" />
    [TestCaseComponentGenerator(Name = "TextBox_MaxLength")]
    public class TextBoxMaxLengthTCGenerator : ITestCaseComponentGenerator
    {
        /// <summary>
        /// Generates the specified arguments.
        /// </summary>
        /// <param name="args">The <see cref="T:Aurigo.Atom.Common.EventArgs.TestCaseGeneratorEventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<TestCaseComponent> Generate(TestCaseComponentGeneratorOptions args)
        {
            var testCaseComponents = new List<TestCaseComponent>();

            if (args.Control.MaxLength > 0)
            {
                if (args.TestModuleConfig.IncludeNegativeTestCase)
                    testCaseComponents.Add(GenerateGreaterThanMaxLengthTestCase(args.Control));
                testCaseComponents.Add(GenerateEqualToMaxLengthTestCase(args.Control));
                testCaseComponents.Add(GenerateLessThanMaxLengthTestCase(args.Control));
            }

            return testCaseComponents;
        }

        /// <summary>
        /// Generates the greater than maximum length test case.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        private TestCaseComponent GenerateGreaterThanMaxLengthTestCase(xControl control)
        {
            var testValue = RandomValueHelper.GenerateRandomString(control.MaxLength + 1);
            
            return new TestCaseComponent
            {
                Name = $"{control.Name}_MaxLength_Negative",
                Type = TestCaseType.NEGATIVE,
                TestCaseSetter = string.Format("SetTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                //TestCaseDBValidator = string.Format("Assert_Data(\"{0}\", \"{1}\");", control.Name, testValue.Substring(0, testValue.Length - 1)),
                //TestCaseEditModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue.Substring(0, testValue.Length - 1)),
                //TestCaseViewModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue.Substring(0, testValue.Length - 1)),
                OnScreenValidator = "AssertIfToasterExist();",
                WillSaveSucceed = false,
                Description = string.Format("TestCase for testing MaxLength of TextBox({0}). Value is greater than MaxLength.", control.Caption),
            };
        }

        /// <summary>
        /// Generates the equal to maximum length test case.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        private TestCaseComponent GenerateEqualToMaxLengthTestCase(xControl control)
        {
            var testValue = RandomValueHelper.GenerateRandomString(control.MaxLength);
            return new TestCaseComponent
            {
                Name = $"{control.Name}_MaxLength_Positive",
                Type = TestCaseType.POSITIVE,
                TestCaseSetter = string.Format("SetTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                TestCaseDBValidator = string.Format("Assert_Data(\"{0}\", \"{1}\");", control.Name, testValue),
                TestCaseEditModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                TestCaseViewModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                WillSaveSucceed = true,
                Description = string.Format("TestCase for testing MaxLength of TextBox ({0}). Value is equal to MaxLength.", control.Caption)
            };
        }

        /// <summary>
        /// Generates the less than maximum length test case.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        private TestCaseComponent GenerateLessThanMaxLengthTestCase(xControl control)
        {
            var testValue = RandomValueHelper.GenerateRandomString(control.MaxLength - 1);
            return new TestCaseComponent
            {
                Name = $"{control.Name}_MaxLength_Negative",
                Type = TestCaseType.POSITIVE,
                TestCaseSetter = string.Format("SetTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                TestCaseDBValidator = string.Format("Assert_Data(\"{0}\", \"{1}\");", control.Name, testValue),
                TestCaseEditModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                TestCaseViewModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                WillSaveSucceed = true,
                Description = string.Format("TestCase for testing MaxLength of TextBox ({0}). Value is less than MaxLength.", control.Caption)
            };
        }
    }
}