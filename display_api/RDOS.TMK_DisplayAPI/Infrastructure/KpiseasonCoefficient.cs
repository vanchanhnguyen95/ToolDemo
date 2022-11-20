using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class KpiseasonCoefficient
    {
        public Guid Id { get; set; }
        public Guid KpisettingId { get; set; }
        public int Month { get; set; }
        public float Value { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string PeriodCode { get; set; }
        public Guid SaleCanlendarId { get; set; }
    }
}
