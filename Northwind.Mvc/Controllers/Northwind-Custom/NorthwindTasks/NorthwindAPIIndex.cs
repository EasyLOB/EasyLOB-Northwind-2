using System.Web.Mvc;

namespace Northwind.Mvc
{
    public partial class NorthwindTasksController
    {
        #region Methods

        // GET: NorthwindTasks/NorthwindAPIIndex
        [HttpGet]
        public ActionResult NorthwindAPIIndex()
        {
            return View();
        }

        #endregion Methods
    }
}