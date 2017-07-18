using EasyLOB;
using EasyLOB.Mvc;

namespace EasyLOB.Activity.Mvc
{
    public partial class ActivityRoleCollectionModel : CollectionModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterActivityId != null; }
        }

        public string MasterActivityId { get; set; }

        public string MasterRoleName { get; set; } // !?!

        #endregion Properties

        #region Methods

        public ActivityRoleCollectionModel()
            : base()
        {
        }

        public ActivityRoleCollectionModel(ZActivityOperations activityOperations, string controllerAction, string masterControllerAction = null, string masterActivityId = null, string masterRoleName = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterControllerAction = masterControllerAction;
            MasterActivityId = masterActivityId;
            MasterRoleName = masterRoleName; // !?!
        }

        #endregion Methods
    }
}
