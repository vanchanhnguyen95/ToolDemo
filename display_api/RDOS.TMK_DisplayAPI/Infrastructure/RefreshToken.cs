using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string Os { get; set; }
        public string AppVersion { get; set; }
        public Guid? AppId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? PrincipleId { get; set; }
        public string RefreshToken1 { get; set; }
    }
}
