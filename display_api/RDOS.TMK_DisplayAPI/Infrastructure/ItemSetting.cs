using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ItemSetting
    {
        public Guid Id { get; set; }
        public string AttributeId { get; set; }
        public string AttributeName { get; set; }
        public string Description { get; set; }
        public bool IsHierarchy { get; set; }
        public bool Used { get; set; }
        public bool IsEdit { get; set; }
        public int? Level { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
