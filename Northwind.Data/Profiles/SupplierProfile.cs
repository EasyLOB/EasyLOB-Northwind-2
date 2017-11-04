using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Supplier
    {
        #region Profile

        public static IZProfile Profile { get; private set; } = new ZProfile
        (
            Name: "Supplier",
            IsIdentity: true,
            Keys: new List<string> { "SupplierId" },
            Lookup: "CompanyName",
            LINQOrderBy: "CompanyName",
            LINQWhere: "SupplierId == @0",
            Associations: new List<string> { },
            Collections: new Dictionary<string, bool>
            {
                    { "Products", true },
            },
            Properties: new List<IZProfileProperty>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZProfileProperty(false, true ,  50, false, false, "col-md-1", "SupplierId"),
                new ZProfileProperty(true , true , 200, true , false, "col-md-4", "CompanyName"),
                new ZProfileProperty(true , true , 200, true , false, "col-md-3", "ContactName"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-3", "ContactTitle"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-4", "Address"),
                new ZProfileProperty(false, true , 150, true , false, "col-md-2", "City"),
                new ZProfileProperty(false, true , 150, true , false, "col-md-2", "Region"),
                new ZProfileProperty(false, true , 100, true , false, "col-md-1", "PostalCode"),
                new ZProfileProperty(false, true , 150, true , false, "col-md-2", "Country"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-3", "Phone"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-3", "Fax"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-2", "HomePage")
            }
        );

        #endregion Profile
    }
}
