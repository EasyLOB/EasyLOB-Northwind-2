using EasyLOB.AuditTrail.Data;
using EasyLOB.AuditTrail.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EasyLOB.AuditTrail.Mvc
{
    public partial class AuditTrailLogViewModel : ZViewBase<AuditTrailLogViewModel, AuditTrailLogDTO, AuditTrailLog>
    {
        #region Properties
        
        [Display(Name = "PropertyId", ResourceType = typeof(AuditTrailLogResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int Id { get; set; }
        
        [Display(Name = "PropertyLogDate", ResourceType = typeof(AuditTrailLogResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public virtual DateTime? LogDate { get; set; }
        
        [Display(Name = "PropertyLogTime", ResourceType = typeof(AuditTrailLogResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public virtual DateTime? LogTime { get; set; }
        
        [Display(Name = "PropertyLogUserName", ResourceType = typeof(AuditTrailLogResources))]
        [StringLength(256)]
        public virtual string LogUserName { get; set; }
        
        [Display(Name = "PropertyLogDomain", ResourceType = typeof(AuditTrailLogResources))]
        [DisplayFormat(ConvertEmptyStringToNull = false)] // !?!
        //[Required(AllowEmptyStrings = true)] // !?!
        [StringLength(256)]
        public virtual string LogDomain { get; set; }
        
        [Display(Name = "PropertyLogEntity", ResourceType = typeof(AuditTrailLogResources))]
        [Required]
        [StringLength(256)]
        public virtual string LogEntity { get; set; }
        
        [Display(Name = "PropertyLogOperation", ResourceType = typeof(AuditTrailLogResources))]
        [StringLength(1)]
        public virtual string LogOperation { get; set; }
        
        [Display(Name = "PropertyLogId", ResourceType = typeof(AuditTrailLogResources))]
        [StringLength(4096)]
        public virtual string LogId { get; set; }
        
        [Display(Name = "PropertyLogEntityBefore", ResourceType = typeof(AuditTrailLogResources))]
        [StringLength(4096)]
        public virtual string LogEntityBefore { get; set; }
        
        [Display(Name = "PropertyLogEntityAfter", ResourceType = typeof(AuditTrailLogResources))]
        [StringLength(4096)]
        public virtual string LogEntityAfter { get; set; }

        #endregion Properties

        #region Methods
        
        public AuditTrailLogViewModel()
        {
            Id = LibraryDefaults.Default_Int32;
            LogDate = null;
            LogTime = null;
            LogUserName = null;
            LogDomain = null;
            LogEntity = null;
            LogOperation = null;
            LogId = null;
            LogEntityBefore = null;
            LogEntityAfter = null;
            LookupText = null;

            OnConstructor();
        }

        public AuditTrailLogViewModel(
            int id,
            DateTime? logDate = null,
            DateTime? logTime = null,
            string logUserName = null,
            string logDomain = null,
            string logEntity = null,
            string logOperation = null,
            string logId = null,
            string logEntityBefore = null,
            string logEntityAfter = null
        )
        {
            Id = id;
            LogDate = logDate;
            LogTime = logTime;
            LogUserName = logUserName;
            LogDomain = logDomain;
            LogEntity = logEntity;
            LogOperation = logOperation;
            LogId = logId;
            LogEntityBefore = logEntityBefore;
            LogEntityAfter = logEntityAfter;
            LookupText = null;
        }

        public AuditTrailLogViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public AuditTrailLogViewModel(IZDTOBase<AuditTrailLogDTO, AuditTrailLog> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<AuditTrailLogViewModel, AuditTrailLogDTO> GetDTOSelector()
        {
            return x => new AuditTrailLogDTO
            (
                x.Id,
                x.LogDate,
                x.LogTime,
                x.LogUserName,
                x.LogDomain,
                x.LogEntity,
                x.LogOperation,
                x.LogId,
                x.LogEntityBefore,
                x.LogEntityAfter
            );
        }

        public override Func<AuditTrailLogDTO, AuditTrailLogViewModel> GetViewSelector()
        {
            return x => new AuditTrailLogViewModel
            (
                x.Id,
                x.LogDate,
                x.LogTime,
                x.LogUserName,
                x.LogDomain,
                x.LogEntity,
                x.LogOperation,
                x.LogId,
                x.LogEntityBefore,
                x.LogEntityAfter
            );
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                AuditTrailLogDTO auditTrailLogDTO = new AuditTrailLogDTO(data);
                AuditTrailLogViewModel view = (new List<AuditTrailLogDTO> { auditTrailLogDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = auditTrailLogDTO.LookupText;

                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<AuditTrailLogDTO, AuditTrailLog> dto)
        {
            if (dto != null)
            {
                AuditTrailLogDTO auditTrailLogDTO = (AuditTrailLogDTO)dto;
                AuditTrailLogViewModel view = (new List<AuditTrailLogDTO> { auditTrailLogDTO })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                view.LookupText = auditTrailLogDTO.LookupText;

                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<AuditTrailLogDTO, AuditTrailLog> ToDTO()
        {
            return (new List<AuditTrailLogViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
