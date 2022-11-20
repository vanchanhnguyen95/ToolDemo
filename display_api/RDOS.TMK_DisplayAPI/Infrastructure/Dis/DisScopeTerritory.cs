using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Infrastructure.Dis
{
    public class DisScopeTerritory : DisAuditableEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string TerritoryStructureCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string SalesTerritoryValue { get; set; }
    }
}
