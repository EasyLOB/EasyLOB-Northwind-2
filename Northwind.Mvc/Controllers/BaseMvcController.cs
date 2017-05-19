using EasyLOB.Library;
using EasyLOB.Library.Syncfusion;
using EasyLOB.Security;
using Syncfusion.EJ.Export;
using Syncfusion.JavaScript.Models;
using Syncfusion.XlsIO;
using System.Collections;
using System.IO;
using System.Net;
using System.Web.Mvc;

// Newtonsoft.JsonResult
// https://github.com/kemmis/Newtonsoft.JsonResult

namespace EasyLOB.Mvc
{
    public class BaseMvcController : BaseMvc
    {
        #region Properties

        protected virtual string Domain
        {
            get { return ""; }
        }

        protected IAuthorizationManager AuthorizationManager { get; }

        #endregion Properties

        #region Methods

        public BaseMvcController()
        {
            AuthorizationManager = DependencyResolver.Current.GetService<IAuthorizationManager>();
        }

        [HttpGet]
        public virtual ActionResult Download(string filePath)
        {
            string extension = System.IO.Path.GetExtension(filePath);
            ZFileTypes fileType = LibraryHelper.GetFileType(extension);

            return File(filePath, LibraryHelper.GetContentType(fileType), Path.GetFileName(filePath));
        }

        #endregion Methods

        #region Methods Controller

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new Newtonsoft.JsonResult.JsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        #endregion Methods Controller

        #region Methods JsonResult

        // Failure

        protected JsonResult JsonResultFailure(ZOperationResult operationResult)
        {
            return JsonResultSuccess(new { OperationResult = operationResult });
        }

        protected JsonResult JsonResultFailure(object data = null)
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            if (data != null)
            {
                return new JsonResult()
                {
                    Data = data,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        // Success

        protected JsonResult JsonResultSuccess(ZOperationResult operationResult)
        {
            return JsonResultSuccess(new { OperationResult = operationResult, Controller = ControllerContext.RouteData.Values["controller"].ToString() });
            //return JsonResultSuccess(new { OperationResult = operationResult, Url = ReadUrlDictionary() });
        }

        protected JsonResult JsonResultSuccess(ZOperationResult operationResult, string url)
        {
            return JsonResultSuccess(new { OperationResult = operationResult, Url = url });
        }

        protected JsonResult JsonResultSuccess(object data = null)
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            if (data != null)
            {
                return new JsonResult()
                {
                    Data = data,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        // ZOperationResult

        protected JsonResult JsonResultOperationResult(ZOperationResult operationResult)
        {
            if (operationResult.Ok)
            {
                return JsonResultSuccess(new { OperationResult = operationResult });
            }
            else
            {
                return JsonResultFailure(new { OperationResult = operationResult });
            }
        }

        #endregion Methods JsonResult

        #region Methods Syncfusion

        protected void ExportToExcel(string gridModel, IEnumerable data, string theme, string fileName)
        {
            GridProperties gridProperties = SyncfusionGrid.ModelToObject(gridModel);

            ExcelExport export = new ExcelExport();
            IWorkbook excel = export.Export(gridProperties, data, fileName, ExcelVersion.Excel2013, false, false, theme, true);
            excel.ActiveSheet.DeleteRow(1, 1);
            excel.SaveAs(fileName, ExcelSaveType.SaveAsXLS, System.Web.HttpContext.Current.Response, ExcelDownloadType.Open);
            //excel.SaveAs(fileName, ExcelSaveType.SaveAsXLS, Controller.Response, ExcelDownloadType.Open);
        }

        #endregion Methods Syncfusion
    }
}