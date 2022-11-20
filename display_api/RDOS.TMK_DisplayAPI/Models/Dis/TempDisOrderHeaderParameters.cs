using RDOS.TMK_DisplayAPI.Models.Paging;
using System;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class TempDisOrderHeaderParameters : EcoParameters
    {
        public string Code { get; set; }
        public string RewardPeriodCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DisplayCode { get; set; }
    }
}
