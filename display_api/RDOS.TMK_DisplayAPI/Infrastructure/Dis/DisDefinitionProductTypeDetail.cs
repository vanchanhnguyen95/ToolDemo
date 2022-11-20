using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisDefinitionProductTypeDetail : DisAuditableEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string LevelCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string ProductType { get; set; }
        public string ItemHierarchyLevel { get; set; }
        [Required]
        [MaxLength(10)]
        public string ProductCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string Packing { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
