using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PrincipalProfile
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string TaxNumber { get; set; }
        public string Fax { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string AttentionTitle { get; set; }
        public string AttentionFirstName { get; set; }
        public string AttentionMiddleName { get; set; }
        public string AttentionLastName { get; set; }
        public string AttentionPosition { get; set; }
        public string AttentionEmailType { get; set; }
        public string AttentionEmail { get; set; }
        public string AttentionPhoneType { get; set; }
        public string AttentionPhoneNumber { get; set; }
        public string AttentionFullName { get; set; }
        public string AddressCountry { get; set; }
        public string AddressRegion { get; set; }
        public string AddressProvince { get; set; }
        public string AddressDistrict { get; set; }
        public string AddressWard { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressStreet { get; set; }
        public string AddressDeparmentNo { get; set; }
        public string FullAddress { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string BankNumber { get; set; }
        public string ContractTitle { get; set; }
        public string ContractFirstName { get; set; }
        public string ContractMiddleName { get; set; }
        public string ContractLastName { get; set; }
        public string ContractPosition { get; set; }
        public string ContractEmailType { get; set; }
        public string ContractEmail { get; set; }
        public string ContractExtraEmail { get; set; }
        public string ContractPhoneType { get; set; }
        public string ContractPhoneNumber { get; set; }
        public string ContractExtraPhone { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
