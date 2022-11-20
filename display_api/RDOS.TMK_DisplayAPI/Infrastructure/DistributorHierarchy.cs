using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class DistributorHierarchy
    {
        public DistributorHierarchy()
        {
            DistributorHierarchyMappings = new HashSet<DistributorHierarchyMapping>();
        }

        public Guid Id { get; set; }
        public int NodeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<DistributorHierarchyMapping> DistributorHierarchyMappings { get; set; }
    }
}
