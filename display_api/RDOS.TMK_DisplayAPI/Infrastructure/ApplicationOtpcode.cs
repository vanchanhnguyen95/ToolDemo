using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ApplicationOtpcode
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Otpcode { get; set; }
        public string Purpose { get; set; }
        public Guid? AppId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? ActiveDate { get; set; }
        public string Status { get; set; }
        public string AppName { get; set; }
    }
}
