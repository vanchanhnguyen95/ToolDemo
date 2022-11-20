using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TpScopeDiscountDetail
    {
        public Guid Id { get; set; }
        public string DiscountCode { get; set; }
        public string ScopeType { get; set; }
        public string SalesTerritoryLevelCode { get; set; }
        public string SalesTerritoryValueCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int DeleteFlag { get; set; }
    }
}
