using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class DisApproveRegistrationCustomerDetailRequest
    {
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

    public record DisApproveRegistrationCustomerCodeRequest
    {
        public string CustomerCode { get; set; }
        public string DisplayLevel { get; set; }
    }
}
