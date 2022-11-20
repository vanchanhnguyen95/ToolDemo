using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class RzParameterLevelApply
    {
        public Guid Id { get; set; }
        public string ParameterSettingCode { get; set; }
        public string ApplicableLevel { get; set; }
        public string ApplicableValue { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
