using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ItemGroup
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public Guid Attribute1 { get; set; }
        public Guid Attribute2 { get; set; }
        public Guid Attribute3 { get; set; }
        public Guid Attribute4 { get; set; }
        public Guid Attribute5 { get; set; }
        public Guid Attribute6 { get; set; }
        public Guid Attribute7 { get; set; }
        public Guid Attribute8 { get; set; }
        public Guid Attribute9 { get; set; }
        public Guid Attribute10 { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
