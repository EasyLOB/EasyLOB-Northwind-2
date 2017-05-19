namespace EasyLOB.Mvc
{
    public partial class CollectionModel
    {
        #region Properties
            
        public virtual ZOperationResult OperationResult { get; set; }

        public virtual ZActivityOperations ActivityOperations { get; set; }

        public virtual string ControllerAction { get; set; }

        public virtual string MasterControllerAction { get; set; }

        public virtual bool IsMasterDetail { get { return false; } }

        #endregion Properties
        
        #region Methods

        public CollectionModel()
        {
            OperationResult = new ZOperationResult();
        }

        #endregion Methods
    }
}
