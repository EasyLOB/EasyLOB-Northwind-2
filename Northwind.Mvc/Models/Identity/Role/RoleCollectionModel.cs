using EasyLOB;
using EasyLOB.Mvc;

namespace EasyLOB.Identity.Mvc
{
    public partial class RoleCollectionModel : CollectionModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        #endregion Properties
        
        #region Methods

        public RoleCollectionModel()
            : base()
        {
        }

        public RoleCollectionModel(ZActivityOperations activityOperations, string controllerAction, string masterControllerAction = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterControllerAction = masterControllerAction;
        }

        #endregion Methods
    }
}
