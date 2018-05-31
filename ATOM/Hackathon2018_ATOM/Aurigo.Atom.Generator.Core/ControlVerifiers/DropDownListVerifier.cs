using System;
using System.Collections.Generic;
using Aurigo.Atom.Common.Attributes;
using Aurigo.Atom.Common.Interfaces;
using Aurigo.Brix.Platform.BusinessLayer.XMLForm;

namespace Aurigo.Atom.Generator.Core.ControlVerifiers
{
    [ControlVerifier(ControlType.DropDownList)]
    public class DropDownListVerifier : IControlVerifier
    {
        public IEnumerable<string> Verify(xControl control)
        {
            var verificationResults = new List<string>();

            if (!string.IsNullOrEmpty(control.DataSource))
            {
                var dataSourceItems = control.DataSource.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (dataSourceItems.Length < 3)
                    verificationResults.Add(string.Format("{0}: DataSource is not formatted correctly. Possible failure. {1}",
                        control.Caption, Environment.NewLine));
            }
            return verificationResults;
        }
    }
}