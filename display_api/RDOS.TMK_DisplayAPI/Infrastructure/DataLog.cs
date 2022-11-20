using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class DataLog
    {
        public int Id { get; set; }
        public string ObjectName { get; set; }
        public string ObjectId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string FieldName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int ActionTypeLog { get; set; }
    }
}
