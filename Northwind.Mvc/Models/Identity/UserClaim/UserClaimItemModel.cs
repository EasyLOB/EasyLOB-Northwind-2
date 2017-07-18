using EasyLOB;
using EasyLOB.Mvc;
using EasyLOB.Identity.Data;

namespace EasyLOB.Identity.Mvc
{
    public partial class UserClaimItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterUserId != null; }
        }

        public string MasterUserId { get; set; }

        public UserClaimViewModel UserClaim { get; set; }

        #endregion Properties
        
        #region Methods

        public UserClaimItemModel()
            : base()
        {
            UserClaim = new UserClaimViewModel();
        }

        public UserClaimItemModel(ZActivityOperations activityOperations, string controllerAction, string masterUserId = null, UserClaimViewModel userClaim = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterUserId = masterUserId;
            UserClaim = userClaim ?? UserClaim;
        }
        
        #endregion Methods
    }
}
