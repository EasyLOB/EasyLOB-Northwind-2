using EasyLOB.Resources;
using System.Web.Mvc;

namespace EasyLOB.Mvc
{
    public partial class TasksController
    {
        // GET: Tasks/CleanLocalStorage
        [HttpGet]
        public ActionResult CleanLocalStorage()
        {
            //if (IsTask("CleanLocalStorage", OperationResult))
            //{
                TaskModel taskModel = new TaskModel("Tasks", "CleanLocalStorage", PresentationResources.TaskCleanLocalStorage);

                return View(taskModel);
            //}

            //return View("OperationResult", new OperationResultViewModel(OperationResult));
        }
    }
}