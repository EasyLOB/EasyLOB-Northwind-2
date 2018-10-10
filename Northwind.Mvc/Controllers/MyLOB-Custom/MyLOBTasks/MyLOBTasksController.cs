using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Mvc;

namespace MyLOB.Mvc
{
    public partial class MyLOBTasksController : BaseMvcControllerTask
    {
        #region Properties

        protected IMyLOBApplication Application { get; }

        protected override string Domain
        {
            get { return AppDefaults.Domain; }
        }

        #endregion Properties

        #region Methods

        public MyLOBTasksController(IMyLOBApplication application)
        {
            Application = application;
        }

        protected override bool IsValid(ZOperationResult operationResult, IZValidatableObject entity)
        {
            bool result = base.IsValid(operationResult, entity);

            if (!result)
            {
                operationResult.Clear(); // Html.BeginForm() + Html.ValidationSummary()
            }

            return result;
        }

        #endregion Methods
    }
}
 