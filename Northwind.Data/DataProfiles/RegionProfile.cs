using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Region
    {
        #region Profile

        public static IZDataProfile DataProfile { get; private set; } = new ZDataProfile
        {
            Class = new ZClassProfile
            (
                Name: "Region",
                IsIdentity: false,
                Keys: new string[] { "RegionId" },
                Lookup: "RegionDescription",
                Associations: new string[] { },
                CollectionsDictionary: new Dictionary<string, bool>
                {
                    { "Territories", true },
                },
                LINQOrderBy: "RegionDescription",
                LINQWhere: "RegionId == @0"
            ),
            Properties = new List<IZPropertyProfile>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZPropertyProfile(false, true ,  50, true , false, "col-md-1", "RegionId"),
                new ZPropertyProfile(true , true , 200, true , false, "col-md-4", "RegionDescription")
            }
        };

        #endregion Profile
    }
}
