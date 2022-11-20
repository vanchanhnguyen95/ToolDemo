using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PriceListType
    {
        public PriceListType()
        {
            PriceListTypeAttributeLists = new HashSet<PriceListTypeAttributeList>();
            PriceLists = new HashSet<PriceList>();
            PriorityPriceListTypes = new HashSet<PriorityPriceListType>();
        }

        public Guid Id { get; set; }
        public string PriceListTypeCode { get; set; }
        public string Description { get; set; }
        public string BasePriceCode { get; set; }
        public bool Status { get; set; }
        public bool Dsa { get; set; }
        public string SalesTerritoryCode { get; set; }
        public string PriceType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int? CurrentPriority { get; set; }
        public int OriginalPriority { get; set; }
        public string SaleTerritoryLevel { get; set; }
        public string SaleTerritoryLevelDescription { get; set; }
        public string SalesTerritoryDescription { get; set; }

        public virtual ICollection<PriceListTypeAttributeList> PriceListTypeAttributeLists { get; set; }
        public virtual ICollection<PriceList> PriceLists { get; set; }
        public virtual ICollection<PriorityPriceListType> PriorityPriceListTypes { get; set; }
    }
}
