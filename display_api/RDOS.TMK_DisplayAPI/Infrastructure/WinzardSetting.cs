using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class WinzardSetting
    {
        public Guid Id { get; set; }
        public Guid FeatureId { get; set; }
        public string DisplayText { get; set; }
        public string Type { get; set; }
        public string RoutePath { get; set; }
        public bool? Required { get; set; }
        public int? OrderBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
