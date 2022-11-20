using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Paging;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class DisConfirmResultDetailDisplayRequest
    {
        [Required]
        public string DisConfirmResultCode { get; set; }

        [Required]
        public string CustomerCode { get; set; }

        [Required]
        public string ShiptoCode { get; set; }

        [Required]
        public string CustomerShiptoName { get; set; }

        [Required]
        public string DisplayLevelCode { get; set; }

        [Required]
        public string DisplayLevelName { get; set; }

        public int NumberMustRating { get; set; }
        public int NumberHasEvaluate { get; set; }
        public int NumberPassed { get; set; }
        public decimal RevenuesRegistered { get; set; }
        public decimal RevenuesPass { get; set; }

        [Required]
        public string DisplayImageResult { get; set; }

        [Required]
        public string DisplaySalesResult { get; set; }
        [Required]
        public string AssessmentPeriodResult { get; set; }
    }
    public class DisConfirmResultDetailDisplayModel
    {
        public Guid Id { get; set; }
        public string DisConfirmResultCode { get; set; }
        public string CustomerCode { get; set; }
        public string ShiptoCode { get; set; }
        public string CustomerShiptoName { get; set; }
        public string DisplayLevelCode { get; set; }
        public string DisplayLevelName { get; set; }

        public int? NumberMustRating { get; set; }
        public int? NumberHasEvaluate { get; set; }
        public int? NumberPassed { get; set; }
        public decimal? RevenuesRegistered { get; set; }
        public decimal? RevenuesPass { get; set; }

        public string DisplayImageResult { get; set; }
        public string DisplaySalesResult { get; set; }
        public string AssessmentPeriodResult { get; set; }
        public string DisplayCode { get; set; }
        public string CustomerShiptoCode { get; set; }
        public decimal SalesPass { get; set; }
        public decimal OutputPass { get; set; }

    }

    public class ConfirmResultDetailListModel
    {
        public List<DisConfirmResultDetailDisplayModel> Items { get; set; } = new();
        public MetaData MetaData { get; set; }
        public ConfirmResultDetailListModel()
        {

        }

        public ConfirmResultDetailListModel(PagedList<DisConfirmResultDetailDisplayModel> items)
        {
            Items = items;
            MetaData = items.MetaData;
        }

        public class DisConfirmResultDetailValueModel
        {
            public Guid Id { get; set; }

            public string DisConfirmResultCode { get; set; }

            public string DisplayCode { get; set; }

            public string DisplayName { get; set; }

            public string DisplayLevelCode { get; set; }

            public string DisplayLevelName { get; set; }

            public bool? IsCheckSalesOutput { get; set; }

            public int SalesOutput { get; set; }

            public bool? IndependentDisplay { get; set; } = false;

            public decimal SalesRegistered { get; set; }

            public decimal OutputRegistered { get; set; }

            public string PeriodCode { get; set; }

            public string CustomerCode { get; set; }

            public string CustomerName { get; set; }

            public string CustomerShiptoCode { get; set; }

            public string CustomerAddress { get; set; }

            public int NumberMustRating { get; set; }

            public int NumberHasEvaluate { get; set; }

            public int NumberPassed { get; set; }

            public decimal SalesPass { get; set; }

            public decimal OutputPass { get; set; }

            public bool DisplayImageResult { get; set; }
            public string DisplayImageResultDes { get; set; }
            public bool DisplaySalesResult { get; set; }
            public string DisplaySalesResultDes { get; set; }
            public bool AssessmentPeriodResult { get; set; }
            public string AssessmentPeriodResultDes { get; set; }

            [NotMapped]
            public DisDefinitionStructure disDefinitionStructure { get; set; }
        }
    }
}
