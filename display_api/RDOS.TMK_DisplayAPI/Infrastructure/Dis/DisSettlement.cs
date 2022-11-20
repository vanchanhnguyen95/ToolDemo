using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisSettlement : DisAuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(10)]
        public string Status { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string RewardPeriodCode { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DisSettlement InitInsert(string createdBy)
        {
            const string IsDefining = "01";
            CreatedDate = DateTime.Now;
            CreatedBy = createdBy;
            Status = IsDefining;
            return this;
        }
        public DisSettlement InitUpdate(string updatedBy)
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
