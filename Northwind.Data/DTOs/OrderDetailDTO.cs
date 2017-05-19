using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Data
{
    public partial class OrderDetailDTO : ZDTOBase<OrderDetailDTO, OrderDetail>
    {
        #region Properties
               
        public virtual int OrderId { get; set; }
               
        public virtual int ProductId { get; set; }
               
        public virtual decimal UnitPrice { get; set; }
               
        public virtual short Quantity { get; set; }
               
        public virtual float Discount { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string OrderLookupText { get; set; } // OrderId

        public virtual string ProductLookupText { get; set; } // ProductId
    
        #endregion Associations FK

        #region Methods
        
        public OrderDetailDTO()
        {
            OrderId = LibraryDefaults.Default_Int32;
            ProductId = LibraryDefaults.Default_Int32;
            UnitPrice = LibraryDefaults.Default_Decimal;
            Quantity = LibraryDefaults.Default_Int16;
            Discount = LibraryDefaults.Default_Single;
            OrderLookupText = null;
            ProductLookupText = null;
            LookupText = null;
        }
        
        public OrderDetailDTO(
            int orderId,
            int productId,
            decimal unitPrice,
            short quantity,
            float discount,
            string orderLookupText = null,
            string productLookupText = null
        )
        {
            OrderId = orderId;
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Discount = discount;
            OrderLookupText = orderLookupText;
            ProductLookupText = productLookupText;
            LookupText = null;
        }

        public OrderDetailDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<OrderDetailDTO, OrderDetail> GetDataSelector()
        {
            return x => new OrderDetail
            (
                x.OrderId,
                x.ProductId,
                x.UnitPrice,
                x.Quantity,
                x.Discount
            );
        }

        public override Func<OrderDetail, OrderDetailDTO> GetDTOSelector()
        {
            return x => new OrderDetailDTO
            (
                x.OrderId,
                x.ProductId,
                x.UnitPrice,
                x.Quantity,
                x.Discount
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                OrderDetail orderDetail = (OrderDetail)data;
                OrderDetailDTO dto = (new List<OrderDetail> { orderDetail })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                dto.OrderLookupText = orderDetail.Order == null ? null : orderDetail.Order.LookupText;
                dto.ProductLookupText = orderDetail.Product == null ? null : orderDetail.Product.LookupText;
                dto.LookupText = orderDetail.LookupText;

                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<OrderDetailDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
