using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisBudgetForCusAttribute : DisAuditableEntity
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
        [MaxLength(10)]
        public string ScopeValue { get; set; }
        [Required]
        [MaxLength(10)]
        public string CustomerLevel { get; set; }
        [Required]
        [MaxLength(10)]
        public string CustomerValue { get; set; }
        public decimal BudgetQuantity { get; set; }
        public decimal NewBudgetQuantity { get; set; }
        public decimal BudgetQuantityWait { get; set; }
        public decimal BudgetQuantityUsed { get; set; }
        public int Type { get; set; }
        public int AdjustmentsCount { get; set; }
        public DateTime AdjustmentDate { get; set; }
    }
}
