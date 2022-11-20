using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Paging;
using Sys.Common.Constants;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RDOS.TMK_DisplayAPI.Models.Dis.ConfirmResultDetailListModel;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class DisConfirmResultDisplayRequest
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string DisplayCode { get; set; }

        [Required]
        public string SalesCalendarCode { get; set; }

        public bool IsNumberVisits { get; set; }
        public int? NumberVisitsType { get; set; }
        public int? NumberVisits { get; set; }
        public int? PercentPass { get; set; }
    }
    public class DisConfirmResultDisplayModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string DisplayCode { get; set; }
        public string SalesCalendarCode { get; set; }

        public bool IsNumberVisits { get; set; }
        public int? NumberVisitsType { get; set; }
        public int? NumberVisits { get; set; }
        public decimal? PercentPass { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        [NotMapped]
        public bool IsConfirm { get; set; }

        [NotMapped]
        public string StatusName { get; set; }
        [NotMapped]
        public List<DisConfirmResultDetail> DisConfirmResultDetail { get; set; }
    }

    public class ConfirmResultListModel
    {
        public List<DisConfirmResultDisplayModel> Items { get; set; } = new();
        public MetaData MetaData { get; set; }
        public ConfirmResultListModel()
        {

        }

        public ConfirmResultListModel(PagedList<DisConfirmResultDisplayModel> items)
        {
            Items = items;
            MetaData = items.MetaData;
        }
    }

    public class DisConfirmResultsModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; } = CommonData.DisplaySetting.Inprogress;
        public string StatusDes { get; set; }
        public string Description { get; set; }
        public string DisplayCode { get; set; }
        public string SalesCalendarCode { get; set; }
        public bool IsNumberVisits { get; set; }
        public int NumberVisitsType { get; set; }
        public int? NumberVisits { get; set; }
        public decimal? PercentPass { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        [NotMapped]
        public string StatusName { get; set; }
        [NotMapped]
        public string SalesCalendarName { get; set; }
        public int SetIsNumberVisits { get; set; }
        [NotMapped]
        public bool IsConfirm { get; set; } = false;

        public List<DisConfirmResultDetailValueModel> DisConfirmResultDetail { get; set; }
    }

    public class ListDisConfirmResultModel
    {
        public List<DisConfirmResultsModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
    public class DisConfirmResultDetailResponse : DisConfirmResultDetail
    {
        public bool IsPassed { get; set; }
    }
    public class DisConfirmResultDetailGrouped
    {
        public int TotalPass { get; set; }
        public int TotalNoPass { get; set; }
        public string LevelName { get; set; }
        public string LevelCode { get; set; }
    }

    public class ConfirmResultDetailReport
    {
        public int Total { get; set; }
        public DisDisplayModel Display { get; set; }
        public DisConfirmResultsModel ConfirmResult { get; set; }
        public IList<DisConfirmResultDetailGrouped> ConfirmResultDetails { get; set; }
    }

    public class ConfirmResultDetailJoinReport
    {
        public string Customer { get; set; }
        public string CustomerShipToCode { get; set; }
        public string CustomerShipToName { get; set; }
        public string ProductName { get; set; }
        public string Packing { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string LevelName { get; set; }
        public string LevelCode { get; set; }
    }
    public class ConfirmResultDetailJoinReportResponse
    {
        public MetaData MetaData { get; set; }
        public List<ConfirmResultDetailJoinReport> Items { get; set; }
    }

    public class ConfirmResultDetailJoiningResponse
    {
        public string CustomerCode { get; set; }
        public string CustomerShiptoCode { get; set; }
        public string DisplayCode { get; set; }
        public string DisConfirmResultCode { get; set; }
        public string DisplayLevelCode { get; set; }
        public bool IsRewardDonate { get; set; }
        public bool IsRewardProduct { get; set; }
        public string RewardProductType { get; set; }
        public string SalesOutputProductType { get; set; }
        public float SalesOutputPercentageOfAmount { get; set; }
        public decimal RewardAmountOfDonation { get; set; }
        public string Address { get; set; }
        public string LevelName { get; set; }
        public string LevelCode { get; set; }
    }
}
