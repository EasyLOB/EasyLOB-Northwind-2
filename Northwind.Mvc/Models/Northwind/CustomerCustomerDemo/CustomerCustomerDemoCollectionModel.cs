using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class CustomerCustomerDemoCollectionModel : CollectionModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterCustomerTypeId != null || MasterCustomerId != null; }
        }

        public string MasterCustomerTypeId { get; set; }

        public string MasterCustomerId { get; set; }

        #endregion Properties
        
        #region Methods

        public CustomerCustomerDemoCollectionModel()
            : base()
        {
        }

        public CustomerCustomerDemoCollectionModel(ZActivityOperations activityOperations, string controllerAction, string masterControllerAction = null, string masterCustomerTypeId = null, string masterCustomerId = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterControllerAction = masterControllerAction;
            MasterCustomerTypeId = masterCustomerTypeId;
            MasterCustomerId = masterCustomerId;
        }

        #endregion Methods
    }
}
