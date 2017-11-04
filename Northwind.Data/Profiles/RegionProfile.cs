using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Region
    {
        #region Profile

        public static IZProfile Profile { get; private set; } = new ZProfile
        (
            Name: "Region",
            IsIdentity: false,
            Keys: new List<string> { "RegionId" },
            Lookup: "RegionDescription",
            LINQOrderBy: "RegionDescription",
            LINQWhere: "RegionId == @0",
            Associations: new List<string> { },
            Collections: new Dictionary<string, bool>
            {
                    { "Territories", true },
            },
            Properties: new List<IZProfileProperty>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZProfileProperty(false, true ,  50, true , false, "col-md-1", "RegionId"),
                new ZProfileProperty(true , true , 200, true , false, "col-md-4", "RegionDescription")
            }
        );

        #endregion Profile
    }
}
