using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Models.Dis.PayReward
{
    public class DisPayRewardDetailModel : ICloneable
    {
        public Guid Id { get; set; }
        public string DisPayRewardCode { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerShiptoCode { get; set; }
        public string ProductCode { get; set; }
        public string PackingCode { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public string DisplayLevelCode { get; set; }
        public string RewardType { get; set; }
        public string ProductType { get; set; }
        public string ItemHierarchyLevel { get; set; }

        [NotMapped]
        public int PayRewardType { get; set; }
        [NotMapped]
        public string CustomerShiptoAddress { get; set; }
        [NotMapped]
        public string ProductDescription { get; set; }
        [NotMapped]
        public string PackingDescription { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public DisPayRewardDetailModel CloneData()
        {
            return this.Clone() as DisPayRewardDetailModel;
        }
    }

    public class TotalProductPayRewardModel
    {
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public string Packing { get; set; }
    }
}
