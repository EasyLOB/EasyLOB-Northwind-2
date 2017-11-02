using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class CustomerDemographic
    {
        #region Profile

        public static IZProfile Profile { get; private set; } = new ZProfile
        (
            Name: "CustomerDemographic",
            IsIdentity: false,
            Keys: new string[] { "CustomerTypeId" },
            Lookup: "CustomerDesc",
            LINQOrderBy: "CustomerDesc",
            LINQWhere: "CustomerTypeId == @0",
            Associations: new string[] { },
            Collections: new Dictionary<string, bool>
            {
                    { "CustomerCustomerDemos", true },
            },
            Properties: new List<IZProfileProperty>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZProfileProperty(false, true , 100, true , false, "col-md-1", "CustomerTypeId"),
                new ZProfileProperty(true , true , 200, true , false, "col-md-2", "CustomerDesc")
            }
        );

        #endregion Profile
    }
}
