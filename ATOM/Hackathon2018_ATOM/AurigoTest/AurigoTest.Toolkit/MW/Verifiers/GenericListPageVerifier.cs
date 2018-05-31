using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AurigoTest.Toolkit.Core;
using System.Runtime.CompilerServices;

namespace AurigoTest.Toolkit.MW
{
    public class GenericListPageVerifier : VerifierBase<GenericListPageVerifier, GenericListPage>
    {
        private GenericListPage ListPageRef { get { return base.PageRef; } }

        public GenericListPageVerifier(GenericListPage listPageRef, string formContext) : base(listPageRef)
        {

        }

    }
}
