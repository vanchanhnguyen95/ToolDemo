using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisCustomerShiptoDetail : DisAuditableEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayLevelCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string CustomerCode { get; set; }
        [MaxLength(200)]
        public string CustomerName { get; set; }
        [Required]
        [MaxLength(10)]
        public string CustomerShiptoCode { get; set; }
        [MaxLength(200)]
        public string CustomerShiptoName { get; set; }
        [MaxLength(1000)]
        public string Address { get; set; }
        [MaxLength(10)]
        public string InventoryItemCode { get; set; }
        public bool Presence { get; set; }
        [MaxLength(10)]
        public string SaleOrgCode { get; set; }
        [MaxLength(10)]
        public string BranchCode { get; set; }
        [MaxLength(10)]
        public string RegionCode { get; set; }
        [MaxLength(10)]
        public string SubRegionCode { get; set; }
        [MaxLength(10)]
        public string AreaCode { get; set; }
        [MaxLength(10)]
        public string SubAreaCode { get; set; }
        [MaxLength(10)]
        public string DsaCode { get; set; }
        [MaxLength(10)]
        public string RouteZoneCode { get; set; }
        [MaxLength(10)]
        public decimal SaleNumbers { get; set; }
        public decimal QuantityNumbers { get; set; }
    }
}
