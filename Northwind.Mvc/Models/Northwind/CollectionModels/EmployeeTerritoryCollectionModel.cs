using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class EmployeeTerritoryCollectionModel : CollectionModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterEmployeeId != null || MasterTerritoryId != null; }
        }

        public int? MasterEmployeeId { get; set; }

        public string MasterTerritoryId { get; set; }

        #endregion Properties
        
        #region Methods

        public EmployeeTerritoryCollectionModel()
            : base()
        {
        }

        public EmployeeTerritoryCollectionModel(ZActivityOperations activityOperations, string controllerAction, string masterControllerAction = null, int? masterEmployeeId = null, string masterTerritoryId = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterControllerAction = masterControllerAction;
            MasterEmployeeId = masterEmployeeId;
            MasterTerritoryId = masterTerritoryId;
        }

        #endregion Methods
    }
}
