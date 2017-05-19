using EasyLOB.Data;
using EasyLOB.Persistence;

namespace Northwind.Persistence
{
    public interface INorthwindGenericRepositoryDTO<TEntityDTO, TEntity> : IGenericRepositoryDTO<TEntityDTO, TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
    }
}
