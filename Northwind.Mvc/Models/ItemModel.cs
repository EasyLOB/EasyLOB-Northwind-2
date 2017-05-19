namespace EasyLOB.Mvc
{
    public partial class ItemModel
    {
        #region Properties
        
        public virtual ZOperationResult OperationResult { get; set; }

        public virtual ZActivityOperations ActivityOperations { get; set; }

        public virtual string ControllerAction { get; set; }

        public virtual bool IsMasterDetail { get { return false; } }

        public bool IsReadOnly { get; set; }

        private bool _isSave = false;

        public bool IsSave
        {
            get { return _isSave; }
            set
            {
                _isSave = IsMasterDetail ? false : value;
            }
        }

        #endregion Properties

        #region Methods

        public ItemModel()
        {
            OperationResult = new ZOperationResult();
            IsReadOnly = false;
            IsSave = false;
        }

        #endregion Methods
    }
}
