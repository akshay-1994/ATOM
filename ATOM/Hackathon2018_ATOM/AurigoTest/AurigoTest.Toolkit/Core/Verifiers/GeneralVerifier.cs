using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.Core.Verifiers
{
    public class GeneralVerifier<T> : VerifierBase<GeneralVerifier<T>, T>
    {
        public GeneralVerifier(T pageRef) : base(pageRef)
        {
        }
    }
}
