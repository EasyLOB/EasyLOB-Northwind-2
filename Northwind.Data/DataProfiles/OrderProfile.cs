using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Order
    {
        #region Profile

        public static IZDataProfile DataProfile { get; private set; } = new ZDataProfile
        {
            Class = new ZClassProfile
            (
                Name: "Order",
                IsIdentity: true,
                Keys: new string[] { "OrderId" },
                Lookup: "CustomerId",
                Associations: new string[]
                {
                    "Customer",
                    "Employee",
                    "Shipper"
                },
                CollectionsDictionary: new Dictionary<string, bool>
                {
                    { "OrderDetails", true },
                },
                LINQOrderBy: "CustomerId",
                LINQWhere: "OrderId == @0"
            ),
            Properties = new List<IZPropertyProfile>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZPropertyProfile(false, true ,  50, false, false, "col-md-1", "OrderId"),
                new ZPropertyProfile(false, false,  50, true , false, "col-md-1", "CustomerId"),
                new ZPropertyProfile(true , true , 200, false, false, "col-md-4", "CustomerLookupText"),
                new ZPropertyProfile(false, false,  50, true , false, "col-md-1", "EmployeeId"),
                new ZPropertyProfile(true , true , 200, false, false, "col-md-4", "EmployeeLookupText"),
                new ZPropertyProfile(true , false, 200, true , false, "col-md-2", "OrderDate"),
                new ZPropertyProfile(true , false, 200, true , false, "col-md-2", "RequiredDate"),
                new ZPropertyProfile(false, false, 200, true , false, "col-md-2", "ShippedDate"),
                new ZPropertyProfile(false, false,  50, true , false, "col-md-1", "ShipVia"),
                new ZPropertyProfile(true , true , 200, false, false, "col-md-4", "ShipperLookupText"),
                new ZPropertyProfile(false, false, 100, true , false, "col-md-1", "Freight"),
                new ZPropertyProfile(false, true , 200, true , false, "col-md-4", "ShipName"),
                new ZPropertyProfile(false, true , 200, true , false, "col-md-4", "ShipAddress"),
                new ZPropertyProfile(false, true , 150, true , false, "col-md-2", "ShipCity"),
                new ZPropertyProfile(false, true , 150, true , false, "col-md-2", "ShipRegion"),
                new ZPropertyProfile(false, true , 100, true , false, "col-md-1", "ShipPostalCode"),
                new ZPropertyProfile(false, true , 150, true , false, "col-md-2", "ShipCountry")
            }
        };

        #endregion Profile
    }
}
