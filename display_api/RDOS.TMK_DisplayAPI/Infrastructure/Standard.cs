using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class Standard
    {
        public Guid Id { get; set; }
        public Guid ItemGroupId { get; set; }
        public string Description { get; set; }
        public int RuleType { get; set; }
        public bool AllocateToItemGroup { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
