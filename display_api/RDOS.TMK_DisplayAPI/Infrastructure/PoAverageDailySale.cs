using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PoAverageDailySale
    {
        public Guid Id { get; set; }
        public string DistributorCode { get; set; }
        public double AverageSaleAmt { get; set; }
        public DateTime CalculatedDate { get; set; }
        public string ItemGroupCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
