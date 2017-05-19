using EasyLOB.Extensions.Edm;
using EasyLOB.Extensions.Mail;
using Microsoft.Practices.Unity;

namespace EasyLOB
{
    public static class UnityHelperExtensions
    {
        public static void RegisterMappings(IUnityContainer container)
        {
            //container.RegisterType(typeof(IEdmManager), typeof(EdmManagerMock), UnityHelper.AppLifetimeManager);
            container.RegisterType(typeof(IEdmManager), typeof(EdmManagerFileSystem), UnityHelper.AppLifetimeManager, new InjectionConstructor());
            //container.RegisterType(typeof(IEdmManager), typeof(EdmFtpSystem), UnityHelper.AppLifetimeManager, new InjectionConstructor());

            //container.RegisterType(typeof(IIniManager), typeof(IniManagerMock), UnityHelper.AppLifetimeManager);
            //container.RegisterType(typeof(IIniManager), typeof(IniManager), UnityHelper.AppLifetimeManager, new InjectionConstructor());

            //container.RegisterType(typeof(IMailManager), typeof(MailManagerMock), UnityHelper.AppLifetimeManager);
            container.RegisterType(typeof(IMailManager), typeof(MailManager), UnityHelper.AppLifetimeManager, new InjectionConstructor());
        }
    }
}