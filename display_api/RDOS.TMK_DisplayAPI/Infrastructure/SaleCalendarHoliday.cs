using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class SaleCalendarHoliday
    {
        public Guid Id { get; set; }
        public Guid SaleCalendarId { get; set; }
        public DateTime HolidayDate { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual SaleCalendar SaleCalendar { get; set; }
    }
}
