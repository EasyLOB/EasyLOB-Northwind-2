using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class CustomerDemographicItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        public CustomerDemographicViewModel CustomerDemographic { get; set; }

        #endregion Properties
        
        #region Methods

        public CustomerDemographicItemModel()
            : base()
        {
            CustomerDemographic = new CustomerDemographicViewModel();
        }

        public CustomerDemographicItemModel(ZActivityOperations activityOperations, string controllerAction, CustomerDemographicViewModel customerDemographic = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            CustomerDemographic = customerDemographic ?? CustomerDemographic;
        }
        
        #endregion Methods
    }
}
