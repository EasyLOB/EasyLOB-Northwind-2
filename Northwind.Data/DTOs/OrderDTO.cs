using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Data
{
    public partial class OrderDTO : ZDTOBase<OrderDTO, Order>
    {
        #region Properties
               
        public virtual int OrderId { get; set; }
               
        public virtual string CustomerId { get; set; }
               
        public virtual int? EmployeeId { get; set; }
               
        public virtual DateTime? OrderDate { get; set; }
               
        public virtual DateTime? RequiredDate { get; set; }
               
        public virtual DateTime? ShippedDate { get; set; }
               
        public virtual int? ShipVia { get; set; }
               
        public virtual decimal? Freight { get; set; }
               
        public virtual string ShipName { get; set; }
               
        public virtual string ShipAddress { get; set; }
               
        public virtual string ShipCity { get; set; }
               
        public virtual string ShipRegion { get; set; }
               
        public virtual string ShipPostalCode { get; set; }
               
        public virtual string ShipCountry { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string CustomerLookupText { get; set; } // CustomerId

        public virtual string EmployeeLookupText { get; set; } // EmployeeId

        public virtual string ShipperLookupText { get; set; } // ShipVia
    
        #endregion Associations FK

        #region Methods
        
        public OrderDTO()
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
        }
        
        public OrderDTO(
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

        public OrderDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<OrderDTO, Order> GetDataSelector()
        {
            return x => new Order
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

        public override Func<Order, OrderDTO> GetDTOSelector()
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

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                Order order = (Order)data;
                OrderDTO dto = (new List<Order> { order })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                dto.CustomerLookupText = order.Customer == null ? null : order.Customer.LookupText;
                dto.EmployeeLookupText = order.Employee == null ? null : order.Employee.LookupText;
                dto.ShipperLookupText = order.Shipper == null ? null : order.Shipper.LookupText;
                dto.LookupText = order.LookupText;

                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<OrderDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
