using RDOS.TMK_DisplayAPI.Models.Paging;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class DisSettlementDetailModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisSettlementCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string OrdNbr { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string DistributorCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string ProductCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string PackageCode { get; set; }

        public decimal? Quantity { get; set; }
        public decimal? Amount { get; set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; set; }
        public DateTime OrdDate { get; set; }
        [MaxLength(10)]
        public string DisplayLevel { get; set; }

        [MaxLength(10)]
        public string CustomerId { get; set; }

        [MaxLength(10)]
        public string ShiptoId { get; set; }

        [NotMapped]
        public string DistributorName { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        [NotMapped]
        public string Packing { get; set; }
        
        [NotMapped]
        public string DisplayLevelName { get; set; }
        [NotMapped]
        public string CustomerName { get; set; }
        [NotMapped]
        public string ShiptoName { get; set; }
        [NotMapped]
        public string DisplayName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class DisSettlementDetailListModel
    {
        public List<DisSettlementDetailModel> Items { get; set; } = new();
        public MetaData MetaData { get; set; }
        public DisSettlementDetailListModel()
        {

        }

        public DisSettlementDetailListModel(PagedList<DisSettlementDetailModel> items)
        {
            Items = items;
            MetaData = items.MetaData;
        }
    }
}
