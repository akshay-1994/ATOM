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
    public class ProjectContentVerifier : AbstractViewPageVerifier<ProjectContentVerifier, ProjectContent>
    {
        public ProjectContentVerifier(ProjectContent viewRef, string formContext) : base(viewRef, formContext)
        {

        }
    }
}
