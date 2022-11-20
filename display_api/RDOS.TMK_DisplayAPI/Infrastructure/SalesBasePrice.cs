using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class SalesBasePrice
    {
        public SalesBasePrice()
        {
            SalesPriceItemGroupReferences = new HashSet<SalesPriceItemGroupReference>();
            SalesPriceItemGroups = new HashSet<SalesPriceItemGroup>();
        }

        public Guid Id { get; set; }
        public string SalesPriceCode { get; set; }
        public string Description { get; set; }
        public string SalesTerritoryCode { get; set; }
        public string PriceType { get; set; }
        public int Status { get; set; }
        public string PurchaseBasePriceCode { get; set; }
        public DateTime EffectiveTimeFrom { get; set; }
        public DateTime? EffectiveTimeTo { get; set; }
        public bool IsItemGroupCode { get; set; }
        public bool IsProductHierachy { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? PurchaseBasePriceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual PurchaseBasePrice PurchaseBasePrice { get; set; }
        public virtual ICollection<SalesPriceItemGroupReference> SalesPriceItemGroupReferences { get; set; }
        public virtual ICollection<SalesPriceItemGroup> SalesPriceItemGroups { get; set; }
    }
}
