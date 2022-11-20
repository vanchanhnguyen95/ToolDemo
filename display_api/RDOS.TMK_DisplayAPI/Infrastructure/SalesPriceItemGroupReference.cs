using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class SalesPriceItemGroupReference
    {
        public SalesPriceItemGroupReference()
        {
            PurchasePriceItemGroups = new HashSet<PurchasePriceItemGroup>();
        }

        public Guid Id { get; set; }
        public string ItemGroupCode { get; set; }
        public string Uom { get; set; }
        public decimal Plus { get; set; }
        public string PurchasePriceCode { get; set; }
        public string SalesPriceCode { get; set; }
        public bool IsDeleted { get; set; }
        public Guid SalesBasePriceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual SalesBasePrice SalesBasePrice { get; set; }
        public virtual ICollection<PurchasePriceItemGroup> PurchasePriceItemGroups { get; set; }
    }
}
