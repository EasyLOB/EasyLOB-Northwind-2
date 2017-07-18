using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class EmployeeTerritoryItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterEmployeeId != null || MasterTerritoryId != null; }
        }

        public int? MasterEmployeeId { get; set; }

        public string MasterTerritoryId { get; set; }

        public EmployeeTerritoryViewModel EmployeeTerritory { get; set; }

        #endregion Properties
        
        #region Methods

        public EmployeeTerritoryItemModel()
            : base()
        {
            EmployeeTerritory = new EmployeeTerritoryViewModel();
        }

        public EmployeeTerritoryItemModel(ZActivityOperations activityOperations, string controllerAction, int? masterEmployeeId = null, string masterTerritoryId = null, EmployeeTerritoryViewModel employeeTerritory = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterEmployeeId = masterEmployeeId;
            MasterTerritoryId = masterTerritoryId;
            EmployeeTerritory = employeeTerritory ?? EmployeeTerritory;
        }
        
        #endregion Methods
    }
}
