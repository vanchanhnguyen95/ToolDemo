using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TpBudgetDefine
    {
        public Guid Id { get; set; }
        public string BudgetCode { get; set; }
        public string PromotionProductType { get; set; }
        public string PromotionProductCode { get; set; }
        public string PackSize { get; set; }
        public decimal BudgetQuantity { get; set; }
        public decimal BudgetQuantityWait { get; set; }
        public decimal BudgetQuantityAvailable { get; set; }
        public decimal BudgetQuantityUsed { get; set; }
        public string ItemHierarchyLevel { get; set; }
        public string ItemHierarchyValue { get; set; }
        public decimal TotalAmountAllotment { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int DeleteFlag { get; set; }
    }
}
