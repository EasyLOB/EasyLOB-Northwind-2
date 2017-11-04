using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Shipper
    {
        #region Profile

        public static IZProfile Profile { get; private set; } = new ZProfile
        (
            Name: "Shipper",
            IsIdentity: true,
            Keys: new List<string> { "ShipperId" },
            Lookup: "CompanyName",
            LINQOrderBy: "CompanyName",
            LINQWhere: "ShipperId == @0",
            Associations: new List<string> { },
            Collections: new Dictionary<string, bool>
            {
                    { "Orders", true },
            },
            Properties: new List<IZProfileProperty>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZProfileProperty(false, true ,  50, false, false, "col-md-1", "ShipperId"),
                new ZProfileProperty(true , true , 200, true , false, "col-md-4", "CompanyName"),
                new ZProfileProperty(true , true , 200, true , false, "col-md-3", "Phone")
            }
        );

        #endregion Profile
    }
}
