using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class VisitStep
    {
        public Guid Id { get; set; }
        public string SirefCode { get; set; }
        public string ParameterCode { get; set; }
        public string StepsVisitCode { get; set; }
        public string StepsVisit { get; set; }
        public string DefaultResultCode { get; set; }
        public string DefaultResult { get; set; }
        public bool? IsDelete { get; set; }
    }
}
