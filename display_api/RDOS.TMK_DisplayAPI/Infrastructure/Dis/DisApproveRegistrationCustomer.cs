using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisApproveRegistrationCustomer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        
        public DateTime? AdditionalRegistrationDate { get; set; }
        
        public string AdditionalReasons { get; set; }
        
        public string FilePath { get; set; }
        
        public string FileName { get; set; }
        public string EffectiveTime { get; set; }
    }
}
