using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ApplicationVersion
    {
        public Guid Id { get; set; }
        public Guid? AppId { get; set; }
        public string AppName { get; set; }
        public string AppVersion { get; set; }
        public string Os { get; set; }
        public string HostType { get; set; }
        public string AppBuildFile { get; set; }
        public double? AppBuildFileRepoId { get; set; }
        public string ReleasedNote { get; set; }
        public string Status { get; set; }
        public Guid? PrincipleId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string Type { get; set; }
        public string ApplyFor { get; set; }
    }
}
