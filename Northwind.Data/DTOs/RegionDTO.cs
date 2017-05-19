using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Data
{
    public partial class RegionDTO : ZDTOBase<RegionDTO, Region>
    {
        #region Properties
               
        public virtual int RegionId { get; set; }
               
        public virtual string RegionDescription { get; set; }

        #endregion Properties

        #region Methods
        
        public RegionDTO()
        {
            RegionId = LibraryDefaults.Default_Int32;
            RegionDescription = LibraryDefaults.Default_String;
            LookupText = null;
        }
        
        public RegionDTO(
            int regionId,
            string regionDescription
        )
        {
            RegionId = regionId;
            RegionDescription = regionDescription;
            LookupText = null;
        }

        public RegionDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<RegionDTO, Region> GetDataSelector()
        {
            return x => new Region
            (
                x.RegionId,
                x.RegionDescription
            );
        }

        public override Func<Region, RegionDTO> GetDTOSelector()
        {
            return x => new RegionDTO
            (
                x.RegionId,
                x.RegionDescription
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                Region region = (Region)data;
                RegionDTO dto = (new List<Region> { region })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                dto.LookupText = region.LookupText;

                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<RegionDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
