using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerApplyToValue
    {
        public Guid Id { get; set; }
        public string Master { get; set; }
        public string Values { get; set; }
        public string Description { get; set; }
        public Guid CustomerAdjustmentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Guid? CustomerAttributeId { get; set; }
        public string DsaArea { get; set; }
        public string DsaCode { get; set; }
        public Guid? DsaId { get; set; }
        public string RouteZoneCode { get; set; }
        public Guid? RouteZoneId { get; set; }
        public Guid? SalemanId { get; set; }
        public Guid? TerritoryMappingId { get; set; }
        public string TerritoryStructureCode { get; set; }
        public string SalemanCode { get; set; }

        public virtual CustomerAdjustment CustomerAdjustment { get; set; }
    }
}
