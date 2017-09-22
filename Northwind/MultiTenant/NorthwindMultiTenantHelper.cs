using EasyLOB.Library;
using EasyLOB.Library.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Northwind
{
    public static class NorthwindMultiTenantHelper
    {
        #region Properties

        private static string SessionName = "EasyLOB.NorthwindMultiTenant";

        public static NorthwindTenant Tenant
        {
            get { return GetTenant(MultiTenantHelper.Tenant.Name); }
        }

        public static List<NorthwindTenant> Tenants
        {
            get
            {
                List<NorthwindTenant> tenants = (List<NorthwindTenant>)SessionHelper.Read(SessionName);
                if (tenants == null || tenants.Count == 0)
                {
                    try
                    {
                        string filePath = Path.Combine(WebHelper.WebDirectory(ConfigurationHelper.AppSettings<string>("Directory.Configuration")),
                            "JSON/NorthwindMultiTenant.json");
                        string json = File.ReadAllText(filePath);
                        tenants = JsonConvert.DeserializeObject<List<NorthwindTenant>>(json);
                    }
                    catch { }
                    tenants = tenants ?? new List<NorthwindTenant>();

                    SessionHelper.Write(SessionName, tenants);
                }

                return tenants;
            }
        }

        #endregion Properties

        #region Methods

        public static NorthwindTenant GetTenant(string name)
        {
            NorthwindTenant NorthwindTenant = null;

            if (Tenants.Count > 0)
            {
                foreach (NorthwindTenant t in Tenants)
                {
                    if (t.Name.Equals(name, System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        NorthwindTenant = t;
                        break;
                    }
                }
            }

            if (NorthwindTenant == null && Tenants.Count > 0)
            {
                NorthwindTenant = Tenants[0];
            }

            NorthwindTenant = NorthwindTenant ?? new NorthwindTenant();

            return NorthwindTenant;
        }

        #endregion
    }
}