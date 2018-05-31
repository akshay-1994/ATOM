using System;
using System.Collections.Generic;
using Aurigo.Atom.Common.Attributes;
using Aurigo.Atom.Common.Helpers;
using Aurigo.Atom.Common.Interfaces;
using Aurigo.Brix.Platform.BusinessLayer.XMLForm;

namespace Aurigo.Atom.UI.ControlVerifiers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Aurigo.Atom.Common.Interfaces.IControlVerifier" />
    [ControlVerifier(ControlType.TextBox)]
    public class TextBoxVerifier : IControlVerifier
    {
        /// <summary>
        /// Verifies the specified control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public IEnumerable<string> Verify(xControl control)
        {
            var verificationResults = new List<string>();
            var dbColumnLength = DbTypeParser.GetColumnLength(control.DBType);
            var maxLength = control.MaxLength;

            if (dbColumnLength < maxLength)
                verificationResults.Add(string.Format("{0}: Database column length is lesser than the value specified in MaxLength attribute. {1}",
                    control.Caption, Environment.NewLine));

            return verificationResults;
        }
    }
}