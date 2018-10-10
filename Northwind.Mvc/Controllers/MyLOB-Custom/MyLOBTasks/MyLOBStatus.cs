using EasyLOB.Library.AspNet;
using EasyLOB.Mvc;
using MyLOB.Mvc.Resources;
using System.Text;
using System.Web.Mvc;
using System.Web.SessionState;

namespace MyLOB.Mvc
{
    public partial class MyLOBTasksController
    {
        #region Methods

        // GET: MyLOBTasks/MyLOBStatus
        [HttpGet]
        public ActionResult MyLOBStatus()
        {
            StringBuilder result = new StringBuilder();

            MyLOBTenant tenant = MyLOBMultiTenantHelper.Tenant;
            result.Append("<br /><b>Multi-Tenant MyLOB</b>");
            result.Append("<br />:: Name: " + tenant.Name);

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
                    case "EasyLOB.MyLOBMultiTenant":
                        //value = JsonConvert.SerializeObject((List<MyLOBTenant>)session[i]);
                        break;
                }

                result.Append("<br />&nbsp;&nbsp;&nbsp;" + session.Keys[i] + ": " + value);
            }

            ViewBag.Status = result.ToString();

            TaskModel viewModel = new TaskModel("MyLOBTasks", "MyLOBStatus", MyLOBPresentationResources.TaskMyLOBStatus);

            return View(viewModel);
        }

        #endregion Methods
    }
}