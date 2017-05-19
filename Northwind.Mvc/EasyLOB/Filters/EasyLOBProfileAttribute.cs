using EasyLOB.Security;
using System;
using System.Web.Mvc;

namespace EasyLOB.Mvc
{
    public class EasyLOBProfileAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (String.IsNullOrEmpty(ProfileHelper.Profile.UserName))
            {
                ProfileHelper.Login(DependencyResolver.Current.GetService<IAuthenticationManager>());
            }

            base.OnActionExecuting(filterContext);
        }
    }
}