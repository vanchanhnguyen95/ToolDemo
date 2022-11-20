using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PriceList
    {
        public PriceList()
        {
            AdjustPriceListUoMitemGroups = new HashSet<AdjustPriceListUoMitemGroup>();
            PriceListDistributeSellingAreas = new HashSet<PriceListDistributeSellingArea>();
            PriceListItemGroups = new HashSet<PriceListItemGroup>();
            PriceListOutletAttributeValues = new HashSet<PriceListOutletAttributeValue>();
            PriceListSalesTerritoryLevels = new HashSet<PriceListSalesTerritoryLevel>();
        }

        public Guid Id { get; set; }
        public string PriceListCode { get; set; }
        public DateTime EffectiveTime { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public string PriceListTypeCode { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public decimal FreightCost { get; set; }
        public decimal DeductedValue { get; set; }
        public bool SynchronizeItemGroup { get; set; }
        public string ObjectApply { get; set; }
        public Guid PriceListTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int Status { get; set; }

        public virtual PriceListType PriceListType { get; set; }
        public virtual ICollection<AdjustPriceListUoMitemGroup> AdjustPriceListUoMitemGroups { get; set; }
        public virtual ICollection<PriceListDistributeSellingArea> PriceListDistributeSellingAreas { get; set; }
        public virtual ICollection<PriceListItemGroup> PriceListItemGroups { get; set; }
        public virtual ICollection<PriceListOutletAttributeValue> PriceListOutletAttributeValues { get; set; }
        public virtual ICollection<PriceListSalesTerritoryLevel> PriceListSalesTerritoryLevels { get; set; }
    }
}
