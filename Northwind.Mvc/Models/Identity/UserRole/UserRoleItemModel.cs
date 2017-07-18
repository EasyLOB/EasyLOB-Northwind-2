using EasyLOB;
using EasyLOB.Mvc;
using EasyLOB.Identity.Data;

namespace EasyLOB.Identity.Mvc
{
    public partial class UserRoleItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterRoleId != null || MasterUserId != null; }
        }

        public string MasterRoleId { get; set; }

        public string MasterUserId { get; set; }

        public UserRoleViewModel UserRole { get; set; }

        #endregion Properties
        
        #region Methods

        public UserRoleItemModel()
            : base()
        {
            UserRole = new UserRoleViewModel();
        }

        public UserRoleItemModel(ZActivityOperations activityOperations, string controllerAction, string masterRoleId = null, string masterUserId = null, UserRoleViewModel userRole = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterRoleId = masterRoleId;
            MasterUserId = masterUserId;
            UserRole = userRole ?? UserRole;
        }
        
        #endregion Methods
    }
}
