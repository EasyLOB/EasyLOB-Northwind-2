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
    public partial class CategoryViewModel : ZViewBase<CategoryViewModel, CategoryDTO, Category>
    {
        #region Properties
        
        [Display(Name = "PropertyCategoryId", ResourceType = typeof(CategoryResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int CategoryId { get; set; }
        
        [Display(Name = "PropertyCategoryName", ResourceType = typeof(CategoryResources))]
        [Required]
        [StringLength(15)]
        public virtual string CategoryName { get; set; }
        
        [Display(Name = "PropertyDescription", ResourceType = typeof(CategoryResources))]
        [StringLength(1024)]
        public virtual string Description { get; set; }
        
        [Display(Name = "PropertyPicture", ResourceType = typeof(CategoryResources))]
        public virtual byte[] Picture { get; set; }

        #endregion Properties

        #region Methods
        
        public CategoryViewModel()
        {
            CategoryId = LibraryDefaults.Default_Int32;
            CategoryName = LibraryDefaults.Default_String;
            Description = null;
            Picture = null;
            LookupText = null;

            OnConstructor();
        }

        public CategoryViewModel(
            int categoryId,
            string categoryName,
            string description = null,
            byte[] picture = null
        )
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            Description = description;
            Picture = picture;
            LookupText = null;
        }

        public CategoryViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public CategoryViewModel(IZDTOBase<CategoryDTO, Category> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<CategoryViewModel, CategoryDTO> GetDTOSelector()
        {
            return x => new CategoryDTO
            (
                x.CategoryId,
                x.CategoryName,
                x.Description,
                x.Picture
            );
        }

        public override Func<CategoryDTO, CategoryViewModel> GetViewSelector()
        {
            return x => new CategoryViewModel
            (
                x.CategoryId,
                x.CategoryName,
                x.Description,
                x.Picture
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                CategoryDTO categoryDTO = new CategoryDTO(data);
                CategoryViewModel view = (new List<CategoryDTO> { categoryDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = categoryDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<CategoryDTO, Category> dto)
        {
            if (dto != null)
            {
                CategoryDTO categoryDTO = (CategoryDTO)dto;
                CategoryViewModel view = (new List<CategoryDTO> { categoryDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = categoryDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<CategoryDTO, Category> ToDTO()
        {
            return (new List<CategoryViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
