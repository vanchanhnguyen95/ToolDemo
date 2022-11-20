using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisCustomerShipto : DisAuditableEntity
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
        [Required]
        [MaxLength(10)]
        public string TypeSalePoint { get; set; }
        public bool? IsSales { get; set; }
        public decimal NumberSalesHas { get; set; }
        public decimal SaleUnit { get; set; }
        public decimal TotalSalePoint { get; set; }
        public decimal TotalSalePointWithPOSM { get; set; }
        public decimal TotalSalePointWithoutPOSM { get; set; }
    }
}
