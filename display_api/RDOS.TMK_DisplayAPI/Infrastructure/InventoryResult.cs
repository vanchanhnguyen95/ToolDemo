using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class InventoryResult
    {
        public Guid Id { get; set; }
        public string RouteZoneCode { get; set; }
        public string OrderCode { get; set; }
        public string CustomerCode { get; set; }
        public string InventoryItem { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Value { get; set; }
        public bool Result { get; set; }
        public string Source { get; set; }
        public int DeleteFlag { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string AreaCode { get; set; }
        public string BranchCode { get; set; }
        public string CountryCode { get; set; }
        public string Dsacode { get; set; }
        public string HierachyCode { get; set; }
        public Guid HierachyId { get; set; }
        public string ItemGroupCode { get; set; }
        public Guid ItemGroupId { get; set; }
        public string RegionCode { get; set; }
        public string SalesOrgCode { get; set; }
        public string SalesTerritoryCode { get; set; }
        public string SubAreaCode { get; set; }
        public string SubRegionCode { get; set; }
        public string SalesPeriod { get; set; }
        public string SalesTerritoryStructure { get; set; }
    }
}
