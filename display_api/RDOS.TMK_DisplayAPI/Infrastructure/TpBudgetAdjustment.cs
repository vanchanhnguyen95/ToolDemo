using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TpBudgetAdjustment
    {
        public Guid Id { get; set; }
        public string BudgetCode { get; set; }
        public decimal BudgetQuantity { get; set; }
        public decimal BudgetQuantityNew { get; set; }
        public decimal TotalAmountAllotment { get; set; }
        public decimal TotalAmountAllotmentNew { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string Account { get; set; }
        public string Reason { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int CountAdjustment { get; set; }
        public int Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int DeleteFlag { get; set; }
    }
}
