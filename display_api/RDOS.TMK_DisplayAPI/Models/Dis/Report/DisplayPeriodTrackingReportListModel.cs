using RDOS.TMK_DisplayAPI.Models.Paging;
using Sys.Common.Models;
using System;
using System.Collections.Generic;

namespace RDOS.TMK_DisplayAPI.Models.Dis.Report
{
    public class DisplayPeriodTrackingReportListModel
    {
        public string DisplayCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string Packing { get; set; }
        public string PackingDescription { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Amount { get; set; }
    }

    public class ListDisplayPeriodTrackingReportListModel
    {
        public List<DisplayPeriodTrackingReportListModel> Items { get; set; }
        public MetaData MetaData { get; set; }
        public ListDisplayPeriodTrackingReportListModel() { }
        public ListDisplayPeriodTrackingReportListModel(PagedList<DisplayPeriodTrackingReportListModel> items)
        {
            Items = items;
            MetaData = items.MetaData;
        }
    }

    public class DisFollowRewardProgressQuantityCustomerModel
    {
        public string DisplayCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CustomerCode { get; set; }
    }

    public class ListDisFollowRewardProgressQuantityCustomerModel
    {
        public List<DisFollowRewardProgressQuantityCustomerModel> Items { get; set; }
        public MetaData MetaData { get; set; }
        public ListDisFollowRewardProgressQuantityCustomerModel() { }
        public ListDisFollowRewardProgressQuantityCustomerModel(PagedList<DisFollowRewardProgressQuantityCustomerModel> items)
        {
            Items = items;
            MetaData = items.MetaData;
        }
    }
}
