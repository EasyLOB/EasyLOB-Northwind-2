﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Activity.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Activity.Data
{
    public partial class ActivityViewModel : ZViewBase<ActivityViewModel, ActivityDTO, EasyLOB.Activity.Data.Activity> // !?!
    {
        #region Properties
        
        [Display(Name = "PropertyId", ResourceType = typeof(ActivityResources))]
        //[Key]
        //[Required] // !?!
        [StringLength(128)]
        public virtual string Id { get; set; }
        
        [Display(Name = "PropertyName", ResourceType = typeof(ActivityResources))]
        [Required]
        [StringLength(256)]
        public virtual string Name { get; set; }

        #endregion Properties

        #region Methods
        
        public ActivityViewModel()
        {
            Id = LibraryDefaults.Default_String;
            Name = LibraryDefaults.Default_String;
            LookupText = null;

            OnConstructor();
        }

        public ActivityViewModel(
            string id,
            string name
        )
        {
            Id = id;
            Name = name;
            LookupText = null;
        }

        public ActivityViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public ActivityViewModel(IZDTOBase<ActivityDTO, EasyLOB.Activity.Data.Activity> dto) // !?!
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<ActivityViewModel, ActivityDTO> GetDTOSelector()
        {
            return x => new ActivityDTO
            (
                x.Id,
                x.Name
            );
        }

        public override Func<ActivityDTO, ActivityViewModel> GetViewSelector()
        {
            return x => new ActivityViewModel
            (
                x.Id,
                x.Name
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                ActivityDTO activityDTO = new ActivityDTO(data);
                ActivityViewModel view = (new List<ActivityDTO> { activityDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = activityDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<ActivityDTO, EasyLOB.Activity.Data.Activity> dto) // !?!
        {
            if (dto != null)
            {
                ActivityDTO activityDTO = (ActivityDTO)dto;
                ActivityViewModel view = (new List<ActivityDTO> { activityDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = activityDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<ActivityDTO, EasyLOB.Activity.Data.Activity> ToDTO() // !?!
        {
            return (new List<ActivityViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
