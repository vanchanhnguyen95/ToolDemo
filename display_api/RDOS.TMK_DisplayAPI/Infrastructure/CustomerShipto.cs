using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerShipto
    {
        public CustomerShipto()
        {
            CustomerAdjustmentShiptos = new HashSet<CustomerAdjustmentShipto>();
            CustomerDmsAttributes = new HashSet<CustomerDmsAttribute>();
            CustomerShiptoContacts = new HashSet<CustomerShiptoContact>();
        }

        public Guid Id { get; set; }
        public string ShiptoCode { get; set; }
        public string Avatar { get; set; }
        public int Status { get; set; }
        public string BusinessStatus { get; set; }
        public string ClassType { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string DeptNo { get; set; }
        public double Longtiue { get; set; }
        public double Lattitue { get; set; }
        public Guid MainContactId { get; set; }
        public Guid CustomerInfomationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Guid? Province { get; set; }
        public Guid? District { get; set; }
        public Guid Country { get; set; }
        public Guid? City { get; set; }
        public Guid? Region { get; set; }
        public Guid? State { get; set; }
        public Guid? Wards { get; set; }
        public string ShiptoName { get; set; }

        public virtual CustomerInformation CustomerInfomation { get; set; }
        public virtual ICollection<CustomerAdjustmentShipto> CustomerAdjustmentShiptos { get; set; }
        public virtual ICollection<CustomerDmsAttribute> CustomerDmsAttributes { get; set; }
        public virtual ICollection<CustomerShiptoContact> CustomerShiptoContacts { get; set; }
    }
}
