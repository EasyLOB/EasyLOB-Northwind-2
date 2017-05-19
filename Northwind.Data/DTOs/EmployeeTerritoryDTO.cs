using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Data
{
    public partial class EmployeeTerritoryDTO : ZDTOBase<EmployeeTerritoryDTO, EmployeeTerritory>
    {
        #region Properties
               
        public virtual int EmployeeId { get; set; }
               
        public virtual string TerritoryId { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string EmployeeLookupText { get; set; } // EmployeeId

        public virtual string TerritoryLookupText { get; set; } // TerritoryId
    
        #endregion Associations FK

        #region Methods
        
        public EmployeeTerritoryDTO()
        {
            EmployeeId = LibraryDefaults.Default_Int32;
            TerritoryId = LibraryDefaults.Default_String;
            EmployeeLookupText = null;
            TerritoryLookupText = null;
            LookupText = null;
        }
        
        public EmployeeTerritoryDTO(
            int employeeId,
            string territoryId,
            string employeeLookupText = null,
            string territoryLookupText = null
        )
        {
            EmployeeId = employeeId;
            TerritoryId = territoryId;
            EmployeeLookupText = employeeLookupText;
            TerritoryLookupText = territoryLookupText;
            LookupText = null;
        }

        public EmployeeTerritoryDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<EmployeeTerritoryDTO, EmployeeTerritory> GetDataSelector()
        {
            return x => new EmployeeTerritory
            (
                x.EmployeeId,
                x.TerritoryId
            );
        }

        public override Func<EmployeeTerritory, EmployeeTerritoryDTO> GetDTOSelector()
        {
            return x => new EmployeeTerritoryDTO
            (
                x.EmployeeId,
                x.TerritoryId
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                EmployeeTerritory employeeTerritory = (EmployeeTerritory)data;
                EmployeeTerritoryDTO dto = (new List<EmployeeTerritory> { employeeTerritory })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                dto.EmployeeLookupText = employeeTerritory.Employee == null ? null : employeeTerritory.Employee.LookupText;
                dto.TerritoryLookupText = employeeTerritory.Territory == null ? null : employeeTerritory.Territory.LookupText;
                dto.LookupText = employeeTerritory.LookupText;

                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<EmployeeTerritoryDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
