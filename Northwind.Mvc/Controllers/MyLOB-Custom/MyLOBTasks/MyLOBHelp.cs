using System.Web.Mvc;

namespace MyLOB.Mvc
{
    public partial class MyLOBTasksController
    {
        #region Methods

        // GET: MyLOBTasks/MyLOBHelp
        [HttpGet]
        public ActionResult MyLOBHelp()
        {
            return View();
        }

        #endregion Methods
    }
}