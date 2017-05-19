using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Data
{
    public partial class ProductDTO : ZDTOBase<ProductDTO, Product>
    {
        #region Properties
               
        public virtual int ProductId { get; set; }
               
        public virtual string ProductName { get; set; }
               
        public virtual int? SupplierId { get; set; }
               
        public virtual int? CategoryId { get; set; }
               
        public virtual string QuantityPerUnit { get; set; }
               
        public virtual decimal? UnitPrice { get; set; }
               
        public virtual short? UnitsInStock { get; set; }
               
        public virtual short? UnitsOnOrder { get; set; }
               
        public virtual short? ReorderLevel { get; set; }
               
        public virtual bool Discontinued { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string CategoryLookupText { get; set; } // CategoryId

        public virtual string SupplierLookupText { get; set; } // SupplierId
    
        #endregion Associations FK

        #region Methods
        
        public ProductDTO()
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
            CategoryLookupText = null;
            SupplierLookupText = null;
            LookupText = null;
        }
        
        public ProductDTO(
            int productId,
            string productName,
            bool discontinued,
            int? supplierId = null,
            int? categoryId = null,
            string quantityPerUnit = null,
            decimal? unitPrice = null,
            short? unitsInStock = null,
            short? unitsOnOrder = null,
            short? reorderLevel = null,
            string categoryLookupText = null,
            string supplierLookupText = null
        )
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
            CategoryLookupText = categoryLookupText;
            SupplierLookupText = supplierLookupText;
            LookupText = null;
        }

        public ProductDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<ProductDTO, Product> GetDataSelector()
        {
            return x => new Product
            (
                x.ProductId,
                x.ProductName,
                x.Discontinued,
                x.SupplierId,
                x.CategoryId,
                x.QuantityPerUnit,
                x.UnitPrice,
                x.UnitsInStock,
                x.UnitsOnOrder,
                x.ReorderLevel
            );
        }

        public override Func<Product, ProductDTO> GetDTOSelector()
        {
            return x => new ProductDTO
            (
                x.ProductId,
                x.ProductName,
                x.Discontinued,
                x.SupplierId,
                x.CategoryId,
                x.QuantityPerUnit,
                x.UnitPrice,
                x.UnitsInStock,
                x.UnitsOnOrder,
                x.ReorderLevel
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                Product product = (Product)data;
                ProductDTO dto = (new List<Product> { product })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                dto.CategoryLookupText = product.Category == null ? null : product.Category.LookupText;
                dto.SupplierLookupText = product.Supplier == null ? null : product.Supplier.LookupText;
                dto.LookupText = product.LookupText;

                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<ProductDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
