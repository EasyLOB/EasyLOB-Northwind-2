using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class CustomerDemographic
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
                        Name: "CustomerDemographic",
                        IsIdentity: false,
                        Keys: new string[] { "CustomerTypeId" },
                        Lookup: "CustomerDesc",
                        Associations: new string[] { },
                        CollectionsDictionary: new Dictionary<string, bool>
                        {
                            { "CustomerCustomerDemos", true },
                        },
                        LINQOrderBy: "CustomerDesc",
                        LINQWhere: "CustomerTypeId == @0"
                    ),
                    Properties = new List<IZPropertyProfile>
                    {
                        //                   Grd    Grd    Grd  Edt    Edt    Edt
                        //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                        new ZPropertyProfile(false, true , 100, true , false, "col-md-1", "CustomerTypeId"),
                        new ZPropertyProfile(true , true , 200, true , false, "col-md-2", "CustomerDesc")
                    }
                };
            }
        }

        #endregion Dictionaries
    }
}
