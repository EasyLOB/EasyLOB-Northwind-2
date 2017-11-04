using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Product
    {
        #region Profile

        public static IZProfile Profile { get; private set; } = new ZProfile
        (
            Name: "Product",
            IsIdentity: true,
            Keys: new List<string> { "ProductId" },
            Lookup: "ProductName",
            LINQOrderBy: "ProductName",
            LINQWhere: "ProductId == @0",
            Associations: new List<string>
            {
                    "Category",
                    "Supplier"
            },
            Collections: new Dictionary<string, bool>
            {
                    { "OrderDetails", true },
            },
            Properties: new List<IZProfileProperty>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZProfileProperty(false, true ,  50, false, false, "col-md-1", "ProductId"),
                new ZProfileProperty(true , true , 200, true , false, "col-md-4", "ProductName"),
                new ZProfileProperty(false, false,  50, true , false, "col-md-1", "SupplierId"),
                new ZProfileProperty(true , true , 200, false, false, "col-md-4", "SupplierLookupText"),
                new ZProfileProperty(false, false,  50, true , false, "col-md-1", "CategoryId"),
                new ZProfileProperty(true , true , 200, false, false, "col-md-4", "CategoryLookupText"),
                new ZProfileProperty(true , true , 200, true , false, "col-md-2", "QuantityPerUnit"),
                new ZProfileProperty(false, false, 100, true , false, "col-md-1", "UnitPrice"),
                new ZProfileProperty(false, false,  50, true , false, "col-md-1", "UnitsInStock"),
                new ZProfileProperty(false, false,  50, true , false, "col-md-1", "UnitsOnOrder"),
                new ZProfileProperty(false, false,  50, true , false, "col-md-1", "ReorderLevel"),
                new ZProfileProperty(false, false, 100, true , false, "col-md-1", "Discontinued")
            }
        );

        #endregion Profile
    }
}
