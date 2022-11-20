using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class PrincipleEmployee
    {
        public PrincipleEmployee()
        {
            PrincipalEmpContracts = new HashSet<PrincipalEmpContract>();
        }

        public Guid Id { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Position { get; set; }
        public string EmailType { get; set; }
        public string Email { get; set; }
        public string MainPhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Idcard { get; set; }
        public string Idcard2 { get; set; }
        public string BankName { get; set; }
        public string Territory { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }
        public string AccountStatus { get; set; }
        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }
        public string AddressDeparmentNo { get; set; }
        public string AddressDistrict { get; set; }
        public string AddressProvince { get; set; }
        public string AddressRegion { get; set; }
        public string AddressState { get; set; }
        public string AddressStreet { get; set; }
        public string AddressWard { get; set; }
        public string AvartarFilePath { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankBranch { get; set; }
        public string ExtraPhoneNumber { get; set; }
        public string FullAddress { get; set; }
        public string InsuranceId { get; set; }
        public string JobTitle { get; set; }
        public string PrincipalEmpCode { get; set; }
        public long? SaleGroup { get; set; }
        public DateTime? StartWorkingDate { get; set; }
        public string TaxNumber { get; set; }
        public DateTime? TerminateDate { get; set; }
        public string Title { get; set; }
        public string DistributorCode { get; set; }
        public string SoStructure { get; set; }

        public virtual ICollection<PrincipalEmpContract> PrincipalEmpContracts { get; set; }
    }
}
