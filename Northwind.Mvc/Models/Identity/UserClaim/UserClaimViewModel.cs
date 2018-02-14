using EasyLOB.Identity.Data;
using EasyLOB.Identity.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EasyLOB.Identity.Mvc
{
    public partial class UserClaimViewModel : ZViewBase<UserClaimViewModel, UserClaimDTO, UserClaim>
    {
        #region Properties
        
        [Display(Name = "PropertyId", ResourceType = typeof(UserClaimResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int Id { get; set; }
        
        [Display(Name = "PropertyUserId", ResourceType = typeof(UserClaimResources))]
        [Required]
        [StringLength(128)]
        public virtual string UserId { get; set; }
        
        [Display(Name = "PropertyClaimType", ResourceType = typeof(UserClaimResources))]
        [StringLength(1024)]
        public virtual string ClaimType { get; set; }
        
        [Display(Name = "PropertyClaimValue", ResourceType = typeof(UserClaimResources))]
        [StringLength(1024)]
        public virtual string ClaimValue { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string UserLookupText { get; set; } // UserId
    
        #endregion Associations FK

        #region Methods
        
        public UserClaimViewModel()
        {
            Id = LibraryDefaults.Default_Int32;
            UserId = LibraryDefaults.Default_String;
            ClaimType = null;
            ClaimValue = null;
            UserLookupText = null;
            LookupText = null;

            OnConstructor();
        }

        public UserClaimViewModel(
            int id,
            string userId,
            string claimType = null,
            string claimValue = null,
            string userLookupText = null
        )
        {
            Id = id;
            UserId = userId;
            ClaimType = claimType;
            ClaimValue = claimValue;
            UserLookupText = userLookupText;
            LookupText = null;
        }

        public UserClaimViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public UserClaimViewModel(IZDTOBase<UserClaimDTO, UserClaim> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<UserClaimViewModel, UserClaimDTO> GetDTOSelector()
        {
            return x => new UserClaimDTO
            (
                x.Id,
                x.UserId,
                x.ClaimType,
                x.ClaimValue
            );
        }

        public override Func<UserClaimDTO, UserClaimViewModel> GetViewSelector()
        {
            return x => new UserClaimViewModel
            (
                x.Id,
                x.UserId,
                x.ClaimType,
                x.ClaimValue
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                UserClaimDTO userClaimDTO = new UserClaimDTO(data);
                UserClaimViewModel view = (new List<UserClaimDTO> { userClaimDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.UserLookupText = userClaimDTO.UserLookupText;
                view.LookupText = userClaimDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<UserClaimDTO, UserClaim> dto)
        {
            if (dto != null)
            {
                UserClaimDTO userClaimDTO = (UserClaimDTO)dto;
                UserClaimViewModel view = (new List<UserClaimDTO> { userClaimDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.UserLookupText = userClaimDTO.UserLookupText;
                view.LookupText = userClaimDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<UserClaimDTO, UserClaim> ToDTO()
        {
            return (new List<UserClaimViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
