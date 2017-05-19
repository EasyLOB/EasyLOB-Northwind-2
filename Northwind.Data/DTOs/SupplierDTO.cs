using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Data
{
    public partial class SupplierDTO : ZDTOBase<SupplierDTO, Supplier>
    {
        #region Properties
               
        public virtual int SupplierId { get; set; }
               
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
               
        public virtual string HomePage { get; set; }

        #endregion Properties

        #region Methods
        
        public SupplierDTO()
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
        }
        
        public SupplierDTO(
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

        public SupplierDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<SupplierDTO, Supplier> GetDataSelector()
        {
            return x => new Supplier
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

        public override Func<Supplier, SupplierDTO> GetDTOSelector()
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

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                Supplier supplier = (Supplier)data;
                SupplierDTO dto = (new List<Supplier> { supplier })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                dto.LookupText = supplier.LookupText;

                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<SupplierDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
