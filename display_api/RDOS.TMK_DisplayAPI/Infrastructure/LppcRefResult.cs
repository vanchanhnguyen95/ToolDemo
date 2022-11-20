using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class LppcRefResult
    {
        public Guid Id { get; set; }
        public string Sitype { get; set; }
        public string Sicode { get; set; }
        public DateTime CalculatorTime { get; set; }
        public string OrderCode { get; set; }
        public string SalesPeriodCode { get; set; }
        public string SalesWeekCode { get; set; }
        public int Sku { get; set; }
        public string VisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitStep { get; set; }
        public string ScorderValue { get; set; }
        public string ScnoofSku { get; set; }
        public string LppcorderorInvoiceValue { get; set; }
        public string LppcnoofSku { get; set; }
        public string VisitStepReason { get; set; }
        public string RecognizedDateForPc { get; set; }
        public string CusOrderOutWorkPlan { get; set; }
        public string SalesManOrderCreatedByDist { get; set; }
        public string DistOrderForCusofSalesman { get; set; }
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
    }
}
