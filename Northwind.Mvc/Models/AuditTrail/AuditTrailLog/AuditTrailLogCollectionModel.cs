using EasyLOB;
using EasyLOB.Mvc;

namespace EasyLOB.AuditTrail.Mvc
{
    public partial class AuditTrailLogCollectionModel : CollectionModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        #endregion Properties
        
        #region Methods

        public AuditTrailLogCollectionModel()
            : base()
        {
        }

        public AuditTrailLogCollectionModel(ZActivityOperations activityOperations, string controllerAction, string masterControllerAction = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterControllerAction = masterControllerAction;
        }

        #endregion Methods
    }
}
