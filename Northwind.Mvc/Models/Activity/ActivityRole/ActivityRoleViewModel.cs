using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Activity.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Activity.Data
{
    public partial class ActivityRoleViewModel : ZViewBase<ActivityRoleViewModel, ActivityRoleDTO, ActivityRole>
    {
        #region Properties
        
        [Display(Name = "PropertyActivityId", ResourceType = typeof(ActivityRoleResources))]
        //[Key]
        //[Column(Order=1)]
        [Required]
        [StringLength(128)]
        public virtual string ActivityId { get; set; }
        
        [Display(Name = "PropertyRoleName", ResourceType = typeof(ActivityRoleResources))]
        //[Key]
        //[Column(Order=2)]
        [Required]
        [StringLength(128)]
        public virtual string RoleName { get; set; }
        
        [Display(Name = "PropertyOperations", ResourceType = typeof(ActivityRoleResources))]
        [StringLength(256)]
        public virtual string Operations { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string ActivityLookupText { get; set; } // ActivityId

        public virtual string RoleLookupText { get; set; } // RoleName
    
        #endregion Associations FK

        #region Methods
        
        public ActivityRoleViewModel()
        {
            ActivityId = LibraryDefaults.Default_String;
            RoleName = LibraryDefaults.Default_String;
            Operations = null;
            ActivityLookupText = null;
            RoleLookupText = null;
            LookupText = null;

            OnConstructor();
        }

        public ActivityRoleViewModel(
            string activityId,
            string roleId,
            string operations = null,
            string activityLookupText = null,
            string roleLookupText = null
        )
        {
            ActivityId = activityId;
            RoleName = roleId;
            Operations = operations;
            ActivityLookupText = activityLookupText;
            RoleLookupText = roleLookupText;
            LookupText = null;
        }

        public ActivityRoleViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public ActivityRoleViewModel(IZDTOBase<ActivityRoleDTO, ActivityRole> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<ActivityRoleViewModel, ActivityRoleDTO> GetDTOSelector()
        {
            return x => new ActivityRoleDTO
            (
                x.ActivityId,
                x.RoleName,
                x.Operations
            );
        }

        public override Func<ActivityRoleDTO, ActivityRoleViewModel> GetViewSelector()
        {
            return x => new ActivityRoleViewModel
            (
                x.ActivityId,
                x.RoleName,
                x.Operations
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                ActivityRoleDTO activityRoleDTO = new ActivityRoleDTO(data);
                ActivityRoleViewModel view = (new List<ActivityRoleDTO> { activityRoleDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.ActivityLookupText = activityRoleDTO.ActivityLookupText;
                view.LookupText = activityRoleDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<ActivityRoleDTO, ActivityRole> dto)
        {
            if (dto != null)
            {
                ActivityRoleDTO activityRoleDTO = (ActivityRoleDTO)dto;
                ActivityRoleViewModel view = (new List<ActivityRoleDTO> { activityRoleDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.ActivityLookupText = activityRoleDTO.ActivityLookupText;
                view.LookupText = activityRoleDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<ActivityRoleDTO, ActivityRole> ToDTO()
        {
            return (new List<ActivityRoleViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
