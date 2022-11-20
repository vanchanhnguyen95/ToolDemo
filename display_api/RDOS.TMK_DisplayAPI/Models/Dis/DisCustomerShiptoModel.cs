using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class DisCustomerShiptoModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayLevelCode { get; set; }
        public string DisplayLevelName { get; set; }
        public string UserName { get; set; }
        [Required]
        [MaxLength(10)]
        public string TypeSalePoint { get; set; }
        public bool? IsSales { get; set; }
        public decimal NumberSalesHas { get; set; }
        public decimal SaleUnit { get; set; }
        public decimal TotalSalePoint { get; set; }
        public decimal TotalSalePointWithPOSM { get; set; }
        public decimal TotalSalePointWithoutPOSM { get; set; }
        public List<DisCustomerShiptoDetailModel> DisCustomerShiptoDetailModels { get; set; }
    }

    public class DisCustomerShiptoDetailModel
    {
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
        public decimal SaleNumbers { get; set; }
        public decimal QuantityNumbers { get; set; }
    }

    public class DeleteDisCustomerShiptosModel
    {
        public string DisplayCode { get; set; }
        public List<string> DisplayLevelCodes { get; set; }
    }
}
