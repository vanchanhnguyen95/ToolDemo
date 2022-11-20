using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisConfirmResultDetail : DisAuditableEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisConfirmResultCode { get; set; }
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayLevelCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string CustomerCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string CustomerShiptoCode { get; set; }
        public int NumberMustRating { get; set; }
        public int NumberHasEvaluate { get; set; }
        public int NumberPassed { get; set; }
        public decimal SalesPass { get; set; }
        public decimal OutputPass { get; set; }
        [Required]
        public bool DisplayImageResult { get; set; }
        public string DisplayImageResultDes { get; set; }
        [Required]
        public bool DisplaySalesResult { get; set; }
        public string DisplaySalesResultDes { get; set; }
        [Required]
        public bool AssessmentPeriodResult { get; set; }
        public string AssessmentPeriodResultDes { get; set; }

        public DisConfirmResultDetail InitInsert(string createdBy)
        {
            CreatedDate = DateTime.Now;
            CreatedBy = createdBy;
            return this;
        }
        public DisConfirmResultDetail InitUpdate(string updatedBy)
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
