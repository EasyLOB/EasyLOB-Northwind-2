using EasyLOB;
using EasyLOB.Mvc;
using EasyLOB.Identity.Data;

namespace EasyLOB.Identity.Mvc
{
    public partial class RoleItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        public RoleViewModel Role { get; set; }

        #endregion Properties
        
        #region Methods

        public RoleItemModel()
            : base()
        {
            Role = new RoleViewModel();
        }

        public RoleItemModel(ZActivityOperations activityOperations, string controllerAction, RoleViewModel role = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            Role = role ?? Role;
        }
        
        #endregion Methods
    }
}
