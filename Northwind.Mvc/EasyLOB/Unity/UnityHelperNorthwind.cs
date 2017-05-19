using Northwind;
using Northwind.Application;
using Northwind.Persistence;
using Microsoft.Practices.Unity;

namespace EasyLOB
{
    public static class UnityHelperNorthwind
    {
        public static void RegisterMappings(IUnityContainer container)
        {
            container.RegisterType(typeof(INorthwindApplication), typeof(NorthwindApplication), UnityHelper.AppLifetimeManager);

            container.RegisterType(typeof(INorthwindGenericApplication<>), typeof(NorthwindGenericApplication<>), UnityHelper.AppLifetimeManager);
            container.RegisterType(typeof(INorthwindGenericApplicationDTO<,>), typeof(NorthwindGenericApplicationDTO<,>), UnityHelper.AppLifetimeManager);

            // Entity Framework
            container.RegisterType(typeof(INorthwindUnitOfWork), typeof(NorthwindUnitOfWorkEF), UnityHelper.AppLifetimeManager);
            container.RegisterType(typeof(INorthwindGenericRepository<>), typeof(NorthwindGenericRepositoryEF<>), UnityHelper.AppLifetimeManager);

            // LINQ to DB
            //container.RegisterType(typeof(INorthwindUnitOfWork), typeof(NorthwindUnitOfWorkLINQ2DB), UnityHelper.AppLifetimeManager);
            //container.RegisterType(typeof(INorthwindGenericRepository<>), typeof(NorthwindGenericRepositoryLINQ2DB<>), UnityHelper.AppLifetimeManager);

            // NHibernate
            //container.RegisterType(typeof(INorthwindUnitOfWork), typeof(NorthwindUnitOfWorkNH), UnityHelper.AppLifetimeManager);
            //container.RegisterType(typeof(INorthwindGenericRepository<>), typeof(NorthwindGenericRepositoryNH<>), UnityHelper.AppLifetimeManager);
        }
    }
}