using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ApplicationThemesSetting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ThemeSuggestId { get; set; }
        public string Status { get; set; }
        public Guid? AppId { get; set; }
        public Guid? PrincipleId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
