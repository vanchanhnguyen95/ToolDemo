using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PurchasePriceItemGroup
    {
        public Guid Id { get; set; }
        public string ItemGroupCode { get; set; }
        public string Description { get; set; }
        public string Uom { get; set; }
        public decimal LastPrice { get; set; }
        public DateTime? LastEffectiveTime { get; set; }
        public decimal NewPrice { get; set; }
        public DateTime NewEffectiveTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Guid PurchaseBasePriceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Guid? SalesPriceItemGroupReferenceId { get; set; }

        public virtual PurchaseBasePrice PurchaseBasePrice { get; set; }
        public virtual SalesPriceItemGroupReference SalesPriceItemGroupReference { get; set; }
    }
}
