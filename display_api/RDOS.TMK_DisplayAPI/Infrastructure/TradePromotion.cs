using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TradePromotion
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Scheme { get; set; }
        public DateTime EffectiveDateFrom { get; set; }
        public DateTime? EffectiveDateBefore { get; set; }
        public DateTime? ValidUntil { get; set; }
        public DateTime? ValidUntilBefore { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string SaleOrg { get; set; }
        public string SicCode { get; set; }
        public bool IsProgram { get; set; }
        public string FreequencyCode { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
