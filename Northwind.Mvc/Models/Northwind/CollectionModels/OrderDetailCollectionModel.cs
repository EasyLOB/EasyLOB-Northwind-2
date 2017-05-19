using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class OrderDetailCollectionModel : CollectionModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterOrderId != null || MasterProductId != null; }
        }

        public int? MasterOrderId { get; set; }

        public int? MasterProductId { get; set; }

        #endregion Properties
        
        #region Methods

        public OrderDetailCollectionModel()
            : base()
        {
        }

        public OrderDetailCollectionModel(ZActivityOperations activityOperations, string controllerAction, string masterControllerAction = null, int? masterOrderId = null, int? masterProductId = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterControllerAction = masterControllerAction;
            MasterOrderId = masterOrderId;
            MasterProductId = masterProductId;
        }

        #endregion Methods
    }
}
