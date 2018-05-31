using System.Collections.Generic;
using Aurigo.Brix.Platform.BusinessLayer.XMLForm;

namespace Aurigo.Atom.Common.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface IControlVerifier
    {
        /// <summary>
        /// Verifies the specified control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        IEnumerable<string> Verify(xControl control);
    }
}