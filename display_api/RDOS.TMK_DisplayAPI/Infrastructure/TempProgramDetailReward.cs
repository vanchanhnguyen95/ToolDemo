using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempProgramDetailReward
    {
        public Guid Id { get; set; }
        public string ProgramDetailRewardCode { get; set; }
        public string ProgramDetailsKey { get; set; }
        public string Type { get; set; }
        public string ItemCode { get; set; }
        public Guid ItemId { get; set; }
        public string Uomcode { get; set; }
        public int Quantities { get; set; }
        public decimal Amount { get; set; }
        public double DiscountPercented { get; set; }
        public bool IsDeleted { get; set; }
    }
}
