using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ApplicationNotiMessage
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string DataJson { get; set; }
        public string DataId { get; set; }
        public string Purpose { get; set; }
        public string DeliveryStatus { get; set; }
        public int? Type { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsRead { get; set; }
        public string NavigatePath { get; set; }
        public Guid? UrgentNotiId { get; set; }
    }
}
