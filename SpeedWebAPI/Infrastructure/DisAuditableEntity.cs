using System;
using System.ComponentModel.DataAnnotations;

namespace SpeedWebAPI.Infrastructure
{
    public class DisAuditableEntity : DisValueObjects
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [MaxLength(256)]
        public string CreatedBy { get; set; }
        [MaxLength(256)]
        public string UpdatedBy { get; set; }
        public int DeleteFlag { get; set; }
        public int UpdateCount { get; set; }
    }
}
