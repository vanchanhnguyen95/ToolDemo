using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class RzBeatPlanShipto
    {
        public Guid Id { get; set; }
        public string BeatPlanCode { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public bool Friday { get; set; }
        public bool IsDeleted { get; set; }
        public bool Monday { get; set; }
        public bool Saturday { get; set; }
        public Guid ShiptoId { get; set; }
        public bool Sunday { get; set; }
        public bool Thursday { get; set; }
        public bool Tuesday { get; set; }
        public DateTime? ValidUntil { get; set; }
        public DateTime? VisitDate { get; set; }
        public string VisitOrder { get; set; }
        public bool Wednesday { get; set; }
    }
}
