using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class OrderItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterCustomerId != null || MasterEmployeeId != null || MasterShipVia != null; }
        }

        public string MasterCustomerId { get; set; }

        public int? MasterEmployeeId { get; set; }

        public int? MasterShipVia { get; set; }

        public OrderViewModel Order { get; set; }

        #endregion Properties
        
        #region Methods

        public OrderItemModel()
            : base()
        {
            Order = new OrderViewModel();
        }

        public OrderItemModel(ZActivityOperations activityOperations, string controllerAction, string masterCustomerId = null, int? masterEmployeeId = null, int? masterShipVia = null, OrderViewModel order = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterCustomerId = masterCustomerId;
            MasterEmployeeId = masterEmployeeId;
            MasterShipVia = masterShipVia;
            Order = order ?? Order;
        }
        
        #endregion Methods
    }
}
