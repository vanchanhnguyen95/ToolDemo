using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class DisBudgetModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayLevelCode { get; set; }
        public string DisplayLevelName { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal NewTotalBudget { get; set; }
        public decimal BudgetQuantityUsed { get; set; }
        public DateTime AdjustmentDate { get; set; }
        [MaxLength(100)]
        public string AdjustmentsAccount { get; set; }
        [MaxLength(255)]
        public string AdjustmentsReason { get; set; }
        [MaxLength(255)]
        public string AdjustmentsFilePath { get; set; }
        [MaxLength(255)]
        public string AdjustmentsFileName { get; set; }
        public int AdjustmentsCount { get; set; }
        public int Type { get; set; }

        public string UserName { get; set; }
        public List<DisBudgetForScopeTerritoryModel> DisBudgetForScopeTerritories { get; set; } = new();
        public List<DisBudgetForScopeDsaModel> DisBudgetForScopeDsas { get; set; } = new();
        public List<DisBudgetForCusAttributeModel> DisBudgetForCusAttributes { get; set; } = new();
    }

    public class DisBudgetForScopeTerritoryModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayLevelCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string ScopeValue { get; set; }
        public string ScopeDescription { get; set; }
        public decimal BudgetQuantity { get; set; }
        public decimal NewBudgetQuantity { get; set; }
        public decimal BudgetQuantityUsed { get; set; }
        public int Type { get; set; }
        public int AdjustmentsCount { get; set; }
        public DateTime AdjustmentDate { get; set; }
    }

    public class DisBudgetForScopeDsaModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayLevelCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string ScopeValue { get; set; }
        public string ScopeDescription { get; set; }
        public decimal BudgetQuantity { get; set; }
        public decimal NewBudgetQuantity { get; set; }
        public decimal BudgetQuantityUsed { get; set; }
        public int Type { get; set; }
        public int AdjustmentsCount { get; set; }
        public DateTime AdjustmentDate { get; set; }
    }

    public class DisBudgetForCusAttributeModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string DisplayLevelCode { get; set; }
        [MaxLength(10)]
        public string ScopeValue { get; set; }
        [Required]
        [MaxLength(10)]
        public string CustomerLevel { get; set; }
        [Required]
        [MaxLength(10)]
        public string CustomerValue { get; set; }
        public string CustomerValueDescription { get; set; }
        public decimal BudgetQuantity { get; set; }
        public decimal NewBudgetQuantity { get; set; }
        public decimal BudgetQuantityUsed { get; set; }
        public int Type { get; set; }
        public int AdjustmentsCount { get; set; }
        public DateTime AdjustmentDate { get; set; }
    }

    public class DeleteDisBudgetsModel
    {
        public string DisplayCode { get; set; }
        public List<string> DisplayLevelCodes { get; set; }
    }

    public class DisBudgetForAdjustmentModel
    {
        public string DisplayCode { get; set; }
        public string UserName { get; set; }
        
        /// <summary>
        /// TypeBudgetNow = 1;
        /// TypeBudgetAdjustment = 2;
        /// TypeBudgetAllotmentAdjustment = 3;
        /// </summary>
        public int Type { get; set; }
        public List<DisBudgetModel> DisBudgets { get; set; } = new();
    }

    public class BudgetAdjustmentListModel
    {
        public string DisplayCode { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string AdjustmentsAccount { get; set; }
        public string AdjustmentsReason { get; set; }
        public string AdjustmentsFilePath { get; set; }
        public string AdjustmentsFileName { get; set; }
        public int AdjustmentsCount { get; set; }
        public int Type { get; set; }
    }

    public class ListBudgetAdjustmentListModel
    {
        public List<BudgetAdjustmentListModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
    
    public class DisBudgetReportModel
    {
        public Guid Id { get; set; }
        public string DisplayCode { get; set; }
        public string DisplayLevelCode { get; set; }
        public string DisplayLevelName { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal BudgetQuantityUsed { get; set; }
    }
}
