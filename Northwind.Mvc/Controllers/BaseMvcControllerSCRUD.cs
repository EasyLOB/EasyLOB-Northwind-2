using EasyLOB.Data;
using EasyLOB.Security;

namespace EasyLOB.Mvc
{
    public class BaseMvcControllerSCRUD<TEntity> : BaseMvcController
        where TEntity : class, IZDataBase
    {
        #region Properties

        protected virtual string Entity
        {
            get { return ""; }
        }

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

        protected virtual bool IsValid(ZOperationResult operationResult, IZValidatableObject entity)
        {
            entity.Validate(operationResult);

            return base.IsValid(operationResult, typeof(TEntity).Name);
        }

        #endregion Methods

        #region Methods IsActivity

        protected virtual bool IsOperation(ZOperationResult operationResult)
        {
            return AuthorizationManager.IsOperation(ActivityOperations, operationResult);
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

        protected virtual bool IsTask(string task, ZOperationResult operationResult)
        {
            return AuthorizationManager.IsTask(Domain, task, operationResult);
        }

        #endregion Methods IsActivity
    }
}