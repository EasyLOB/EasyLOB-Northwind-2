using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class TerritoryCollectionModel : CollectionModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterRegionId != null; }
        }

        public int? MasterRegionId { get; set; }

        #endregion Properties
        
        #region Methods

        public TerritoryCollectionModel()
            : base()
        {
        }

        public TerritoryCollectionModel(ZActivityOperations activityOperations, string controllerAction, string masterControllerAction = null, int? masterRegionId = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterControllerAction = masterControllerAction;
            MasterRegionId = masterRegionId;
        }

        #endregion Methods
    }
}
