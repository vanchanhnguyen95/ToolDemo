using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class KitInventoryItemConversion
    {
        public Guid Id { get; set; }
        public bool IsStock { get; set; }
        public Guid KitId { get; set; }
        public Guid InventoryItemIddb { get; set; }
        public string InventoryItemId { get; set; }
        public int? Quantity { get; set; }
        public Guid Uom { get; set; }
        public string UomName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
