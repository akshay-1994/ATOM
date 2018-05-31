using AurigoTest.Toolkit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW.Customizations
{
    public class ProjectViewPage : AbstractViewPage<ProjectViewPage, ProjectViewPageVerifier, GenericListPage>
    {
        private string URL_Template = "Default.aspx#/Modules/PROJECT/ProjectDetails.aspx?pid={0}&Context=PROJECT&InstanceID=0&Mode=View&nt=1";

        private ElementManipulator _elementHelper = null;
        private ElementManipulator EM
        {
            get
            {
                if (_elementHelper == null)
                    _elementHelper = new ElementManipulator(this.IFrameDriver);

                //base.IFrameDriver_Flush();

                return _elementHelper;
            }
        }

        #region Properties : Span properties
        public string ProjectName { get { return this.SpanGet("txtProjName"); } }
        public string ProjectCode { get { return this.SpanGet("txtProjNumber"); } }
        public string ProjectOwner { get { return this.SpanGet("txtOwner"); } }
        public string ProjectStatus { get { return this.SpanGet("txtStatus"); } }

        public string Calendar { get { return this.SpanGet("lblCalendar"); } }

        public int ContractDays { get { return Convert.ToInt32(this.SpanGet("lblDays") ?? "0"); } }
        public DateTime? StartDate { get { return DateTime.ParseExact(this.SpanGet("txtStartDate"), MWApplicationSettingSingleton.Instance.FORMAT_DATE, null); } }
        public DateTime? EndDate { get { return DateTime.ParseExact(this.SpanGet("txtEndDate"), MWApplicationSettingSingleton.Instance.FORMAT_DATE, null); } }

        public string ProjectCategory { get { return this.SpanGet("lblProjectClass"); } }
        public string BusinessUnit_Text { get { return this.SpanGet("txtBusinessUnit"); } }

        public string ProgramYear { get { return this.SpanGet("lblProgramYear"); } }

        #endregion Properties : Span properties

        public ProjectViewPage(GenericListPage parentObject, string listPageURL) : base(parentObject, listPageURL)
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string SpanGet(string idPrefix) { return EM.GetEle_SpanTag_IdEndsWith(idPrefix)?.Text; }

        public ProjectViewPage CheckSomeThingCustomHere()
        {
            return this;
        }

    }
}
