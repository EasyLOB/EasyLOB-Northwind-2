using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Data
{
    public partial class TerritoryDTO : ZDTOBase<TerritoryDTO, Territory>
    {
        #region Properties
               
        public virtual string TerritoryId { get; set; }
               
        public virtual string TerritoryDescription { get; set; }
               
        public virtual int RegionId { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string RegionLookupText { get; set; } // RegionId
    
        #endregion Associations FK

        #region Methods
        
        public TerritoryDTO()
        {
            TerritoryId = LibraryDefaults.Default_String;
            TerritoryDescription = LibraryDefaults.Default_String;
            RegionId = LibraryDefaults.Default_Int32;
            RegionLookupText = null;
            LookupText = null;
        }
        
        public TerritoryDTO(
            string territoryId,
            string territoryDescription,
            int regionId,
            string regionLookupText = null
        )
        {
            TerritoryId = territoryId;
            TerritoryDescription = territoryDescription;
            RegionId = regionId;
            RegionLookupText = regionLookupText;
            LookupText = null;
        }

        public TerritoryDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<TerritoryDTO, Territory> GetDataSelector()
        {
            return x => new Territory
            (
                x.TerritoryId,
                x.TerritoryDescription,
                x.RegionId
            );
        }

        public override Func<Territory, TerritoryDTO> GetDTOSelector()
        {
            return x => new TerritoryDTO
            (
                x.TerritoryId,
                x.TerritoryDescription,
                x.RegionId
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                Territory territory = (Territory)data;
                TerritoryDTO dto = (new List<Territory> { territory })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                dto.RegionLookupText = territory.Region == null ? null : territory.Region.LookupText;
                dto.LookupText = territory.LookupText;

                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<TerritoryDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
