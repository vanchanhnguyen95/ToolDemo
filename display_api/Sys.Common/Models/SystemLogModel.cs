using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Common.Models
{
    public class SystemLogModel
    {
        public Guid Id { get; set; }
        [MaxLength(256)]
        public string ObjectName { get; set; }
        [MaxLength(10)]
        public int ErrorCode { get; set; }
        public int LogLevel { get; set; }
        public string Message { get; set; }
        public DateTime LogTime { get; set; }
    }
    public class SystemLogListModel
    {
        public List<SystemLogModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
