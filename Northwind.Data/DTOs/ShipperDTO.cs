using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Data
{
    public partial class ShipperDTO : ZDTOBase<ShipperDTO, Shipper>
    {
        #region Properties
               
        public virtual int ShipperId { get; set; }
               
        public virtual string CompanyName { get; set; }
               
        public virtual string Phone { get; set; }

        #endregion Properties

        #region Methods
        
        public ShipperDTO()
        {
            ShipperId = LibraryDefaults.Default_Int32;
            CompanyName = LibraryDefaults.Default_String;
            Phone = null;
            LookupText = null;
        }
        
        public ShipperDTO(
            int shipperId,
            string companyName,
            string phone = null
        )
        {
            ShipperId = shipperId;
            CompanyName = companyName;
            Phone = phone;
            LookupText = null;
        }

        public ShipperDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<ShipperDTO, Shipper> GetDataSelector()
        {
            return x => new Shipper
            (
                x.ShipperId,
                x.CompanyName,
                x.Phone
            );
        }

        public override Func<Shipper, ShipperDTO> GetDTOSelector()
        {
            return x => new ShipperDTO
            (
                x.ShipperId,
                x.CompanyName,
                x.Phone
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                Shipper shipper = (Shipper)data;
                ShipperDTO dto = (new List<Shipper> { shipper })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                dto.LookupText = shipper.LookupText;

                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<ShipperDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
