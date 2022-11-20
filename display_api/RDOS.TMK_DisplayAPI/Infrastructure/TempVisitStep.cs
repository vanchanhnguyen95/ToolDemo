using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempVisitStep
    {
        public Guid Id { get; set; }
        public string VisitStepsCode { get; set; }
        public string VisitStepsDescription { get; set; }
        public string Module { get; set; }
    }
}
