using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Data
{
    public partial class CustomerCustomerDemoDTO : ZDTOBase<CustomerCustomerDemoDTO, CustomerCustomerDemo>
    {
        #region Properties
               
        public virtual string CustomerId { get; set; }
               
        public virtual string CustomerTypeId { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string CustomerDemographicLookupText { get; set; } // CustomerTypeId

        public virtual string CustomerLookupText { get; set; } // CustomerId
    
        #endregion Associations FK

        #region Methods
        
        public CustomerCustomerDemoDTO()
        {
            CustomerId = LibraryDefaults.Default_String;
            CustomerTypeId = LibraryDefaults.Default_String;
            CustomerDemographicLookupText = null;
            CustomerLookupText = null;
            LookupText = null;
        }
        
        public CustomerCustomerDemoDTO(
            string customerId,
            string customerTypeId,
            string customerDemographicLookupText = null,
            string customerLookupText = null
        )
        {
            CustomerId = customerId;
            CustomerTypeId = customerTypeId;
            CustomerDemographicLookupText = customerDemographicLookupText;
            CustomerLookupText = customerLookupText;
            LookupText = null;
        }

        public CustomerCustomerDemoDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<CustomerCustomerDemoDTO, CustomerCustomerDemo> GetDataSelector()
        {
            return x => new CustomerCustomerDemo
            (
                x.CustomerId,
                x.CustomerTypeId
            );
        }

        public override Func<CustomerCustomerDemo, CustomerCustomerDemoDTO> GetDTOSelector()
        {
            return x => new CustomerCustomerDemoDTO
            (
                x.CustomerId,
                x.CustomerTypeId
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                CustomerCustomerDemo customerCustomerDemo = (CustomerCustomerDemo)data;
                CustomerCustomerDemoDTO dto = (new List<CustomerCustomerDemo> { customerCustomerDemo })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                dto.CustomerDemographicLookupText = customerCustomerDemo.CustomerDemographic == null ? null : customerCustomerDemo.CustomerDemographic.LookupText;
                dto.CustomerLookupText = customerCustomerDemo.Customer == null ? null : customerCustomerDemo.Customer.LookupText;
                dto.LookupText = customerCustomerDemo.LookupText;

                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<CustomerCustomerDemoDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
