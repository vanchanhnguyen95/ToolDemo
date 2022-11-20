using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisDefinitionStructure : DisAuditableEntity
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
        [Required]
        [MaxLength(255)]
        public string LevelName { get; set; }
        [Required]
        public int DisplaySupportToolCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string ProductTypeForDisplay { get; set; }
        public string ItemHierarchyLevelDisplay { get; set; }
        [Required]
        public bool IsImagesOK { get; set; }
        public int? PercentImagesOK { get; set; }
        [Required]
        public bool IsRewardProduct { get; set; }
        [Required]
        public bool IsRewardDonate { get; set; }
        [Required]
        public bool IsRewardFixMoney { get; set; }
        [Required]
        public bool RewardRuleOfGiving { get; set; }
        [MaxLength(10)]
        public string RewardProductType { get; set; }
        public string ItemHierarchyLevelReward { get; set; }
        [Required]
        public decimal RewardAmountOfDonation { get; set; }
        [Required]
        public float RewardPercentageOfAmount { get; set; }
        public bool? IsCheckSalesOutput { get; set; }
        public decimal? SalesToBeAchieved { get; set; }
        public decimal? OutputToBeAchieved { get; set; }
        [MaxLength(2)]
        public string Conditon { get; set; }
        public bool? IsAllProucts { get; set; }
        [Required]
        public bool IsSalesOutputProduct { get; set; }
        [Required]
        public bool IsSalesOutputDonate { get; set; }
        [Required]
        public bool IsSalesOutputFixMoney { get; set; }
        [Required]
        public bool SalesOutputRuleOfGiving { get; set; }
        [MaxLength(10)]
        public string SalesOutputProductType { get; set; }
        public string ItemHierarchyLevelSalesOut { get; set; }
        [Required]
        public decimal SalesOutputAmountOfDonation { get; set; }
        [Required]
        public float SalesOutputPercentageOfAmount { get; set; }
        [Required]
        public bool IsUseWeights { get; set; }
        [MaxLength(10)]
        public string UseWeightsType { get; set; }
        public bool? IsBonusGiftProduct { get; set; }
        public bool? IsBonusDonate { get; set; }
        public bool? IsBonusFixMoney { get; set; }
    }
}
