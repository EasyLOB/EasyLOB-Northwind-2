using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Product
    {
        #region Profile

        public static IZDataProfile DataProfile { get; private set; } = new ZDataProfile
        {
            Class = new ZClassProfile
            (
                Name: "Product",
                IsIdentity: true,
                Keys: new string[] { "ProductId" },
                Lookup: "ProductName",
                Associations: new string[]
                {
                    "Category",
                    "Supplier"
                },
                CollectionsDictionary: new Dictionary<string, bool>
                {
                    { "OrderDetails", true },
                },
                LINQOrderBy: "ProductName",
                LINQWhere: "ProductId == @0"
            ),
            Properties = new List<IZPropertyProfile>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZPropertyProfile(false, true ,  50, false, false, "col-md-1", "ProductId"),
                new ZPropertyProfile(true , true , 200, true , false, "col-md-4", "ProductName"),
                new ZPropertyProfile(false, false,  50, true , false, "col-md-1", "SupplierId"),
                new ZPropertyProfile(true , true , 200, false, false, "col-md-4", "SupplierLookupText"),
                new ZPropertyProfile(false, false,  50, true , false, "col-md-1", "CategoryId"),
                new ZPropertyProfile(true , true , 200, false, false, "col-md-4", "CategoryLookupText"),
                new ZPropertyProfile(true , true , 200, true , false, "col-md-2", "QuantityPerUnit"),
                new ZPropertyProfile(false, false, 100, true , false, "col-md-1", "UnitPrice"),
                new ZPropertyProfile(false, false,  50, true , false, "col-md-1", "UnitsInStock"),
                new ZPropertyProfile(false, false,  50, true , false, "col-md-1", "UnitsOnOrder"),
                new ZPropertyProfile(false, false,  50, true , false, "col-md-1", "ReorderLevel"),
                new ZPropertyProfile(false, false, 100, true , false, "col-md-1", "Discontinued")
            }
        };

        #endregion Profile
    }
}
