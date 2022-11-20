using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PriceDefinitionDistributor
    {
        public Guid Id { get; set; }
        public string DistributorCode { get; set; }
        public string DistributorDescription { get; set; }
        public string TerritoryStructureCode { get; set; }
        public Guid DsaId { get; set; }
        public string Dsacode { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupDescription { get; set; }
        public string Uom { get; set; }
        public string Operator { get; set; }
        public decimal ExchangeValue { get; set; }
        public decimal Price { get; set; }
        public DateTime EffectiveTime { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
