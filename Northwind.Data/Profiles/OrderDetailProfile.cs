using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class OrderDetail
    {
        #region Profile

        public static IZProfile Profile { get; private set; } = new ZProfile
        (
            Name: "OrderDetail",
            IsIdentity: false,
            Keys: new List<string> { "OrderId", "ProductId" },
            Lookup: "ProductId",
            LINQOrderBy: "ProductId",
            LINQWhere: "OrderId == @0 && ProductId == @1",
            Associations: new List<string>
            {
                    "Order",
                    "Product"
            },
            Collections: new Dictionary<string, bool> { },
            Properties: new List<IZProfileProperty>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZProfileProperty(false, true ,  50, true , false, "col-md-1", "OrderId"),
                new ZProfileProperty(true , true , 200, false, false, "col-md-4", "OrderLookupText"),
                new ZProfileProperty(false, true ,  50, true , false, "col-md-1", "ProductId"),
                new ZProfileProperty(true , true , 200, false, false, "col-md-4", "ProductLookupText"),
                new ZProfileProperty(true , false, 100, true , false, "col-md-1", "UnitPrice"),
                new ZProfileProperty(true , false,  50, true , false, "col-md-1", "Quantity"),
                new ZProfileProperty(false, false, 100, true , false, "col-md-1", "Discount")
            }
        );

        #endregion Profile
    }
}
