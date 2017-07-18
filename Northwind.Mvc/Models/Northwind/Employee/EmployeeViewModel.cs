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
    public partial class EmployeeViewModel : ZViewBase<EmployeeViewModel, EmployeeDTO, Employee>
    {
        #region Properties
        
        [Display(Name = "PropertyEmployeeId", ResourceType = typeof(EmployeeResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int EmployeeId { get; set; }
        
        [Display(Name = "PropertyLastName", ResourceType = typeof(EmployeeResources))]
        [Required]
        [StringLength(20)]
        public virtual string LastName { get; set; }
        
        [Display(Name = "PropertyFirstName", ResourceType = typeof(EmployeeResources))]
        [Required]
        [StringLength(10)]
        public virtual string FirstName { get; set; }
        
        [Display(Name = "PropertyTitle", ResourceType = typeof(EmployeeResources))]
        [StringLength(30)]
        public virtual string Title { get; set; }
        
        [Display(Name = "PropertyTitleOfCourtesy", ResourceType = typeof(EmployeeResources))]
        [StringLength(25)]
        public virtual string TitleOfCourtesy { get; set; }
        
        [Display(Name = "PropertyBirthDate", ResourceType = typeof(EmployeeResources))]
        //[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        public virtual DateTime? BirthDate { get; set; }
        
        [Display(Name = "PropertyHireDate", ResourceType = typeof(EmployeeResources))]
        //[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        public virtual DateTime? HireDate { get; set; }
        
        [Display(Name = "PropertyAddress", ResourceType = typeof(EmployeeResources))]
        [StringLength(60)]
        public virtual string Address { get; set; }
        
        [Display(Name = "PropertyCity", ResourceType = typeof(EmployeeResources))]
        [StringLength(15)]
        public virtual string City { get; set; }
        
        [Display(Name = "PropertyRegion", ResourceType = typeof(EmployeeResources))]
        [StringLength(15)]
        public virtual string Region { get; set; }
        
        [Display(Name = "PropertyPostalCode", ResourceType = typeof(EmployeeResources))]
        [StringLength(10)]
        public virtual string PostalCode { get; set; }
        
        [Display(Name = "PropertyCountry", ResourceType = typeof(EmployeeResources))]
        [StringLength(15)]
        public virtual string Country { get; set; }
        
        [Display(Name = "PropertyHomePhone", ResourceType = typeof(EmployeeResources))]
        [StringLength(24)]
        public virtual string HomePhone { get; set; }
        
        [Display(Name = "PropertyExtension", ResourceType = typeof(EmployeeResources))]
        [StringLength(4)]
        public virtual string Extension { get; set; }
        
        [Display(Name = "PropertyPhoto", ResourceType = typeof(EmployeeResources))]
        public virtual byte[] Photo { get; set; }
        
        [Display(Name = "PropertyNotes", ResourceType = typeof(EmployeeResources))]
        [StringLength(1024)]
        public virtual string Notes { get; set; }
        
        [Display(Name = "PropertyReportsTo", ResourceType = typeof(EmployeeResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public virtual int? ReportsTo { get; set; }
        
        [Display(Name = "PropertyPhotoPath", ResourceType = typeof(EmployeeResources))]
        [StringLength(255)]
        public virtual string PhotoPath { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string EmployeeLookupText { get; set; } // ReportsTo
    
        #endregion Associations FK

        #region Methods
        
        public EmployeeViewModel()
        {
            EmployeeId = LibraryDefaults.Default_Int32;
            LastName = LibraryDefaults.Default_String;
            FirstName = LibraryDefaults.Default_String;
            Title = null;
            TitleOfCourtesy = null;
            BirthDate = null;
            HireDate = null;
            Address = null;
            City = null;
            Region = null;
            PostalCode = null;
            Country = null;
            HomePhone = null;
            Extension = null;
            Photo = null;
            Notes = null;
            ReportsTo = null;
            PhotoPath = null;
            EmployeeLookupText = null;
            LookupText = null;

            OnConstructor();
        }

        public EmployeeViewModel(
            int employeeId,
            string lastName,
            string firstName,
            string title = null,
            string titleOfCourtesy = null,
            DateTime? birthDate = null,
            DateTime? hireDate = null,
            string address = null,
            string city = null,
            string region = null,
            string postalCode = null,
            string country = null,
            string homePhone = null,
            string extension = null,
            byte[] photo = null,
            string notes = null,
            int? reportsTo = null,
            string photoPath = null,
            string employeeLookupText = null
        )
        {
            EmployeeId = employeeId;
            LastName = lastName;
            FirstName = firstName;
            Title = title;
            TitleOfCourtesy = titleOfCourtesy;
            BirthDate = birthDate;
            HireDate = hireDate;
            Address = address;
            City = city;
            Region = region;
            PostalCode = postalCode;
            Country = country;
            HomePhone = homePhone;
            Extension = extension;
            Photo = photo;
            Notes = notes;
            ReportsTo = reportsTo;
            PhotoPath = photoPath;
            EmployeeLookupText = employeeLookupText;
            LookupText = null;
        }

        public EmployeeViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public EmployeeViewModel(IZDTOBase<EmployeeDTO, Employee> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<EmployeeViewModel, EmployeeDTO> GetDTOSelector()
        {
            return x => new EmployeeDTO
            (
                x.EmployeeId,
                x.LastName,
                x.FirstName,
                x.Title,
                x.TitleOfCourtesy,
                x.BirthDate,
                x.HireDate,
                x.Address,
                x.City,
                x.Region,
                x.PostalCode,
                x.Country,
                x.HomePhone,
                x.Extension,
                x.Photo,
                x.Notes,
                x.ReportsTo,
                x.PhotoPath
            );
        }

        public override Func<EmployeeDTO, EmployeeViewModel> GetViewSelector()
        {
            return x => new EmployeeViewModel
            (
                x.EmployeeId,
                x.LastName,
                x.FirstName,
                x.Title,
                x.TitleOfCourtesy,
                x.BirthDate,
                x.HireDate,
                x.Address,
                x.City,
                x.Region,
                x.PostalCode,
                x.Country,
                x.HomePhone,
                x.Extension,
                x.Photo,
                x.Notes,
                x.ReportsTo,
                x.PhotoPath
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                EmployeeDTO employeeDTO = new EmployeeDTO(data);
                EmployeeViewModel view = (new List<EmployeeDTO> { employeeDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.EmployeeLookupText = employeeDTO.EmployeeLookupText;
                view.LookupText = employeeDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<EmployeeDTO, Employee> dto)
        {
            if (dto != null)
            {
                EmployeeDTO employeeDTO = (EmployeeDTO)dto;
                EmployeeViewModel view = (new List<EmployeeDTO> { employeeDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.EmployeeLookupText = employeeDTO.EmployeeLookupText;
                view.LookupText = employeeDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<EmployeeDTO, Employee> ToDTO()
        {
            return (new List<EmployeeViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
