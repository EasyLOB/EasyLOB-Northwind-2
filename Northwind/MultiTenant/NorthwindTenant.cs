namespace Northwind
{
    public class NorthwindTenant
    {
        #region Properties

        public string Name { get; set; }

        public string URL { get; set; }

        #endregion Properties

        #region Methods

        public NorthwindTenant()
        {
            Name = "";
            URL = "";
        }

        #endregion Methods
    }
}