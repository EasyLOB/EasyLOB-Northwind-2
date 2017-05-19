using EasyLOB.Library.WebApi;
using System.Web.Http;

namespace EasyLOB.WebApi
{
    public class BaseApi : ApiController
    {
        #region Methods

        protected virtual bool IsValid(ZOperationResult operationResult, string entity)
        {
            ModelState.AddOperationResults(operationResult, entity);

            return ModelState.IsValid;
        }

        #endregion Methods
    }
}