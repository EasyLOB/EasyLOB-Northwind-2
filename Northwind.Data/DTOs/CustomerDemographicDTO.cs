using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Data
{
    public partial class CustomerDemographicDTO : ZDTOBase<CustomerDemographicDTO, CustomerDemographic>
    {
        #region Properties
               
        public virtual string CustomerTypeId { get; set; }
               
        public virtual string CustomerDesc { get; set; }

        #endregion Properties

        #region Methods
        
        public CustomerDemographicDTO()
        {
            CustomerTypeId = LibraryDefaults.Default_String;
            CustomerDesc = null;
            LookupText = null;
        }
        
        public CustomerDemographicDTO(
            string customerTypeId,
            string customerDesc = null
        )
        {
            CustomerTypeId = customerTypeId;
            CustomerDesc = customerDesc;
            LookupText = null;
        }

        public CustomerDemographicDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<CustomerDemographicDTO, CustomerDemographic> GetDataSelector()
        {
            return x => new CustomerDemographic
            (
                x.CustomerTypeId,
                x.CustomerDesc
            );
        }

        public override Func<CustomerDemographic, CustomerDemographicDTO> GetDTOSelector()
        {
            return x => new CustomerDemographicDTO
            (
                x.CustomerTypeId,
                x.CustomerDesc
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                CustomerDemographic customerDemographic = (CustomerDemographic)data;
                CustomerDemographicDTO dto = (new List<CustomerDemographic> { customerDemographic })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                dto.LookupText = customerDemographic.LookupText;

                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<CustomerDemographicDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
