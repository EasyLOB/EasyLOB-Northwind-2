using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Data
{
    public partial class CustomerDTO : ZDTOBase<CustomerDTO, Customer>
    {
        #region Properties
               
        public virtual string CustomerId { get; set; }
               
        public virtual string CompanyName { get; set; }
               
        public virtual string ContactName { get; set; }
               
        public virtual string ContactTitle { get; set; }
               
        public virtual string Address { get; set; }
               
        public virtual string City { get; set; }
               
        public virtual string Region { get; set; }
               
        public virtual string PostalCode { get; set; }
               
        public virtual string Country { get; set; }
               
        public virtual string Phone { get; set; }
               
        public virtual string Fax { get; set; }

        #endregion Properties

        #region Methods
        
        public CustomerDTO()
        {
            CustomerId = LibraryDefaults.Default_String;
            CompanyName = LibraryDefaults.Default_String;
            ContactName = null;
            ContactTitle = null;
            Address = null;
            City = null;
            Region = null;
            PostalCode = null;
            Country = null;
            Phone = null;
            Fax = null;
            LookupText = null;
        }
        
        public CustomerDTO(
            string customerId,
            string companyName,
            string contactName = null,
            string contactTitle = null,
            string address = null,
            string city = null,
            string region = null,
            string postalCode = null,
            string country = null,
            string phone = null,
            string fax = null
        )
        {
            CustomerId = customerId;
            CompanyName = companyName;
            ContactName = contactName;
            ContactTitle = contactTitle;
            Address = address;
            City = city;
            Region = region;
            PostalCode = postalCode;
            Country = country;
            Phone = phone;
            Fax = fax;
            LookupText = null;
        }

        public CustomerDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<CustomerDTO, Customer> GetDataSelector()
        {
            return x => new Customer
            (
                x.CustomerId,
                x.CompanyName,
                x.ContactName,
                x.ContactTitle,
                x.Address,
                x.City,
                x.Region,
                x.PostalCode,
                x.Country,
                x.Phone,
                x.Fax
            );
        }

        public override Func<Customer, CustomerDTO> GetDTOSelector()
        {
            return x => new CustomerDTO
            (
                x.CustomerId,
                x.CompanyName,
                x.ContactName,
                x.ContactTitle,
                x.Address,
                x.City,
                x.Region,
                x.PostalCode,
                x.Country,
                x.Phone,
                x.Fax
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                Customer customer = (Customer)data;
                CustomerDTO dto = (new List<Customer> { customer })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                dto.LookupText = customer.LookupText;

                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<CustomerDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
