using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Category
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
                        Name: "Category",
                        IsIdentity: true,
                        Keys: new string[] { "CategoryId" },
                        Lookup: "CategoryName",
                        Associations: new string[] { },
                        CollectionsDictionary: new Dictionary<string, bool>
                        {
                            { "Products", true },
                        },
                        LINQOrderBy: "CategoryName",
                        LINQWhere: "CategoryId == @0"
                    ),
                    Properties = new List<IZPropertyProfile>
                    {
                        //                   Grd    Grd    Grd  Edt    Edt    Edt
                        //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                        new ZPropertyProfile(false, true ,  50, false, false, "col-md-1", "CategoryId"),
                        new ZPropertyProfile(true , true , 150, true , false, "col-md-2", "CategoryName"),
                        new ZPropertyProfile(true , true , 200, true , false, "col-md-2", "Description"),
                        new ZPropertyProfile(false, false, 100, true , false, "col-md-2", "Picture")
                    }
                };
            }
        }

        #endregion Dictionaries
    }
}
