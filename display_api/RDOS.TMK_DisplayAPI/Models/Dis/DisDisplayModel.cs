using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.External;
using RDOS.TMK_DisplayAPI.Models.External.Enum;
using RDOS.TMK_DisplayAPI.Models.Paging;
using RDOS.TMK_DisplayAPI.Models.SalesOrg;
using Sys.Common.Constants;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOS.TMK_DisplayAPI.Models.Dis
{
    public class DisDisplayModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }
        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(100)]
        public string ShortName { get; set; }
        [Required]
        [MaxLength(10)]
        public string Status { get; set; }
        [Required]
        [MaxLength(10)]
        public string SaleOrg { get; set; }
        [Required]
        [MaxLength(10)]
        public string SicCode { get; set; }
        [Required]
        [MaxLength(10)]
        public string FrequencyDisplay { get; set; }
        public string Image1Path { get; set; }
        public string Image1Name { get; set; }
        public string Image2Path { get; set; }
        public string Image2Name { get; set; }
        public string Image3Path { get; set; }
        public string Image3Name { get; set; }
        public string Image4Path { get; set; }
        public string Image4Name { get; set; }

        public string ScopeType { get; set; }
        public string ScopeSaleTerritoryLevel { get; set; }
        public string TerritoryStructureCode { get; set; }
        public string ApplicableObjectType { get; set; }
        public bool? IsCheckSalesOutput { get; set; }
        public int SalesOutput { get; set; }

        public bool? IndependentDisplay { get; set; }
        public bool? ToolDisplay { get; set; }
        public bool? ManageNumberRegister { get; set; }

        public DateTime? RegistrationStartDate { get; set; }
        public DateTime? RegistrationEndDate { get; set; }
        public bool? IsOverbudget { get; set; }
        public DateTime? ImplementationStartDate { get; set; }
        public DateTime? ImplementationEndDate { get; set; }
        public DateTime? ProgramCloseDate { get; set; }
        public string ReasonCloseProgram { get; set; }
        public string FilePathReasonCloseProgram { get; set; }
        public string FileNameReasonCloseProgram { get; set; }

        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string StatusName { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [MaxLength(256)]
        public string CreatedBy { get; set; }
        [MaxLength(256)]
        public string UpdatedBy { get; set; }
        public int DeleteFlag { get; set; }
        public bool IsImplementation
        {
            get
            {
                var now = DateTime.Now;
                var minDate = DateTime.MinValue;
                var endDate = ImplementationEndDate ?? minDate;
                var startDate = ImplementationStartDate ?? minDate;
                return now <= endDate && now >= startDate && Status == CommonData.DisplaySetting.Implementation;
            }
        }

        public List<TpTerritoryStructureLevelModel> TerritoryStructureLevels { get; set; }
        public List<DisplayScopeModel> DisplayScopes { get; set; }
        public List<DisBudgetModel> DisBudgets { get; set; }
        public List<DisCustomerShiptoModel> DisCustomerShiptos { get; set; }
        public List<DisplayCustomerSettingModel> DisplayCustomerSettings { get; set; }
        public List<TpSalesTerritoryValueModel> ListScopeSalesTerritory { get; set; } = new List<TpSalesTerritoryValueModel>();
        public List<TpSalesOrgDsaModel> ListScopeDSA { get; set; } = new List<TpSalesOrgDsaModel>();
        public List<CustomerSettingModel> ListCustomerSetting { get; set; } = new List<CustomerSettingModel>();
        public List<CustomerAttributeModel> ListCustomerAttribute { get; set; } = new List<CustomerAttributeModel>();
        public List<DisDefinitionStructureModel> ListDefinitionStructure { get; set; } = new List<DisDefinitionStructureModel>();
    }

    public class DisDisplayUpdModel
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }

        public DateTime? RegistrationStartDate { get; set; }
        public DateTime? RegistrationEndDate { get; set; }
        public bool? IsOverbudget { get; set; }
        public DateTime? ImplementationStartDate { get; set; }
        public DateTime? ImplementationEndDate { get; set; }
        public DateTime? ProgramCloseDate { get; set; }
        public string ReasonCloseProgram { get; set; }
        public string FilePathReasonCloseProgram { get; set; }
        public string FileNameReasonCloseProgram { get; set; }
    }

    public class ListDisDisplayModelListModel
    {
        public List<DisDisplayModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }

    public class DisplayPopupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }    
        public string ShortName { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public string ScopeType { get; set; }
        public string ScopeTypeDescription { get; set; }
        public string ApplicableObjectType { get; set; }
        public string ApplicableObjectTypeDescription { get; set; }
        public bool IsOverbudget { get; set; }
        public DateTime? ImplementationStartDate { get; set; }
        public DateTime? ImplementationEndDate { get; set; }
    }

    public class ListDisplayPopupModel
    {
        public List<DisplayPopupModel> Items { get; set; }
        public MetaData MetaData { get; set; }
        public ListDisplayPopupModel() { }
        public ListDisplayPopupModel(PagedList<DisplayPopupModel> items)
        {
            Items = items;
            MetaData = items.MetaData;
        }
    }

    public class DisplayScopeModel
    {
        public Guid Id { get; set; }
        public string ScopeType { get; set; }
        public string ScopeTypeName { get; set; }
        public string TerritoryLevel { get; set; }
        public string TerritoryLevelName { get; set; }
        public string ScopeCode { get; set; }
        public string ScopeName { get; set; }
    }

    public class DisplayCustomerSettingModel
    {
        public Guid CustomerAttributeId { get; set; }
        public string CustomerLevelCode { get; set; }
        public string CustomerLevelName { get; set; }
        public bool IsCustomerAttribute { get; set; } = false;
        public bool IsApply { get; set; }
        public List<DisplayCustomerAttributeModel> CustomerAttributeModels { get; set; } = new List<DisplayCustomerAttributeModel>();
    }

    public class DisplayCustomerAttributeModel
    {
        public string CustomerLevelCode { get; set; }
        public string CustomerValueCode { get; set; }
        public string CustomerValueName { get; set; }
    }

    public class DisScopeCustomerShiptoModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ShiptoCode { get; set; }
        public string ShiptoName { get; set; }
        public Status Status { get; set; }
        public string Address { get; set; }
        public string SaleTerritoryLevel { get; set; }
        public string SaleTerritoryValue { get; set; }
        public string DsaCode { get; set; }
    }
    public class ListDisScopeCustomerShiptoModel
    {
        public List<DisScopeCustomerShiptoModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }

    public class DisplayGeneralModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public string SaleOrg { get; set; }
        public string SaleOrgDescription { get; set; }
        public string SicCode { get; set; }
        public string SicCodeDescription { get; set; }
        public string FrequencyDisplay { get; set; }
        public string FrequencyDescription { get; set; }
        public string ScopeType { get; set; }
        public string ScopeTypeDescription { get; set; }
        public string ApplicableObjectType { get; set; }
        public string ApplicableObjectTypeDescription { get; set; }
    }

    public class DisplayGeneralListModel
    {
        public List<DisplayGeneralModel> Items { get; set; }
        public MetaData MetaData { get; set; }
    }

    public class DisplayAutoSearchModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
    }

    public class DisplayListSearchModel
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string Status { get; set; }
        public string SaleOrg { get; set; }
        public string SicCode { get; set; }
        public string FrequencyDisplay { get; set; }
        public string ScopeType { get; set; }
        public string ApplicableObjectType { get; set; }
    }

    public class DisplayHeaderReportModel
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string ScopeType { get; set; }
        public string ScopeTypeName { get; set; }
        public string TerritoryStructureCode { get; set; }
        public int SettlementQuantity { get; set; }
        public string ApplicableObjectType { get; set; }
        public string ApplicableObjectTypeDescription { get; set; }
        public DateTime ImplementationStartDate { get; set; }
        public DateTime ImplementationEndDate { get; set; }
        public List<DisplayScopeModel> ListScope { get; set; }
        public List<TpTerritoryStructureLevelModel> TerritoryStructureLevels { get; set; }
        public List<CustomerSettingModel> ListCustomerSetting { get; set; }
        //public List<CustomerAttributeModel> ListApplicableObject { get; set; }
        public List<DisplayCustomerSettingModel> ListApplicableObject { get; set; }
        public List<DisBudgetReportModel> ListDataBudget { get; set; }
        public List<DisApproveRegistrationCustomerDetail> ApproveRegistrationCustomerDetails { get; set; }
    }

}
