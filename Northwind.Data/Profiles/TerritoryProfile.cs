using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Territory
    {
        #region Profile

        public static IZProfile Profile { get; private set; } = new ZProfile
        (
            Name: "Territory",
            IsIdentity: false,
            Keys: new List<string> { "TerritoryId" },
            Lookup: "TerritoryDescription",
            LINQOrderBy: "TerritoryDescription",
            LINQWhere: "TerritoryId == @0",
            Associations: new List<string>
            {
                    "Region",
            },
            Collections: new Dictionary<string, bool>
            {
                    { "EmployeeTerritories", true },
            },
            Properties: new List<IZProfileProperty>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZProfileProperty(false, true , 200, true , false, "col-md-2", "TerritoryId"),
                new ZProfileProperty(true , true , 200, true , false, "col-md-4", "TerritoryDescription"),
                new ZProfileProperty(false, false,  50, true , false, "col-md-1", "RegionId"),
                new ZProfileProperty(true , true , 200, false, false, "col-md-4", "RegionLookupText")
            }
        );

        #endregion Profile
    }
}
