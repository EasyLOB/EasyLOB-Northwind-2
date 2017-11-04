using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Order
    {
        #region Profile

        public static IZProfile Profile { get; private set; } = new ZProfile
        (
            Name: "Order",
            IsIdentity: true,
            Keys: new List<string> { "OrderId" },
            Lookup: "CustomerId",
            LINQOrderBy: "CustomerId",
            LINQWhere: "OrderId == @0",
            Associations: new List<string>
            {
                    "Customer",
                    "Employee",
                    "Shipper"
            },
            Collections: new Dictionary<string, bool>
            {
                    { "OrderDetails", true },
            },
            Properties: new List<IZProfileProperty>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZProfileProperty(false, true ,  50, false, false, "col-md-1", "OrderId"),
                new ZProfileProperty(false, false,  50, true , false, "col-md-1", "CustomerId"),
                new ZProfileProperty(true , true , 200, false, false, "col-md-4", "CustomerLookupText"),
                new ZProfileProperty(false, false,  50, true , false, "col-md-1", "EmployeeId"),
                new ZProfileProperty(true , true , 200, false, false, "col-md-4", "EmployeeLookupText"),
                new ZProfileProperty(true , false, 200, true , false, "col-md-2", "OrderDate"),
                new ZProfileProperty(true , false, 200, true , false, "col-md-2", "RequiredDate"),
                new ZProfileProperty(false, false, 200, true , false, "col-md-2", "ShippedDate"),
                new ZProfileProperty(false, false,  50, true , false, "col-md-1", "ShipVia"),
                new ZProfileProperty(true , true , 200, false, false, "col-md-4", "ShipperLookupText"),
                new ZProfileProperty(false, false, 100, true , false, "col-md-1", "Freight"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-4", "ShipName"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-4", "ShipAddress"),
                new ZProfileProperty(false, true , 150, true , false, "col-md-2", "ShipCity"),
                new ZProfileProperty(false, true , 150, true , false, "col-md-2", "ShipRegion"),
                new ZProfileProperty(false, true , 100, true , false, "col-md-1", "ShipPostalCode"),
                new ZProfileProperty(false, true , 150, true , false, "col-md-2", "ShipCountry")
            }
        );

        #endregion Profile
    }
}
