using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PriorityPriceListType
    {
        public Guid Id { get; set; }
        public Guid PriceListTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int? CurrentPriority { get; set; }
        public bool IsDeleted { get; set; }
        public int OriginalPriority { get; set; }

        public virtual PriceListType PriceListType { get; set; }
    }
}
