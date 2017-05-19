using EasyLOB;
using EasyLOB.Mvc;

namespace Northwind.Mvc
{
    public partial class CategoryItemModel : ItemModel
    {
        #region Properties

        public override bool IsMasterDetail
        {
            get { return false; }
        }

        public CategoryViewModel Category { get; set; }

        #endregion Properties
        
        #region Methods

        public CategoryItemModel()
            : base()
        {
            Category = new CategoryViewModel();
        }

        public CategoryItemModel(ZActivityOperations activityOperations, string controllerAction, CategoryViewModel category = null)
            : this()
        {
            ActivityOperations = activityOperations;
            ControllerAction = controllerAction;
            Category = category ?? Category;
        }
        
        #endregion Methods
    }
}
