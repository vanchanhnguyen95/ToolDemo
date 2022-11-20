using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ScTerritoryMapping
    {
        public Guid Id { get; set; }
        public string MappingNode { get; set; }
        public string TerritoryValueKey { get; set; }
        public string ParentMappingNode { get; set; }
        public string TerritoryStructureCode { get; set; }
        public int Level { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsMoved { get; set; }
        public DateTime? UntilDate { get; set; }
    }
}
