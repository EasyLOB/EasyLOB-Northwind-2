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
    public partial class ShipperViewModel : ZViewBase<ShipperViewModel, ShipperDTO, Shipper>
    {
        #region Properties
        
        [Display(Name = "PropertyShipperId", ResourceType = typeof(ShipperResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int ShipperId { get; set; }
        
        [Display(Name = "PropertyCompanyName", ResourceType = typeof(ShipperResources))]
        [Required]
        [StringLength(40)]
        public virtual string CompanyName { get; set; }
        
        [Display(Name = "PropertyPhone", ResourceType = typeof(ShipperResources))]
        [StringLength(24)]
        public virtual string Phone { get; set; }

        #endregion Properties

        #region Methods
        
        public ShipperViewModel()
        {
            ShipperId = LibraryDefaults.Default_Int32;
            CompanyName = LibraryDefaults.Default_String;
            Phone = null;
            LookupText = null;

            OnConstructor();
        }

        public ShipperViewModel(
            int shipperId,
            string companyName,
            string phone = null
        )
        {
            ShipperId = shipperId;
            CompanyName = companyName;
            Phone = phone;
            LookupText = null;
        }

        public ShipperViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public ShipperViewModel(IZDTOBase<ShipperDTO, Shipper> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<ShipperViewModel, ShipperDTO> GetDTOSelector()
        {
            return x => new ShipperDTO
            (
                x.ShipperId,
                x.CompanyName,
                x.Phone
            );
        }

        public override Func<ShipperDTO, ShipperViewModel> GetViewSelector()
        {
            return x => new ShipperViewModel
            (
                x.ShipperId,
                x.CompanyName,
                x.Phone
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                ShipperDTO shipperDTO = new ShipperDTO(data);
                ShipperViewModel view = (new List<ShipperDTO> { shipperDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = shipperDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<ShipperDTO, Shipper> dto)
        {
            if (dto != null)
            {
                ShipperDTO shipperDTO = (ShipperDTO)dto;
                ShipperViewModel view = (new List<ShipperDTO> { shipperDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = shipperDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<ShipperDTO, Shipper> ToDTO()
        {
            return (new List<ShipperViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
