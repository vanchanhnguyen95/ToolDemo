using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Paging;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class DisSettlementModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(10)]
        public string Status { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string RewardPeriodCode { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        [NotMapped]
        public string StatusName { get; set; }
        [NotMapped]
        public List<DisSettlementDetail> DisSettlementDetail { get; set; }
    }

    public class ListDisSettlementModel
    {
        public List<DisSettlementModel> Items { get; set; } = new();
        public MetaData MetaData { get; set; }
        public ListDisSettlementModel()
        {

        }

        public ListDisSettlementModel(PagedList<DisSettlementModel> items)
        {
            Items = items;
            MetaData = items.MetaData;
        }
    }


}
