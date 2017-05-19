using EasyLOB;
using EasyLOB.Mvc;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity.Mvc5;

// Major Events in GLOBAL.ASAX file
// http://www.c-sharpcorner.com/uploadfile/aa04e6/major-events-in-global-asax-file

namespace Northwind.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Performance
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();

            // Dependency Injection
            // ASP.NET MVC
            UnityHelper.RegisterMappings();
            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityHelper.Container));
            // ASP.NET Web API + Syncfusion Report Viewer
            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Json.NET
            JsonConvert.DefaultSettings = () => AppHelper.JsonSettings;

            // NHibernate
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new NHibernateContractResolver();

            // Razor        
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomRazorViewEngine());

            // Validation
            // /App_GlobalResources/ValidationResources.*.resx

            ClientDataTypeModelValidatorProvider.ResourceClassKey = "ValidationResources";
            DefaultModelBinder.ResourceClassKey = "ValidationResources";
        }

        //protected void Application_End()
        //{
        //}

        protected void Application_BeginRequest(object sender, EventArgs e) // ???
        //protected void Application_AcquireRequestState(object sender, EventArgs e) // ???
        {
            HttpCookie cookie = Request.Cookies["ZCulture"];
            if (cookie == null)
            {
                cookie = new HttpCookie("ZCulture");
                cookie.Value = "pt-BR"; // !?!
                cookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(cookie);
            }

            CultureInfo ci = new CultureInfo(cookie.Value);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }

        //protected void Session_OnStart(Object sender, EventArgs e)
        //{
        //}

        //protected void Session_OnEnd(Object sender, EventArgs e)
        //{
        //}
    }
}