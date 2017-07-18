using EasyLOB;
using EasyLOB.Mvc;
using EasyLOB.Identity.Data;

namespace EasyLOB.Identity.Mvc
{
    public partial class UserItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        public UserViewModel User { get; set; }

        #endregion Properties
        
        #region Methods

        public UserItemModel()
            : base()
        {
            User = new UserViewModel();
        }

        public UserItemModel(ZActivityOperations activityOperations, string controllerAction, UserViewModel user = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            User = user ?? User;
        }
        
        #endregion Methods
    }
}
