using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class EmployeeCollectionModel : CollectionModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterReportsTo != null; }
        }

        public int? MasterReportsTo { get; set; }

        #endregion Properties
        
        #region Methods

        public EmployeeCollectionModel()
            : base()
        {
        }

        public EmployeeCollectionModel(ZActivityOperations activityOperations, string controllerAction, string masterControllerAction = null, int? masterReportsTo = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterControllerAction = masterControllerAction;
            MasterReportsTo = masterReportsTo;
        }

        #endregion Methods
    }
}
