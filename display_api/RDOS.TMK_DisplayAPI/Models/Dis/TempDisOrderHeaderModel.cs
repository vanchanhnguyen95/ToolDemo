using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class TempDisOrderHeaderModel
    {
        public Guid Id { get; set; }

        public string OrdNbr { get; set; }

        public DateTime OrdDate { get; set; }

        public string PrincipalId { get; set; }

        public string DistyBilltoCode { get; set; }

        public string PeriodCode { get; set; }

        public string SalesRepCode { get; set; }

        public string RouteZoneId { get; set; }

        public string CustomerId { get; set; }

        public string ShiptoId { get; set; }

        public string CustomerAttribute0 { get; set; }
        public string CustomerAttribute1 { get; set; }
        public string CustomerAttribute2 { get; set; }
        public string CustomerAttribute3 { get; set; }
        public string CustomerAttribute4 { get; set; }
        public string CustomerAttribute5 { get; set; }
        public string CustomerAttribute6 { get; set; }
        public string CustomerAttribute7 { get; set; }
        public string CustomerAttribute8 { get; set; }
        public string CustomerAttribute9 { get; set; }

        public string Status { get; set; }
        public string RecallOrderCode { get; set; }
        public string DiscountCode { get; set; }

        public decimal? SoShippedDiscAmt { get; set; }

        public string AreaCode { get; set; }

        public string AreaManagerCode { get; set; }

        public string BranchCode { get; set; }

        public string BranchManagerCode { get; set; }

        public string DsaCode { get; set; }

        public string NsdCode { get; set; }

        public string RzSuppervisorCode { get; set; }

        public string RegionCode { get; set; }

        public string RegionManagerCode { get; set; }

        public string SicCode { get; set; }

        public string SalesOrgCode { get; set; }

        public string SubAreaCode { get; set; }

        public string SubRegionCode { get; set; }

        public string SubAreaManagerCode { get; set; }

        public string SubRegionManagerCode { get; set; }

        public string CustomerName { get; set; }

        public string RouteZoneName { get; set; }

        public string ShiptoName { get; set; }

        public string ReferenceLink { get; set; }

        public string DiscountName { get; set; }

        public string DistyBilltoName { get; set; }
    }
}
