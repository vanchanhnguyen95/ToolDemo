using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class StandardItem
    {
        public Guid Id { get; set; }
        public Guid StandardId { get; set; }
        public Guid InventoryItemId { get; set; }
        public int? Priority { get; set; }
        public DateTime? EffectiveDateFrom { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Ratio { get; set; }
        public int DeleteFlag { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
