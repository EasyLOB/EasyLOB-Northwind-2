using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class OrderDetail
    {
        #region Profile

        public static IZDataProfile DataProfile { get; private set; } = new ZDataProfile
        {
            Class = new ZClassProfile
            (
                Name: "OrderDetail",
                IsIdentity: false,
                Keys: new string[] { "OrderId", "ProductId" },
                Lookup: "ProductId",
                Associations: new string[]
                {
                    "Order",
                    "Product"
                },
                CollectionsDictionary: new Dictionary<string, bool> { },
                LINQOrderBy: "ProductId",
                LINQWhere: "OrderId == @0 && ProductId == @1"
            ),
            Properties = new List<IZPropertyProfile>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZPropertyProfile(false, true ,  50, true , false, "col-md-1", "OrderId"),
                new ZPropertyProfile(true , true , 200, false, false, "col-md-4", "OrderLookupText"),
                new ZPropertyProfile(false, true ,  50, true , false, "col-md-1", "ProductId"),
                new ZPropertyProfile(true , true , 200, false, false, "col-md-4", "ProductLookupText"),
                new ZPropertyProfile(true , false, 100, true , false, "col-md-1", "UnitPrice"),
                new ZPropertyProfile(true , false,  50, true , false, "col-md-1", "Quantity"),
                new ZPropertyProfile(false, false, 100, true , false, "col-md-1", "Discount")
            }
        };

        #endregion Profile
    }
}
