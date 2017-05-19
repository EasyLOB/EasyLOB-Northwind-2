using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class OrderDetail : ZDataBase
    {        
        #region Properties
        
        private int _orderId;
        
        public virtual int OrderId
        {
            get { return this.Order == null ? _orderId : this.Order.OrderId; }
            set
            {
                _orderId = value;
                Order = null;
            }
        }
        
        private int _productId;
        
        public virtual int ProductId
        {
            get { return this.Product == null ? _productId : this.Product.ProductId; }
            set
            {
                _productId = value;
                Product = null;
            }
        }
        
        public virtual decimal UnitPrice { get; set; }
        
        public virtual short Quantity { get; set; }
        
        public virtual float Discount { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual Order Order { get; set; } // OrderId

        public virtual Product Product { get; set; } // ProductId

        #endregion Associations FK

        #region Methods
        
        public OrderDetail()
        {            
            OrderId = LibraryDefaults.Default_Int32;
            ProductId = LibraryDefaults.Default_Int32;
            UnitPrice = LibraryDefaults.Default_Decimal;
            Quantity = LibraryDefaults.Default_Int16;
            Discount = LibraryDefaults.Default_Single;

            //Order = new Order();
            //Product = new Product();
    
            OnConstructor();
        }

        public OrderDetail(int orderId, int productId)
            : this()
        {            
            OrderId = orderId;
            ProductId = productId;
        }

        public OrderDetail(
            int orderId,
            int productId,
            decimal unitPrice,
            short quantity,
            float discount
        )
            : this()
        {
            OrderId = orderId;
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Discount = discount;
        }

        public override object[] GetId()
        {
            return new object[] { OrderId, ProductId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                OrderId = DataHelper.IdToInt32(ids[0]);
            }
            if (ids != null && ids[1] != null)
            {
                ProductId = DataHelper.IdToInt32(ids[1]);
            }
        }

        #endregion Methods

        #region Methods NHibernate

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is OrderDetail)
            {
                var orderDetail = (OrderDetail)obj;
                if (orderDetail == null)
                {
                    return false;
                }

                if (OrderId == orderDetail.OrderId && ProductId == orderDetail.ProductId)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (OrderId.ToString() + "|" + ProductId.ToString()).GetHashCode();
        }

        #endregion Methods NHibernate
    }
}
