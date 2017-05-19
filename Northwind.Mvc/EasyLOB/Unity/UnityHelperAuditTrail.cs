using EasyLOB.AuditTrail;
using EasyLOB.AuditTrail.Application;
using EasyLOB.AuditTrail.Persistence;
using Microsoft.Practices.Unity;

namespace EasyLOB
{
    public static class UnityHelperAuditTrail
    {
        public static void RegisterMappings(IUnityContainer container)
        {
            //container.RegisterType(typeof(IAuditTrailManager), typeof(AuditTrailManagerMock), UnityHelper.AppLifetimeManager);
            container.RegisterType(typeof(IAuditTrailManager), typeof(AuditTrailManager), UnityHelper.AppLifetimeManager); // !!!

            container.RegisterType(typeof(IAuditTrailGenericApplication<>), typeof(AuditTrailGenericApplication<>), UnityHelper.AppLifetimeManager);
            container.RegisterType(typeof(IAuditTrailGenericApplicationDTO<,>), typeof(AuditTrailGenericApplicationDTO<,>), UnityHelper.AppLifetimeManager);

            // Entity Framework
            container.RegisterType(typeof(IAuditTrailUnitOfWork), typeof(AuditTrailUnitOfWorkEF), UnityHelper.AppLifetimeManager);
            container.RegisterType(typeof(IAuditTrailGenericRepository<>), typeof(AuditTrailGenericRepositoryEF<>), UnityHelper.AppLifetimeManager);

            // LINQ to DB
            //container.RegisterType(typeof(IAuditTrailUnitOfWork), typeof(AuditTrailUnitOfWorkLINQ2DB), UnityHelper.AppLifetimeManager);
            //container.RegisterType(typeof(IAuditTrailGenericRepository<>), typeof(AuditTrailGenericRepositoryLINQ2DB<>), UnityHelper.AppLifetimeManager);

            // NHibernate
            //container.RegisterType(typeof(IAuditTrailUnitOfWork), typeof(AuditTrailUnitOfWorkNH), UnityHelper.AppLifetimeManager);
            //container.RegisterType(typeof(IAuditTrailGenericRepository<>), typeof(AuditTrailGenericRepositoryNH<>), UnityHelper.AppLifetimeManager);
        }
    }
}