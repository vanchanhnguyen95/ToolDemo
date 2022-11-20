using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class BasePrice
    {
        public BasePrice()
        {
            BasePriceGrossMargins = new HashSet<BasePriceGrossMargin>();
            BasePriceItemGroups = new HashSet<BasePriceItemGroup>();
        }

        public Guid Id { get; set; }
        public string BasePriceCode { get; set; }
        public string Description { get; set; }
        public string PriceType { get; set; }
        public string ContractType { get; set; }
        public string PurchaseBasePriceCode { get; set; }
        public string SalesTerritoryCode { get; set; }
        public string SalesTerritoryDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<BasePriceGrossMargin> BasePriceGrossMargins { get; set; }
        public virtual ICollection<BasePriceItemGroup> BasePriceItemGroups { get; set; }
    }
}
