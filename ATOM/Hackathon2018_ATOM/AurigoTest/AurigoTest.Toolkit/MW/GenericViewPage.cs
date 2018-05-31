using AurigoTest.Toolkit.Common;
using AurigoTest.Toolkit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW
{
    public class GenericViewPage : AbstractViewPage<GenericViewPage, GenericViewPageVerifier, GenericListPage>
    {
        
        public GenericViewPage(GenericListPage listPageReference, string listPageURL) : base(listPageReference, listPageURL)
        {
            
        }


    }
}
