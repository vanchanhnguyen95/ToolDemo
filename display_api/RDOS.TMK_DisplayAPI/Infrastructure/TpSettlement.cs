using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TpSettlement
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime SettlementDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool SchemeType { get; set; }
        public int DeleteFlag { get; set; }
        public string FrequencyCode { get; set; }
        public int FrequencySettlement { get; set; }
        public string ProgramType { get; set; }
        public string PromotionDiscountCode { get; set; }
        public string PromotionDiscountScheme { get; set; }
        public string SaleCalendarCode { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDistributor { get; set; }
    }
}
