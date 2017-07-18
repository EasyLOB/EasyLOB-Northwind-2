using EasyLOB;
using EasyLOB.Mvc;
using EasyLOB.Activity.Data;

namespace EasyLOB.Activity.Mvc
{
    public partial class ActivityRoleItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterActivityId != null; }
        }

        public string MasterActivityId { get; set; }

        public string MasterRoleName { get; set; } // !?!

        public ActivityRoleViewModel ActivityRole { get; set; }

        #endregion Properties
        
        #region Methods

        public ActivityRoleItemModel()
            : base()
        {
            ActivityRole = new ActivityRoleViewModel();
        }

        public ActivityRoleItemModel(ZActivityOperations activityOperations, string controllerAction, string masterActivityId = null, string masterRoleName = null, ActivityRoleViewModel activityRole = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterActivityId = masterActivityId;
            MasterRoleName = masterRoleName; // !?!
            ActivityRole = activityRole ?? ActivityRole;
        }
        
        #endregion Methods
    }
}
