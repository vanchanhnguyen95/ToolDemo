using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ApplicationInviteCode
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string EmployeeName { get; set; }
        public string InviteCode { get; set; }
        public string AcitveLink { get; set; }
        public Guid? AppId { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public Guid? PrincipleId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? ActiveDate { get; set; }
        public string Status { get; set; }
        public string EmployeeCode { get; set; }
        public string MessageDetail { get; set; }
        public string AppName { get; set; }
    }
}
