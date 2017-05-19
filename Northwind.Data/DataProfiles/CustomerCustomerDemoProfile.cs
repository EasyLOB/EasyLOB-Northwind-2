using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class CustomerCustomerDemo
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
                        Name: "CustomerCustomerDemo",
                        IsIdentity: false,
                        Keys: new string[] { "CustomerId", "CustomerTypeId" },
                        Lookup: "CustomerTypeId",
                        Associations: new string[]
                        {
                            "CustomerDemographic",
                            "Customer"
                        },
                        CollectionsDictionary: new Dictionary<string, bool> { },
                        LINQOrderBy: "CustomerTypeId",
                        LINQWhere: "CustomerId == @0 && CustomerTypeId == @1"
                    ),
                    Properties = new List<IZPropertyProfile>
                    {
                        //                   Grd    Grd    Grd  Edt    Edt    Edt
                        //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                        new ZPropertyProfile(true , true ,  50, true , false, "col-md-1", "CustomerId"),
                        new ZPropertyProfile(true , true , 200, false, false, "col-md-4", "CustomerLookupText"),
                        new ZPropertyProfile(true , true , 100, true , false, "col-md-1", "CustomerTypeId"),
                        new ZPropertyProfile(true , true , 200, false, false, "col-md-4", "CustomerDemographicLookupText")
                    }
                };
            }
        }

        #endregion Dictionaries
    }
}
