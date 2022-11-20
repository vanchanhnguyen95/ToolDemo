using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerContact1
    {
        public CustomerContact1()
        {
            CustomerContactEmail1s = new HashSet<CustomerContactEmail1>();
            CustomerContactPhone1s = new HashSet<CustomerContactPhone1>();
            CustomerShiptoContacts = new HashSet<CustomerShiptoContact>();
        }

        public Guid Id { get; set; }
        public string ContactCode { get; set; }
        public int Title { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string DisplayName { get; set; }
        public string BusinessTitle { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Hobbies { get; set; }
        public string Note { get; set; }
        public int MarriedStatus { get; set; }
        public Guid CustomerInfomationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual CustomerInformation CustomerInfomation { get; set; }
        public virtual ICollection<CustomerContactEmail1> CustomerContactEmail1s { get; set; }
        public virtual ICollection<CustomerContactPhone1> CustomerContactPhone1s { get; set; }
        public virtual ICollection<CustomerShiptoContact> CustomerShiptoContacts { get; set; }
    }
}
