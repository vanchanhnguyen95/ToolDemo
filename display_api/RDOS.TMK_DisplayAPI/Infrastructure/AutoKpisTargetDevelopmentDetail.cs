using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class AutoKpisTargetDevelopmentDetail
    {
        public Guid Id { get; set; }
        public Guid AutoKpisTargetDevelopmentId { get; set; }
        public string SalePeriod { get; set; }
        public decimal? Value { get; set; }
    }
}
