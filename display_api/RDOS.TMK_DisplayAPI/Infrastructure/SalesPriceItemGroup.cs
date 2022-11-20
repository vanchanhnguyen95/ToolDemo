using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class SalesPriceItemGroup
    {
        public Guid Id { get; set; }
        public string ItemGroupCode { get; set; }
        public string Description { get; set; }
        public string Uom { get; set; }
        public decimal SalesPrice { get; set; }
        public bool IsDeleted { get; set; }
        public Guid SalesBasePriceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual SalesBasePrice SalesBasePrice { get; set; }
    }
}
