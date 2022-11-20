using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class MobileUserSetting
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool FingerPrint { get; set; }
        public bool IsPushNotification { get; set; }
        public bool IsSendEmail { get; set; }
        public string Language { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
