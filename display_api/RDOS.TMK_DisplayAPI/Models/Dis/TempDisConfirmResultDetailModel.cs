using RDOS.TMK_DisplayAPI.Models.Paging;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class TempDisConfirmResultDetailModel
    {
        public Guid Id { get; set; }
        public string DisConfirmResultCode { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ShiptoCode { get; set; }
        public string CustomerShiptoName { get; set; }
        public string DisplayLevelCode { get; set; }
        public string DisplayLevelName { get; set; }
        public int NumberMustRating { get; set; }
        public int NumberHasEvaluate { get; set; }
        public int NumberPassed { get; set; }
        public decimal RevenuesRegistered { get; set; }
        public decimal RevenuesPass { get; set; }
        public string DisplayImageResult { get; set; }
        public string DisplaySalesResult { get; set; }
        public string AssessmentPeriodResult { get; set; }

        public string DisplayCode { get; set; }

        public string DisplayName { get; set; }

        public bool? IsCheckSalesOutput { get; set; }
        public int SalesOutput { get; set; }

        public bool? IndependentDisplay { get; set; } = false;

        public decimal SalesRegistered { get; set; }

        public decimal OutputRegistered { get; set; }

        public string CustomerShiptoCode { get; set; }

        public string CustomerAddress { get; set; }

        public decimal SalesPass { get; set; }

        public decimal OutputPass { get; set; }
    }

    public class TempDisConfirmResultDetailListModel
    {
        public List<TempDisConfirmResultDetailModel> Items { get; set; } = new();
        public MetaData MetaData { get; set; }
        public TempDisConfirmResultDetailListModel()
        {

        }

        public TempDisConfirmResultDetailListModel(PagedList<TempDisConfirmResultDetailModel> items)
        {
            Items = items;
            MetaData = items.MetaData;
        }
    }
}
