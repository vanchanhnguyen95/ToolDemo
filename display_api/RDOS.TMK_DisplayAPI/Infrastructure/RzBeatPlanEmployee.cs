using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class RzBeatPlanEmployee
    {
        public Guid Id { get; set; }
        public string BeatPlanCode { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ValidUntil { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsDeleted { get; set; }
    }
}
