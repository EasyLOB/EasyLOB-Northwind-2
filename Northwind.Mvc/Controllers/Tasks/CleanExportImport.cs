using EasyLOB.Library;
using EasyLOB.Resources;
using System;
using System.Web.Mvc;

namespace EasyLOB.Mvc
{
    public partial class TasksController
    {
        // GET: Tasks/CleanExportImport
        [HttpGet]
        public ActionResult CleanExportImport()
        {
            //if (IsTask("CleanExportImport", OperationResult))
            //{
                TaskModel taskModel = new TaskModel("Tasks", "CleanExportImport", PresentationResources.TaskCleanExportImport);

                return View("TaskAjax", taskModel);
            //}

            //return View("OperationResult", new OperationResultModel(OperationResult));
        }

        // POST: Tasks/CleanExportImport
        [HttpPost]
        public ActionResult CleanExportImport(TaskModel taskModel)
        {
            taskModel.OperationResult.Clear();

            try
            {
                //if (IsTask("CleanExportImport", viewModel.OperationResult))
                //{
                //    if (ValidateModelState())
                //    {
                        // Z-Export

                        if (taskModel.OperationResult.Ok)
                        {
                            LibraryHelper.CleanDirectory(taskModel.OperationResult, Server.MapPath(ConfigurationHelper.AppSettings<string>("Directory.Export")));
                        }

                        // Z-Import

                        if (taskModel.OperationResult.Ok)
                        {
                            LibraryHelper.CleanDirectory(taskModel.OperationResult, Server.MapPath(ConfigurationHelper.AppSettings<string>("Directory.Import")));
                        }

                        taskModel.OperationResult.StatusMessage = String.Format(ErrorResources.Successful, PresentationResources.TaskCleanExportImport);
                //    }
                //}
            }
            catch (Exception exception)
            {
                taskModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(taskModel.OperationResult);
        }
    }
}