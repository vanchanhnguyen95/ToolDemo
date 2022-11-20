using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class CustomerInformation
    {
        public CustomerInformation()
        {
            CustomerContacts = new HashSet<CustomerContact>();
            CustomerContracts = new HashSet<CustomerContract>();
            CustomerShiptos = new HashSet<CustomerShipto>();
        }

        public Guid Id { get; set; }
        public string CustomerCode { get; set; }
        public bool IsDirectCustomer { get; set; }
        public int Status { get; set; }
        public string ShortName { get; set; }
        public string CodeAtVendor { get; set; }
        public string CodeAtDistributor { get; set; }
        public string LegalInformation { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string BankNumber { get; set; }
        public string TaxCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string BusinessTitle { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string StreetLine { get; set; }
        public double Longtiue { get; set; }
        public double Lattitue { get; set; }
        public string BusinessAddress { get; set; }
        public string ErpCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Guid? Wards { get; set; }
        public Guid? Province { get; set; }
        public Guid? District { get; set; }
        public Guid Country { get; set; }
        public Guid? City { get; set; }
        public string DeptNo { get; set; }
        public Guid? Region { get; set; }
        public Guid? State { get; set; }

        public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
        public virtual ICollection<CustomerContract> CustomerContracts { get; set; }
        public virtual ICollection<CustomerShipto> CustomerShiptos { get; set; }
    }
}
