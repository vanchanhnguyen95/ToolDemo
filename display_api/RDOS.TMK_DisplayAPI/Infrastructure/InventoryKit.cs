using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class InventoryKit
    {
        public Guid Id { get; set; }
        public string InventoryItemId { get; set; }
        public bool? NonStock { get; set; }
        public string KitDescription { get; set; }
        public string ShortName { get; set; }
        public string Status { get; set; }
        public string StockItem { get; set; }
        public int Quantity { get; set; }
        public string Uom { get; set; }
        public bool? Vat { get; set; }
        public string NonStockItem { get; set; }
        public bool NonQuantity { get; set; }
        public bool NonUom { get; set; }
    }
}
