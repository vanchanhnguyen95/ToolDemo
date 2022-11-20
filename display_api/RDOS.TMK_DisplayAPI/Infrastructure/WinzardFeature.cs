using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class WinzardFeature
    {
        public Guid Id { get; set; }
        public string FeatureCode { get; set; }
        public string FeatureName { get; set; }
        public string Icon { get; set; }
        public string ListFormPath { get; set; }
        public string DetailFormPath { get; set; }
        public string NewFormPath { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
