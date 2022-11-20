using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class AutoKpisTargetDevelopment
    {
        public Guid Id { get; set; }
        public string AutoKpisTargetCode { get; set; }
        public string BusinessModel { get; set; }
        public decimal? Value { get; set; }
        public decimal? BaseValue { get; set; }
    }
}
