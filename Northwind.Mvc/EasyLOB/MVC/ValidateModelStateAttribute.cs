using System.Net;
using System.Web.Mvc;

namespace EasyLOB.Mvc
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        #region Methods

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller.ViewData.ModelState.IsValid == false)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        #endregion Methods
    }
}