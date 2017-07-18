using EasyLOB;
using EasyLOB.Mvc;

namespace EasyLOB.Identity.Mvc
{
    public partial class UserLoginCollectionModel : CollectionModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterUserId != null; }
        }

        public string MasterUserId { get; set; }

        #endregion Properties
        
        #region Methods

        public UserLoginCollectionModel()
            : base()
        {
        }

        public UserLoginCollectionModel(ZActivityOperations activityOperations, string controllerAction, string masterControllerAction = null, string masterUserId = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterControllerAction = masterControllerAction;
            MasterUserId = masterUserId;
        }

        #endregion Methods
    }
}
