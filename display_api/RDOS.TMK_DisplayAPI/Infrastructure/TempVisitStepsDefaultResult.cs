using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempVisitStepsDefaultResult
    {
        public Guid Id { get; set; }
        public string VisitStepsDefaultResultCode { get; set; }
        public string VisitStepsCode { get; set; }
        public string VisitStepsDefaultResultDescription { get; set; }
    }
}
