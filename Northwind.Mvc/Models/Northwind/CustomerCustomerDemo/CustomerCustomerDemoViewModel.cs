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
    public partial class CustomerCustomerDemoViewModel : ZViewBase<CustomerCustomerDemoViewModel, CustomerCustomerDemoDTO, CustomerCustomerDemo>
    {
        #region Properties
        
        [Display(Name = "PropertyCustomerId", ResourceType = typeof(CustomerCustomerDemoResources))]
        //[Key]
        //[Column(Order=1)]
        [Required]
        [StringLength(5)]
        public virtual string CustomerId { get; set; }
        
        [Display(Name = "PropertyCustomerTypeId", ResourceType = typeof(CustomerCustomerDemoResources))]
        //[Key]
        //[Column(Order=2)]
        [Required]
        [StringLength(10)]
        public virtual string CustomerTypeId { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string CustomerDemographicLookupText { get; set; } // CustomerTypeId

        public virtual string CustomerLookupText { get; set; } // CustomerId
    
        #endregion Associations FK

        #region Methods
        
        public CustomerCustomerDemoViewModel()
        {
            CustomerId = LibraryDefaults.Default_String;
            CustomerTypeId = LibraryDefaults.Default_String;
            CustomerDemographicLookupText = null;
            CustomerLookupText = null;
            LookupText = null;

            OnConstructor();
        }

        public CustomerCustomerDemoViewModel(
            string customerId,
            string customerTypeId,
            string customerDemographicLookupText = null,
            string customerLookupText = null
        )
        {
            CustomerId = customerId;
            CustomerTypeId = customerTypeId;
            CustomerDemographicLookupText = customerDemographicLookupText;
            CustomerLookupText = customerLookupText;
            LookupText = null;
        }

        public CustomerCustomerDemoViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public CustomerCustomerDemoViewModel(IZDTOBase<CustomerCustomerDemoDTO, CustomerCustomerDemo> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<CustomerCustomerDemoViewModel, CustomerCustomerDemoDTO> GetDTOSelector()
        {
            return x => new CustomerCustomerDemoDTO
            (
                x.CustomerId,
                x.CustomerTypeId
            );
        }

        public override Func<CustomerCustomerDemoDTO, CustomerCustomerDemoViewModel> GetViewSelector()
        {
            return x => new CustomerCustomerDemoViewModel
            (
                x.CustomerId,
                x.CustomerTypeId
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                CustomerCustomerDemoDTO customerCustomerDemoDTO = new CustomerCustomerDemoDTO(data);
                CustomerCustomerDemoViewModel view = (new List<CustomerCustomerDemoDTO> { customerCustomerDemoDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.CustomerDemographicLookupText = customerCustomerDemoDTO.CustomerDemographicLookupText;
                view.CustomerLookupText = customerCustomerDemoDTO.CustomerLookupText;
                view.LookupText = customerCustomerDemoDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<CustomerCustomerDemoDTO, CustomerCustomerDemo> dto)
        {
            if (dto != null)
            {
                CustomerCustomerDemoDTO customerCustomerDemoDTO = (CustomerCustomerDemoDTO)dto;
                CustomerCustomerDemoViewModel view = (new List<CustomerCustomerDemoDTO> { customerCustomerDemoDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.CustomerDemographicLookupText = customerCustomerDemoDTO.CustomerDemographicLookupText;
                view.CustomerLookupText = customerCustomerDemoDTO.CustomerLookupText;
                view.LookupText = customerCustomerDemoDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<CustomerCustomerDemoDTO, CustomerCustomerDemo> ToDTO()
        {
            return (new List<CustomerCustomerDemoViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
