using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class AdjustPriceListUoMitemGroup
    {
        public Guid Id { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupDescription { get; set; }
        public string UoMitemGroup { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal CurrentFreightCost { get; set; }
        public decimal CurrentDeductedValue { get; set; }
        public DateTime? PriceListEffectiveTime { get; set; }
        public DateTime? PriceListExpirationTime { get; set; }
        public decimal FreightCost { get; set; }
        public decimal DeductedValue { get; set; }
        public decimal Price { get; set; }
        public DateTime EffectiveTime { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public Guid PriceListId { get; set; }
        public Guid AdjustItemGroupPriceId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual AdjustItemGroupPrice AdjustItemGroupPrice { get; set; }
        public virtual PriceList PriceList { get; set; }
    }
}
