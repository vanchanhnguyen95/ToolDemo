using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class SalesIndicatorRef
    {
        public Guid Id { get; set; }
        public string SirefCode { get; set; }
        public string SitypeCode { get; set; }
        public string SirefDescription { get; set; }
        public string Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDelete { get; set; }
        public int LevelSitype { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsManual { get; set; }
    }
}
