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
    public partial class OrderViewModel : ZViewBase<OrderViewModel, OrderDTO, Order>
    {
        #region Properties
        
        [Display(Name = "PropertyOrderId", ResourceType = typeof(OrderResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int OrderId { get; set; }
        
        [Display(Name = "PropertyCustomerId", ResourceType = typeof(OrderResources))]
        [StringLength(5)]
        public virtual string CustomerId { get; set; }
        
        [Display(Name = "PropertyEmployeeId", ResourceType = typeof(OrderResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public virtual int? EmployeeId { get; set; }
        
        [Display(Name = "PropertyOrderDate", ResourceType = typeof(OrderResources))]
        //[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        public virtual DateTime? OrderDate { get; set; }
        
        [Display(Name = "PropertyRequiredDate", ResourceType = typeof(OrderResources))]
        //[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        public virtual DateTime? RequiredDate { get; set; }
        
        [Display(Name = "PropertyShippedDate", ResourceType = typeof(OrderResources))]
        //[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        public virtual DateTime? ShippedDate { get; set; }
        
        [Display(Name = "PropertyShipVia", ResourceType = typeof(OrderResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public virtual int? ShipVia { get; set; }
        
        [Display(Name = "PropertyFreight", ResourceType = typeof(OrderResources))]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        public virtual decimal? Freight { get; set; }
        
        [Display(Name = "PropertyShipName", ResourceType = typeof(OrderResources))]
        [StringLength(40)]
        public virtual string ShipName { get; set; }
        
        [Display(Name = "PropertyShipAddress", ResourceType = typeof(OrderResources))]
        [StringLength(60)]
        public virtual string ShipAddress { get; set; }
        
        [Display(Name = "PropertyShipCity", ResourceType = typeof(OrderResources))]
        [StringLength(15)]
        public virtual string ShipCity { get; set; }
        
        [Display(Name = "PropertyShipRegion", ResourceType = typeof(OrderResources))]
        [StringLength(15)]
        public virtual string ShipRegion { get; set; }
        
        [Display(Name = "PropertyShipPostalCode", ResourceType = typeof(OrderResources))]
        [StringLength(10)]
        public virtual string ShipPostalCode { get; set; }
        
        [Display(Name = "PropertyShipCountry", ResourceType = typeof(OrderResources))]
        [StringLength(15)]
        public virtual string ShipCountry { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string CustomerLookupText { get; set; } // CustomerId

        public virtual string EmployeeLookupText { get; set; } // EmployeeId

        public virtual string ShipperLookupText { get; set; } // ShipVia
    
        #endregion Associations FK

        #region Methods
        
        public OrderViewModel()
        {
            OrderId = LibraryDefaults.Default_Int32;
            CustomerId = null;
            EmployeeId = null;
            OrderDate = null;
            RequiredDate = null;
            ShippedDate = null;
            ShipVia = null;
            Freight = null;
            ShipName = null;
            ShipAddress = null;
            ShipCity = null;
            ShipRegion = null;
            ShipPostalCode = null;
            ShipCountry = null;
            CustomerLookupText = null;
            EmployeeLookupText = null;
            ShipperLookupText = null;
            LookupText = null;

            OnConstructor();
        }

        public OrderViewModel(
            int orderId,
            string customerId = null,
            int? employeeId = null,
            DateTime? orderDate = null,
            DateTime? requiredDate = null,
            DateTime? shippedDate = null,
            int? shipVia = null,
            decimal? freight = null,
            string shipName = null,
            string shipAddress = null,
            string shipCity = null,
            string shipRegion = null,
            string shipPostalCode = null,
            string shipCountry = null,
            string customerLookupText = null,
            string employeeLookupText = null,
            string shipperLookupText = null
        )
        {
            OrderId = orderId;
            CustomerId = customerId;
            EmployeeId = employeeId;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            ShipVia = shipVia;
            Freight = freight;
            ShipName = shipName;
            ShipAddress = shipAddress;
            ShipCity = shipCity;
            ShipRegion = shipRegion;
            ShipPostalCode = shipPostalCode;
            ShipCountry = shipCountry;
            CustomerLookupText = customerLookupText;
            EmployeeLookupText = employeeLookupText;
            ShipperLookupText = shipperLookupText;
            LookupText = null;
        }

        public OrderViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public OrderViewModel(IZDTOBase<OrderDTO, Order> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<OrderViewModel, OrderDTO> GetDTOSelector()
        {
            return x => new OrderDTO
            (
                x.OrderId,
                x.CustomerId,
                x.EmployeeId,
                x.OrderDate,
                x.RequiredDate,
                x.ShippedDate,
                x.ShipVia,
                x.Freight,
                x.ShipName,
                x.ShipAddress,
                x.ShipCity,
                x.ShipRegion,
                x.ShipPostalCode,
                x.ShipCountry
            );
        }

        public override Func<OrderDTO, OrderViewModel> GetViewSelector()
        {
            return x => new OrderViewModel
            (
                x.OrderId,
                x.CustomerId,
                x.EmployeeId,
                x.OrderDate,
                x.RequiredDate,
                x.ShippedDate,
                x.ShipVia,
                x.Freight,
                x.ShipName,
                x.ShipAddress,
                x.ShipCity,
                x.ShipRegion,
                x.ShipPostalCode,
                x.ShipCountry
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                OrderDTO orderDTO = new OrderDTO(data);
                OrderViewModel view = (new List<OrderDTO> { orderDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.CustomerLookupText = orderDTO.CustomerLookupText;
                view.EmployeeLookupText = orderDTO.EmployeeLookupText;
                view.ShipperLookupText = orderDTO.ShipperLookupText;
                view.LookupText = orderDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<OrderDTO, Order> dto)
        {
            if (dto != null)
            {
                OrderDTO orderDTO = (OrderDTO)dto;
                OrderViewModel view = (new List<OrderDTO> { orderDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.CustomerLookupText = orderDTO.CustomerLookupText;
                view.EmployeeLookupText = orderDTO.EmployeeLookupText;
                view.ShipperLookupText = orderDTO.ShipperLookupText;
                view.LookupText = orderDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<OrderDTO, Order> ToDTO()
        {
            return (new List<OrderViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
