using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class RzVisitFrequency
    {
        public Guid Id { get; set; }
        public string VisitFrequencyCode { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
