using System;
using System.Collections.Generic;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class SystemLog
    {
        public Guid Id { get; set; }
        public string ObjectName { get; set; }
        public int ErrorCode { get; set; }
        public int LogLevel { get; set; }
        public string Message { get; set; }
        public DateTime LogTime { get; set; }
    }
}
