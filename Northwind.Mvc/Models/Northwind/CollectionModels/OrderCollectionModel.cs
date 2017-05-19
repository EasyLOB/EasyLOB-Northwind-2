using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class OrderCollectionModel : CollectionModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterCustomerId != null || MasterEmployeeId != null || MasterShipVia != null; }
        }

        public string MasterCustomerId { get; set; }

        public int? MasterEmployeeId { get; set; }

        public int? MasterShipVia { get; set; }

        #endregion Properties
        
        #region Methods

        public OrderCollectionModel()
            : base()
        {
        }

        public OrderCollectionModel(ZActivityOperations activityOperations, string controllerAction, string masterControllerAction = null, string masterCustomerId = null, int? masterEmployeeId = null, int? masterShipVia = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterControllerAction = masterControllerAction;
            MasterCustomerId = masterCustomerId;
            MasterEmployeeId = masterEmployeeId;
            MasterShipVia = masterShipVia;
        }

        #endregion Methods
    }
}
