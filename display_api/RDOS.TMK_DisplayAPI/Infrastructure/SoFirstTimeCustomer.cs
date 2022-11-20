using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class SoFirstTimeCustomer
    {
        public Guid Id { get; set; }
        public string CustomerCode { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? Country { get; set; }
        public Guid? Region { get; set; }
        public Guid? State { get; set; }
        public Guid? Province { get; set; }
        public Guid? District { get; set; }
        public Guid? Wards { get; set; }
        public Guid? City { get; set; }
        public string BusinessAddress { get; set; }
        public string StreetLine { get; set; }
        public string DeptNo { get; set; }
        public string DistributorShiptoId { get; set; }
        public string DistributorCode { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
