using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PriceSettingAuditLog
    {
        public Guid Id { get; set; }
        public bool OverwiteDefaultSalesPriceOld { get; set; }
        public bool DistributerSalesPriceOld { get; set; }
        public bool ReferenceSellingPriceOld { get; set; }
        public int SalesPriceRoudingOld { get; set; }
        public bool OverwiteDefaultSalesPriceNew { get; set; }
        public bool DistributerSalesPriceNew { get; set; }
        public bool ReferenceSellingPriceNew { get; set; }
        public int SalesPriceRoudingNew { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string IpAdress { get; set; }
    }
}
