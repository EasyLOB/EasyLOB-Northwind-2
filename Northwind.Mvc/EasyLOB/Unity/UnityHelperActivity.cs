using EasyLOB.Activity;
using EasyLOB.Activity.Application;
using EasyLOB.Activity.Persistence;
using EasyLOB.Security;
using Microsoft.Practices.Unity;

namespace EasyLOB
{
    public static class UnityHelperActivity
    {
        public static void RegisterMappings(IUnityContainer container)
        {
            //container.RegisterType(typeof(IAuthorizationManager), typeof(AuthorizationManagerMock), UnityHelper.AppLifetimeManager);
            container.RegisterType(typeof(IAuthorizationManager), typeof(AuthorizationManager), UnityHelper.AppLifetimeManager);

            container.RegisterType(typeof(IActivityGenericApplication<>), typeof(ActivityGenericApplication<>), UnityHelper.AppLifetimeManager);
            container.RegisterType(typeof(IActivityGenericApplicationDTO<,>), typeof(ActivityGenericApplicationDTO<,>), UnityHelper.AppLifetimeManager);

            // Entity Framework
            container.RegisterType(typeof(IActivityUnitOfWork), typeof(ActivityUnitOfWorkEF), UnityHelper.AppLifetimeManager);
            container.RegisterType(typeof(IActivityGenericRepository<>), typeof(ActivityGenericRepositoryEF<>), UnityHelper.AppLifetimeManager);

            // LINQ to DB
            //container.RegisterType(typeof(IActivityUnitOfWork), typeof(ActivityUnitOfWorkLINQ2DB), UnityHelper.AppLifetimeManager);
            //container.RegisterType(typeof(IActivityGenericRepository<>), typeof(ActivityGenericRepositoryLINQ2DB<>), UnityHelper.AppLifetimeManager);

            // NHibernate
            //container.RegisterType(typeof(IActivityUnitOfWork), typeof(ActivityUnitOfWorkNH), UnityHelper.AppLifetimeManager);
            //container.RegisterType(typeof(IActivityGenericRepository<>), typeof(ActivityGenericRepositoryNH<>), UnityHelper.AppLifetimeManager);
        }
    }
}