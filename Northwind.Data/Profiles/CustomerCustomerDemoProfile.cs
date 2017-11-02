using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class CustomerCustomerDemo
    {
        #region Profile

        public static IZProfile Profile { get; private set; } = new ZProfile
        (
            Name: "CustomerCustomerDemo",
            IsIdentity: false,
            Keys: new string[] { "CustomerId", "CustomerTypeId" },
            Lookup: "CustomerTypeId",
            LINQOrderBy: "CustomerTypeId",
            LINQWhere: "CustomerId == @0 && CustomerTypeId == @1",
            Associations: new string[]
            {
                    "CustomerDemographic",
                    "Customer"
            },
            Collections: new Dictionary<string, bool> { },
            Properties: new List<IZProfileProperty>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZProfileProperty(true , true ,  50, true , false, "col-md-1", "CustomerId"),
                new ZProfileProperty(true , true , 200, false, false, "col-md-4", "CustomerLookupText"),
                new ZProfileProperty(true , true , 100, true , false, "col-md-1", "CustomerTypeId"),
                new ZProfileProperty(true , true , 200, false, false, "col-md-4", "CustomerDemographicLookupText")
            }
        );

        #endregion Profile
    }
}
