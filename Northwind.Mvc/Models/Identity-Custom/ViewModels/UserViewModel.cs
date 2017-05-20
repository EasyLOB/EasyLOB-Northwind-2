﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Identity.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace EasyLOB.Identity.Data
{
    public partial class UserViewModel : ZViewBase<UserViewModel, UserDTO, User>
    {
        #region Properties
        
        [Display(Name = "PropertyId", ResourceType = typeof(UserResources))]
        //[Key]
        //[Required] // !?!
        [StringLength(128)]
        public virtual string Id { get; set; }
        
        [Display(Name = "PropertyEmail", ResourceType = typeof(UserResources))]
        [StringLength(256)]
        public virtual string Email { get; set; }
        
        [Display(Name = "PropertyEmailConfirmed", ResourceType = typeof(UserResources))]
        //[Required] // !?!
        public virtual bool EmailConfirmed { get; set; }
        
        [Display(Name = "PropertyPasswordHash", ResourceType = typeof(UserResources))]
        [StringLength(1024)]
        public virtual string PasswordHash { get; set; }
        
        [Display(Name = "PropertySecurityStamp", ResourceType = typeof(UserResources))]
        [StringLength(1024)]
        public virtual string SecurityStamp { get; set; }
        
        [Display(Name = "PropertyPhoneNumber", ResourceType = typeof(UserResources))]
        [StringLength(1024)]
        public virtual string PhoneNumber { get; set; }
        
        [Display(Name = "PropertyPhoneNumberConfirmed", ResourceType = typeof(UserResources))]
        //[Required] // !?!
        public virtual bool PhoneNumberConfirmed { get; set; }
        
        [Display(Name = "PropertyTwoFactorEnabled", ResourceType = typeof(UserResources))]
        //[Required] // !?!
        public virtual bool TwoFactorEnabled { get; set; }
        
        [Display(Name = "PropertyLockoutEndDateUtc", ResourceType = typeof(UserResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public virtual DateTime? LockoutEndDateUtc { get; set; }
        
        [Display(Name = "PropertyLockoutEnabled", ResourceType = typeof(UserResources))]
        //[Required] // !?!
        public virtual bool LockoutEnabled { get; set; }
        
        [Display(Name = "PropertyAccessFailedCount", ResourceType = typeof(UserResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Required] // !?!
        public virtual int AccessFailedCount { get; set; }
        
        [Display(Name = "PropertyUserName", ResourceType = typeof(UserResources))]
        //[Required] // !?!
        [StringLength(256)]
        public virtual string UserName { get; set; }

        #endregion Properties

        #region Methods
        
        public UserViewModel()
        {
            Id = LibraryDefaults.Default_String;
            EmailConfirmed = LibraryDefaults.Default_Boolean;
            PhoneNumberConfirmed = LibraryDefaults.Default_Boolean;
            TwoFactorEnabled = LibraryDefaults.Default_Boolean;
            LockoutEnabled = LibraryDefaults.Default_Boolean;
            AccessFailedCount = LibraryDefaults.Default_Int32;
            UserName = LibraryDefaults.Default_String;
            Email = null;
            PasswordHash = null;
            SecurityStamp = null;
            PhoneNumber = null;
            LockoutEndDateUtc = null;
            LookupText = null;

            OnConstructor();
        }

        public UserViewModel(
            string id,
            bool emailConfirmed,
            bool phoneNumberConfirmed,
            bool twoFactorEnabled,
            bool lockoutEnabled,
            int accessFailedCount,
            string userName,
            string email = null,
            string passwordHash = null,
            string securityStamp = null,
            string phoneNumber = null,
            DateTime? lockoutEndDateUtc = null
        )
        {
            Id = id;
            Email = email;
            EmailConfirmed = emailConfirmed;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            TwoFactorEnabled = twoFactorEnabled;
            LockoutEndDateUtc = lockoutEndDateUtc;
            LockoutEnabled = lockoutEnabled;
            AccessFailedCount = accessFailedCount;
            UserName = userName;
            LookupText = null;
        }

        public UserViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public UserViewModel(IZDTOBase<UserDTO, User> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<UserViewModel, UserDTO> GetDTOSelector()
        {
            return x => new UserDTO
            (
                x.Id,
                x.EmailConfirmed,
                x.PhoneNumberConfirmed,
                x.TwoFactorEnabled,
                x.LockoutEnabled,
                x.AccessFailedCount,
                x.UserName,
                x.Email,
                x.PasswordHash,
                x.SecurityStamp,
                x.PhoneNumber,
                x.LockoutEndDateUtc
            );
        }

        public override Func<UserDTO, UserViewModel> GetViewSelector()
        {
            return x => new UserViewModel
            (
                x.Id,
                x.EmailConfirmed,
                x.PhoneNumberConfirmed,
                x.TwoFactorEnabled,
                x.LockoutEnabled,
                x.AccessFailedCount,
                x.UserName,
                x.Email,
                x.PasswordHash,
                x.SecurityStamp,
                x.PhoneNumber,
                x.LockoutEndDateUtc
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                UserDTO userDTO = new UserDTO(data);
                UserViewModel view = (new List<UserDTO> { userDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = userDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<UserDTO, User> dto)
        {
            if (dto != null)
            {
                UserDTO userDTO = (UserDTO)dto;
                UserViewModel view = (new List<UserDTO> { userDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = userDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<UserDTO, User> ToDTO()
        {
            return (new List<UserViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
