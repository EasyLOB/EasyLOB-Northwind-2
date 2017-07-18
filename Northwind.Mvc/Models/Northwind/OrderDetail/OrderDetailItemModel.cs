using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class OrderDetailItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterOrderId != null || MasterProductId != null; }
        }

        public int? MasterOrderId { get; set; }

        public int? MasterProductId { get; set; }

        public OrderDetailViewModel OrderDetail { get; set; }

        #endregion Properties
        
        #region Methods

        public OrderDetailItemModel()
            : base()
        {
            OrderDetail = new OrderDetailViewModel();
        }

        public OrderDetailItemModel(ZActivityOperations activityOperations, string controllerAction, int? masterOrderId = null, int? masterProductId = null, OrderDetailViewModel orderDetail = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterOrderId = masterOrderId;
            MasterProductId = masterProductId;
            OrderDetail = orderDetail ?? OrderDetail;
        }
        
        #endregion Methods
    }
}
