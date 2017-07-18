using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class CustomerDemographicCollectionModel : CollectionModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        #endregion Properties
        
        #region Methods

        public CustomerDemographicCollectionModel()
            : base()
        {
        }

        public CustomerDemographicCollectionModel(ZActivityOperations activityOperations, string controllerAction, string masterControllerAction = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterControllerAction = masterControllerAction;
        }

        #endregion Methods
    }
}
