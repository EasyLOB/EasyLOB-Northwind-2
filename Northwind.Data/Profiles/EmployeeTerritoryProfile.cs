using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class EmployeeTerritory
    {
        #region Profile

        public static IZProfile Profile { get; private set; } = new ZProfile
        (
            Name: "EmployeeTerritory",
            IsIdentity: false,
            Keys: new string[] { "EmployeeId", "TerritoryId" },
            Lookup: "TerritoryId",
            LINQOrderBy: "TerritoryId",
            LINQWhere: "EmployeeId == @0 && TerritoryId == @1",
            Associations: new string[]
            {
                    "Employee",
                    "Territory"
            },
            Collections: new Dictionary<string, bool> { },
            Properties: new List<IZProfileProperty>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZProfileProperty(true , true ,  50, true , false, "col-md-1", "EmployeeId"),
                new ZProfileProperty(true , true , 200, false, false, "col-md-4", "EmployeeLookupText"),
                new ZProfileProperty(true , true , 200, true , false, "col-md-2", "TerritoryId"),
                new ZProfileProperty(true , true , 200, false, false, "col-md-4", "TerritoryLookupText")
            }
        );

        #endregion Profile
    }
}
