using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TerritoryStructureDetail
    {
        public Guid Id { get; set; }
        public Guid? TerritoryStructureId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int Level { get; set; }
        public string Source { get; set; }
        public string TerritoryStructureCode { get; set; }

        public virtual TerritoryStructure TerritoryStructure { get; set; }
    }
}
