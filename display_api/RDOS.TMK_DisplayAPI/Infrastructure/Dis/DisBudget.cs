using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisBudget : DisAuditableEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayLevelCode { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal NewTotalBudget { get; set; }
        public decimal BudgetQuantityWait { get; set; }
        public decimal BudgetQuantityUsed { get; set; }
        public DateTime AdjustmentDate { get; set; }
        [MaxLength(100)]
        public string AdjustmentsAccount { get; set; }
        [MaxLength(255)]
        public string AdjustmentsReason { get; set; }
        [MaxLength(255)]
        public string AdjustmentsFilePath { get; set; }
        [MaxLength(255)]
        public string AdjustmentsFileName { get; set; }
        public int AdjustmentsCount { get; set; }
        public int Type { get; set; }
    }
}
