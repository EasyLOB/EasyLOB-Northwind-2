using EasyLOB.Data;
using EasyLOB.Security;
using System.Web.Mvc;

namespace EasyLOB.WebApi
{
    public class BaseApiController<TEntity> : BaseApi
        where TEntity : class, IZDataBase
    {
        #region Properties

        protected virtual string Domain
        {
            get { return ""; } // ???
        }

        protected virtual string Entity
        {
            get { return ""; }
        }

        protected IAuthorizationManager AuthorizationManager { get; }

        private ZActivityOperations _activityOperations;

        protected virtual ZActivityOperations ActivityOperations
        {
            get
            {
                if (_activityOperations == null)
                {
                   _activityOperations = AuthorizationManager.GetOperations(SecurityHelper.EntityActivity(Domain, Entity));
                }
                return _activityOperations;
            }
            set 
            {
                _activityOperations = value;
            }
        }

        #endregion Properties

        #region Methods

        public BaseApiController()
        {
            AuthorizationManager = DependencyResolver.Current.GetService<IAuthorizationManager>();
        }

        protected virtual bool IsValid(ZOperationResult operationResult, IZValidatableObject entity)
        {
            entity.Validate(operationResult);

            return base.IsValid(operationResult, typeof(TEntity).Name);
        }

        #endregion Methods

        #region Methods IsActivity

        protected virtual bool IsSearch()
        {
            ZOperationResult operationResult = new ZOperationResult();

            return AuthorizationManager.IsSearch(ActivityOperations, operationResult);
        }

        protected virtual bool IsSearch(ZOperationResult operationResult)
        {
            return AuthorizationManager.IsSearch(ActivityOperations, operationResult);
        }

        protected virtual bool IsCreate(ZOperationResult operationResult)
        {
            return AuthorizationManager.IsCreate(ActivityOperations, operationResult);
        }

        protected virtual bool IsRead(ZOperationResult operationResult)
        {
            return AuthorizationManager.IsRead(ActivityOperations, operationResult);
        }

        protected virtual bool IsUpdate(ZOperationResult operationResult)
        {
            return AuthorizationManager.IsUpdate(ActivityOperations, operationResult);
        }

        protected virtual bool IsDelete(ZOperationResult operationResult)
        {
            return AuthorizationManager.IsDelete(ActivityOperations, operationResult);
        }

        protected virtual bool IsExecute(ZOperationResult operationResult)
        {
            return AuthorizationManager.IsExecute(ActivityOperations, operationResult);
        }

        #endregion Methods IsActivity
    }
}