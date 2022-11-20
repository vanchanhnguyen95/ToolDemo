using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class DistributorPriceVolume
    {
        public DistributorPriceVolume()
        {
            DistributorPriceApplyToOutletAttributes = new HashSet<DistributorPriceApplyToOutletAttribute>();
            DistributorPriceItemGroups = new HashSet<DistributorPriceItemGroup>();
            DistributorPriceVolumeLevels = new HashSet<DistributorPriceVolumeLevel>();
        }

        public Guid Id { get; set; }
        public string DistributorCode { get; set; }
        public string DistributorDescription { get; set; }
        public string SellingPriceByVolumeCode { get; set; }
        public string SellingPriceByVolumeDescription { get; set; }
        public string Status { get; set; }
        public string BreakType { get; set; }
        public string ObjectApply { get; set; }
        public string PriceDiscountApplyType { get; set; }
        public DateTime EffectiveTime { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<DistributorPriceApplyToOutletAttribute> DistributorPriceApplyToOutletAttributes { get; set; }
        public virtual ICollection<DistributorPriceItemGroup> DistributorPriceItemGroups { get; set; }
        public virtual ICollection<DistributorPriceVolumeLevel> DistributorPriceVolumeLevels { get; set; }
    }
}
