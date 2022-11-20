using RDOS.TMK_DisplayAPI.Models.Paging;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class DisCriteriaEvaluatePictureDisplayRequest
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string CriteriaDescription { get; set; }

        [Required]
        public string Result { get; set; }
    }
    public class DisCriteriaEvaluatePictureDisplayModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Status { get; set; }
        
        public string StatusDisplay { get; set; }

        public string CriteriaDescription { get; set; }

        public string Result { get; set; }
        
        public string ResultDisplay { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
        
        public string CreatedBy { get; set; }
        
        public string UpdatedBy { get; set; }
        
        public int DeleteFlag { get; set; }
    }

    public class CriteriaEvaluatePictureDisplayListModel
    {
        public List<DisCriteriaEvaluatePictureDisplayModel> Items { get; set; } = new();
        public MetaData MetaData { get; set; }
        public CriteriaEvaluatePictureDisplayListModel()
        {

        }

        public CriteriaEvaluatePictureDisplayListModel(PagedList<DisCriteriaEvaluatePictureDisplayModel> items)
        {
            Items = items;
            MetaData = items.MetaData;
        }
    }
}
