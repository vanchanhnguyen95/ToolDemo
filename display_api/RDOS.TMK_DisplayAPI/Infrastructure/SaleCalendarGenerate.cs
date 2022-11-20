using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class SaleCalendarGenerate
    {
        public Guid Id { get; set; }
        public Guid SaleCalendarId { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public int? Ordinal { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual SaleCalendar SaleCalendar { get; set; }
    }
}
