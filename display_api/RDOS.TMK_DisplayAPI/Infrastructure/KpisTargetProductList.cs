using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class KpisTargetProductList
    {
        public Guid Id { get; set; }
        public string KpisTargetCode { get; set; }
        public string FrequencyCode { get; set; }
        public string ProductListCode { get; set; }
    }
}
