using EasyLOB.Library.App;
using System.Web.Mvc;

namespace EasyLOB.Mvc
{
    public class EasyLOBMultiTenantAttribute : ActionFilterAttribute // !?! Multi-Tenant
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            AppTenant appTenant = MultiTenantHelper.Tenant; // !?! 100% sure Tenant is updated...
        }
    }
}