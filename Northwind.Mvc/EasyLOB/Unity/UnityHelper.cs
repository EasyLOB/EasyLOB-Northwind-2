using EasyLOB.Application;
using EasyLOB.Authentication;
using Microsoft.Practices.Unity;

// UnityDependencyResolver :: ASP.NET MVC
// UnityHierarchicalDependencyResolver :: ASP.NET Web API

namespace EasyLOB
{
    public static class UnityHelper
    {
        #region Properties

        public static LifetimeManager AppLifetimeManager
        {
            // A new object for every HttpRequest
            get { return new HttpRequestLifetimeManager(); }

            // Just one Singleton for ALL connections leads to EF errors
            //get { return new ContainerControlledLifetimeManager(); }

            // Unity.Mvc5
            // All components that implement IDisposable should be registered with the HierarchicalLifetimeManager
            // to ensure that they are properly disposed at the end of the request.
            //get { return new HierarchicalLifetimeManager(); }

            // Although the PerRequestLifetimeManager class works correctly
            // and can help you to work with stateful or thread-unsafe dependencies within the scope of an HTTP request,
            // it is generally not a good idea to use it if you can avoid it.
            // ...not a good idea to use...
            //get { return new PerRequestLifetimeManager(); }

            // Just one Singleton (Single IIS Thread) for ALL connections leads to EF errors
            //get { return new PerThreadLifetimeManager(); }

            // A new object for every Resolve()
            //get { return new TransientLifetimeManager(); }
        }

        private static IUnityContainer _container;

        public static IUnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new UnityContainer();
                }

                return _container;
            }
        }

        #endregion Properties

        #region Methods

        public static void RegisterMappings()
        {
            Container.RegisterType(typeof(IDIManager), typeof(DIManager), UnityHelper.AppLifetimeManager,
                new InjectionConstructor(Container));

            UnityHelperNorthwind.RegisterMappings(Container);

            UnityHelperActivity.RegisterMappings(Container);
            UnityHelperAuditTrail.RegisterMappings(Container);
            UnityHelperIdentity.RegisterMappings(Container);
            UnityHelperLog.RegisterMappings(Container);

            // Extensions

            UnityHelperExtensions.RegisterMappings(Container);

            // Authentication

            Container.RegisterType<AuthenticationController>(new InjectionConstructor());
        }

        #endregion Methods
    }
}