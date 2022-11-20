using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ApiMapping
    {
        public Guid Id { get; set; }
        public string ApiName { get; set; }
        public string ApiVersion { get; set; }
        public string ApiUrl { get; set; }
        public string ServiceCode { get; set; }
        public string FeatureCode { get; set; }
        public string TempTableName { get; set; }
        public string IsCompleted { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}
