using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TpPromotion
    {
        public Guid Id { get; set; }
        public string PromotionType { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public string Scheme { get; set; }
        public DateTime EffectiveDateFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public string SaleOrg { get; set; }
        public string SicCode { get; set; }
        public int SettlementFrequency { get; set; }
        public string FrequencyPromotion { get; set; }
        public string ImageName1 { get; set; }
        public string ImagePath1 { get; set; }
        public string ImageName2 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImageName3 { get; set; }
        public string ImagePath3 { get; set; }
        public string ImageName4 { get; set; }
        public string ImagePath4 { get; set; }
        public string ScopeType { get; set; }
        public string ScopeSaleTerritoryLevel { get; set; }
        public bool IsProgram { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ApplicableObjectType { get; set; }
        public bool PromotionCheckBy { get; set; }
        public bool RuleOfGiving { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsApplyBudget { get; set; }
        public int DeleteFlag { get; set; }
        public string ReasonStep1 { get; set; }
        public string ReasonStep2 { get; set; }
        public string ReasonStep3 { get; set; }
        public string ReasonStep4 { get; set; }
        public string ReasonStep5 { get; set; }
        public string UserName { get; set; }
        public bool RuleOfGivingByValue { get; set; }
    }
}
