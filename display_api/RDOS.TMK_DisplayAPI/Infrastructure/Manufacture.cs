using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class Manufacture
    {
        public Guid Id { get; set; }
        public string ManufactureCode { get; set; }
        public string TaxCode { get; set; }
        public string ManufactureName { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string FirtName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Guid? EmailType { get; set; }
        public string Email { get; set; }
        public Guid PhoneType { get; set; }
        public string TitleType { get; set; }
        public int? Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string GpslocationLat { get; set; }
        public string GpslocationLng { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public Guid? Wards { get; set; }
        public Guid? District { get; set; }
        public Guid? City { get; set; }
        public Guid? Province { get; set; }
        public Guid? State { get; set; }
        public Guid? Region { get; set; }
        public Guid? Country { get; set; }
        public string Status { get; set; }
        public int DeleteFlag { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string LegalInformation { get; set; }
    }
}
