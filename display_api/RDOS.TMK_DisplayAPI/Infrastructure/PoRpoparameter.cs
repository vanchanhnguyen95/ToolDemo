using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PoRpoparameter
    {
        public Guid Id { get; set; }
        public bool AllowAdjustment { get; set; }
        public decimal? IncreasingAmplitude { get; set; }
        public decimal? DecreasingAmplitude { get; set; }
        public bool IncludedPotransit { get; set; }
        public bool IncludedPoshipping { get; set; }
        public int? SellOutRunningRate { get; set; }
        public string RoundingRule { get; set; }
        public int StockKeepingDefaultValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
