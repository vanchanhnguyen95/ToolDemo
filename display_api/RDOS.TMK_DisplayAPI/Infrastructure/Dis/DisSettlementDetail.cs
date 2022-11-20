using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisSettlementDetail : DisAuditableEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisSettlementCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string OrdNbr { get; set; }
        public DateTime OrdDate { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [MaxLength(10)]
        public string DisplayLevel { get; set; }

        [MaxLength(10)]
        public string CustomerId { get; set; }

        [MaxLength(10)]
        public string ShiptoId { get; set; }

        [MaxLength(10)]
        public string DistributorCode { get; set; }
        
        [MaxLength(10)]
        public string ProductCode { get; set; }
        
        [MaxLength(10)]
        public string PackageCode { get; set; }
        
        [MaxLength(10)]
        public string Status { get; set; }

        public decimal? Quantity { get; set; }
        public decimal? Amount { get; set; }

        public DisSettlementDetail InitInsert(string createdBy)
        {
            CreatedDate = DateTime.Now;
            CreatedBy = createdBy;
            return this;
        }
        public DisSettlementDetail InitUpdate(string updatedBy)
        {
            if (DeleteFlag != 1)
            {
                UpdatedBy = updatedBy;
                UpdatedDate = DateTime.Now;
            }

            return this;
        }

    }
}
