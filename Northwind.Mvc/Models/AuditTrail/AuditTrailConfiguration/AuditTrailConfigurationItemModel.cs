using EasyLOB;
using EasyLOB.Mvc;

namespace EasyLOB.AuditTrail.Mvc
{
    public partial class AuditTrailConfigurationItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        public AuditTrailConfigurationViewModel AuditTrailConfiguration { get; set; }

        #endregion Properties
        
        #region Methods

        public AuditTrailConfigurationItemModel()
            : base()
        {
            AuditTrailConfiguration = new AuditTrailConfigurationViewModel();
        }

        public AuditTrailConfigurationItemModel(ZActivityOperations activityOperations, string controllerAction, AuditTrailConfigurationViewModel auditTrailConfiguration = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            AuditTrailConfiguration = auditTrailConfiguration ?? AuditTrailConfiguration;
        }
        
        #endregion Methods
    }
}
