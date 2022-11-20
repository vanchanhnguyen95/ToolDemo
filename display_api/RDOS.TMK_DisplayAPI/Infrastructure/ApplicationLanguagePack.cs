using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ApplicationLanguagePack
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public string FileVersion { get; set; }
        public string AppVersion { get; set; }
        public string ExcelFileName { get; set; }
        public double? ExcelFileRepoId { get; set; }
        public string JsonFileName { get; set; }
        public double? JsonFileRepoId { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public Guid? PrincipleId { get; set; }
        public Guid? AppId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? IosversionId { get; set; }
        public Guid? AndroidVersionId { get; set; }
    }
}
