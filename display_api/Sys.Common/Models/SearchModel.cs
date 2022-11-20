using System;

namespace Sys.Common.Models
{
    public class SearchModel
    {
        public double? CompanyId { get; set; }
        public string SearchACondition { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool? IsDesc { get; set; }
        public bool IsPaging { get; set; }
    }
}