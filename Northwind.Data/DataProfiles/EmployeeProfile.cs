using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Employee
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
                        Name: "Employee",
                        IsIdentity: true,
                        Keys: new string[] { "EmployeeId" },
                        Lookup: "LastName",
                        Associations: new string[]
                        {
                            "EmployeeReportsTo",
                        },
                        CollectionsDictionary: new Dictionary<string, bool>
                        {
                            { "Employees", true },
                            { "EmployeeTerritories", true },
                            { "Orders", true }
                        },
                        LINQOrderBy: "LastName",
                        LINQWhere: "EmployeeId == @0"
                    ),
                    Properties = new List<IZPropertyProfile>
                    {
                        //                   Grd    Grd    Grd  Edt    Edt    Edt
                        //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                        new ZPropertyProfile(false, true ,  50, false, false, "col-md-1", "EmployeeId"),
                        new ZPropertyProfile(true , true , 200, true , false, "col-md-2", "LastName"),
                        new ZPropertyProfile(true , true , 100, true , false, "col-md-1", "FirstName"),
                        new ZPropertyProfile(false, true , 200, true , false, "col-md-3", "Title"),
                        new ZPropertyProfile(false, true , 200, true , false, "col-md-3", "TitleOfCourtesy"),
                        new ZPropertyProfile(false, false, 200, true , false, "col-md-2", "BirthDate"),
                        new ZPropertyProfile(false, false, 200, true , false, "col-md-2", "HireDate"),
                        new ZPropertyProfile(false, true , 200, true , false, "col-md-4", "Address"),
                        new ZPropertyProfile(false, true , 150, true , false, "col-md-2", "City"),
                        new ZPropertyProfile(false, true , 150, true , false, "col-md-2", "Region"),
                        new ZPropertyProfile(false, true , 100, true , false, "col-md-1", "PostalCode"),
                        new ZPropertyProfile(false, true , 150, true , false, "col-md-2", "Country"),
                        new ZPropertyProfile(false, true , 200, true , false, "col-md-3", "HomePhone"),
                        new ZPropertyProfile(false, true ,  50, true , false, "col-md-1", "Extension"),
                        new ZPropertyProfile(false, false, 100, true , false, "col-md-2", "Photo"),
                        new ZPropertyProfile(false, true , 200, true , false, "col-md-2", "Notes"),
                        new ZPropertyProfile(false, false,  50, true , false, "col-md-1", "ReportsTo"),
                        new ZPropertyProfile(true , true , 200, false, false, "col-md-4", "EmployeeLookupText"),
                        new ZPropertyProfile(false, true , 200, true , false, "col-md-4", "PhotoPath")
                    }
                };
            }
        }

        #endregion Dictionaries
    }
}
