using EasyLOB;
using EasyLOB.Mvc;
using EasyLOB.AuditTrail.Data;

namespace EasyLOB.AuditTrail.Mvc
{
    public partial class AuditTrailLogItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        public AuditTrailLogViewModel AuditTrailLog { get; set; }

        #endregion Properties
        
        #region Methods

        public AuditTrailLogItemModel()
            : base()
        {
            AuditTrailLog = new AuditTrailLogViewModel();
        }

        public AuditTrailLogItemModel(ZActivityOperations activityOperations, string controllerAction, AuditTrailLogViewModel auditTrailLog = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            AuditTrailLog = auditTrailLog ?? AuditTrailLog;
        }
        
        #endregion Methods
    }
}
