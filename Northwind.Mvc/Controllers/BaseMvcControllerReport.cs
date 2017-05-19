namespace EasyLOB.Mvc
{
    public class BaseMvcControllerReport : BaseMvcController
    {
        #region Methods

        //public BaseMvcControllerReport()
        //{
        //}

        #endregion Methods

        #region Methods IsActivity

        protected bool IsReport(string reportDirectory, string reportName)
        {
            ZOperationResult operationResult = new ZOperationResult();

            return IsReport(reportDirectory, reportName, operationResult);
        }

        protected bool IsReport(string reportDirectory, string reportName, ZOperationResult operationResult)
        {
            return AuthorizationManager.IsReport(Domain, reportDirectory, reportName, operationResult);
        }

        #endregion Methods IsActivity
    }
}