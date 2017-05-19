using EasyLOB.Application;
using EasyLOB.Data;

namespace EasyLOB.WebApi
{
    public class BaseApiControllerApplication<TEntityDTO, TEntity> : BaseApiController<TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
        #region Properties

        protected override string Domain
        {
            get { return Application.UnitOfWork.Domain; }
        }

        protected override string Entity
        {
            get { return Application.Repository.Entity; }
        }

        protected IGenericApplicationDTO<TEntityDTO, TEntity> Application { get; set; }

        #endregion Properties
    }
}