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
    public partial class CustomerViewModel : ZViewBase<CustomerViewModel, CustomerDTO, Customer>
    {
        #region Properties
        
        [Display(Name = "PropertyCustomerId", ResourceType = typeof(CustomerResources))]
        //[Key]
        [Required]
        [StringLength(5)]
        public virtual string CustomerId { get; set; }
        
        [Display(Name = "PropertyCompanyName", ResourceType = typeof(CustomerResources))]
        [Required]
        [StringLength(40)]
        public virtual string CompanyName { get; set; }
        
        [Display(Name = "PropertyContactName", ResourceType = typeof(CustomerResources))]
        [StringLength(30)]
        public virtual string ContactName { get; set; }
        
        [Display(Name = "PropertyContactTitle", ResourceType = typeof(CustomerResources))]
        [StringLength(30)]
        public virtual string ContactTitle { get; set; }
        
        [Display(Name = "PropertyAddress", ResourceType = typeof(CustomerResources))]
        [StringLength(60)]
        public virtual string Address { get; set; }
        
        [Display(Name = "PropertyCity", ResourceType = typeof(CustomerResources))]
        [StringLength(15)]
        public virtual string City { get; set; }
        
        [Display(Name = "PropertyRegion", ResourceType = typeof(CustomerResources))]
        [StringLength(15)]
        public virtual string Region { get; set; }
        
        [Display(Name = "PropertyPostalCode", ResourceType = typeof(CustomerResources))]
        [StringLength(10)]
        public virtual string PostalCode { get; set; }
        
        [Display(Name = "PropertyCountry", ResourceType = typeof(CustomerResources))]
        [StringLength(15)]
        public virtual string Country { get; set; }
        
        [Display(Name = "PropertyPhone", ResourceType = typeof(CustomerResources))]
        [StringLength(24)]
        public virtual string Phone { get; set; }
        
        [Display(Name = "PropertyFax", ResourceType = typeof(CustomerResources))]
        [StringLength(24)]
        public virtual string Fax { get; set; }

        #endregion Properties

        #region Methods
        
        public CustomerViewModel()
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

            OnConstructor();
        }

        public CustomerViewModel(
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

        public CustomerViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public CustomerViewModel(IZDTOBase<CustomerDTO, Customer> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<CustomerViewModel, CustomerDTO> GetDTOSelector()
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

        public override Func<CustomerDTO, CustomerViewModel> GetViewSelector()
        {
            return x => new CustomerViewModel
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
                CustomerDTO customerDTO = new CustomerDTO(data);
                CustomerViewModel view = (new List<CustomerDTO> { customerDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = customerDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<CustomerDTO, Customer> dto)
        {
            if (dto != null)
            {
                CustomerDTO customerDTO = (CustomerDTO)dto;
                CustomerViewModel view = (new List<CustomerDTO> { customerDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = customerDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<CustomerDTO, Customer> ToDTO()
        {
            return (new List<CustomerViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
