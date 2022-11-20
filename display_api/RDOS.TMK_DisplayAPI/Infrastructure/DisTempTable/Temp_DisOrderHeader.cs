using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable
{
    public class TempDisOrderHeader
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string OrdNbr { get; set; }

        [Required]
        public DateTime OrdDate { get; set; }

        [MaxLength(10)]
        public string TMKType { get; set; }

        [MaxLength(10)]
        public string DiscountCode { get; set; }

        [MaxLength(255)]
        public string DiscountName { get; set; }

        [MaxLength(10)]
        public string DisplayLevel { get; set; }

        [MaxLength(10)]
        public string DisplayLevelName { get; set; }

        [MaxLength(10)]
        public string PrincipalId { get; set; }

        [MaxLength(10)]
        public string DistyBilltoCode { get; set; }
        
        [MaxLength(10)]
        public string PeriodCode { get; set; }

        [MaxLength(10)]
        public string SalesRepCode { get; set; }

        [MaxLength(10)]
        public string RouteZoneId { get; set; }

        [MaxLength(10)]
        public string CustomerId { get; set; }

        [MaxLength(10)]
        public string ShiptoId { get; set; }

        [MaxLength(100)]
        public string CustomerAttribute0 { get; set; }

        [MaxLength(100)]
        public string CustomerAttribute1 { get; set; }

        [MaxLength(100)]
        public string CustomerAttribute2 { get; set; }

        [MaxLength(100)]
        public string CustomerAttribute3 { get; set; }

        [MaxLength(100)]
        public string CustomerAttribute4 { get; set; }

        [MaxLength(100)]
        public string CustomerAttribute5 { get; set; }

        [MaxLength(100)]
        public string CustomerAttribute6 { get; set; }

        [MaxLength(100)]
        public string CustomerAttribute7 { get; set; }

        [MaxLength(100)]
        public string CustomerAttribute8 { get; set; }

        [MaxLength(100)]
        public string CustomerAttribute9 { get; set; }

        [MaxLength(100)]
        public string Status { get; set; }

        [MaxLength(10)]
        public string RecallOrderCode { get; set; }

        public decimal? SoShippedDiscAmt { get; set; }

        [MaxLength(10)]
        public string AreaCode { get; set; }

        [MaxLength(10)]
        public string AreaManagerCode { get; set; }

        [MaxLength(10)]
        public string BranchCode { get; set; }

        [MaxLength(10)]
        public string BranchManagerCode { get; set; }

        [MaxLength(10)]
        public string DsaCode { get; set; }

        [MaxLength(10)]
        public string NsdCode { get; set; }

        [MaxLength(10)]
        public string RzSuppervisorCode { get; set; }

        [MaxLength(10)]
        public string RegionCode { get; set; }

        [MaxLength(10)]
        public string RegionManagerCode { get; set; }

        [MaxLength(10)]
        public string SicCode { get; set; }

        [MaxLength(10)]
        public string SalesOrgCode { get; set; }

        [MaxLength(10)]
        public string SubAreaCode { get; set; }

        [MaxLength(10)]
        public string SubRegionCode { get; set; }

        [MaxLength(10)]
        public string SubAreaManagerCode { get; set; }

        [MaxLength(10)]
        public string SubRegionManagerCode { get; set; }

        [MaxLength(255)]
        public string CustomerName { get; set; }

        [MaxLength(255)]
        public string RouteZoneName { get; set; }

        [MaxLength(255)]
        public string ShiptoName { get; set; }

        [MaxLength(255)]
        public string ReferenceLink { get; set; }

        [MaxLength(255)]
        public string DistyBilltoName { get; set; }
    }
}
