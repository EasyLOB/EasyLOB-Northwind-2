using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;

namespace Northwind.Data
{
    public partial class Shipper : ZDataBase
    {        
        #region Properties
        
        public virtual int ShipperId { get; set; }
        
        public virtual string CompanyName { get; set; }
        
        public virtual string Phone { get; set; }

        #endregion Properties

        #region Collections (PK)

        public virtual IList<Order> Orders { get; }

        #endregion Collections (PK)

        #region Methods
        
        public Shipper()
        {            
            ShipperId = LibraryDefaults.Default_Int32;
            CompanyName = LibraryDefaults.Default_String;
            Phone = null;

            Orders = new List<Order>();
    
            OnConstructor();
        }

        public Shipper(int shipperId)
            : this()
        {            
            ShipperId = shipperId;
        }

        public Shipper(
            int shipperId,
            string companyName,
            string phone = null
        )
            : this()
        {
            ShipperId = shipperId;
            CompanyName = companyName;
            Phone = phone;
        }

        public override object[] GetId()
        {
            return new object[] { ShipperId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                ShipperId = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}
