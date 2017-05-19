using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Territory
    {
        #region Dictionaries

        public static IZDataProfile DataProfile
        {
            get
            {
                return new ZDataProfile
                {
                    Class = new ZClassProfile
                    (
                        Name: "Territory",
                        IsIdentity: false,
                        Keys: new string[] { "TerritoryId" },
                        Lookup: "TerritoryDescription",
                        Associations: new string[]
                        {
                            "Region",
                        },
                        CollectionsDictionary: new Dictionary<string, bool>
                        {
                            { "EmployeeTerritories", true },
                        },
                        LINQOrderBy: "TerritoryDescription",
                        LINQWhere: "TerritoryId == @0"
                    ),
                    Properties = new List<IZPropertyProfile>
                    {
                        //                   Grd    Grd    Grd  Edt    Edt    Edt
                        //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                        new ZPropertyProfile(false, true , 200, true , false, "col-md-2", "TerritoryId"),
                        new ZPropertyProfile(true , true , 200, true , false, "col-md-4", "TerritoryDescription"),
                        new ZPropertyProfile(false, false,  50, true , false, "col-md-1", "RegionId"),
                        new ZPropertyProfile(true , true , 200, false, false, "col-md-4", "RegionLookupText")
                    }
                };
            }
        }

        #endregion Dictionaries
    }
}
