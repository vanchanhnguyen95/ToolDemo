using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable
{
    public class TempDisCustomerShiptoNotHave : DisAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(10)]
        public string CustomerCode { get; set; }
        [MaxLength(200)]
        public string CustomerName { get; set; }
        [MaxLength(10)]
        public string CustomerShiptoCode { get; set; }
        [MaxLength(200)]
        public string CustomerShiptoName { get; set; }
        [MaxLength(1000)]
        public string Address { get; set; }
        [MaxLength(10)]
        public string InventoryItemCode { get; set; }
        public bool Presence { get; set; }
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
    }
}
