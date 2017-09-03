using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Shipper
    {
        #region Profile

        public static IZDataProfile DataProfile { get; private set; } = new ZDataProfile
        {
            Class = new ZClassProfile
            (
                Name: "Shipper",
                IsIdentity: true,
                Keys: new string[] { "ShipperId" },
                Lookup: "CompanyName",
                Associations: new string[] { },
                CollectionsDictionary: new Dictionary<string, bool>
                {
                    { "Orders", true },
                },
                LINQOrderBy: "CompanyName",
                LINQWhere: "ShipperId == @0"
            ),
            Properties = new List<IZPropertyProfile>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZPropertyProfile(false, true ,  50, false, false, "col-md-1", "ShipperId"),
                new ZPropertyProfile(true , true , 200, true , false, "col-md-4", "CompanyName"),
                new ZPropertyProfile(true , true , 200, true , false, "col-md-3", "Phone")
            }
        };

        #endregion Profile
    }
}
