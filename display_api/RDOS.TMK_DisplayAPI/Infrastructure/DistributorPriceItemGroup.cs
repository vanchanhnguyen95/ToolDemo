using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class DistributorPriceItemGroup
    {
        public Guid Id { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupDescription { get; set; }
        public bool IsDeleted { get; set; }
        public Guid DistributorPriceVolumeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual DistributorPriceVolume DistributorPriceVolume { get; set; }
    }
}
