using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisApproveRegistrationCustomerDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }

        [Required]
        [MaxLength(10)]
        public string CustomerCode { get; set; }

        [Required]
        [MaxLength(255)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(10)]
        public string CustomerShipToCode { get; set; }

        [Required]
        [MaxLength(10)]
        public string DisplayLevel { get; set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; set; }
    }
}