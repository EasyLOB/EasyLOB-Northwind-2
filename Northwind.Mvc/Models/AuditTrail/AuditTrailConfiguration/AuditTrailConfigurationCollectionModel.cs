using EasyLOB;
using EasyLOB.Mvc;

namespace EasyLOB.AuditTrail.Mvc
{
    public partial class AuditTrailConfigurationCollectionModel : CollectionModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        #endregion Properties
        
        #region Methods

        public AuditTrailConfigurationCollectionModel()
            : base()
        {
        }

        public AuditTrailConfigurationCollectionModel(ZActivityOperations activityOperations, string controllerAction, string masterControllerAction = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterControllerAction = masterControllerAction;
        }

        #endregion Methods
    }
}
