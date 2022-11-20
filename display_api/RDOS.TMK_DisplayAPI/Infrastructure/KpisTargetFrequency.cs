using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class KpisTargetFrequency
    {
        public Guid Id { get; set; }
        public string KpisTargetCode { get; set; }
        public string FrequencyCode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string FrequencyValue { get; set; }
    }
}
