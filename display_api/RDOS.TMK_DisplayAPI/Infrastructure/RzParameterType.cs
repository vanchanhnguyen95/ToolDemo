using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class RzParameterType
    {
        public Guid Id { get; set; }
        public string ParameterTypeCode { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }
        public string DataValue { get; set; }
        public string DefaultValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
