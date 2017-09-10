using System.Web.Mvc;

namespace Northwind.Mvc
{
    public partial class NorthwindTasksController
    {
        #region Methods

        // GET: NorthwindTasks/NorthwindHelp
        [HttpGet]
        public ActionResult NorthwindHelp()
        {
            return View();
        }

        #endregion Methods
    }
}