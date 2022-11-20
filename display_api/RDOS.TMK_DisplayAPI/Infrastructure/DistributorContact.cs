using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class DistributorContact
    {
        public DistributorContact()
        {
            ShiptoContacts = new HashSet<ShiptoContact>();
        }

        public Guid Id { get; set; }
        public string ContactCode { get; set; }
        public Guid DistributorId { get; set; }
        public bool MainContact { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailType { get; set; }
        public string Email { get; set; }
        public string MarriedStatus { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Hobbies { get; set; }
        public Guid? Country { get; set; }
        public Guid? City { get; set; }
        public Guid? Province { get; set; }
        public Guid? State { get; set; }
        public Guid? Region { get; set; }
        public Guid? District { get; set; }
        public Guid? Wards { get; set; }
        public string Street { get; set; }
        public string FullAddress { get; set; }
        public string DepartmentNumber { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public int DeleteFlag { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Distributor Distributor { get; set; }
        public virtual ICollection<ShiptoContact> ShiptoContacts { get; set; }
    }
}
