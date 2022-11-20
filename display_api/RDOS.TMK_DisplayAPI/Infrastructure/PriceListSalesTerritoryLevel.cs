using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PriceListSalesTerritoryLevel
    {
        public Guid Id { get; set; }
        public Guid TerritoryValueId { get; set; }
        public string SalesTerritoryLevelCode { get; set; }
        public string SalesTerritoryLevelDescription { get; set; }
        public bool IsDeleted { get; set; }
        public Guid PriceListId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual PriceList PriceList { get; set; }
    }
}
