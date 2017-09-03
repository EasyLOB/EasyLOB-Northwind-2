using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Customer
    {
        #region Profile

        public static IZDataProfile DataProfile { get; private set; } = new ZDataProfile
        {
            Class = new ZClassProfile
            (
                Name: "Customer",
                IsIdentity: false,
                Keys: new string[] { "CustomerId" },
                Lookup: "CompanyName",
                Associations: new string[] { },
                CollectionsDictionary: new Dictionary<string, bool>
                {
                    { "CustomerCustomerDemos", true },
                    { "Orders", true }
                },
                LINQOrderBy: "CompanyName",
                LINQWhere: "CustomerId == @0"
            ),
            Properties = new List<IZPropertyProfile>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZPropertyProfile(false, true ,  50, true , false, "col-md-1", "CustomerId"),
                new ZPropertyProfile(true , true , 200, true , false, "col-md-4", "CompanyName"),
                new ZPropertyProfile(true , true , 200, true , false, "col-md-3", "ContactName"),
                new ZPropertyProfile(false, true , 200, true , false, "col-md-3", "ContactTitle"),
                new ZPropertyProfile(false, true , 200, true , false, "col-md-4", "Address"),
                new ZPropertyProfile(false, true , 150, true , false, "col-md-2", "City"),
                new ZPropertyProfile(false, true , 150, true , false, "col-md-2", "Region"),
                new ZPropertyProfile(false, true , 100, true , false, "col-md-1", "PostalCode"),
                new ZPropertyProfile(false, true , 150, true , false, "col-md-2", "Country"),
                new ZPropertyProfile(false, true , 200, true , false, "col-md-3", "Phone"),
                new ZPropertyProfile(false, true , 200, true , false, "col-md-3", "Fax")
            }
        };

        #endregion Profile
    }
}
