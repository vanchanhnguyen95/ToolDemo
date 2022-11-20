using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable
{
    public class TempDisConfirmResultDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string DisplayName { get; set; }

        [Required]
        [MaxLength(10)]
        public string DisplayLevelCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string DisplayLevelName { get; set; }

        public bool? IsCheckSalesOutput { get; set; }

        public int SalesOutput { get; set; }

        public bool? IndependentDisplay { get; set; } = false;

        public decimal SalesRegistered { get; set; }

        public decimal OutputRegistered { get; set; }

        [MaxLength(10)]
        public string PeriodCode { get; set; }

        [Required]
        [MaxLength(10)]
        public string CustomerCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(10)]
        public string CustomerShiptoCode { get; set; }

        [Required]
        [MaxLength(1000)]
        public string CustomerAddress { get; set; }

        public int NumberMustRating { get; set; }
        public int NumberHasEvaluate { get; set; }
        public int NumberPassed { get; set; }
        
        public decimal SalesPass { get; set; }

        public decimal OutputPass { get; set; }
    }
}
