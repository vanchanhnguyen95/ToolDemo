using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class DisApproveRegistrationCustomerRequest
    {
        public bool IsAdditionalRegistration { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }

        public string FilePath { get; set; }

        public string FileName { get; set; }
        
        public string AdditionalReasons { get; set; }
        
        public DateTime? AdditionalRegistrationDate { get; set; }
        
        public string EffectiveTime { get; set; }
        
        public List<DisApproveRegistrationCustomerDetailRequest> Details { get; set; }
    }
}