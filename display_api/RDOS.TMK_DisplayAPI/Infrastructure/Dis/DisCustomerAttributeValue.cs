using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisCustomerAttributeValue : DisAuditableEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string CustomerAttributerLevel { get; set; }
        [Required]
        [MaxLength(10)]
        public string CustomerAttributerValue { get; set; }
    }
}
