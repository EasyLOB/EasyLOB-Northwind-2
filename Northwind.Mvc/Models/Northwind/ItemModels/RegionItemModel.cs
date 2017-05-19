using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class RegionItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        public RegionViewModel Region { get; set; }

        #endregion Properties
        
        #region Methods

        public RegionItemModel()
            : base()
        {
            Region = new RegionViewModel();
        }

        public RegionItemModel(ZActivityOperations activityOperations, string controllerAction, RegionViewModel region = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            Region = region ?? Region;
        }
        
        #endregion Methods
    }
}
