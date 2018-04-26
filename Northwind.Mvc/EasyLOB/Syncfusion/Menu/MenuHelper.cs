using EasyLOB.Library.App;
using EasyLOB.Library.Web;
using EasyLOB.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// How to apply url, htmlAttribute, imageUrl, linkAttribute for Menu Items?
// https://www.syncfusion.com/kb/3008/how-to-apply-url-htmlattribute-imageurl-linkattribute-for-menu-items

namespace EasyLOB.Library.Syncfusion
{
    public static partial class MenuHelper
    {
        #region Fields

        private static string SessionName = "EasyLOB.Menu";

        #endregion Fields

        #region Methods

        public static List<AppMenu> Menu(IAuthenticationManager authenticationManager)
        {
            List<AppMenu> menu = (List<AppMenu>)SessionHelper.Read(SessionName);
            if (menu == null || menu.Count == 0)
            {
                menu = new List<AppMenu>();

                if (authenticationManager.IsAuthenticated)
                {
                    List<AppMenuJson> menuJson = new List<AppMenuJson>();
                    string tenantName = MultiTenantHelper.Tenant.Name;
                    // Menu.TenantName.json
                    string filePath = Path.Combine(WebHelper.WebDirectory(ConfigurationHelper.AppSettings<string>("Directory.Configuration")),
                        "JSON/Menu" + "." + tenantName + ".json");
                    // Menu.json | Menu.TenantName.json ???
                    //string filePath = Path.Combine(MvcHelper.WebDirectory(ConfigurationHelper.AppSettings<string>("Directory.Configuration")),
                    //    "JSON/Menu" + (String.IsNullOrEmpty(tenantName) ? "" : "." + tenantName) + ".json");
                    string json = File.ReadAllText(filePath);
                    menuJson = JsonConvert.DeserializeObject<List<AppMenuJson>>(json);

                    Parse(menu, menuJson, null, authenticationManager.IsAdministrator, authenticationManager.Roles);

                    ResourcesManager resourcesManager = new ResourcesManager();

                    foreach (AppMenu appMenu in menu)
                    {
                        if (appMenu.Text.Contains("Resources."))
                        {
                            string[] words = appMenu.Text.Split('.');
                            if (words.Length == 2)
                            {
                                switch (words[0])
                                {
                                    case "DashboardResources":
                                        appMenu.Text = resourcesManager.GetDashboardResource(words[1]);
                                        break;

                                    case "MenuResources":
                                        appMenu.Text = resourcesManager.GetMenuResource(words[1]);
                                        break;

                                    case "ReportResources":
                                        appMenu.Text = resourcesManager.GetReportResource(words[1]);
                                        break;

                                    default:
                                        appMenu.Text = resourcesManager.GetResource(words[0], words[1]);
                                        break;
                                }
                            }
                        }
                    }
                }

                SessionHelper.Write(SessionName, menu);
            }

            return menu;
        }

        private static void Parse(List<AppMenu> menu, List<AppMenuJson> menuJson, int? parentId,
            bool isAdministrator, List<string> roles)
        {
            if (menuJson != null)
            {
                int id = (parentId ?? 0) * 100;
                foreach (AppMenuJson appMenuJson in menuJson)
                {
                    bool authorized = true;

                    if (!isAdministrator && !String.IsNullOrEmpty(appMenuJson.Roles))
                    {
                        if (appMenuJson.Roles == "*")
                        {
                            authorized = true;
                        }
                        else
                        {
                            authorized = false;
                            string[] menuRoles = appMenuJson.Roles.Split(',');
                            foreach (string role in roles)
                            {
                                authorized = authorized || menuRoles.Contains(role);
                                if (authorized)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    if (authorized)
                    {
                        id++;
                        menu.Add(new AppMenu(id, appMenuJson.Text, parentId, appMenuJson.Url));
                        Parse(menu, appMenuJson.SubMenus, id, isAdministrator, roles);
                    }
                }
            }
        }

        #endregion Methods
    }
    /*
    public class HtmlAttribute
    {
        #region Properties

        public string Title { get; set; }
        public string Style { get; set; }

        #endregion Properties
    }

    public class LinkAttribute
    {
        #region Properties

        public string Target { get; set; }
        public string Class { get; set; }

        #endregion Properties
    }

    public class ImageAttribute
    {
        #region Properties

        public string Height { get; set; }
        public string Width { get; set; }

        #endregion Properties
    }
     */
}