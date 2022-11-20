using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisConfirmResult : DisAuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }
        [Required]
        [MaxLength(10)]
        public string Status { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string SalesCalendarCode { get; set; }

        public bool IsNumberVisits { get; set; }
        public int? NumberVisitsType { get; set; }
        public int? NumberVisits { get; set; }
        public decimal? PercentPass { get; set; }

        public DisConfirmResult InitInsert(string createdBy)
        {
            const string IsDefining = "01";
            CreatedDate = DateTime.Now;
            CreatedBy = createdBy;
            Status = IsDefining;
            return this;
        }
        public DisConfirmResult InitUpdate(string updatedBy)
        {
            if (DeleteFlag != 1)
            {
                UpdatedBy = updatedBy;
                UpdatedDate = DateTime.Now;
            }

            return this;
        }

    }

    
}
