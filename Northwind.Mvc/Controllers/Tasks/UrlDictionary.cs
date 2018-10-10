using EasyLOB.Resources;
using System.Web.Mvc;

namespace EasyLOB.Mvc
{
    public partial class TasksController
    {
        #region Methods

        // GET: Tasks/UrlDictionary
        [HttpGet]
        public ActionResult UrlDictionary()
        {
            TaskModel taskModel = new TaskModel("Tasks", "UrlDictionary", PresentationResources.TaskUrlDictionary);

            return View(taskModel);
        }

        #endregion Methods
    }
}