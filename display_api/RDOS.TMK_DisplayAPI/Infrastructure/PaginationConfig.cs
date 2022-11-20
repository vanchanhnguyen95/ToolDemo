using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PaginationConfig
    {
        public Guid Id { get; set; }
        public int PaginationCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Guid FeatureId { get; set; }
        public string FeatureCode { get; set; }

        public virtual Feature Feature { get; set; }
    }
}
