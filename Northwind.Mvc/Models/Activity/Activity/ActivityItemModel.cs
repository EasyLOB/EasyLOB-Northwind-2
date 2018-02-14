using EasyLOB;
using EasyLOB.Mvc;

namespace EasyLOB.Activity.Mvc
{
    public partial class ActivityItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        public ActivityViewModel Activity { get; set; }

        #endregion Properties
        
        #region Methods

        public ActivityItemModel()
            : base()
        {
            Activity = new ActivityViewModel();
        }

        public ActivityItemModel(ZActivityOperations activityOperations, string controllerAction, ActivityViewModel activity = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            Activity = activity ?? Activity;
        }
        
        #endregion Methods
    }
}
