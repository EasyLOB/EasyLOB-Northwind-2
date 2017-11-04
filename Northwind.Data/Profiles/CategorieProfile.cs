using EasyLOB.Data;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Categorie
    {
        #region Profile

        public static IZProfile Profile { get; private set; } = new ZProfile
        (
            Name: "Categorie",
            IsIdentity: true,
            Keys: new List<string> { "CategoryId" },
            Lookup: "CategoryName",
            LINQOrderBy: "CategoryName",
            LINQWhere: "CategoryId == @0",
            Associations: new List<string> { },
            Collections: new Dictionary<string, bool>
            {
                    { "Products", true },
            },
            Properties: new List<IZProfileProperty>
            {
                //                   Grd    Grd    Grd  Edt    Edt    Edt
                //                   Vis    Src    Wdt  Vis    RO     CSS         Name
                new ZProfileProperty(false, true ,  50, false, false, "col-md-1", "CategoryId"),
                new ZProfileProperty(true , true , 150, true , false, "col-md-2", "CategoryName"),
                new ZProfileProperty(true , true , 200, true , false, "col-md-2", "Description"),
                new ZProfileProperty(false, false, 100, true , false, "col-md-2", "Picture")
            }
        );

        #endregion Profile
    }
}
