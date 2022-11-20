using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PurchaseBasePrice
    {
        public PurchaseBasePrice()
        {
            PurchasePriceItemGroups = new HashSet<PurchasePriceItemGroup>();
            SalesBasePrices = new HashSet<SalesBasePrice>();
        }

        public Guid Id { get; set; }
        public string PurchasePriceCode { get; set; }
        public string Description { get; set; }
        public string PriceType { get; set; }
        public string ContractType { get; set; }
        public string SalesTerritoryCode { get; set; }
        public bool IsItemGroupCode { get; set; }
        public bool IsProductHierachy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<PurchasePriceItemGroup> PurchasePriceItemGroups { get; set; }
        public virtual ICollection<SalesBasePrice> SalesBasePrices { get; set; }
    }
}
