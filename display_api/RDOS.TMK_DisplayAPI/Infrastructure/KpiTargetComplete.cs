using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class KpiTargetComplete
    {
        public Guid Id { get; set; }
        public string KpiTargetCode { get; set; }
        public string Frequency { get; set; }
        public int? TotalNumberObject { get; set; }
        public int? TotalNumberObjectTarget { get; set; }
        public bool? IsFinishTarget { get; set; }
    }
}
