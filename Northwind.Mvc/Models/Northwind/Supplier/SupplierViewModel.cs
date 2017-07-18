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
    public partial class SupplierViewModel : ZViewBase<SupplierViewModel, SupplierDTO, Supplier>
    {
        #region Properties
        
        [Display(Name = "PropertySupplierId", ResourceType = typeof(SupplierResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int SupplierId { get; set; }
        
        [Display(Name = "PropertyCompanyName", ResourceType = typeof(SupplierResources))]
        [Required]
        [StringLength(40)]
        public virtual string CompanyName { get; set; }
        
        [Display(Name = "PropertyContactName", ResourceType = typeof(SupplierResources))]
        [StringLength(30)]
        public virtual string ContactName { get; set; }
        
        [Display(Name = "PropertyContactTitle", ResourceType = typeof(SupplierResources))]
        [StringLength(30)]
        public virtual string ContactTitle { get; set; }
        
        [Display(Name = "PropertyAddress", ResourceType = typeof(SupplierResources))]
        [StringLength(60)]
        public virtual string Address { get; set; }
        
        [Display(Name = "PropertyCity", ResourceType = typeof(SupplierResources))]
        [StringLength(15)]
        public virtual string City { get; set; }
        
        [Display(Name = "PropertyRegion", ResourceType = typeof(SupplierResources))]
        [StringLength(15)]
        public virtual string Region { get; set; }
        
        [Display(Name = "PropertyPostalCode", ResourceType = typeof(SupplierResources))]
        [StringLength(10)]
        public virtual string PostalCode { get; set; }
        
        [Display(Name = "PropertyCountry", ResourceType = typeof(SupplierResources))]
        [StringLength(15)]
        public virtual string Country { get; set; }
        
        [Display(Name = "PropertyPhone", ResourceType = typeof(SupplierResources))]
        [StringLength(24)]
        public virtual string Phone { get; set; }
        
        [Display(Name = "PropertyFax", ResourceType = typeof(SupplierResources))]
        [StringLength(24)]
        public virtual string Fax { get; set; }
        
        [Display(Name = "PropertyHomePage", ResourceType = typeof(SupplierResources))]
        [StringLength(1024)]
        public virtual string HomePage { get; set; }

        #endregion Properties

        #region Methods
        
        public SupplierViewModel()
        {
            SupplierId = LibraryDefaults.Default_Int32;
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
            HomePage = null;
            LookupText = null;

            OnConstructor();
        }

        public SupplierViewModel(
            int supplierId,
            string companyName,
            string contactName = null,
            string contactTitle = null,
            string address = null,
            string city = null,
            string region = null,
            string postalCode = null,
            string country = null,
            string phone = null,
            string fax = null,
            string homePage = null
        )
        {
            SupplierId = supplierId;
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
            HomePage = homePage;
            LookupText = null;
        }

        public SupplierViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public SupplierViewModel(IZDTOBase<SupplierDTO, Supplier> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<SupplierViewModel, SupplierDTO> GetDTOSelector()
        {
            return x => new SupplierDTO
            (
                x.SupplierId,
                x.CompanyName,
                x.ContactName,
                x.ContactTitle,
                x.Address,
                x.City,
                x.Region,
                x.PostalCode,
                x.Country,
                x.Phone,
                x.Fax,
                x.HomePage
            );
        }

        public override Func<SupplierDTO, SupplierViewModel> GetViewSelector()
        {
            return x => new SupplierViewModel
            (
                x.SupplierId,
                x.CompanyName,
                x.ContactName,
                x.ContactTitle,
                x.Address,
                x.City,
                x.Region,
                x.PostalCode,
                x.Country,
                x.Phone,
                x.Fax,
                x.HomePage
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                SupplierDTO supplierDTO = new SupplierDTO(data);
                SupplierViewModel view = (new List<SupplierDTO> { supplierDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = supplierDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<SupplierDTO, Supplier> dto)
        {
            if (dto != null)
            {
                SupplierDTO supplierDTO = (SupplierDTO)dto;
                SupplierViewModel view = (new List<SupplierDTO> { supplierDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = supplierDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<SupplierDTO, Supplier> ToDTO()
        {
            return (new List<SupplierViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
