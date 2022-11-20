using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempBaselineDataVisit
    {
        public Guid Id { get; set; }
        public string VisitCode { get; set; }
        public DateTime VisitDate { get; set; }
        public string PeriodCode { get; set; }
        public string VisitTimeFrom { get; set; }
        public string VisitTimeTo { get; set; }
        public string TimeDuration { get; set; }
        public string SalesmanCode { get; set; }
        public string CustomerCode { get; set; }
        public bool VisitOutRoute { get; set; }
        public bool HaveOrder { get; set; }
        public string Principal { get; set; }
        public string DistributorCode { get; set; }
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
        public string Siccode { get; set; }
        public string WeekCode { get; set; }
    }
}
