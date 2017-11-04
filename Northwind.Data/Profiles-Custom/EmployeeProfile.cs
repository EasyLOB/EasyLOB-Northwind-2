using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Employee
    {
        #region Profile

        public static IZProfile Profile { get; private set; } = new ZProfile
        (
            Name: "Employee",
            IsIdentity: true,
            Keys: new List<string> { "EmployeeId" },
            Lookup: "LastName",
            LINQOrderBy: "LastName",
            LINQWhere: "EmployeeId == @0",
            Associations: new List<string>
            {
                "EmployeeReportsTo",
            },
            Collections: new Dictionary<string, bool>
            {
                { "Employees", false }, // !!!
                { "EmployeeTerritories", true },
                { "Orders", true }
            },
            Properties: new List<IZProfileProperty>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZProfileProperty(false, true ,  50, false, false, "col-md-1", "EmployeeId"),
                new ZProfileProperty(true , true , 200, true , false, "col-md-2", "LastName"),
                new ZProfileProperty(true , true , 100, true , false, "col-md-1", "FirstName"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-3", "Title"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-3", "TitleOfCourtesy"),
                new ZProfileProperty(false, false, 200, true , false, "col-md-2", "BirthDate"),
                new ZProfileProperty(false, false, 200, true , false, "col-md-2", "HireDate"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-4", "Address"),
                new ZProfileProperty(false, true , 150, true , false, "col-md-2", "City"),
                new ZProfileProperty(false, true , 150, true , false, "col-md-2", "Region"),
                new ZProfileProperty(false, true , 100, true , false, "col-md-1", "PostalCode"),
                new ZProfileProperty(false, true , 150, true , false, "col-md-2", "Country"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-3", "HomePhone"),
                new ZProfileProperty(false, true ,  50, true , false, "col-md-1", "Extension"),
                new ZProfileProperty(false, false, 100, true , false, "col-md-2", "Photo"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-2", "Notes"),
                new ZProfileProperty(false, false,  50, true , false, "col-md-1", "ReportsTo"),
                new ZProfileProperty(true , true , 200, false, false, "col-md-4", "EmployeeLookupText"),
                new ZProfileProperty(false, true , 200, true , false, "col-md-4", "PhotoPath")
            }
        );

        #endregion Profile
    }
}
