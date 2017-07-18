using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class ProductItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterCategoryId != null || MasterSupplierId != null; }
        }

        public int? MasterCategoryId { get; set; }

        public int? MasterSupplierId { get; set; }

        public ProductViewModel Product { get; set; }

        #endregion Properties
        
        #region Methods

        public ProductItemModel()
            : base()
        {
            Product = new ProductViewModel();
        }

        public ProductItemModel(ZActivityOperations activityOperations, string controllerAction, int? masterCategoryId = null, int? masterSupplierId = null, ProductViewModel product = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterCategoryId = masterCategoryId;
            MasterSupplierId = masterSupplierId;
            Product = product ?? Product;
        }
        
        #endregion Methods
    }
}
