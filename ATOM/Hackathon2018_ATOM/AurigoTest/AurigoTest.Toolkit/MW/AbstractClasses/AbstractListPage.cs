using AurigoTest.Toolkit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AurigoTest.Toolkit.MW
{
    public abstract class AbstractListPage<LSelf, TVerifier, TForm> : AutomationBase<LSelf, TVerifier>, IListPage
        where LSelf : AutomationBase<LSelf, TVerifier>
        where TVerifier : VerifierBase<TVerifier, LSelf>
        where TForm : IDriverLinker
    {

        protected string ListPageUrl { get; set; }

        protected RibbonBarAccessor _ribbonBar = null;

        public RibbonBarAccessor RibbonBar
        {
            get
            {
                if (_ribbonBar == null)
                {
                    _ribbonBar = new RibbonBarAccessor(this);
                }

                return _ribbonBar;
            }
        }

        public AbstractListPage(IDriverLinker driverLinker, string listPageURL) : base(driverLinker)
        {
            ListPageUrl = listPageURL;
        }

        public abstract TForm OpenCreateRecordForm(string optionalNewButtonId = null);

        /// <summary>
        /// Use this when there is custom aspx used for viewer
        /// </summary>
        /// <typeparam name="TViewerPage"></typeparam>
        /// <param name="isTelerikGrid"></param>
        /// <returns></returns>
        public virtual TViewerPage View_FirstRow_CustomViewer<TViewerPage>(bool isTelerikGrid = true)
            where TViewerPage : IViewPage<TViewerPage>
        {
            TViewerPage customViewer = (TViewerPage)Activator.CreateInstance(typeof(TViewerPage), this, this.ListPageUrl);

            return customViewer;
        }
    }
}
