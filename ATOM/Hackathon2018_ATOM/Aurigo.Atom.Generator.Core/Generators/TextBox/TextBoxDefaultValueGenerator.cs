using System;
using System.Collections.Generic;
using Aurigo.Atom.Common.Attributes;
using Aurigo.Atom.Common.DTO;
using Aurigo.Atom.Common.Helpers;
using Aurigo.Atom.Common.Interfaces;
using Aurigo.Atom.Generator.Core.Helpers;
using Aurigo.Brix.Platform.BusinessLayer.XMLForm;

namespace Aurigo.Atom.Generator.Core.Generators.TextBox
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Aurigo.Atom.Common.Interfaces.ITestCaseComponentGenerator" />
    [TestCaseComponentGenerator(Name = "TextBox_DefaultValue")]
    public class TextBoxDefaultValueGenerator : ITestCaseComponentGenerator
    {
        /// <summary>
        /// Generates the specified arguments.
        /// </summary>
        /// <param name="args">The <see cref="T:Aurigo.Atom.Common.EventArgs.TestCaseComponentGeneratorEventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        public List<TestCaseComponent> Generate(TestCaseComponentGeneratorOptions args)
        {
            var testCaseComponents = new List<TestCaseComponent>();
            var result = GenerateDefaultValueTestCase(args.Control);

            if (result != null)
                testCaseComponents.Add(result);

            return testCaseComponents;
        }

        /// <summary>
        /// Generates the default value test case.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        private TestCaseComponent GenerateDefaultValueTestCase(xControl control)
        {
            if (!string.IsNullOrEmpty(control.Value) || control.PrimaryKey)
                return null;

            var length = Math.Min(DbTypeParser.GetColumnLength(control.DBType), control.MaxLength);
            var testValue = RandomValueHelper.GenerateRandomString(length);

            return new TestCaseComponent
            {
                Name = $"{control.Name}_Default",
                Type = TestCaseType.DEFAULT,
                TestCaseSetter = string.Format("SetTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                TestCaseDBValidator = string.Format("Assert_Data(\"{0}\", \"{1}\");", control.Name, testValue),
                TestCaseEditModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                TestCaseViewModeValidator = string.Format("AssertTextbox(\"{0}\", \"{1}\");", control.Name, testValue),
                Description = string.Empty,
                WillSaveSucceed = true
            };
        }
    }
}