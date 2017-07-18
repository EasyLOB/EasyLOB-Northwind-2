using EasyLOB;
using EasyLOB.Mvc;

namespace EasyLOB.Identity.Mvc
{
    public partial class UserRoleCollectionModel : CollectionModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterRoleId != null || MasterUserId != null; }
        }

        public string MasterRoleId { get; set; }

        public string MasterUserId { get; set; }

        #endregion Properties
        
        #region Methods

        public UserRoleCollectionModel()
            : base()
        {
        }

        public UserRoleCollectionModel(ZActivityOperations activityOperations, string controllerAction, string masterControllerAction = null, string masterRoleId = null, string masterUserId = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterControllerAction = masterControllerAction;
            MasterRoleId = masterRoleId;
            MasterUserId = masterUserId;
        }

        #endregion Methods
    }
}
