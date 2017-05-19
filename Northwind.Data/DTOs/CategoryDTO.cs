using EasyLOB.Data;
using EasyLOB.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Data
{
    public partial class CategoryDTO : ZDTOBase<CategoryDTO, Category>
    {
        #region Properties
               
        public virtual int CategoryId { get; set; }
               
        public virtual string CategoryName { get; set; }
               
        public virtual string Description { get; set; }
               
        public virtual byte[] Picture { get; set; }

        #endregion Properties

        #region Methods
        
        public CategoryDTO()
        {
            CategoryId = LibraryDefaults.Default_Int32;
            CategoryName = LibraryDefaults.Default_String;
            Description = null;
            Picture = null;
            LookupText = null;
        }
        
        public CategoryDTO(
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

        public CategoryDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<CategoryDTO, Category> GetDataSelector()
        {
            return x => new Category
            (
                x.CategoryId,
                x.CategoryName,
                x.Description,
                x.Picture
            );
        }

        public override Func<Category, CategoryDTO> GetDTOSelector()
        {
            return x => new CategoryDTO
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
                Category category = (Category)data;
                CategoryDTO dto = (new List<Category> { category })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                dto.LookupText = category.LookupText;

                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<CategoryDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
