using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerShipto1
    {
        public CustomerShipto1()
        {
            CustomerAdjustmentShiptos = new HashSet<CustomerAdjustmentShipto>();
            CustomerShiptoContacts = new HashSet<CustomerShiptoContact>();
        }

        public Guid Id { get; set; }
        public string ShiptoCode { get; set; }
        public string Avatar { get; set; }
        public int Status { get; set; }
        public string BusinessStatus { get; set; }
        public string ClassType { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public int Province { get; set; }
        public int District { get; set; }
        public int Ward { get; set; }
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

        public virtual CustomerInformation CustomerInfomation { get; set; }
        public virtual CustomerDmsAttribute CustomerDmsAttribute { get; set; }
        public virtual ICollection<CustomerAdjustmentShipto> CustomerAdjustmentShiptos { get; set; }
        public virtual ICollection<CustomerShiptoContact> CustomerShiptoContacts { get; set; }
    }
}
