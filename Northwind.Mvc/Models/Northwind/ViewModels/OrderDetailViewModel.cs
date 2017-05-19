using Northwind.Data;
using Northwind.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Northwind.Mvc
{
    public partial class OrderDetailViewModel : ZViewBase<OrderDetailViewModel, OrderDetailDTO, OrderDetail>
    {
        #region Properties
        
        [Display(Name = "PropertyOrderId", ResourceType = typeof(OrderDetailResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        //[Column(Order=1)]
        [Range(1, System.Int32.MaxValue, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(DataAnnotationResources))]
        [Required]
        public virtual int OrderId { get; set; }
        
        [Display(Name = "PropertyProductId", ResourceType = typeof(OrderDetailResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        //[Column(Order=2)]
        [Range(1, System.Int32.MaxValue, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(DataAnnotationResources))]
        [Required]
        public virtual int ProductId { get; set; }
        
        [Display(Name = "PropertyUnitPrice", ResourceType = typeof(OrderDetailResources))]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual decimal UnitPrice { get; set; }
        
        [Display(Name = "PropertyQuantity", ResourceType = typeof(OrderDetailResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual short Quantity { get; set; }
        
        [Display(Name = "PropertyDiscount", ResourceType = typeof(OrderDetailResources))]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual float Discount { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string OrderLookupText { get; set; } // OrderId

        public virtual string ProductLookupText { get; set; } // ProductId
    
        #endregion Associations FK

        #region Methods
        
        public OrderDetailViewModel()
        {
            OrderId = LibraryDefaults.Default_Int32;
            ProductId = LibraryDefaults.Default_Int32;
            UnitPrice = LibraryDefaults.Default_Decimal;
            Quantity = LibraryDefaults.Default_Int16;
            Discount = LibraryDefaults.Default_Single;
            OrderLookupText = null;
            ProductLookupText = null;
            LookupText = null;

            OnConstructor();
        }

        public OrderDetailViewModel(
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

        public OrderDetailViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public OrderDetailViewModel(IZDTOBase<OrderDetailDTO, OrderDetail> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<OrderDetailViewModel, OrderDetailDTO> GetDTOSelector()
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

        public override Func<OrderDetailDTO, OrderDetailViewModel> GetViewSelector()
        {
            return x => new OrderDetailViewModel
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
                OrderDetailDTO orderDetailDTO = new OrderDetailDTO(data);
                OrderDetailViewModel view = (new List<OrderDetailDTO> { orderDetailDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.OrderLookupText = orderDetailDTO.OrderLookupText;
                view.ProductLookupText = orderDetailDTO.ProductLookupText;
                view.LookupText = orderDetailDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<OrderDetailDTO, OrderDetail> dto)
        {
            if (dto != null)
            {
                OrderDetailDTO orderDetailDTO = (OrderDetailDTO)dto;
                OrderDetailViewModel view = (new List<OrderDetailDTO> { orderDetailDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.OrderLookupText = orderDetailDTO.OrderLookupText;
                view.ProductLookupText = orderDetailDTO.ProductLookupText;
                view.LookupText = orderDetailDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<OrderDetailDTO, OrderDetail> ToDTO()
        {
            return (new List<OrderDetailViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
