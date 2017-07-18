using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class TerritoryItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterRegionId != null; }
        }

        public int? MasterRegionId { get; set; }

        public TerritoryViewModel Territory { get; set; }

        #endregion Properties
        
        #region Methods

        public TerritoryItemModel()
            : base()
        {
            Territory = new TerritoryViewModel();
        }

        public TerritoryItemModel(ZActivityOperations activityOperations, string controllerAction, int? masterRegionId = null, TerritoryViewModel territory = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterRegionId = masterRegionId;
            Territory = territory ?? Territory;
        }
        
        #endregion Methods
    }
}
