using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisCriteriaEvaluatePictureDisplay : DisAuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; set; }

        [Required]
        [MaxLength(255)]
        public string CriteriaDescription { get; set; }

        [Required]
        [MaxLength(10)]
        public string Result { get; set; }

        public DisCriteriaEvaluatePictureDisplay InitInsert(string createdBy)
        {
            const string DefinitionConfirmed = "02";
            CreatedDate = DateTime.Now;
            CreatedBy = createdBy;
            Status = DefinitionConfirmed;
            return this;
        }
        public DisCriteriaEvaluatePictureDisplay InitUpdate(string updatedBy)
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
