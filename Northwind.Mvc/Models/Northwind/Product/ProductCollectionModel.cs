using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class ProductCollectionModel : CollectionModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return MasterCategoryId != null || MasterSupplierId != null; }
        }

        public int? MasterCategoryId { get; set; }

        public int? MasterSupplierId { get; set; }

        #endregion Properties
        
        #region Methods

        public ProductCollectionModel()
            : base()
        {
        }

        public ProductCollectionModel(ZActivityOperations activityOperations, string controllerAction, string masterControllerAction = null, int? masterCategoryId = null, int? masterSupplierId = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            MasterControllerAction = masterControllerAction;
            MasterCategoryId = masterCategoryId;
            MasterSupplierId = masterSupplierId;
        }

        #endregion Methods
    }
}
