using RDOS.TMK_DisplayAPI.Models.Paging;
using Sys.Common.Models;
using System;
using System.Collections.Generic;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public partial class TempDisOrderDetailModel
    {
        public Guid Id { get; set; }

        public string OrdNbr { get; set; }

        public string InventoryId { get; set; }
        public string InventoryName { get; set; }

        public string Uom { get; set; }

        public bool IsFree { get; set; }
        public decimal ShippedQty { get; set; }
        public decimal UnitPrice { get; set; }

        public string DiscountId { get; set; }
        public string DiscountName { get; set; }

        public string DiscountType { get; set; }

        public string DiscountSchemeId { get; set; }
        public decimal ShippedLineDiscAmt { get; set; }

        public string PromotionLevel { get; set; }
        public string TMKType { get; set; }
        public string RewardPeriodCode { get; set; }
        public string RewardPeriodName { get; set; }

        public string UomName { get; set; }

        public string DisplayLevel { get; set; }

        public string DisplayLevelName { get; set; }

    }
    public class TempDisOrderDetailListModel
    {
        public List<TempDisOrderDetailModel> Items { get; set; } = new();
        public MetaData MetaData { get; set; }
        public TempDisOrderDetailListModel()
        {

        }

        public TempDisOrderDetailListModel(PagedList<TempDisOrderDetailModel> items)
        {
            Items = items;
            MetaData = items.MetaData;
        }
    }
}
