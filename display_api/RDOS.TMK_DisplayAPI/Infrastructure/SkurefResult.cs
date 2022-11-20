using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class SkurefResult
    {
        public Guid Id { get; set; }
        public string Sitype { get; set; }
        public string Sicode { get; set; }
        public DateTime CalculatorTime { get; set; }
        public string OrderCode { get; set; }
        public string SalesWeekCode { get; set; }
        public string SalesmanCode { get; set; }
        public string CustomerCode { get; set; }
        public string DistributorCode { get; set; }
        public string VisitCode { get; set; }
        public DateTime VisitDate { get; set; }
        public string InventoryCode { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Volumne { get; set; }
        public decimal? Point { get; set; }
        public decimal? ShippedQty { get; set; }
        public decimal? ShippedBaseQty { get; set; }
        public bool CusOrderOutWorkPlan { get; set; }
        public bool SalesManOrderCreatedByDist { get; set; }
        public bool DistOrderForCusofSalesman { get; set; }
        public string SocreateBy { get; set; }
        public string SoowerCode { get; set; }
        public bool OrderOutRoute { get; set; }
        public decimal ShippedLineExtendAmt { get; set; }
        public bool Result { get; set; }
        public string SalesOrgCode { get; set; }
        public string SalesTerritoryCode { get; set; }
        public string CountryCode { get; set; }
        public string BranchCode { get; set; }
        public string RegionCode { get; set; }
        public string SubRegionCode { get; set; }
        public string AreaCode { get; set; }
        public string SubAreaCode { get; set; }
        public string Dsacode { get; set; }
        public string RouteZoneCode { get; set; }
        public int CountSku { get; set; }
        public string SalesPeriodCode { get; set; }
    }
}
