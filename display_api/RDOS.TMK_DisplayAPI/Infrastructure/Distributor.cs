using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class Distributor
    {
        public Distributor()
        {
            DistributorContacts = new HashSet<DistributorContact>();
            DistributorContracts = new HashSet<DistributorContract>();
            DistributorHistoricals = new HashSet<DistributorHistorical>();
            DistributorShiptos = new HashSet<DistributorShipto>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string AttentionEmailValue { get; set; }
        public string AttentionFirstName { get; set; }
        public string AttentionFullName { get; set; }
        public string AttentionLastName { get; set; }
        public string AttentionMiddleName { get; set; }
        public string AttentionPhoneCode { get; set; }
        public string AttentionPhoneValue { get; set; }
        public string AttentionPosition { get; set; }
        public string AttentionTitle { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public Guid? BusinessAddressCity { get; set; }
        public Guid? BusinessAddressCountry { get; set; }
        public string BusinessAddressDept { get; set; }
        public Guid? BusinessAddressDistrict { get; set; }
        public string BusinessAddressLat { get; set; }
        public string BusinessAddressLong { get; set; }
        public Guid? BusinessAddressProvince { get; set; }
        public Guid? BusinessAddressRegion { get; set; }
        public Guid? BusinessAddressState { get; set; }
        public string BusinessAddressStreet { get; set; }
        public Guid? BusinessAddressWard { get; set; }
        public string BussinessFullAddress { get; set; }
        public string Dmscode { get; set; }
        public int DeleteFlag { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string FullName { get; set; }
        public bool IsChecked { get; set; }
        public int? NumberOfAccountingNp { get; set; }
        public int? NumberOfDelivery13 { get; set; }
        public int? NumberOfDelivery5 { get; set; }
        public int? NumberOfDeliveryMoto { get; set; }
        public int? NumberOfWorkingAccounting { get; set; }
        public string Phone { get; set; }
        public string PrincipalLinkedCode { get; set; }
        public string Status { get; set; }
        public string TaxCode { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string Website { get; set; }
        public DateTime? DateTimeImport { get; set; }
        public string BusinessPartnerType { get; set; }
        public string AttentionEmailCode { get; set; }

        public virtual ICollection<DistributorContact> DistributorContacts { get; set; }
        public virtual ICollection<DistributorContract> DistributorContracts { get; set; }
        public virtual ICollection<DistributorHistorical> DistributorHistoricals { get; set; }
        public virtual ICollection<DistributorShipto> DistributorShiptos { get; set; }
    }
}
