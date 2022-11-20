using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ApplicationInviteCodeCount
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public float? SendCount { get; set; }
        public DateTime? SendDate { get; set; }
        public string AppId { get; set; }
        public Guid? PrincipleId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
