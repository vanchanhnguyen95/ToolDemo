using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerAdjustmentShipto
    {
        public Guid Id { get; set; }
        public string OutletShiptoId { get; set; }
        public string Status { get; set; }
        public string SaleMan { get; set; }
        public Guid CustomerAdjustmentId { get; set; }
        public Guid CustomerShiptoId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public virtual CustomerAdjustment CustomerAdjustment { get; set; }
        public virtual CustomerShipto CustomerShipto { get; set; }
    }
}
