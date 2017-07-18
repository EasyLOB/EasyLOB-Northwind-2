using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class EmployeeItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterReportsTo != null; }
        }

        public int? MasterReportsTo { get; set; }

        public EmployeeViewModel Employee { get; set; }

        #endregion Properties
        
        #region Methods

        public EmployeeItemModel()
            : base()
        {
            Employee = new EmployeeViewModel();
        }

        public EmployeeItemModel(ZActivityOperations activityOperations, string controllerAction, int? masterReportsTo = null, EmployeeViewModel employee = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterReportsTo = masterReportsTo;
            Employee = employee ?? Employee;
        }
        
        #endregion Methods
    }
}
