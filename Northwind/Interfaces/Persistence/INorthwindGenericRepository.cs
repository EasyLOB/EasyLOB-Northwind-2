using EasyLOB.Data;
using EasyLOB.Persistence;

namespace Northwind.Persistence
{
    public interface INorthwindGenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
    }
}
