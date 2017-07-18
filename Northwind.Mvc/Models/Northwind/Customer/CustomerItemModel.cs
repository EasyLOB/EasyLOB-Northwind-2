using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class CustomerItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        public CustomerViewModel Customer { get; set; }

        #endregion Properties
        
        #region Methods

        public CustomerItemModel()
            : base()
        {
            Customer = new CustomerViewModel();
        }

        public CustomerItemModel(ZActivityOperations activityOperations, string controllerAction, CustomerViewModel customer = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            Customer = customer ?? Customer;
        }
        
        #endregion Methods
    }
}
