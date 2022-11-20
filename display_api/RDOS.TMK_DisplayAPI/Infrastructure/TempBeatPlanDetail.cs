using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempBeatPlanDetail
    {
        public Guid Id { get; set; }
        public Guid BeatPlanId { get; set; }
        public Guid CustomerShiptoId { get; set; }
    }
}
