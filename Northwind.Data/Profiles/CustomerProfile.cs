using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Customer
    {
        #region Profile

        public static IZProfile Profile { get; private set; } = new ZProfile
        (
            Name: "Customer",
            IsIdentity: false,
            Keys: new List<string> { "CustomerId" },
            Lookup: "CompanyName",
            LINQOrderBy: "CompanyName",
            LINQWhere: "CustomerId == @0",
            Associations: new List<string> { },
            Collections: new Dictionary<string, bool>
            {
                    { "CustomerCustomerDemos", true },
                    { "Orders", true }
            },
            Properties: new List<IZProfileProperty>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZProfileProperty(false, true ,  50, true , false, "col-md-1", "CustomerId"),
                new ZProfileProperty(true , true , 200, true , false, "col-md-4", "CompanyName"),
                new ZProfileProperty(true , true , 200, true , false, "col-md-3", "ContactName"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-3", "ContactTitle"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-4", "Address"),
                new ZProfileProperty(false, true , 150, true , false, "col-md-2", "City"),
                new ZProfileProperty(false, true , 150, true , false, "col-md-2", "Region"),
                new ZProfileProperty(false, true , 100, true , false, "col-md-1", "PostalCode"),
                new ZProfileProperty(false, true , 150, true , false, "col-md-2", "Country"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-3", "Phone"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-3", "Fax")
            }
        );

        #endregion Profile
    }
}
