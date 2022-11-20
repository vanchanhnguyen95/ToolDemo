using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempKpidistributor
    {
        public Guid Id { get; set; }
        public string DistributorCode { get; set; }
        public string Kpicode { get; set; }
        public string Description { get; set; }
        public int Dsatarget { get; set; }
        public int ActualCurrent { get; set; }
        public int Remain { get; set; }
        public int SuggestKpi { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
