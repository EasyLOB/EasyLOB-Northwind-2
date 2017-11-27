using EasyLOB.Library.Mvc;
using EasyLOB.Library.Syncfusion;
using EasyLOB.Security;
using System.Net;
using System.Web.Mvc;

// JsonNETResult
// https://github.com/kemmis/Newtonsoft.JsonResult

namespace EasyLOB.Mvc
{
    [Authorize]
    [EasyLOBProfile]
    public class BaseMvc : Controller
    {
        #region Methods

        public BaseMvc()
        {
            IAuthenticationManager AuthenticationManager = DependencyResolver.Current.GetService<IAuthenticationManager>();
            ViewBag.Menu = MenuHelper.Menu(AuthenticationManager); // !?!
        }

        protected virtual bool IsValid(ZOperationResult operationResult, string entity)
        {
            ModelState.AddOperationResults(operationResult, entity);

            return ModelState.IsValid;
        }

        #endregion Methods

        #region Methods Controller

        //protected override JsonResult Json(object data)
        //{
        //    return new JsonNETResult
        //    {
        //        Data = data
        //    };
        //}

        //protected override JsonResult Json(object data, JsonRequestBehavior behavior)
        //{
        //    return new JsonNETResult
        //    {
        //        Data = data,
        //        JsonRequestBehavior = behavior
        //    };
        //}

        //protected override JsonResult Json(object data, string contentType)
        //{
        //    return new JsonNETResult
        //    {
        //        Data = data,
        //        ContentType = contentType
        //    };
        //}

        //protected override JsonResult Json(object data, string contentType, JsonRequestBehavior behavior)
        //{
        //    return new JsonNETResult
        //    {
        //        Data = data,
        //        ContentType = contentType,
        //        JsonRequestBehavior = behavior
        //    };
        //}

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding)
        {
            return new JsonNETResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNETResult
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
            return JsonResultSuccess(new {
                OperationResult = operationResult
            });
        }

        protected JsonResult JsonResultFailure(ZOperationResult operationResult, string url)
        {
            return JsonResultFailure(new
            {
                OperationResult = operationResult,
                Url = url
            });
        }

        protected JsonResult JsonResultFailure(object data = null)
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            if (data != null)
            {
                //return new JsonResult()
                return new JsonNETResult()
                {
                    Data = data,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                //return new JsonResult()
                return new JsonNETResult()
                {
                    Data = false, // ???
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        // Success

        protected JsonResult JsonResultSuccess(ZOperationResult operationResult)
        {
            return JsonResultSuccess(new {
                OperationResult = operationResult,
                Controller = ControllerContext.RouteData.Values["controller"].ToString() }
            );
            //return JsonResultSuccess(new {
            //    OperationResult = operationResult,
            //    Url = ReadUrlDictionary()
            //});
        }

        protected JsonResult JsonResultSuccess(ZOperationResult operationResult, string url)
        {
            return JsonResultSuccess(new {
                OperationResult = operationResult,
                Url = url
            });
        }

        protected JsonResult JsonResultSuccess(object data = null)
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            if (data != null)
            {
                //return new JsonResult()
                return new JsonNETResult()
                {
                    Data = data,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                //return new JsonResult()
                return new JsonNETResult()
                {
                    Data = true, // ???
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
    }
}