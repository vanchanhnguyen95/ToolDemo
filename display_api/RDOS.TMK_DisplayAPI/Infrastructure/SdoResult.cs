using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class SdoResult
    {
        public Guid Id { get; set; }
        public string InventoryItem { get; set; }
        public string PeriodCode { get; set; }
        public int SdoInStoreAmount { get; set; }
        public int TotalStore { get; set; }
        public int DeleteFlag { get; set; }
        public decimal SdopercentConverage { get; set; }
        public decimal SdopercentGrow { get; set; }
        public bool Sdoresult1 { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int SaleCycle { get; set; }
        public string AreaCode { get; set; }
        public string BranchCode { get; set; }
        public string CountryCode { get; set; }
        public string Dsacode { get; set; }
        public string ItemGroupCode { get; set; }
        public string RegionCode { get; set; }
        public string RouteZoneCode { get; set; }
        public string SalesOrgCode { get; set; }
        public string SalesTerritoryCode { get; set; }
        public string SubAreaCode { get; set; }
        public string SubRegionCode { get; set; }
        public string HierachyCode { get; set; }
        public Guid HierachyId { get; set; }
    }
}
