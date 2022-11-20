using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PrincipalWarehouse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string LinkedCode { get; set; }
        public string Decscription { get; set; }
        public string Status { get; set; }
        public string ManagerTitle { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerMiddleName { get; set; }
        public string ManagerLastName { get; set; }
        public string ManagerPhoneType { get; set; }
        public string ManagerPhoneNumber { get; set; }
        public string ManagerEmailType { get; set; }
        public string ManagerEmail { get; set; }
        public DateTime? ManagerDob { get; set; }
        public string ManagerGender { get; set; }
        public string ManagerNote { get; set; }
        public string AddressCountry { get; set; }
        public string AddressProvince { get; set; }
        public string AddressDistrict { get; set; }
        public string AddressWard { get; set; }
        public string AddressStreet { get; set; }
        public string AddressDeparmentNo { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string AddressCity { get; set; }
        public string AddressRegion { get; set; }
        public string AddressState { get; set; }
        public string FullAddress { get; set; }
        public string ManagerFullName { get; set; }
    }
}
