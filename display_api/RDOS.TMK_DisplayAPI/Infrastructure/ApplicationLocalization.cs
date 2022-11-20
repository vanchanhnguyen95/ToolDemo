using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ApplicationLocalization
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileVersion { get; set; }
        public string ExcelFileName { get; set; }
        public float? ExcelFileRepoId { get; set; }
        public Guid? AppId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string JsonFileName { get; set; }
        public string JsonFileRepoId { get; set; }
        public string Note { get; set; }
    }
}
