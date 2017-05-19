using EasyLOB.Library.Mvc;
using EasyLOB.Library.Syncfusion;
using EasyLOB.Security;
using System.Web.Mvc;

namespace EasyLOB.Mvc
{
    [Authorize]
    [EasyLOBProfile]
    [EasyLOBNorthwindProfile]
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
    }
}