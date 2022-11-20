using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ApplicationThemesElement
    {
        public Guid Id { get; set; }
        public string ComponentName { get; set; }
        public string ElementName { get; set; }
        public string ElementType { get; set; }
        public string DefaultValue { get; set; }
        public bool? IsEffectMainColor { get; set; }
        public Guid? AppId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
