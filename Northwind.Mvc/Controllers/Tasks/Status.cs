using EasyLOB.Activity;
using EasyLOB.AuditTrail;
using EasyLOB.Extensions.Edm;
using EasyLOB.Identity;
using EasyLOB.Library;
using EasyLOB.Library.App;
using EasyLOB.Library.Web;
using EasyLOB.Log;
using EasyLOB.Resources;
using EasyLOB.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

// - AnonymousIdentification
// BundleModule
// - DefaultAuthentication
// FileAuthorization
// - FormsAuthentication
// - OutputCache
// Profile
// RoleManager
// ScriptModule-4.0
// ServiceModel-4.0
// Session
// UrlAuthorization
// UrlMappingsModule
// UrlRoutingModule-4.0
// - WindowsAuthentication

// __DynamicModule_Microsoft.Owin.Host.SystemWeb.OwinHttpModule, Microsoft.Owin.Host.SystemWeb, Version=3.0.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35_023e3c98-5d3a-409a-9620-33f8a4aab5a2
// __DynamicModule_Microsoft.VisualStudio.Web.PageInspector.Runtime.Tracing.PageInspectorHttpModule, Microsoft.VisualStudio.Web.PageInspector.Runtime, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a_a128e1f0-126c-4da1-a89b-a22c5f8b4cd2
// __DynamicModule_System.Web.WebPages.WebPageHttpModule, System.Web.WebPages, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35_3f8e3a11-24c3-4d3e-bd8c-ea9f6661206c
// __DynamicModule_System.Web.Optimization.BundleModule, System.Web.Optimization, Version=1.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35_df7ee17b-c173-4e72-8039-f59b50ed6d48
// __DynamicModule_Unity.Mvc5.RequestLifetimeHttpModule, Unity.Mvc5, Version=1.2.3.0, Culture=neutral, PublicKeyToken=43da31bc42a85347_0a7ba3c5-70f0-4b50-996f-566a904d4459

namespace EasyLOB.Mvc
{
    public partial class TasksController
    {
        #region Methods

