using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempBaselineDataPurchaseOrderDetail
    {
        public Guid Id { get; set; }
        public string RequestPurchaseOrder { get; set; }
        public string InventoryCode { get; set; }
        public string UoM { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsFree { get; set; }
        public decimal Discount { get; set; }
        public decimal UnitRate { get; set; }
        public decimal Revenue { get; set; }
        public decimal Volumne { get; set; }
        public decimal OrigOrdLineExtendAmt { get; set; }
        public decimal Point { get; set; }
        public decimal ShippedBaseQty { get; set; }
        public decimal ShippedQty { get; set; }
        public decimal OriginalBaseQty { get; set; }
        public decimal ShippedLineExtendAmt { get; set; }
    }
}
