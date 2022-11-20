using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PriceSetting
    {
        public Guid Id { get; set; }
        public bool OverwiteDefaultSalesPrice { get; set; }
        public bool DistributerSalesPrice { get; set; }
        public bool ReferenceSellingPrice { get; set; }
        public int SalesPriceRouding { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
