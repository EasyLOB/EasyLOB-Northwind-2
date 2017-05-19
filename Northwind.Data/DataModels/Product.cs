using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Product : ZDataBase
    {        
        #region Properties
        
        public virtual int ProductId { get; set; }
        
        public virtual string ProductName { get; set; }
        
        private int? _supplierId;
        
        public virtual int? SupplierId
        {
            get { return this.Supplier == null ? _supplierId : this.Supplier.SupplierId; }
            set
            {
                _supplierId = value;
                Supplier = null;
            }
        }
        
        private int? _categoryId;
        
        public virtual int? CategoryId
        {
            get { return this.Category == null ? _categoryId : this.Category.CategoryId; }
            set
            {
                _categoryId = value;
                Category = null;
            }
        }
        
        public virtual string QuantityPerUnit { get; set; }
        
        public virtual decimal? UnitPrice { get; set; }
        
        public virtual short? UnitsInStock { get; set; }
        
        public virtual short? UnitsOnOrder { get; set; }
        
        public virtual short? ReorderLevel { get; set; }
        
        public virtual bool Discontinued { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual Category Category { get; set; } // CategoryId

        public virtual Supplier Supplier { get; set; } // SupplierId

        #endregion Associations FK

        #region Collections (PK)

        public virtual IList<OrderDetail> OrderDetails { get; }

        #endregion Collections (PK)

        #region Methods
        
        public Product()
        {            
            ProductId = LibraryDefaults.Default_Int32;
            ProductName = LibraryDefaults.Default_String;
            Discontinued = LibraryDefaults.Default_Boolean;
            SupplierId = null;
            CategoryId = null;
            QuantityPerUnit = null;
            UnitPrice = null;
            UnitsInStock = null;
            UnitsOnOrder = null;
            ReorderLevel = null;

            //Category = new Category();
            //Supplier = new Supplier();

            OrderDetails = new List<OrderDetail>();
    
            OnConstructor();
        }

        public Product(int productId)
            : this()
        {            
            ProductId = productId;
        }

        public Product(
            int productId,
            string productName,
            bool discontinued,
            int? supplierId = null,
            int? categoryId = null,
            string quantityPerUnit = null,
            decimal? unitPrice = null,
            short? unitsInStock = null,
            short? unitsOnOrder = null,
            short? reorderLevel = null
        )
            : this()
        {
            ProductId = productId;
            ProductName = productName;
            SupplierId = supplierId;
            CategoryId = categoryId;
            QuantityPerUnit = quantityPerUnit;
            UnitPrice = unitPrice;
            UnitsInStock = unitsInStock;
            UnitsOnOrder = unitsOnOrder;
            ReorderLevel = reorderLevel;
            Discontinued = discontinued;
        }

        public override object[] GetId()
        {
            return new object[] { ProductId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                ProductId = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}
