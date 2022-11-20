using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisDisplay : DisAuditableEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string ShortName { get; set; }
        [Required]
        [MaxLength(10)]
        public string Status { get; set; }

        [Required]
        [MaxLength(10)]
        public string SaleOrg { get; set; }

        [Required]
        [MaxLength(10)]
        public string TerritoryStructureCode { get; set; }

        [Required]
        [MaxLength(10)]
        public string SicCode { get; set; }

        [Required]
        [MaxLength(10)]
        public string FrequencyDisplay { get; set; }
        
        [MaxLength(200)]
        public string Image1Path { get; set; }
        
        [MaxLength(200)]
        public string Image1Name { get; set; }
        
        [MaxLength(200)]
        public string Image2Path { get; set; }
        
        [MaxLength(200)]
        public string Image2Name { get; set; }
       
        [MaxLength(200)]
        public string Image3Path { get; set; }
        
        [MaxLength(200)]
        public string Image3Name { get; set; }
       
        [MaxLength(200)]
        public string Image4Path { get; set; }
        
        [MaxLength(200)]
        public string Image4Name { get; set; }

        [MaxLength(200)]
        public string ScopeType { get; set; }

        [MaxLength(200)]
        public string ScopeSaleTerritoryLevel { get; set; }

        [MaxLength(200)]
        public string ApplicableObjectType { get; set; }
        public bool? IsCheckSalesOutput { get; set; }
        public int SalesOutput { get; set; }

        public bool? IndependentDisplay { get; set; }
        public bool? ToolDisplay { get; set; }
        public bool? ManageNumberRegister { get; set; }

        public DateTime? RegistrationStartDate { get; set; }
        public DateTime? RegistrationEndDate { get; set; }
        public bool? IsOverbudget { get; set; }
        public DateTime? ImplementationStartDate { get; set; }
        public DateTime? ImplementationEndDate { get; set; }
        public DateTime? ProgramCloseDate { get; set; }
        
        [MaxLength(200)]
        public string ReasonCloseProgram { get; set; }
       
        [MaxLength(200)]
        public string FilePathReasonCloseProgram { get; set; }
        
        [MaxLength(200)]
        public string FileNameReasonCloseProgram { get; set; }
    }
}
