using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Supplier : ZDataBase
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

        #region Collections (PK)

        public virtual IList<Product> Products { get; }

        #endregion Collections (PK)

        #region Methods
        
        public Supplier()
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

            Products = new List<Product>();
    
            OnConstructor();
        }

        public Supplier(int supplierId)
            : this()
        {            
            SupplierId = supplierId;
        }

        public Supplier(
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
            : this()
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
        }

        public override object[] GetId()
        {
            return new object[] { SupplierId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                SupplierId = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}
