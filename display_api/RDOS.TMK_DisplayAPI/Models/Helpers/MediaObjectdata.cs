using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Models.Helpers
{
    public partial class MediaObjectdata
    {
        public string Base64String { get; set; }
        public string KeyName { get; set; } // like: /[folder-type]/[file-name]-[folderid][yyyy][month][day].ext
    }

    public partial class Media
    {
        [Key] public string Id { get; set; }
        [Required] public string KeyS3 { get; set; }
        [Required] public string UserName { get; set; }
        [Required] public string FolderId { get; set; } // as FolderId field in [Account] table
        [Required] public long FileSize { get; set; }
        public string Base64 { get; set; }
        public string RootFolder { get; set; } // rdos-uploads
        public string FolderType { get; set; } // tmp, files, images, videos
        public string DateTimeString { get; set; } // yyyy/MM/dd
        public string FilePath { get; set; } // full path without domain
        public string FileName { get; set; } //key S3 object
        public string FileExt { get; set; }
        public int FileDimension { get; set; } = 0; //0: all, 1: vertical, 2:horizontal, 3:square
        public string FileNote { get; set; }
        public string YearString { get; set; }
        public string MonthString { get; set; }
        public string DayString { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedDateStr { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int Status { get; set; } = 1; //0: removed
    }
}
