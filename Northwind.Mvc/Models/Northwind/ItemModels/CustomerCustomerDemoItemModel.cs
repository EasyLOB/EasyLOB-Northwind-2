using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class CustomerCustomerDemoItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterCustomerTypeId != null || MasterCustomerId != null; }
        }

        public string MasterCustomerTypeId { get; set; }

        public string MasterCustomerId { get; set; }

        public CustomerCustomerDemoViewModel CustomerCustomerDemo { get; set; }

        #endregion Properties
        
        #region Methods

        public CustomerCustomerDemoItemModel()
            : base()
        {
            CustomerCustomerDemo = new CustomerCustomerDemoViewModel();
        }

        public CustomerCustomerDemoItemModel(ZActivityOperations activityOperations, string controllerAction, string masterCustomerTypeId = null, string masterCustomerId = null, CustomerCustomerDemoViewModel customerCustomerDemo = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterCustomerTypeId = masterCustomerTypeId;
            MasterCustomerId = masterCustomerId;
            CustomerCustomerDemo = customerCustomerDemo ?? CustomerCustomerDemo;
        }
        
        #endregion Methods
    }
}
