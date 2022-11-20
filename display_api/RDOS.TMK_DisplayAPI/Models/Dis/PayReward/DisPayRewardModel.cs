using RDOS.TMK_DisplayAPI.Models.Paging;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Models.Dis.PayReward
{
    public class DisPayRewardModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string DisplayCode { get; set; }
        public string ConfirmResultDisplayCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool PayRewardMethod { get; set; }
    }

    public class ListPayRewardModel
    {
        public List<DisPayRewardModel> Items { get; set; } = new();
        public MetaData MetaData { get; set; }
        public ListPayRewardModel() { }
        public ListPayRewardModel(PagedList<DisPayRewardModel> items)
        {
            Items = items;
            MetaData = items.MetaData;
        }
    }
}
