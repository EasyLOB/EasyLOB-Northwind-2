using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class SupplierItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        public SupplierViewModel Supplier { get; set; }

        #endregion Properties
        
        #region Methods

        public SupplierItemModel()
            : base()
        {
            Supplier = new SupplierViewModel();
        }

        public SupplierItemModel(ZActivityOperations activityOperations, string controllerAction, SupplierViewModel supplier = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            Supplier = supplier ?? Supplier;
        }
        
        #endregion Methods
    }
}
