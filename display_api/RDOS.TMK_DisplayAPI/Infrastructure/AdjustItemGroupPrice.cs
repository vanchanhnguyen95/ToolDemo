using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class AdjustItemGroupPrice
    {
        public AdjustItemGroupPrice()
        {
            AdjustPriceListUoMitemGroups = new HashSet<AdjustPriceListUoMitemGroup>();
        }

        public Guid Id { get; set; }
        public string AdjustPricesCode { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string PriceType { get; set; }
        public string ItemGroupCode { get; set; }
        public bool IsDeleted { get; set; }
        public string UoMitemGroup { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<AdjustPriceListUoMitemGroup> AdjustPriceListUoMitemGroups { get; set; }
    }
}