        // GET: Tasks/Status
        [HttpGet]
        public ActionResult Status() // !!!
        {
            StringBuilder result = new StringBuilder();

            // EasyLOB

            result.Append("<br /><b>Directory</b>");
            result.Append("<br />:: Configuration: " + WebHelper.WebDirectory(ConfigurationHelper.AppSettings<string>("Directory.Configuration")));
            result.Append("<br />:: Export: " + WebHelper.WebDirectory(ConfigurationHelper.AppSettings<string>("Directory.Export")));
            result.Append("<br />:: Import: " + WebHelper.WebDirectory(ConfigurationHelper.AppSettings<string>("Directory.Import")));
            result.Append("<br />:: Template: " + WebHelper.WebDirectory(ConfigurationHelper.AppSettings<string>("Directory.Template")));
            result.Append("<br /><b>Report</b>");
            result.Append("<br />:: RDL: " + ConfigurationHelper.AppSettings<string>("Report.RDL.Url"));
            result.Append("<br />:: RDLC: " + WebHelper.WebDirectory(ConfigurationHelper.AppSettings<string>("Report.RDLC.Directory")));

            result.Append("<br />");

            result.Append("<br /><b>AuditTrail</b>");
            result.Append("<br />:: IAuditTrailUnitOfWork: " + (DependencyResolver.Current.GetService<IAuditTrailUnitOfWork>()).GetType().ToString());
            result.Append("<br />:: IAuditTrailManager: " + (DependencyResolver.Current.GetService<IAuditTrailManager>()).GetType().ToString());

            result.Append("<br /><b>EDM</b>");
            result.Append("<br />:: IEdmManager: " + (DependencyResolver.Current.GetService<IEdmManager>()).GetType().ToString());
            result.Append("<br />:: Directory: " + ConfigurationHelper.AppSettings<string>("EDM.FileSystemDirectory"));

            result.Append("<br /><b>Log</b>");
            result.Append("<br />:: ILogManager: " + (DependencyResolver.Current.GetService<ILogManager>()).GetType().ToString());

            result.Append("<br /><b>MVC</b>");
            result.Append("<br />:: Application Directory: " + WebHelper.ApplicationBaseDirectory);
            result.Append("<br />:: Web Url: " + WebHelper.WebUrl);
            result.Append("<br />:: Web Path: " + WebHelper.WebPath);
            result.Append("<br />:: Web Domain: " + WebHelper.WebDomain);
            result.Append("<br />:: Web SubDomain: " + WebHelper.WebSubDomain);

            AppTenant tenant = MultiTenantHelper.Tenant;
            result.Append("<br /><b>Multi-Tenant</b>");
            result.Append("<br />:: Name: " + tenant.Name);
            result.Append("<br />:: Description: " + tenant.Name);
            result.Append("<br />:: Connections: " + tenant.Connections.Count.ToString());

            AppProfile profile = ProfileHelper.Profile;
            result.Append("<br /><b>Profile</b>");
            result.Append("<br />:: User Name: " + profile.UserName);
            result.Append("<br />:: Role Name(s): " + String.Join(",", profile.Roles.ToArray()));
            result.Append("<br />:: Is Administrator ? " + profile.IsAdministrator.ToString());
            result.Append("<br />:: Is Authenticated ? " + profile.IsAuthenticated.ToString());
            result.Append("<br />:: Audit Trail");
            foreach (AppProfileAuditTrail auditTrail in profile.AuditTrail)
            {
                string domainEntity = (String.IsNullOrEmpty(auditTrail.Domain) ? "" : auditTrail.Domain + ".") +
                    auditTrail.Entity;
                result.Append("<br />&nbsp;&nbsp;&nbsp;" + domainEntity + " " +
                    auditTrail.LogOperations.Trim() + " " +
                    auditTrail.LogMode);
            }

            result.Append("<br /><b>Security - Authentication</b>");
            result.Append("<br />:: IAuthenticationManager: " + (DependencyResolver.Current.GetService<IAuthenticationManager>()).GetType().ToString());
            result.Append("<br />:: IIdentityUnitOfWork: " + (DependencyResolver.Current.GetService<IIdentityUnitOfWork>()).GetType().ToString());
            //IAuthenticationManager authenticationManager = DependencyResolver.Current.GetService<IAuthenticationManager>();
            //result.Append("<br />:: User Name: " + authenticationManager.UserName);
            //result.Append("<br />:: Role Name(s): " + String.Join(",", authenticationManager.Roles.ToArray()));
            //result.Append("<br />:: Is Administrator ? " + authenticationManager.IsAdministrator.ToString());
            //result.Append("<br />:: Is Authenticated ? " + authenticationManager.IsAuthenticated.ToString());

            result.Append("<br /><b>Security - Authorization</b>");
            result.Append("<br />:: IAuthorizationManager: " + (DependencyResolver.Current.GetService<IAuthorizationManager>()).GetType().ToString());
            result.Append("<br />:: IActivityUnitOfWork: " + (DependencyResolver.Current.GetService<IActivityUnitOfWork>()).GetType().ToString());

            HttpSessionState session = SessionHelper.Session;
            result.Append("<br />");
            result.Append("<br /><b>Session</b>");
            result.Append("<br />:: SessionID: " + Session.SessionID);
            result.Append("<br />:: SessionHelper.Session.SessionID: " + session.SessionID);
            result.Append("<br />:: Key(s)");
            for (int i = 0; i < session.Contents.Count; i++)
            {
                string value = session[i].ToString();
                switch (session.Keys[i])
                {
                    case "EasyLOB.Menu":
                        //value = JsonConvert.SerializeObject((List<AppMenu>)session[i]);
                        break;
                    case "EasyLOB.MultiTenant":
                        //value = JsonConvert.SerializeObject((List<AppTenant>)session[i]);
                        break;
                    case "EasyLOB.Profile":
                        value = JsonConvert.SerializeObject((AppProfile)session[i]);
                        break;
                    case "EasyLOB.UrlDictionary":
                        value = JsonConvert.SerializeObject((Dictionary<string, string>)session[i]);
                        break;                        
                }

                result.Append("<br />&nbsp;&nbsp;&nbsp;" + session.Keys[i] + ": " + value);
            }

            result.Append("<br />");
            result.Append("<br /><b>HTTP Modules</b>");
            HttpApplication httpApplication = HttpContext.ApplicationInstance;
            HttpModuleCollection modules = httpApplication.Modules;
            string[] keys = modules.AllKeys;
            Array.Sort(keys);
            foreach (string key in keys)
            {
                result.Append("<br />:: " + key);
            }

            ViewBag.Status = result.ToString();

            TaskViewModel viewModel = new TaskViewModel("Tasks", "Status", PresentationResources.TaskStatus);

            return View(viewModel);
        }

        #endregion Methods
    }
}