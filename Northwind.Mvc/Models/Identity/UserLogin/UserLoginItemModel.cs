using EasyLOB;
using EasyLOB.Mvc;
using EasyLOB.Identity.Data;

namespace EasyLOB.Identity.Mvc
{
    public partial class UserLoginItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterUserId != null; }
        }

        public string MasterUserId { get; set; }

        public UserLoginViewModel UserLogin { get; set; }

        #endregion Properties
        
        #region Methods

        public UserLoginItemModel()
            : base()
        {
            UserLogin = new UserLoginViewModel();
        }

        public UserLoginItemModel(ZActivityOperations activityOperations, string controllerAction, string masterUserId = null, UserLoginViewModel userLogin = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterUserId = masterUserId;
            UserLogin = userLogin ?? UserLogin;
        }
        
        #endregion Methods
    }
}
