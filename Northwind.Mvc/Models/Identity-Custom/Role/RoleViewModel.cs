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
    public partial class RoleViewModel : ZViewBase<RoleViewModel, RoleDTO, Role>
    {
        #region Properties
        
        [Display(Name = "PropertyId", ResourceType = typeof(RoleResources))]
        //[Key]
        //[Required] // !?!
        [StringLength(128)]
        public virtual string Id { get; set; }
        
        [Display(Name = "PropertyName", ResourceType = typeof(RoleResources))]
        [Required]
        [StringLength(256)]
        public virtual string Name { get; set; }
        
        [Display(Name = "PropertyDiscriminator", ResourceType = typeof(RoleResources))]
        //[Required] // !?!
        [StringLength(128)]
        public virtual string Discriminator { get; set; }

        #endregion Properties

        #region Methods
        
        public RoleViewModel()
        {
            Id = LibraryDefaults.Default_String;
            Name = LibraryDefaults.Default_String;
            Discriminator = LibraryDefaults.Default_String;
            LookupText = null;

            OnConstructor();
        }

        public RoleViewModel(
            string id,
            string name,
            string discriminator
        )
        {
            Id = id;
            Name = name;
            Discriminator = discriminator;
            LookupText = null;
        }

        public RoleViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public RoleViewModel(IZDTOBase<RoleDTO, Role> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<RoleViewModel, RoleDTO> GetDTOSelector()
        {
            return x => new RoleDTO
            (
                x.Id,
                x.Name,
                x.Discriminator
            );
        }

        public override Func<RoleDTO, RoleViewModel> GetViewSelector()
        {
            return x => new RoleViewModel
            (
                x.Id,
                x.Name,
                x.Discriminator
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                RoleDTO roleDTO = new RoleDTO(data);
                RoleViewModel view = (new List<RoleDTO> { roleDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = roleDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<RoleDTO, Role> dto)
        {
            if (dto != null)
            {
                RoleDTO roleDTO = (RoleDTO)dto;
                RoleViewModel view = (new List<RoleDTO> { roleDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = roleDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<RoleDTO, Role> ToDTO()
        {
            return (new List<RoleViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
