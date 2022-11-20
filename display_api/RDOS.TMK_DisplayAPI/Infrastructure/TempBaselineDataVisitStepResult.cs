using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempBaselineDataVisitStepResult
    {
        public Guid Id { get; set; }
        public string Module { get; set; }
        public string VisitCode { get; set; }
        public string VisitStepCode { get; set; }
        public string VisitStepDescription { get; set; }
        public string VisitStepResultCode { get; set; }
    }
}
