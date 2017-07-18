using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Identity.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Identity.Data
{
    public partial class UserLoginViewModel : ZViewBase<UserLoginViewModel, UserLoginDTO, UserLogin>
    {
        #region Properties
        
        [Display(Name = "PropertyLoginProvider", ResourceType = typeof(UserLoginResources))]
        //[Key]
        //[Column(Order=1)]
        [Required]
        [StringLength(128)]
        public virtual string LoginProvider { get; set; }
        
        [Display(Name = "PropertyProviderKey", ResourceType = typeof(UserLoginResources))]
        //[Key]
        //[Column(Order=2)]
        [Required]
        [StringLength(128)]
        public virtual string ProviderKey { get; set; }
        
        [Display(Name = "PropertyUserId", ResourceType = typeof(UserLoginResources))]
        //[Key]
        //[Column(Order=3)]
        [Required]
        [StringLength(128)]
        public virtual string UserId { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string UserLookupText { get; set; } // UserId
    
        #endregion Associations FK

        #region Methods
        
        public UserLoginViewModel()
        {
            LoginProvider = LibraryDefaults.Default_String;
            ProviderKey = LibraryDefaults.Default_String;
            UserId = LibraryDefaults.Default_String;
            UserLookupText = null;
            LookupText = null;

            OnConstructor();
        }

        public UserLoginViewModel(
            string loginProvider,
            string providerKey,
            string userId,
            string userLookupText = null
        )
        {
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
            UserId = userId;
            UserLookupText = userLookupText;
            LookupText = null;
        }

        public UserLoginViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public UserLoginViewModel(IZDTOBase<UserLoginDTO, UserLogin> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<UserLoginViewModel, UserLoginDTO> GetDTOSelector()
        {
            return x => new UserLoginDTO
            (
                x.LoginProvider,
                x.ProviderKey,
                x.UserId
            );
        }

        public override Func<UserLoginDTO, UserLoginViewModel> GetViewSelector()
        {
            return x => new UserLoginViewModel
            (
                x.LoginProvider,
                x.ProviderKey,
                x.UserId
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                UserLoginDTO userLoginDTO = new UserLoginDTO(data);
                UserLoginViewModel view = (new List<UserLoginDTO> { userLoginDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.UserLookupText = userLoginDTO.UserLookupText;
                view.LookupText = userLoginDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<UserLoginDTO, UserLogin> dto)
        {
            if (dto != null)
            {
                UserLoginDTO userLoginDTO = (UserLoginDTO)dto;
                UserLoginViewModel view = (new List<UserLoginDTO> { userLoginDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.UserLookupText = userLoginDTO.UserLookupText;
                view.LookupText = userLoginDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<UserLoginDTO, UserLogin> ToDTO()
        {
            return (new List<UserLoginViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
