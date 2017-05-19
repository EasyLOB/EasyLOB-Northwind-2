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
    public partial class EmployeeTerritoryViewModel : ZViewBase<EmployeeTerritoryViewModel, EmployeeTerritoryDTO, EmployeeTerritory>
    {
        #region Properties
        
        [Display(Name = "PropertyEmployeeId", ResourceType = typeof(EmployeeTerritoryResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        //[Column(Order=1)]
        [Range(1, System.Int32.MaxValue, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(DataAnnotationResources))]
        [Required]
        public virtual int EmployeeId { get; set; }
        
        [Display(Name = "PropertyTerritoryId", ResourceType = typeof(EmployeeTerritoryResources))]
        //[Key]
        //[Column(Order=2)]
        [Required]
        [StringLength(20)]
        public virtual string TerritoryId { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string EmployeeLookupText { get; set; } // EmployeeId

        public virtual string TerritoryLookupText { get; set; } // TerritoryId
    
        #endregion Associations FK

        #region Methods
        
        public EmployeeTerritoryViewModel()
        {
            EmployeeId = LibraryDefaults.Default_Int32;
            TerritoryId = LibraryDefaults.Default_String;
            EmployeeLookupText = null;
            TerritoryLookupText = null;
            LookupText = null;

            OnConstructor();
        }

        public EmployeeTerritoryViewModel(
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

        public EmployeeTerritoryViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public EmployeeTerritoryViewModel(IZDTOBase<EmployeeTerritoryDTO, EmployeeTerritory> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<EmployeeTerritoryViewModel, EmployeeTerritoryDTO> GetDTOSelector()
        {
            return x => new EmployeeTerritoryDTO
            (
                x.EmployeeId,
                x.TerritoryId
            );
        }

        public override Func<EmployeeTerritoryDTO, EmployeeTerritoryViewModel> GetViewSelector()
        {
            return x => new EmployeeTerritoryViewModel
            (
                x.EmployeeId,
                x.TerritoryId
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                EmployeeTerritoryDTO employeeTerritoryDTO = new EmployeeTerritoryDTO(data);
                EmployeeTerritoryViewModel view = (new List<EmployeeTerritoryDTO> { employeeTerritoryDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.EmployeeLookupText = employeeTerritoryDTO.EmployeeLookupText;
                view.TerritoryLookupText = employeeTerritoryDTO.TerritoryLookupText;
                view.LookupText = employeeTerritoryDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<EmployeeTerritoryDTO, EmployeeTerritory> dto)
        {
            if (dto != null)
            {
                EmployeeTerritoryDTO employeeTerritoryDTO = (EmployeeTerritoryDTO)dto;
                EmployeeTerritoryViewModel view = (new List<EmployeeTerritoryDTO> { employeeTerritoryDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.EmployeeLookupText = employeeTerritoryDTO.EmployeeLookupText;
                view.TerritoryLookupText = employeeTerritoryDTO.TerritoryLookupText;
                view.LookupText = employeeTerritoryDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<EmployeeTerritoryDTO, EmployeeTerritory> ToDTO()
        {
            return (new List<EmployeeTerritoryViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
