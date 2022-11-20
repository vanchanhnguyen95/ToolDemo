using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CronJobSchedule
    {
        public int Id { get; set; }
        public DateTime? PreviousFireTime { get; set; }
        public DateTime? NextFireTime { get; set; }
        public string Timezone { get; set; }
        public string CronExpression { get; set; }
        public string SchedulerName { get; set; }
        public string JobName { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
