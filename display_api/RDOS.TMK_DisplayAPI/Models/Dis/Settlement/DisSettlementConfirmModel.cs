using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Models.Dis.Settlement
{
    public class DisSettlementConfirmModel
    {
        public string SettlementCode { get; set; }
        public string SettlementName { get; set; }
        public string DistributorCode { get; set; }
        public string DistributorName { get; set; }
        public string DisplayCode { get; set; }
        public string DisplayName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
    }

    public class DisSettlementConfirmListModel
    {
        public List<DisSettlementConfirmModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
