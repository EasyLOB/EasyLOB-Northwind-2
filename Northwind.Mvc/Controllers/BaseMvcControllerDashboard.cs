using System.Web.Mvc;

namespace EasyLOB.Mvc
{
    [Authorize]
    public class BaseMvcControllerDashboard : BaseMvcController
    {
        #region Methods

        //public BaseMvcControllerDashboard()
        //{
        //}

        #endregion Methods

        #region Methods IsActivity

        protected bool IsDashboard(string dashboardDirectory, string dashboardName)
        {
            ZOperationResult operationResult = new ZOperationResult();

            return IsDashboard(dashboardDirectory, dashboardName, operationResult);
        }

        protected bool IsDashboard(string dashboardDirectory, string dashboardName, ZOperationResult operationResult)
        {
            return AuthorizationManager.IsDashboard(Domain, dashboardDirectory, dashboardName, operationResult);
        }

        #endregion Methods IsActivity
    }
}