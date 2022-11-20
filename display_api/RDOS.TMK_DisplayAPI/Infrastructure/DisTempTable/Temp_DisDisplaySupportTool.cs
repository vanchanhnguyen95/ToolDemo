using System;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable
{
    public class Temp_DisDisplaySupportTool
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
