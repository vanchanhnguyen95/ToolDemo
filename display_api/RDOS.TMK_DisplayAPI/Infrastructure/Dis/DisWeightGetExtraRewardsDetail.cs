using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisWeightGetExtraRewardsDetail : DisAuditableEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string LevelCode { get; set; }
        [MaxLength(10)]
        public string ProductCode { get; set; }
        [MaxLength(10)]
        public string Packing { get; set; }
        public decimal? NumberOfGift { get; set; }
        public decimal? AmountOfGift { get; set; }
        public decimal? AmountOfDonation { get; set; }
        public float? PercentageOfAmount { get; set; }
        public float? PercentageToBeAchieved { get; set; }
        public decimal? SalesToBeAchieved { get; set; }
        public decimal? OutputToBeAchieved { get; set; }
        [Required]
        [MaxLength(10)]
        public string Status { get; set; }
    }
}
