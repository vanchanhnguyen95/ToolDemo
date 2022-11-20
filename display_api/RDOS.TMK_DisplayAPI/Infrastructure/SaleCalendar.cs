using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class SaleCalendar
    {
        public SaleCalendar()
        {
            SaleCalendarActionHistories = new HashSet<SaleCalendarActionHistory>();
            SaleCalendarGenerates = new HashSet<SaleCalendarGenerate>();
            SaleCalendarHolidays = new HashSet<SaleCalendarHoliday>();
        }

        public Guid Id { get; set; }
        public int? SaleYear { get; set; }
        public string StartDayOfWeek { get; set; }
        public DateTime? LastDayOfFirstWeek { get; set; }
        public string IncludeWeekend { get; set; }
        public int? NumberOfWorkingDay { get; set; }
        public string QuarterStructure { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? ReleasedDate { get; set; }

        public virtual ICollection<SaleCalendarActionHistory> SaleCalendarActionHistories { get; set; }
        public virtual ICollection<SaleCalendarGenerate> SaleCalendarGenerates { get; set; }
        public virtual ICollection<SaleCalendarHoliday> SaleCalendarHolidays { get; set; }
    }
}
