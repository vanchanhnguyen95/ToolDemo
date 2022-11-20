using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PoStockKeepingDayItemHierarchy
    {
        public Guid Id { get; set; }
        public string ItemHierarchyValue { get; set; }
        public int DayNumber { get; set; }
        public string StockKeepingDayNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Guid ItemHierarchyId { get; set; }
    }
}
