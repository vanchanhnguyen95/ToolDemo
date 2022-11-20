using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.External;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class DisDefinitionStructureDataModel
    {
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

    public class DisDefinitionProductTypeDetailModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string LevelCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string ProductType { get; set; }
        public string ItemHierarchyLevel { get; set; }
        [Required]
        [MaxLength(10)]
        public string ProductCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string Packing { get; set; }
        [Required]
        public int Quantity { get; set; }

        [NotMapped]
        public string ProductDescription { get; set; }
        [NotMapped]
        public string PackingDescription { get; set; }
        [NotMapped]
        public List<UomsModel> ListUom { get; set; } = new List<UomsModel>();
    }

    public class DisDefinitionCriteriaEvaluateModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string LevelCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string CriteriaCode { get; set; }

        [NotMapped]
        public string CriteriaDescription { get; set; }
        [NotMapped]
        public string Result { get; set; }
    }

    public class DisWeightGetExtraRewardsDetailModel
    {
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

        [NotMapped]
        public string ProductDescription { get; set; }
        [NotMapped]
        public string PackingDescription { get; set; }
        [NotMapped]
        public List<UomsModel> ListUom { get; set; } = new List<UomsModel>();
    }

    public class DisDefinitionGuideImageModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string LevelCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        [Required]
        [MaxLength(10)]
        public string Status { get; set; }
    }

    public class DisDefinitionStructureModel
    {
        public DisDefinitionStructureDataModel Structure { get; set; } = new();
        public List<DisDefinitionProductTypeDetailModel> ProductDisplay { get; set; } = new();
        public List<DisDefinitionProductTypeDetailModel> ProductReward { get; set; } = new();
        public List<DisDefinitionProductTypeDetailModel> ProductSaleOut { get; set; } = new();
        public List<DisDefinitionProductTypeDetailModel> ProductRewardReview { get; set; } = new();
        public List<DisDefinitionCriteriaEvaluateModel> CriteriaEvaluates { get; set; } = new();
        public List<DisWeightGetExtraRewardsDetailModel> WeightGetExtraRewards { get; set; } = new();
        public List<DisDefinitionGuideImageModel> GuideImages { get; set; } = new();
    }
}
