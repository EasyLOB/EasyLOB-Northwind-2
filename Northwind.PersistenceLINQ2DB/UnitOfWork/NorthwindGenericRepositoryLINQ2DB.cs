using EasyLOB.Data;
using EasyLOB.Persistence;

namespace Northwind.Persistence
{
    public class NorthwindGenericRepositoryLINQ2DB<TEntity> : GenericRepositoryLINQ2DB<TEntity>, INorthwindGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public NorthwindGenericRepositoryLINQ2DB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Connection = (unitOfWork as NorthwindUnitOfWorkLINQ2DB).Connection;
        }

        #endregion Methods
    }
}

