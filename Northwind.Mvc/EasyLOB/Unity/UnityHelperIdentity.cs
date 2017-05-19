using EasyLOB.Identity;
using EasyLOB.Identity.Application;
using EasyLOB.Identity.Persistence;
using Microsoft.Practices.Unity;
using EasyLOB.Security;

namespace EasyLOB
{
    public static class UnityHelperIdentity
    {
        public static void RegisterMappings(IUnityContainer container)
        {
            //container.RegisterType(typeof(IAuthenticationManager), typeof(AuthenticationManagerMock), UnityHelper.AppLifetimeManager);
            container.RegisterType(typeof(IAuthenticationManager), typeof(AuthenticationManager), UnityHelper.AppLifetimeManager);

            container.RegisterType(typeof(IIdentityGenericApplication<>), typeof(IdentityGenericApplication<>), UnityHelper.AppLifetimeManager);
            container.RegisterType(typeof(IIdentityGenericApplicationDTO<,>), typeof(IdentityGenericApplicationDTO<,>), UnityHelper.AppLifetimeManager);

            // Entity Framework
            container.RegisterType(typeof(IIdentityUnitOfWork), typeof(IdentityUnitOfWorkEF), UnityHelper.AppLifetimeManager);
            container.RegisterType(typeof(IIdentityGenericRepository<>), typeof(IdentityGenericRepositoryEF<>), UnityHelper.AppLifetimeManager);

            // NHibernate
            //container.RegisterType(typeof(IIdentityUnitOfWork), typeof(IdentityUnitOfWorkNH), UnityHelper.AppLifetimeManager);
            //container.RegisterType(typeof(IIdentityGenericRepository<>), typeof(IdentityGenericRepositoryNH<>), UnityHelper.AppLifetimeManager);
        }
    }
}