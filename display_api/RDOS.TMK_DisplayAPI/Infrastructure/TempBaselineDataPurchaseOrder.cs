using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempBaselineDataPurchaseOrder
    {
        public Guid Id { get; set; }
        public string RequestPurchaseOrder { get; set; }
        public string VisitCode { get; set; }
        public DateTime VisitDate { get; set; }
        public string OrderType { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime CompleteDate { get; set; }
        public string CustomerCode { get; set; }
        public string SocreateBy { get; set; }
        public string SoownerCode { get; set; }
        public bool OrderOutRoute { get; set; }
        public decimal SoshippedAmt { get; set; }
        public decimal SoshippedExtendAmt { get; set; }
        public bool OrderWithoutVisit { get; set; }
        public string Status { get; set; }
        public string PrincipalCode { get; set; }
        public string PeriodCode { get; set; }
        public string WeekCode { get; set; }
        public string DistributorShiptoCode { get; set; }
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
        public string Nsdcode { get; set; }
        public string BranchManagerCode { get; set; }
        public string RegionManagerCode { get; set; }
        public string SubRegionManagerCode { get; set; }
        public string AreaManagerCode { get; set; }
        public string SubAreaManagerCode { get; set; }
        public string DsamanagerCode { get; set; }
        public string RzsuppervisorCode { get; set; }
        public string Siccode { get; set; }
    }
}
