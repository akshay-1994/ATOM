using System.Collections.Generic;
using Aurigo.Atom.Common.Attributes;
using Aurigo.Atom.Common.DTO;
using Aurigo.Atom.Common.Interfaces;
using Aurigo.Brix.Platform.BusinessLayer.XMLForm;
using Aurigo.Atom.Generator.Core.Helpers;

namespace Aurigo.Atom.Generator.Core.Generators.TextBox
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Aurigo.Atom.Common.Interfaces.ITestCaseComponentGenerator" />
    [TestCaseComponentGenerator(Name = "TextBox_SecurityTest")]
    public class TextBoxSecurityTestGenerator : ITestCaseComponentGenerator
    {
        /// <summary>
        /// Generates the specified arguments.
        /// </summary>
        /// <param name="args">The <see cref="T:Aurigo.Atom.Common.EventArgs.TestCaseComponentGeneratorEventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        public List<TestCaseComponent> Generate(TestCaseComponentGeneratorOptions args)
        {
            var testCaseComponents = new List<TestCaseComponent>();
            var result = GeneratePositiveSecurityTestCase(args.Control);

            if (result != null)
                testCaseComponents.Add(result);

            result = GenerateNegativeSecurityTestCase(args.Control);

            if (result != null)
                testCaseComponents.Add(result);

            return testCaseComponents;
        }

        /// <summary>
        /// Generates the negative security test case.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        private TestCaseComponent GenerateNegativeSecurityTestCase(xControl control)
        {
            if (control.ReadOnly)
            {
                var testValue = RandomValueHelper.GenerateRandomString(control.MaxLength - 1);
                return new TestCaseComponent
                {
                    Name = $"{control.Name}_DataTamper_Security",
                    Type = TestCaseType.NEGATIVE,
                    TestCaseSetter = string.Format("SetTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                    //TestCaseDBValidator = string.Format("Assert_Data(\"{0}\", \"{1}\");", control.Name, testValue),
                    TestCaseEditModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                    //TestCaseViewModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                    OnScreenValidator = "AssertIfToasterExist();",
                    WillSaveSucceed = false,
                    Description = string.Format("TestCase for testing data tampering in form TextBox ({0}). Server side validation implemented!", control.Caption)
                };
            }
            return null;
        }

        /// <summary>
        /// Generates the default value test case.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        private TestCaseComponent GeneratePositiveSecurityTestCase(xControl control)
        {
            return null;
        }
    }
}