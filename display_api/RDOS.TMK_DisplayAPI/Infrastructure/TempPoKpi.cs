using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class TempPoKpi
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string DistributorCode { get; set; }
        public string DistributorName { get; set; }
        public string ItemGroupId { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupDescription { get; set; }
        public int Target { get; set; }
        public int Actual { get; set; }
        public int Renain { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
