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
    public class HomePageVerifier : AbstractViewPageVerifier<HomePageVerifier, HomePage>
    {
        public HomePageVerifier(HomePage viewRef, string formContext) : base(viewRef, formContext)
        {

        }
    }
}
