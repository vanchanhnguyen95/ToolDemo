using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TerritoryMapping
    {
        public TerritoryMapping()
        {
            DistributorSellingAreas = new HashSet<DistributorSellingArea>();
        }

        public Guid Id { get; set; }
        public string TerritoryStructureCode { get; set; }
        public Guid BranchId { get; set; }
        public Guid RegionId { get; set; }
        public Guid AreaId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Guid SubAreaId { get; set; }
        public Guid SubRegionId { get; set; }

        public virtual ICollection<DistributorSellingArea> DistributorSellingAreas { get; set; }
    }
}
