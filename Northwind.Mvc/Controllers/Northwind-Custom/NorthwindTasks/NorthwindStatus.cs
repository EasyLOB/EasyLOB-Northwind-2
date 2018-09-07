using EasyLOB.Library.AspNet;
using EasyLOB.Mvc;
using Northwind.Mvc.Resources;
using System.Text;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Northwind.Mvc
{
    public partial class NorthwindTasksController
    {
        #region Methods

        // GET: NorthwindTasks/NorthwindStatus
        [HttpGet]
        public ActionResult NorthwindStatus()
        {
            StringBuilder result = new StringBuilder();

            NorthwindTenant tenant = NorthwindMultiTenantHelper.Tenant;
            result.Append("<br /><b>Multi-Tenant Northwind</b>");
            result.Append("<br />:: URL: " + tenant.URL);

            HttpSessionState session = SessionHelper.Session;
            result.Append("<br />");
            result.Append("<br /><b>Session</b>");
            result.Append("<br />:: SessionID: " + Session.SessionID);
            result.Append("<br />:: SessionHelper.Session.SessionID: " + session.SessionID);
            result.Append("<br />:: Key(s)");
            for (int i = 0; i < session.Contents.Count; i++)
            {
                string value = session[i].ToString();
                switch (session.Keys[i])
                {
                    case "EasyLOB.NorthwindMultiTenant":
                        //value = JsonConvert.SerializeObject((List<NorthwindTenant>)session[i]);
                        break;
                }

                result.Append("<br />&nbsp;&nbsp;&nbsp;" + session.Keys[i] + ": " + value);
            }

            ViewBag.Status = result.ToString();

            TaskViewModel viewModel = new TaskViewModel("NorthwindTasks", "NorthwindStatus", NorthwindPresentationResources.TaskNorthwindStatus);

            return View(viewModel);
        }

        #endregion Methods
    }
}