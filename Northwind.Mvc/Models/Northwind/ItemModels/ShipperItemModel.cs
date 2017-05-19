using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class ShipperItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        public ShipperViewModel Shipper { get; set; }

        #endregion Properties
        
        #region Methods

        public ShipperItemModel()
            : base()
        {
            Shipper = new ShipperViewModel();
        }

        public ShipperItemModel(ZActivityOperations activityOperations, string controllerAction, ShipperViewModel shipper = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            Shipper = shipper ?? Shipper;
        }
        
        #endregion Methods
    }
}
