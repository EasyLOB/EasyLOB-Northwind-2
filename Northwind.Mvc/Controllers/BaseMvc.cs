using EasyLOB.Library.AspNet;
using EasyLOB.Security;
using System.Net;
using System.Web.Mvc;

namespace EasyLOB.Mvc
{
    [Authorize]
    [EasyLOBProfile]
    [NorthwindProfile]
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
        //    return new ZJsonResultMvc
        //    {
        //        Data = data
        //    };
        //}

        //protected override JsonResult Json(object data, JsonRequestBehavior behavior)
        //{
        //    return new ZJsonResultMvc
        //    {
        //        Data = data,
        //        JsonRequestBehavior = behavior
        //    };
        //}

        //protected override JsonResult Json(object data, string contentType)
        //{
        //    return new ZJsonResultMvc
        //    {
        //        Data = data,
        //        ContentType = contentType
        //    };
        //}

        //protected override JsonResult Json(object data, string contentType, JsonRequestBehavior behavior)
        //{
        //    return new ZJsonResultMvc
        //    {
        //        Data = data,
        //        ContentType = contentType,
        //        JsonRequestBehavior = behavior
        //    };
        //}

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding)
        {
            return new ZJsonResultMvc
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new ZJsonResultMvc
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        #endregion Methods Controller

        #region Methods ZJsonResultMvc

        // Failure

        protected JsonResult JsonResultFailure(ZOperationResult operationResult)
        {
            //AppHelper.Log(Request.Url.OriginalString, operationResult);

            return JsonResultFailure(new
            {
                OperationResult = operationResult
            });
        }

        protected JsonResult JsonResultFailure(ZOperationResult operationResult, string url)
        {
            //AppHelper.Log(Request.Url.OriginalString, operationResult);

            return JsonResultFailure(new
            {
                OperationResult = operationResult,
                Url = url
            });
        }

        private JsonResult JsonResultFailure(object data = null)
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            if (data != null)
            {
                //return new JsonResult()
                return new ZJsonResultMvc()
                {
                    Data = data,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                //return new JsonResult()
                return new ZJsonResultMvc()
                {
                    Data = false, // ???
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        // Success

        protected JsonResult JsonResultSuccess(ZOperationResult operationResult, string url)
        {
            return JsonResultSuccess(new
            {
                OperationResult = operationResult,
                Url = url
            });
        }

        protected JsonResult JsonResultSuccess(ZOperationResult operationResult, object data = null)
        {
            if (data != null)
            {
                return JsonResultSuccess(data);
            }
            else
            {
                return JsonResultSuccess(new
                {
                    OperationResult = operationResult,
                    Controller = ControllerContext.RouteData.Values["controller"].ToString()
                });
            }
        }

        private JsonResult JsonResultSuccess(object data = null)
        {
            Response.StatusCode = (int)HttpStatusCode.OK;

            if (data != null)
            {
                //return new JsonResult()
                return new ZJsonResultMvc()
                {
                    Data = data,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                //return new JsonResult()
                return new ZJsonResultMvc()
                {
                    Data = true, // ???
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        // ZOperationResult

        protected JsonResult JsonResultOperationResult(ZOperationResult operationResult, object data = null)
        {
            if (operationResult.Ok)
            {
                if (data != null)
                {
                    return JsonResultSuccess(data);
                }
                else
                {
                    return JsonResultSuccess(new { OperationResult = operationResult });
                }
            }
            else
            {
                return JsonResultFailure(new { OperationResult = operationResult });
            }
        }

        #endregion Methods ZJsonResultMvc

        #region Methods ZView

        protected ViewResult ZView(CollectionModel collectionModel)
        {
            //AppHelper.Log(Request.Url.OriginalString, collectionModel.OperationResult);

            return View(collectionModel);
        }

        //protected ViewResult ZView(string view, CollectionModel collectionModel)
        //{
        //    //AppHelper.Log(Request.Url.OriginalString, collectionModel.OperationResult);

        //    return View(view, collectionModel);
        //}

        protected ViewResult ZView(ItemModel itemModel)
        {
            //AppHelper.Log(Request.Url.OriginalString, itemModel.OperationResult);

            return View(itemModel);
        }

        //protected ViewResult ZView(string view, ItemModel itemModel)
        //{
        //    //AppHelper.Log(Request.Url.OriginalString, itemModel.OperationResult);

        //    return View(view, itemModel);
        //}

        protected PartialViewResult ZPartialView(string partialView, LookupModel lookupModel)
        {
            //AppHelper.Log(Request.Url.OriginalString, lookupModel.OperationResult);

            return PartialView(partialView, lookupModel);
        }

        protected ViewResult ZView(string view, ReportModelRDL reportModelRDL)
        {
            //AppHelper.Log(Request.Url.OriginalString, reportModelRDL.OperationResult);

            return View(view, reportModelRDL);
        }

        protected ViewResult ZView(string view, ReportModelRDLC reportModelRDLC)
        {
            //AppHelper.Log(Request.Url.OriginalString, reportModelRDLC.OperationResult);

            return View(view, reportModelRDLC);
        }

        protected ViewResult ZView(TaskModel taskModel)
        {
            //AppHelper.Log(Request.Url.OriginalString, taskModel.OperationResult);

            return View(taskModel);
        }

        protected ViewResult ZView(string view, TaskModel taskModel)
        {
            //AppHelper.Log(Request.Url.OriginalString, taskModel.OperationResult);

            return View(view, taskModel);
        }

        protected ViewResult ZViewOperationResult(ZOperationResult operationResult)
        {
            //AppHelper.Log(Request.Url.OriginalString, operationResult);

            return View("OperationResult", new OperationResultModel(operationResult));
        }

        #endregion Methods ZView
    }
}