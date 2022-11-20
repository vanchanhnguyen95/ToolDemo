using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class Kpi
    {
        public Guid Id { get; set; }
        public string KpisCode { get; set; }
        public string KpisDescription { get; set; }
        public string KpisCalType { get; set; }
        public string CalculationFormulaType { get; set; }
        public string TargetDisplayType { get; set; }
        public string Status { get; set; }
        public string ProductListCode { get; set; }
        public bool? IsManual { get; set; }
    }
}
