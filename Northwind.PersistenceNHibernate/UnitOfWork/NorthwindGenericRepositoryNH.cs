using EasyLOB.Data;
using EasyLOB.Persistence;

namespace Northwind.Persistence
{
    public class NorthwindGenericRepositoryNH<TEntity> : GenericRepositoryNH<TEntity>, INorthwindGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public NorthwindGenericRepositoryNH(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Session = (unitOfWork as NorthwindUnitOfWorkNH).Session;
        }

        #endregion Methods
    }
}

