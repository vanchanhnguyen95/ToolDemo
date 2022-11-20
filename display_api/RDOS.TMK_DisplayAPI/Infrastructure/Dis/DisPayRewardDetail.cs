using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisPayRewardDetail : DisAuditableEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisPayRewardCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayLevelCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string CustomerCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string CustomerShiptoCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string RewardType { get; set; }
        [MaxLength(10)]
        public string ProductType { get; set; }
        public string ItemHierarchyLevel { get; set; }
        [MaxLength(10)]
        public string ProductCode { get; set; }
        [MaxLength(10)]
        public string PackingCode { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Amount { get; set; }
    }
}
