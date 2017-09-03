using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class EmployeeTerritory
    {
        #region Profile

        public static IZDataProfile DataProfile { get; private set; } = new ZDataProfile
        {
            Class = new ZClassProfile
            (
                Name: "EmployeeTerritory",
                IsIdentity: false,
                Keys: new string[] { "EmployeeId", "TerritoryId" },
                Lookup: "TerritoryId",
                Associations: new string[]
                {
                    "Employee",
                    "Territory"
                },
                CollectionsDictionary: new Dictionary<string, bool> { },
                LINQOrderBy: "TerritoryId",
                LINQWhere: "EmployeeId == @0 && TerritoryId == @1"
            ),
            Properties = new List<IZPropertyProfile>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZPropertyProfile(true , true ,  50, true , false, "col-md-1", "EmployeeId"),
                new ZPropertyProfile(true , true , 200, false, false, "col-md-4", "EmployeeLookupText"),
                new ZPropertyProfile(true , true , 200, true , false, "col-md-2", "TerritoryId"),
                new ZPropertyProfile(true , true , 200, false, false, "col-md-4", "TerritoryLookupText")
            }
        };

        #endregion Profile
    }
}
