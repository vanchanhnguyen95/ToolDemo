using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Paging;
using Sys.Common.Models;
using System;
using System.Collections.Generic;

namespace RDOS.TMK_DisplayAPI.Models.Dis.PayReward
{
    public class RequestPayRewardModel
    {
        public DisPayRewardModel PayReward { get; set; }
        public List<DisPayRewardDetailModel> ListPayRewardDetail { get; set; }
    }

    public class RequestPayRewardDetailModel
    {
        public string DisPayRewardCode { get; set; }
        public string DisplayCode { get; set; }
        public string ConfirmResultDisplayCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EcoParameters parameters { get; set; }
    }

    public class ResponsePayRewardDetailModel
    {
        public List<DisPayRewardDetailModel> Items { get; set; }
        public MetaData MetaData { get; set; }
        public int TotalCustomerPayReward { get; set; }
        public decimal TotalAmountPayReward { get; set; }
        public List<TotalProductPayRewardModel> ListSumProductPayReward { get; set; } = new();
        public ResponsePayRewardDetailModel() { }
        public ResponsePayRewardDetailModel(PagedList<DisPayRewardDetailModel> items)
        {
            Items = items;
            MetaData = items.MetaData;
        }
    }
}
