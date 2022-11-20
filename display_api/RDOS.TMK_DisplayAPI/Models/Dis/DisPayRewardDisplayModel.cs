using RDOS.TMK_DisplayAPI.Models.Paging;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class DisPayRewardDisplayModel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }
        public string Status { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        public string ConfirmResultDisplayCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool PayRewardMethod { get; set; }

    }

    public class ListPayRewardDisplayModel
    {
        public List<DisPayRewardDisplayModel> Items { get; set; } = new();
        public MetaData MetaData { get; set; }
        public ListPayRewardDisplayModel()
        {

        }

        public ListPayRewardDisplayModel(PagedList<DisPayRewardDisplayModel> items)
        {
            Items = items;
            MetaData = items.MetaData;
        }
    }


}
