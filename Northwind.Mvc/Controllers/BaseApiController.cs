using EasyLOB.Data;
using EasyLOB.Identity.Resources;
using EasyLOB.Security;
using System;
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

        #endregion Methods

        #region Methods Authentication

        protected virtual bool IsUserAdministrator()
        {
            return AuthorizationManager.AuthenticationManager.IsAdministrator;
        }

        protected virtual bool IsUserInRole(string role)
        {
            return AuthorizationManager.AuthenticationManager.Roles.Contains(role);
        }

        protected virtual bool IsUserInRole(ZOperationResult operationResult, string role)
        {
            bool result = AuthorizationManager.AuthenticationManager.Roles.Contains(role);
            operationResult.AddOperationError("",
                String.Format(SecurityIdentityResources.OperationAuthorizedRole, role));

            return result;
        }

        #endregion Methods Authentication

        #region Methods Authorization

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

        protected virtual bool IsExport()
        {
            ZOperationResult operationResult = new ZOperationResult();

            return IsExport(operationResult);
        }

        protected virtual bool IsExport(ZOperationResult operationResult)
        {
            return AuthorizationManager.IsExport(ActivityOperations, operationResult);
        }

        protected virtual bool IsImport()
        {
            ZOperationResult operationResult = new ZOperationResult();

            return IsImport(operationResult);
        }

        protected virtual bool IsImport(ZOperationResult operationResult)
        {
            return AuthorizationManager.IsImport(ActivityOperations, operationResult);
        }

        protected virtual bool IsExecute(ZOperationResult operationResult)
        {
            return AuthorizationManager.IsExecute(ActivityOperations, operationResult);
        }

        protected virtual bool IsTask(ZOperationResult operationResult, string task)
        {
            return AuthorizationManager.IsTask(Domain, task, operationResult);
        }

        #endregion Methods Authorization
    }
}