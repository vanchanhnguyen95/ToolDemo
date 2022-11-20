using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable;

#nullable disable

namespace RDOS.TMK_DisplayAPI.Infrastructure
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<AdjustItemGroupPrice> AdjustItemGroupPrices { get; set; }
        public virtual DbSet<AdjustPriceListUoMitemGroup> AdjustPriceListUoMitemGroups { get; set; }
        public virtual DbSet<ApiMapping> ApiMappings { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ApplicationInviteCode> ApplicationInviteCodes { get; set; }
        public virtual DbSet<ApplicationLanguagePack> ApplicationLanguagePacks { get; set; }
        public virtual DbSet<ApplicationLocalization> ApplicationLocalizations { get; set; }
        public virtual DbSet<ApplicationLocalizationHistory> ApplicationLocalizationHistories { get; set; }
        public virtual DbSet<ApplicationNotiDeviceToken> ApplicationNotiDeviceTokens { get; set; }
        public virtual DbSet<ApplicationNotiMessage> ApplicationNotiMessages { get; set; }
        public virtual DbSet<ApplicationNotiUrgent> ApplicationNotiUrgents { get; set; }
        public virtual DbSet<ApplicationOtpcode> ApplicationOtpcodes { get; set; }
        public virtual DbSet<ApplicationService> ApplicationServices { get; set; }
        public virtual DbSet<ApplicationThemesConfigure> ApplicationThemesConfigures { get; set; }
        public virtual DbSet<ApplicationThemesElement> ApplicationThemesElements { get; set; }
        public virtual DbSet<ApplicationThemesSetting> ApplicationThemesSettings { get; set; }
        public virtual DbSet<ApplicationThemesSuggestion> ApplicationThemesSuggestions { get; set; }
        public virtual DbSet<ApplicationUserMapping> ApplicationUserMappings { get; set; }
        public virtual DbSet<ApplicationVersion> ApplicationVersions { get; set; }
        public virtual DbSet<ApplicationVersionPrinciple> ApplicationVersionPrinciples { get; set; }
        public virtual DbSet<AsoRefResult> AsoRefResults { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<AutoKpisTarget> AutoKpisTargets { get; set; }
        public virtual DbSet<AutoKpisTargetAchievementsCurrentYear> AutoKpisTargetAchievementsCurrentYears { get; set; }
        public virtual DbSet<AutoKpisTargetAchievementsCurrentYearValue> AutoKpisTargetAchievementsCurrentYearValues { get; set; }
        public virtual DbSet<AutoKpisTargetBusinessModel> AutoKpisTargetBusinessModels { get; set; }
        public virtual DbSet<AutoKpisTargetBusinessModelValue> AutoKpisTargetBusinessModelValues { get; set; }
        public virtual DbSet<AutoKpisTargetContributionBySic> AutoKpisTargetContributionBySics { get; set; }
        public virtual DbSet<AutoKpisTargetContributionBySicValue> AutoKpisTargetContributionBySicValues { get; set; }
        public virtual DbSet<AutoKpisTargetDevelopment> AutoKpisTargetDevelopments { get; set; }
        public virtual DbSet<AutoKpisTargetDevelopmentDetail> AutoKpisTargetDevelopmentDetails { get; set; }
        public virtual DbSet<City> Citys { get; set; }
        public virtual DbSet<CleanDataConfigure> CleanDataConfigures { get; set; }
        public virtual DbSet<Competitor> Competitors { get; set; }
        public virtual DbSet<ConditionstoExcludeSc> ConditionstoExcludeScs { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<Container> Containers { get; set; }
        public virtual DbSet<ContractType> ContractTypes { get; set; }
        public virtual DbSet<Country> Countrys { get; set; }
        public virtual DbSet<CronJobSchedule> CronJobSchedules { get; set; }
        public virtual DbSet<CustomerAdjustment> CustomerAdjustments { get; set; }
        public virtual DbSet<CustomerAdjustmentDataType> CustomerAdjustmentDataTypes { get; set; }
        public virtual DbSet<CustomerAdjustmentShipto> CustomerAdjustmentShiptos { get; set; }
        public virtual DbSet<CustomerApplyToValue> CustomerApplyToValues { get; set; }
        public virtual DbSet<CustomerAttribute> CustomerAttributes { get; set; }
        public virtual DbSet<CustomerContact> CustomerContacts { get; set; }
        public virtual DbSet<CustomerContactEmail> CustomerContactEmails { get; set; }
        public virtual DbSet<CustomerContactPhone> CustomerContactPhones { get; set; }
        public virtual DbSet<CustomerContract> CustomerContracts { get; set; }
        public virtual DbSet<CustomerDmsAttribute> CustomerDmsAttributes { get; set; }
        public virtual DbSet<CustomerHierarchy> CustomerHierarchies { get; set; }
        public virtual DbSet<CustomerHierarchyMapping> CustomerHierarchyMappings { get; set; }
        public virtual DbSet<CustomerInformation> CustomerInformations { get; set; }
        public virtual DbSet<CustomerSetting> CustomerSettings { get; set; }
        public virtual DbSet<CustomerSettingHierarchy> CustomerSettingHierarchies { get; set; }
        public virtual DbSet<CustomerShipto> CustomerShiptos { get; set; }
        public virtual DbSet<CustomerShiptoContact> CustomerShiptoContacts { get; set; }
        public virtual DbSet<DataLog> DataLogs { get; set; }
        public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public virtual DbSet<DataTypeDefinition> DataTypeDefinitions { get; set; }
        public virtual DbSet<DistCache> DistCaches { get; set; }
        public virtual DbSet<Distributor> Distributors { get; set; }
        public virtual DbSet<DistributorContact> DistributorContacts { get; set; }
        public virtual DbSet<DistributorContract> DistributorContracts { get; set; }
        public virtual DbSet<DistributorHierarchy> DistributorHierarchies { get; set; }
        public virtual DbSet<DistributorHierarchyMapping> DistributorHierarchyMappings { get; set; }
        public virtual DbSet<DistributorHistorical> DistributorHistoricals { get; set; }
        public virtual DbSet<DistributorPriceApplyToOutletAttribute> DistributorPriceApplyToOutletAttributes { get; set; }
        public virtual DbSet<DistributorPriceItemGroup> DistributorPriceItemGroups { get; set; }
        public virtual DbSet<DistributorPriceVolume> DistributorPriceVolumes { get; set; }
        public virtual DbSet<DistributorPriceVolumeLevel> DistributorPriceVolumeLevels { get; set; }
        public virtual DbSet<DistributorSellingArea> DistributorSellingAreas { get; set; }
        public virtual DbSet<DistributorShipto> DistributorShiptos { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<DropSizeRefResult> DropSizeRefResults { get; set; }
        public virtual DbSet<DsaDelivery> DsaDeliveries { get; set; }
        public virtual DbSet<DsaDistributorSellingArea> DsaDistributorSellingAreas { get; set; }
        public virtual DbSet<DsaGeographicalMapping> DsaGeographicalMappings { get; set; }
        public virtual DbSet<DsaSalesTeamAssignment> DsaSalesTeamAssignments { get; set; }
        public virtual DbSet<DsadistributorShipTo> DsadistributorShipTos { get; set; }
        public virtual DbSet<Dsageographical> Dsageographicals { get; set; }
        public virtual DbSet<DynamicFieldConfigure> DynamicFieldConfigures { get; set; }
        public virtual DbSet<EcoLocalization> EcoLocalizations { get; set; }
        public virtual DbSet<EmailType> EmailTypes { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<GeographicalMapping> GeographicalMappings { get; set; }
        public virtual DbSet<GeographicalMaster> GeographicalMasters { get; set; }
        public virtual DbSet<GeographicalStructure> GeographicalStructures { get; set; }
        public virtual DbSet<InvAdjustmentDetail> InvAdjustmentDetails { get; set; }
        public virtual DbSet<InvAdjustmentHeader> InvAdjustmentHeaders { get; set; }
        public virtual DbSet<InvAllocationDetail> InvAllocationDetails { get; set; }
        public virtual DbSet<InvInventoryTransaction> InvInventoryTransactions { get; set; }
        public virtual DbSet<InvLotAvailable> InvLotAvailables { get; set; }
        public virtual DbSet<InvReason> InvReasons { get; set; }
        public virtual DbSet<InvSellInLotByDate> InvSellInLotByDates { get; set; }
        public virtual DbSet<InvSellOutLotByDate> InvSellOutLotByDates { get; set; }
        public virtual DbSet<InvWhTransferDetail> InvWhTransferDetails { get; set; }
        public virtual DbSet<InvWhTransferHeader> InvWhTransferHeaders { get; set; }
        public virtual DbSet<InvWhTransferToEmployeeHeader> InvWhTransferToEmployeeHeaders { get; set; }
        public virtual DbSet<InventoryItem> InventoryItems { get; set; }
        public virtual DbSet<InventoryKit> InventoryKits { get; set; }
        public virtual DbSet<InventoryResult> InventoryResults { get; set; }
        public virtual DbSet<ItemAttribute> ItemAttributes { get; set; }
        public virtual DbSet<ItemGroup> ItemGroups { get; set; }
        public virtual DbSet<ItemHierarchyMapping> ItemHierarchyMappings { get; set; }
        public virtual DbSet<ItemHierarchyMappingCompetitor> ItemHierarchyMappingCompetitors { get; set; }
        public virtual DbSet<ItemManufacture> ItemManufactures { get; set; }
        public virtual DbSet<ItemSetting> ItemSettings { get; set; }
        public virtual DbSet<ItemsCompetitor> ItemsCompetitors { get; set; }
        public virtual DbSet<ItemsFile> ItemsFiles { get; set; }
        public virtual DbSet<ItemsUomconversion> ItemsUomconversions { get; set; }
        public virtual DbSet<JobTitle> JobTitles { get; set; }
        public virtual DbSet<JobTitleRole> JobTitleRoles { get; set; }
        public virtual DbSet<Kit> Kits { get; set; }
        public virtual DbSet<KitInventoryItemConversion> KitInventoryItemConversions { get; set; }
        public virtual DbSet<KitUomConversion> KitUomConversions { get; set; }
        public virtual DbSet<Kpi> Kpis { get; set; }
        public virtual DbSet<KpiResult> KpiResults { get; set; }
        public virtual DbSet<KpiTargetComplete> KpiTargetCompletes { get; set; }
        public virtual DbSet<KpisForObjectRef> KpisForObjectRefs { get; set; }
        public virtual DbSet<KpisObject> KpisObjects { get; set; }
        public virtual DbSet<KpisSiref> KpisSirefs { get; set; }
        public virtual DbSet<KpisTarget> KpisTargets { get; set; }
        public virtual DbSet<KpisTargetForObject> KpisTargetForObjects { get; set; }
        public virtual DbSet<KpisTargetFrequency> KpisTargetFrequencies { get; set; }
        public virtual DbSet<KpisTargetGroupByKpisRepeat> KpisTargetGroupByKpisRepeats { get; set; }
        public virtual DbSet<KpisTargetProductList> KpisTargetProductLists { get; set; }
        public virtual DbSet<KpisTargetProductListItemCode> KpisTargetProductListItemCodes { get; set; }
        public virtual DbSet<KpisTargetProductListKpi> KpisTargetProductListKpis { get; set; }
        public virtual DbSet<KpiseasonCoefficient> KpiseasonCoefficients { get; set; }
        public virtual DbSet<Kpisetting> Kpisettings { get; set; }
        public virtual DbSet<KpivisitFrequency> KpivisitFrequencies { get; set; }
        public virtual DbSet<Localization> Localizations { get; set; }
        public virtual DbSet<LocalizationsBackup> LocalizationsBackups { get; set; }
        public virtual DbSet<LppcRefResult> LppcRefResults { get; set; }
        public virtual DbSet<Manufacture> Manufactures { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MobileFeaturesPermission> MobileFeaturesPermissions { get; set; }
        public virtual DbSet<MobileUser> MobileUsers { get; set; }
        public virtual DbSet<MobileUserApplication> MobileUserApplications { get; set; }
        public virtual DbSet<MobileUserDevice> MobileUserDevices { get; set; }
        public virtual DbSet<MobileUserEmployee> MobileUserEmployees { get; set; }
        public virtual DbSet<MobileUserInfo> MobileUserInfos { get; set; }
        public virtual DbSet<MobileUserPrinciple> MobileUserPrinciples { get; set; }
        public virtual DbSet<MobileUserPrinciplesHistory> MobileUserPrinciplesHistories { get; set; }
        public virtual DbSet<MobileUserSetting> MobileUserSettings { get; set; }
        public virtual DbSet<MobileUsersLocked> MobileUsersLockeds { get; set; }
        public virtual DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PaginationConfig> PaginationConfigs { get; set; }
        public virtual DbSet<ParameterWithSiref> ParameterWithSirefs { get; set; }
        public virtual DbSet<ParameterWithSitype> ParameterWithSitypes { get; set; }
        public virtual DbSet<PcrefResult> PcrefResults { get; set; }
        public virtual DbSet<PhoneType> PhoneTypes { get; set; }
        public virtual DbSet<PoAllocationSetting> PoAllocationSettings { get; set; }
        public virtual DbSet<PoAllocationSettingItemGroup> PoAllocationSettingItemGroups { get; set; }
        public virtual DbSet<PoAverageDailySale> PoAverageDailySales { get; set; }
        public virtual DbSet<PoDeliveryLeadTime> PoDeliveryLeadTimes { get; set; }
        public virtual DbSet<PoGrpodetailItem> PoGrpodetailItems { get; set; }
        public virtual DbSet<PoGrpoheader> PoGrpoheaders { get; set; }
        public virtual DbSet<PoOrderDetail> PoOrderDetails { get; set; }
        public virtual DbSet<PoOrderHeader> PoOrderHeaders { get; set; }
        public virtual DbSet<PoPoconfirmDetailItem> PoPoconfirmDetailItems { get; set; }
        public virtual DbSet<PoPoconfirmHeader> PoPoconfirmHeaders { get; set; }
        public virtual DbSet<PoPurchaseSchedule> PoPurchaseSchedules { get; set; }
        public virtual DbSet<PoPurchaseScheduleDetail> PoPurchaseScheduleDetails { get; set; }
        public virtual DbSet<PoReturnDetailItem> PoReturnDetailItems { get; set; }
        public virtual DbSet<PoReturnHeader> PoReturnHeaders { get; set; }
        public virtual DbSet<PoRpoparameter> PoRpoparameters { get; set; }
        public virtual DbSet<PoStockKeepingDay> PoStockKeepingDays { get; set; }
        public virtual DbSet<PoStockKeepingDayItemHierarchy> PoStockKeepingDayItemHierarchies { get; set; }
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<PriceDefinitionDistributor> PriceDefinitionDistributors { get; set; }
        public virtual DbSet<PriceList> PriceLists { get; set; }
        public virtual DbSet<PriceListDistributeSellingArea> PriceListDistributeSellingAreas { get; set; }
        public virtual DbSet<PriceListItemGroup> PriceListItemGroups { get; set; }
        public virtual DbSet<PriceListOutletAttributeValue> PriceListOutletAttributeValues { get; set; }
        public virtual DbSet<PriceListSalesTerritoryLevel> PriceListSalesTerritoryLevels { get; set; }
        public virtual DbSet<PriceListType> PriceListTypes { get; set; }
        public virtual DbSet<PriceListTypeAttributeList> PriceListTypeAttributeLists { get; set; }
        public virtual DbSet<PriceSetting> PriceSettings { get; set; }
        public virtual DbSet<PriceSettingAuditLog> PriceSettingAuditLogs { get; set; }
        public virtual DbSet<PrimarySic> PrimarySics { get; set; }
        public virtual DbSet<PrimarySicExcludeHierarchyDetail> PrimarySicExcludeHierarchyDetails { get; set; }
        public virtual DbSet<PrimarySicExcludeItemGroupDetail> PrimarySicExcludeItemGroupDetails { get; set; }
        public virtual DbSet<PrimarySicIncludeDetail> PrimarySicIncludeDetails { get; set; }
        public virtual DbSet<Principal> Principals { get; set; }
        public virtual DbSet<PrincipalEmpContract> PrincipalEmpContracts { get; set; }
        public virtual DbSet<PrincipalProfile> PrincipalProfiles { get; set; }
        public virtual DbSet<PrincipalSetting> PrincipalSettings { get; set; }
        public virtual DbSet<PrincipalWarehouse> PrincipalWarehouses { get; set; }
        public virtual DbSet<PrincipalWarehouseLocation> PrincipalWarehouseLocations { get; set; }
        public virtual DbSet<PrincipalWinzardSetup> PrincipalWinzardSetups { get; set; }
        public virtual DbSet<PrincipleEmployee> PrincipleEmployees { get; set; }
        public virtual DbSet<PriorityPriceListType> PriorityPriceListTypes { get; set; }
        public virtual DbSet<ProductList> ProductLists { get; set; }
        public virtual DbSet<ProductListItemCode> ProductListItemCodes { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<PurchaseBasePrice> PurchaseBasePrices { get; set; }
        public virtual DbSet<PurchasePriceItemGroup> PurchasePriceItemGroups { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<RefreshTokenModel> RefreshTokenModels { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleClaim> RoleClaims { get; set; }
        public virtual DbSet<RzBeatPlan> RzBeatPlans { get; set; }
        public virtual DbSet<RzBeatPlanEmployee> RzBeatPlanEmployees { get; set; }
        public virtual DbSet<RzBeatPlanShipto> RzBeatPlanShiptos { get; set; }
        public virtual DbSet<RzLocation> RzLocations { get; set; }
        public virtual DbSet<RzParameterLevelApply> RzParameterLevelApplies { get; set; }
        public virtual DbSet<RzParameterSetting> RzParameterSettings { get; set; }
        public virtual DbSet<RzParameterType> RzParameterTypes { get; set; }
        public virtual DbSet<RzParameterValue> RzParameterValues { get; set; }
        public virtual DbSet<RzRouteZoneInfomation> RzRouteZoneInfomations { get; set; }
        public virtual DbSet<RzRouteZoneParameter> RzRouteZoneParameters { get; set; }
        public virtual DbSet<RzRouteZoneShipto> RzRouteZoneShiptos { get; set; }
        public virtual DbSet<RzRouteZoneType> RzRouteZoneTypes { get; set; }
        public virtual DbSet<RzVisitFrequency> RzVisitFrequencies { get; set; }
        public virtual DbSet<SaUserWithDistributorShipto> SaUserWithDistributorShiptos { get; set; }
        public virtual DbSet<SaleCalendar> SaleCalendars { get; set; }
        public virtual DbSet<SaleCalendarActionHistory> SaleCalendarActionHistories { get; set; }
        public virtual DbSet<SaleCalendarGenerate> SaleCalendarGenerates { get; set; }
        public virtual DbSet<SaleCalendarHoliday> SaleCalendarHolidays { get; set; }
        public virtual DbSet<SaleGroup> SaleGroups { get; set; }
        public virtual DbSet<SalesBasePrice> SalesBasePrices { get; set; }
        public virtual DbSet<SalesIndicatorRef> SalesIndicatorRefs { get; set; }
        public virtual DbSet<SalesIndicatorType> SalesIndicatorTypes { get; set; }
        public virtual DbSet<SalesOganization> SalesOganizations { get; set; }
        public virtual DbSet<SalesPriceItemGroup> SalesPriceItemGroups { get; set; }
        public virtual DbSet<SalesPriceItemGroupReference> SalesPriceItemGroupReferences { get; set; }
        public virtual DbSet<ScAuditlogReconcile> ScAuditlogReconciles { get; set; }
        public virtual DbSet<ScSalesOrganizationStructure> ScSalesOrganizationStructures { get; set; }
        public virtual DbSet<ScSalesTeamAssignment> ScSalesTeamAssignments { get; set; }
        public virtual DbSet<ScTerritoryLevel> ScTerritoryLevels { get; set; }
        public virtual DbSet<ScTerritoryMapping> ScTerritoryMappings { get; set; }
        public virtual DbSet<ScTerritoryStructure> ScTerritoryStructures { get; set; }
        public virtual DbSet<ScTerritoryStructureDetail> ScTerritoryStructureDetails { get; set; }
        public virtual DbSet<ScTerritoryStructureGeographicalMapping> ScTerritoryStructureGeographicalMappings { get; set; }
        public virtual DbSet<ScTerritoryValue> ScTerritoryValues { get; set; }
        public virtual DbSet<ScrefResult> ScrefResults { get; set; }
        public virtual DbSet<SdoResult> SdoResults { get; set; }
        public virtual DbSet<Sdoconfig> Sdoconfigs { get; set; }
        public virtual DbSet<SdoconfigSalesOrder> SdoconfigSalesOrders { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceDetail> ServiceDetails { get; set; }
        public virtual DbSet<ShiptoContact> ShiptoContacts { get; set; }
        public virtual DbSet<ShiptoHistorical> ShiptoHistoricals { get; set; }
        public virtual DbSet<SivRefResult> SivRefResults { get; set; }
        public virtual DbSet<SkurefResult> SkurefResults { get; set; }
        public virtual DbSet<SoFirstTimeCustomer> SoFirstTimeCustomers { get; set; }
        public virtual DbSet<SoOrderInformation> SoOrderInformations { get; set; }
        public virtual DbSet<SoOrderItem> SoOrderItems { get; set; }
        public virtual DbSet<SoReason> SoReasons { get; set; }
        public virtual DbSet<SoSumPickingListDetail> SoSumPickingListDetails { get; set; }
        public virtual DbSet<SoSumPickingListHeader> SoSumPickingListHeaders { get; set; }
        public virtual DbSet<SovRefResult> SovRefResults { get; set; }
        public virtual DbSet<Standard> Standards { get; set; }
        public virtual DbSet<StandardItem> StandardItems { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<SystemLog> SystemLogs { get; set; }
        public virtual DbSet<SystemSetting> SystemSettings { get; set; }
        public virtual DbSet<TempBaselineDataBusinessModel> TempBaselineDataBusinessModels { get; set; }
        public virtual DbSet<TempBaselineDataConditionExclude> TempBaselineDataConditionExcludes { get; set; }
        public virtual DbSet<TempBaselineDataOrder> TempBaselineDataOrders { get; set; }
        public virtual DbSet<TempBaselineDataOrderDetail> TempBaselineDataOrderDetails { get; set; }
        public virtual DbSet<TempBaselineDataPurchaseOrder> TempBaselineDataPurchaseOrders { get; set; }
        public virtual DbSet<TempBaselineDataPurchaseOrderDetail> TempBaselineDataPurchaseOrderDetails { get; set; }
        public virtual DbSet<TempBaselineDataVisit> TempBaselineDataVisits { get; set; }
        public virtual DbSet<TempBaselineDataVisitStepResult> TempBaselineDataVisitStepResults { get; set; }
        public virtual DbSet<TempBaselineDetailRequestPo> TempBaselineDetailRequestPos { get; set; }
        public virtual DbSet<TempBaselineHeaderRequestPo> TempBaselineHeaderRequestPos { get; set; }
        public virtual DbSet<TempBeatPlan> TempBeatPlans { get; set; }
        public virtual DbSet<TempBeatPlanDetail> TempBeatPlanDetails { get; set; }
        public virtual DbSet<TempCheckInventoryVisit> TempCheckInventoryVisits { get; set; }
        public virtual DbSet<TempEvaluationPhotoVisit> TempEvaluationPhotoVisits { get; set; }
        public virtual DbSet<TempInventoryItemInfor> TempInventoryItemInfors { get; set; }
        public virtual DbSet<TempInvreport> TempInvreports { get; set; }
        public virtual DbSet<TempInvreportLot> TempInvreportLots { get; set; }
        public virtual DbSet<TempKpidistributor> TempKpidistributors { get; set; }
        public virtual DbSet<TempKpiemployee> TempKpiemployees { get; set; }
        public virtual DbSet<TempParameterWithSitype> TempParameterWithSitypes { get; set; }
        public virtual DbSet<TempPoKpi> TempPoKpis { get; set; }
        public virtual DbSet<TempProgram> TempPrograms { get; set; }
        public virtual DbSet<TempProgramCustomer> TempProgramCustomers { get; set; }
        public virtual DbSet<TempProgramCustomerDetailsItem> TempProgramCustomerDetailsItems { get; set; }
        public virtual DbSet<TempProgramCustomerItemsGroup> TempProgramCustomerItemsGroups { get; set; }
        public virtual DbSet<TempProgramCustomersDetail> TempProgramCustomersDetails { get; set; }
        public virtual DbSet<TempProgramDetailReward> TempProgramDetailRewards { get; set; }
        public virtual DbSet<TempProgramDetailsItemsGroup> TempProgramDetailsItemsGroups { get; set; }
        public virtual DbSet<TempProgramsDetail> TempProgramsDetails { get; set; }
        public virtual DbSet<TempPromotionOrderRefNumber> TempPromotionOrderRefNumbers { get; set; }
        public virtual DbSet<TempRoundingRule> TempRoundingRules { get; set; }
        public virtual DbSet<TempRouteZone> TempRouteZones { get; set; }
        public virtual DbSet<TempSalesIndicatorType> TempSalesIndicatorTypes { get; set; }
        public virtual DbSet<TempTpOrderDetail> TempTpOrderDetails { get; set; }
        public virtual DbSet<TempTpOrderHeader> TempTpOrderHeaders { get; set; }
        public virtual DbSet<TempVisitStep> TempVisitSteps { get; set; }
        public virtual DbSet<TempVisitStepsDefaultResult> TempVisitStepsDefaultResults { get; set; }
        public virtual DbSet<TempVisitStepsReasonResult> TempVisitStepsReasonResults { get; set; }
        public virtual DbSet<TemporarySic> TemporarySics { get; set; }
        public virtual DbSet<TemporarySicItemGroupDetail> TemporarySicItemGroupDetails { get; set; }
        public virtual DbSet<TemporarySicKitDetail> TemporarySicKitDetails { get; set; }
        public virtual DbSet<TerritoryMapping> TerritoryMappings { get; set; }
        public virtual DbSet<TerritoryStructure> TerritoryStructures { get; set; }
        public virtual DbSet<TerritoryStructureDetail> TerritoryStructureDetails { get; set; }
        public virtual DbSet<TerritoryValue> TerritoryValues { get; set; }
        public virtual DbSet<TpBudget> TpBudgets { get; set; }
        public virtual DbSet<TpBudgetAdjustment> TpBudgetAdjustments { get; set; }
        public virtual DbSet<TpBudgetAllotment> TpBudgetAllotments { get; set; }
        public virtual DbSet<TpBudgetAllotmentAdjustment> TpBudgetAllotmentAdjustments { get; set; }
        public virtual DbSet<TpBudgetDefine> TpBudgetDefines { get; set; }
        public virtual DbSet<TpDiscount> TpDiscounts { get; set; }
        public virtual DbSet<TpDiscountStructure> TpDiscountStructures { get; set; }
        public virtual DbSet<TpDiscountStructureDetail> TpDiscountStructureDetails { get; set; }
        public virtual DbSet<TpObjectDiscount> TpObjectDiscounts { get; set; }
        public virtual DbSet<TpObjectDiscountDetail> TpObjectDiscountDetails { get; set; }
        public virtual DbSet<TpObjectSalesAttributeDiscount> TpObjectSalesAttributeDiscounts { get; set; }
        public virtual DbSet<TpPromotion> TpPromotions { get; set; }
        public virtual DbSet<TpPromotionDefinitionProductForGift> TpPromotionDefinitionProductForGifts { get; set; }
        public virtual DbSet<TpPromotionDefinitionProductForSale> TpPromotionDefinitionProductForSales { get; set; }
        public virtual DbSet<TpPromotionDefinitionStructure> TpPromotionDefinitionStructures { get; set; }
        public virtual DbSet<TpPromotionObjectCustomerAttributeLevel> TpPromotionObjectCustomerAttributeLevels { get; set; }
        public virtual DbSet<TpPromotionObjectCustomerAttributeValue> TpPromotionObjectCustomerAttributeValues { get; set; }
        public virtual DbSet<TpPromotionObjectCustomerShipto> TpPromotionObjectCustomerShiptos { get; set; }
        public virtual DbSet<TpPromotionScopeDsa> TpPromotionScopeDsas { get; set; }
        public virtual DbSet<TpPromotionScopeTerritory> TpPromotionScopeTerritorys { get; set; }
        public virtual DbSet<TpScopeDiscount> TpScopeDiscounts { get; set; }
        public virtual DbSet<TpScopeDiscountDetail> TpScopeDiscountDetails { get; set; }
        public virtual DbSet<TpSettlement> TpSettlements { get; set; }
        public virtual DbSet<TpSettlementDetail> TpSettlementDetails { get; set; }
        public virtual DbSet<TpSettlementObject> TpSettlementObjects { get; set; }
        public virtual DbSet<TradePromotion> TradePromotions { get; set; }
        public virtual DbSet<Uom> Uoms { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserLoginLog> UserLoginLogs { get; set; }
        public virtual DbSet<UserPolicy> UserPolicies { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<Vat> Vats { get; set; }
        public virtual DbSet<Version> Versions { get; set; }
        public virtual DbSet<VisitStep> VisitSteps { get; set; }
        public virtual DbSet<VpoRefResult> VpoRefResults { get; set; }
        public virtual DbSet<VvrefResult> VvrefResults { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<WebNotiMessagese> WebNotiMessageses { get; set; }
        public virtual DbSet<WinzardFeature> WinzardFeatures { get; set; }
        public virtual DbSet<WinzardSetting> WinzardSettings { get; set; }

        #region MKT Display
        public virtual DbSet<DisDisplay> DisDisplays { get; set; }
        public virtual DbSet<DisScopeDsa> DisScopeDsas { get; set; }
        public virtual DbSet<DisScopeTerritory> DisScopeTerritorys { get; set; }
        public virtual DbSet<DisCustomerAttributeLevel> DisCustomerAttributeLevels { get; set; }
        public virtual DbSet<DisCustomerAttributeValue> DisCustomerAttributeValues { get; set; }
        public virtual DbSet<DisDefinitionStructure> DisDefinitionStructures { get; set; }
        public virtual DbSet<DisDefinitionProductTypeDetail> DisDefinitionProductTypeDetails { get; set; }

        public virtual DbSet<DisBudget> DisBudgets { get; set; }
        public virtual DbSet<DisBudgetForScopeTerritory> DisBudgetForScopeTerritorys { get; set; }
        public virtual DbSet<DisBudgetForScopeDsa> DisBudgetForScopeDsas { get; set; }
        public virtual DbSet<DisBudgetForCusAttribute> DisBudgetForCusAttributes { get; set; }
        
        public virtual DbSet<DisCustomerShipto> DisCustomerShiptos { get; set; }
        public virtual DbSet<DisCustomerShiptoDetail> DisCustomerShiptoDetails { get; set; }

        public virtual DbSet<DisConfirmResult> DisConfirmResults { get; set; }
        public virtual DbSet<DisConfirmResultDetail> DisConfirmResultDetails { get; set; }
        public virtual DbSet<DisCriteriaEvaluatePictureDisplay> DisCriteriaEvaluatePictureDisplays { get; set; }
        public virtual DbSet<DisDefinitionCriteriaEvaluate> DisDefinitionCriteriaEvaluates { get; set; }
        public virtual DbSet<DisWeightGetExtraRewardsDetail> DisWeightGetExtraRewardsDetails { get; set; }
        public virtual DbSet<DisDefinitionGuideImage> DisDefinitionGuideImages { get; set; }
        public virtual DbSet<DisApproveRegistrationCustomer> DisApproveRegistrationCustomers { get; set; }
        public virtual DbSet<DisApproveRegistrationCustomerDetail> DisApproveRegistrationCustomerDetails { get; set; }
        public virtual DbSet<DisPayReward> DisPayRewards { get; set; }
        public virtual DbSet<DisPayRewardDetail> DisPayRewardDetails { get; set; }
        public virtual DbSet<DisSettlement> DisSettlements { get; set; }
        public virtual DbSet<DisSettlementDetail> DisSettlementDetails { get; set; }
        #endregion

        #region Temp Table For MKT Display
        public virtual DbSet<TempDisApproveRegistrationCustomer> Temp_DisApproveRegistrationCustomers { get; set; }
        public virtual DbSet<TempDisConfirmResultDetail> TempDisConfirmResultDetails { get; set; }
        public virtual DbSet<Temp_DisDisplaySupportTool> TempDisDisplaySupportTools { get; set; }
        public virtual DbSet<TempDisCustomerShiptoSaleOrQuantity> TempDisCustomerShiptoSaleOrQuantitys { get; set; }
        public virtual DbSet<TempDisCustomerShiptoNotHave> TempDisCustomerShiptoNotHaves { get; set; }
        public virtual DbSet<TempDisPosmForCustomerShipto> TempDisPosmForCustomerShiptos { get; set; }
        public virtual DbSet<TempDisOrderHeader> TempDisOrderHeaders { get; set; }
        public virtual DbSet<TempDisOrderDetail> TempDisOrderDetails { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp")
                .HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Action>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<AdjustItemGroupPrice>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdjustPricesCode).HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ItemGroupCode).HasMaxLength(255);

                entity.Property(e => e.PriceType).HasMaxLength(25);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.UoMitemGroup)
                    .HasMaxLength(255)
                    .HasColumnName("UoMItemGroup");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<AdjustPriceListUoMitemGroup>(entity =>
            {
                entity.ToTable("AdjustPriceListUoMItemGroups");

                entity.HasIndex(e => e.AdjustItemGroupPriceId, "IX_AdjustPriceListUoMItemGroups_AdjustItemGroupPriceId");

                entity.HasIndex(e => e.PriceListId, "IX_AdjustPriceListUoMItemGroups_PriceListId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.ItemGroupCode).HasMaxLength(255);

                entity.Property(e => e.ItemGroupDescription).HasMaxLength(255);

                entity.Property(e => e.UoMitemGroup)
                    .HasMaxLength(255)
                    .HasColumnName("UoMItemGroup");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.AdjustItemGroupPrice)
                    .WithMany(p => p.AdjustPriceListUoMitemGroups)
                    .HasForeignKey(d => d.AdjustItemGroupPriceId)
                    .HasConstraintName("FK_AdjustPriceListUoMItemGroups_AdjustItemGroupPrices_AdjustIt~");

                entity.HasOne(d => d.PriceList)
                    .WithMany(p => p.AdjustPriceListUoMitemGroups)
                    .HasForeignKey(d => d.PriceListId);
            });

            modelBuilder.Entity<ApiMapping>(entity =>
            {
                entity.ToTable("ApiMapping");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AndroidPackageName).HasMaxLength(100);

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.IosbundleId)
                    .HasMaxLength(100)
                    .HasColumnName("IOSBundleId");

                entity.Property(e => e.SecretId).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ApplicationInviteCode>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.AcitveLink).HasMaxLength(255);

                entity.Property(e => e.AppName).HasMaxLength(20);

                entity.Property(e => e.EmployeeCode).HasMaxLength(10);

                entity.Property(e => e.EmployeeName).HasMaxLength(50);

                entity.Property(e => e.InviteCode).HasMaxLength(10);

                entity.Property(e => e.MessageDetail).HasMaxLength(1000);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(10);
            });

            modelBuilder.Entity<ApplicationLanguagePack>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AppVersion).HasMaxLength(20);

                entity.Property(e => e.ExcelFileName).HasMaxLength(255);

                entity.Property(e => e.FileVersion).HasMaxLength(20);

                entity.Property(e => e.IosversionId).HasColumnName("IOsversionId");

                entity.Property(e => e.JsonFileName).HasMaxLength(255);

                entity.Property(e => e.LanguageCode).HasMaxLength(20);

                entity.Property(e => e.LanguageName).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(10);
            });

            modelBuilder.Entity<ApplicationLocalization>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasPrecision(6);

                entity.Property(e => e.DeletedDate).HasPrecision(6);

                entity.Property(e => e.ExcelFileName).HasMaxLength(255);

                entity.Property(e => e.FileName).HasMaxLength(50);

                entity.Property(e => e.FileVersion).HasMaxLength(20);

                entity.Property(e => e.JsonFileName).HasMaxLength(255);

                entity.Property(e => e.JsonFileRepoId).HasColumnType("character varying");

                entity.Property(e => e.UpdatedDate).HasPrecision(6);
            });

            modelBuilder.Entity<ApplicationLocalizationHistory>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ExcelFileName).HasMaxLength(255);

                entity.Property(e => e.FileName).HasMaxLength(50);

                entity.Property(e => e.FileVersion).HasMaxLength(20);

                entity.Property(e => e.JsonFileName).HasMaxLength(255);
            });

            modelBuilder.Entity<ApplicationNotiDeviceToken>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.AppName).HasMaxLength(20);

                entity.Property(e => e.DeviceToken).HasMaxLength(255);

                entity.Property(e => e.Os)
                    .HasMaxLength(10)
                    .HasColumnName("OS");
            });

            modelBuilder.Entity<ApplicationNotiMessage>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.DataId).HasMaxLength(100);

                entity.Property(e => e.DataJson).HasMaxLength(4000);

                entity.Property(e => e.DeliveryStatus).HasMaxLength(20);

                entity.Property(e => e.Purpose).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.UrgentNotiId).HasDefaultValueSql("'00000000-0000-0000-0000-000000000000'::uuid");
            });

            modelBuilder.Entity<ApplicationNotiUrgent>(entity =>
            {
                entity.ToTable("ApplicationNotiUrgent");

                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Action).HasMaxLength(50);

                entity.Property(e => e.DataId).HasMaxLength(100);

                entity.Property(e => e.Priority).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.SyncCode).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<ApplicationOtpcode>(entity =>
            {
                entity.ToTable("ApplicationOTPCodes");

                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.AppName).HasMaxLength(20);

                entity.Property(e => e.Otpcode)
                    .HasMaxLength(8)
                    .HasColumnName("OTPCode");

                entity.Property(e => e.Purpose).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(10);
            });

            modelBuilder.Entity<ApplicationService>(entity =>
            {
                entity.HasIndex(e => e.ApplicationId, "IX_ApplicationServices_ApplicationId");

                entity.HasIndex(e => e.EcoServiceId, "IX_ApplicationServices_EcoServiceId");

                entity.HasIndex(e => e.EcoVersionId, "IX_ApplicationServices_EcoVersionId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationServices)
                    .HasForeignKey(d => d.ApplicationId);

                entity.HasOne(d => d.EcoService)
                    .WithMany(p => p.ApplicationServices)
                    .HasForeignKey(d => d.EcoServiceId);

                entity.HasOne(d => d.EcoVersion)
                    .WithMany(p => p.ApplicationServices)
                    .HasForeignKey(d => d.EcoVersionId);
            });

            modelBuilder.Entity<ApplicationThemesConfigure>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ComponentName).HasMaxLength(50);

                entity.Property(e => e.ElementName).HasMaxLength(50);

                entity.Property(e => e.ElementType).HasMaxLength(50);

                entity.Property(e => e.ElementValue).HasMaxLength(255);
            });

            modelBuilder.Entity<ApplicationThemesElement>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ComponentName).HasMaxLength(50);

                entity.Property(e => e.DefaultValue).HasMaxLength(255);

                entity.Property(e => e.ElementName).HasMaxLength(50);

                entity.Property(e => e.ElementType).HasMaxLength(50);
            });

            modelBuilder.Entity<ApplicationThemesSetting>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(10);
            });

            modelBuilder.Entity<ApplicationThemesSuggestion>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(10);
            });

            modelBuilder.Entity<ApplicationUserMapping>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ApplicationVersion>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AppBuildFile).HasMaxLength(255);

                entity.Property(e => e.AppName).HasMaxLength(20);

                entity.Property(e => e.AppVersion).HasMaxLength(10);

                entity.Property(e => e.HostType).HasMaxLength(10);

                entity.Property(e => e.Os)
                    .HasMaxLength(10)
                    .HasColumnName("OS");

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.Property(e => e.Type).HasMaxLength(10);
            });

            modelBuilder.Entity<ApplicationVersionPrinciple>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AppVersion).HasMaxLength(10);

                entity.Property(e => e.CreatedDate).HasPrecision(6);

                entity.Property(e => e.DeletedDate).HasPrecision(6);

                entity.Property(e => e.PrincipleCode).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasPrecision(6);
            });

            modelBuilder.Entity<AsoRefResult>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.PcinvoiceValue).HasColumnName("PCInvoiceValue");

                entity.Property(e => e.PcnoofSku).HasColumnName("PCNoofSKU");

                entity.Property(e => e.Sicode).HasColumnName("SICode");

                entity.Property(e => e.Sitype).HasColumnName("SIType");

                entity.Property(e => e.Sku).HasColumnName("SKU");
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.FeatureCode).HasMaxLength(100);

                entity.Property(e => e.Fields).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<AutoKpisTarget>(entity =>
            {
                entity.ToTable("AutoKPIsTargets");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AutoKpisTargetCode).HasColumnName("AutoKPIsTargetCode");
            });

            modelBuilder.Entity<AutoKpisTargetAchievementsCurrentYear>(entity =>
            {
                entity.ToTable("AutoKPIsTargetAchievementsCurrentYears");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Aso).HasColumnName("ASO");

                entity.Property(e => e.AutoKpisTargetCode).HasColumnName("AutoKPIsTargetCode");

                entity.Property(e => e.Lppc).HasColumnName("LPPC");

                entity.Property(e => e.Lppcvalue).HasColumnName("LPPCValue");

                entity.Property(e => e.Pc).HasColumnName("PC");

                entity.Property(e => e.Vpo).HasColumnName("VPO");
            });

            modelBuilder.Entity<AutoKpisTargetAchievementsCurrentYearValue>(entity =>
            {
                entity.ToTable("AutoKPIsTargetAchievementsCurrentYearValues");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Aso).HasColumnName("ASO");

                entity.Property(e => e.AutoKpisTargetCode).HasColumnName("AutoKPIsTargetCode");

                entity.Property(e => e.Lppc).HasColumnName("LPPC");

                entity.Property(e => e.Lppcvalue).HasColumnName("LPPCValue");

                entity.Property(e => e.Pc).HasColumnName("PC");

                entity.Property(e => e.Vpo).HasColumnName("VPO");
            });

            modelBuilder.Entity<AutoKpisTargetBusinessModel>(entity =>
            {
                entity.ToTable("AutoKPIsTargetBusinessModels");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AutoKpisTargetCode).HasColumnName("AutoKPIsTargetCode");

                entity.Property(e => e.BaseAso).HasColumnName("BaseASO");

                entity.Property(e => e.BaseLppc).HasColumnName("BaseLPPC");

                entity.Property(e => e.BaseLppcvalue).HasColumnName("BaseLPPCValue");

                entity.Property(e => e.BasePc).HasColumnName("BasePC");

                entity.Property(e => e.BaseVpo).HasColumnName("BaseVPO");

                entity.Property(e => e.NewTargetAso).HasColumnName("NewTargetASO");

                entity.Property(e => e.NewTargetLppc).HasColumnName("NewTargetLPPC");

                entity.Property(e => e.NewTargetLppcvalue).HasColumnName("NewTargetLPPCValue");

                entity.Property(e => e.NewTargetPc).HasColumnName("NewTargetPC");

                entity.Property(e => e.NewTargetVpo).HasColumnName("NewTargetVPO");

                entity.Property(e => e.TargetAso).HasColumnName("TargetASO");

                entity.Property(e => e.TargetLppc).HasColumnName("TargetLPPC");

                entity.Property(e => e.TargetLppcvalue).HasColumnName("TargetLPPCValue");

                entity.Property(e => e.TargetPc).HasColumnName("TargetPC");

                entity.Property(e => e.TargetVpo).HasColumnName("TargetVPO");
            });

            modelBuilder.Entity<AutoKpisTargetBusinessModelValue>(entity =>
            {
                entity.ToTable("AutoKPIsTargetBusinessModelValues");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AutoKpisTargetCode).HasColumnName("AutoKPIsTargetCode");

                entity.Property(e => e.BaseAso).HasColumnName("BaseASO");

                entity.Property(e => e.BaseLppc).HasColumnName("BaseLPPC");

                entity.Property(e => e.BaseLppcvalue).HasColumnName("BaseLPPCValue");

                entity.Property(e => e.BasePc).HasColumnName("BasePC");

                entity.Property(e => e.BaseVpo).HasColumnName("BaseVPO");

                entity.Property(e => e.Level).HasDefaultValueSql("0");

                entity.Property(e => e.NewTargetAso).HasColumnName("NewTargetASO");

                entity.Property(e => e.NewTargetLppc).HasColumnName("NewTargetLPPC");

                entity.Property(e => e.NewTargetLppcvalue).HasColumnName("NewTargetLPPCValue");

                entity.Property(e => e.NewTargetPc).HasColumnName("NewTargetPC");

                entity.Property(e => e.NewTargetVpo).HasColumnName("NewTargetVPO");

                entity.Property(e => e.TargetAso).HasColumnName("TargetASO");

                entity.Property(e => e.TargetLppc).HasColumnName("TargetLPPC");

                entity.Property(e => e.TargetLppcvalue).HasColumnName("TargetLPPCValue");

                entity.Property(e => e.TargetPc).HasColumnName("TargetPC");

                entity.Property(e => e.TargetVpo).HasColumnName("TargetVPO");

                entity.Property(e => e.Year).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<AutoKpisTargetContributionBySic>(entity =>
            {
                entity.ToTable("AutoKPIsTargetContributionBySICs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AutoKpisTargetCode).HasColumnName("AutoKPIsTargetCode");

                entity.Property(e => e.Sic).HasColumnName("SIC");
            });

            modelBuilder.Entity<AutoKpisTargetContributionBySicValue>(entity =>
            {
                entity.ToTable("AutoKPIsTargetContributionBySicValues");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AutoKpisTargetCode).HasColumnName("AutoKPIsTargetCode");

                entity.Property(e => e.Sic).HasColumnName("SIC");
            });

            modelBuilder.Entity<AutoKpisTargetDevelopment>(entity =>
            {
                entity.ToTable("AutoKPIsTargetDevelopments");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AutoKpisTargetCode).HasColumnName("AutoKPIsTargetCode");
            });

            modelBuilder.Entity<AutoKpisTargetDevelopmentDetail>(entity =>
            {
                entity.ToTable("AutoKPIsTargetDevelopmentDetails");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AutoKpisTargetDevelopmentId).HasColumnName("AutoKPIsTargetDevelopmentId");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CityCode).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.ShortName).HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<CleanDataConfigure>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DeletedBy).HasMaxLength(255);

                entity.Property(e => e.Operation).HasMaxLength(50);

                entity.Property(e => e.TableName).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<Competitor>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CompetitorCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ConditionstoExcludeSc>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(80);

                entity.Property(e => e.Hobbies).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(11);
            });

            modelBuilder.Entity<Container>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.ContainerName).HasMaxLength(256);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DeletedBy).HasMaxLength(256);

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(e => e.Key).IsRequired();

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ContractType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CountryCode).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.ShortName).HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<CronJobSchedule>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<CustomerAdjustment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdjustmentId).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<CustomerAdjustmentDataType>(entity =>
            {
                entity.ToTable("CustomerAdjustmentDataType");

                entity.HasIndex(e => e.CustomerAdjustmentId, "IX_CustomerAdjustmentDataType_CustomerAdjustmentId");

                entity.HasIndex(e => e.DataTypeDefinitionId, "IX_CustomerAdjustmentDataType_DataTypeDefinitionId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.CustomerAdjustment)
                    .WithMany(p => p.CustomerAdjustmentDataTypes)
                    .HasForeignKey(d => d.CustomerAdjustmentId)
                    .HasConstraintName("FK_CustomerAdjustmentDataType_CustomerAdjustments_CustomerAdju~");

                entity.HasOne(d => d.DataTypeDefinition)
                    .WithMany(p => p.CustomerAdjustmentDataTypes)
                    .HasForeignKey(d => d.DataTypeDefinitionId)
                    .HasConstraintName("FK_CustomerAdjustmentDataType_DataTypeDefinition_DataTypeDefin~");
            });

            modelBuilder.Entity<CustomerAdjustmentShipto>(entity =>
            {
                entity.ToTable("CustomerAdjustmentShipto");

                entity.HasIndex(e => e.CustomerAdjustmentId, "IX_CustomerAdjustmentShipto_CustomerAdjustmentId");

                entity.HasIndex(e => e.CustomerShiptoId, "IX_CustomerAdjustmentShipto_CustomerShiptoId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.SaleMan).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.CustomerAdjustment)
                    .WithMany(p => p.CustomerAdjustmentShiptos)
                    .HasForeignKey(d => d.CustomerAdjustmentId)
                    .HasConstraintName("FK_CustomerAdjustmentShipto_CustomerAdjustments_CustomerAdjust~");

                entity.HasOne(d => d.CustomerShipto)
                    .WithMany(p => p.CustomerAdjustmentShiptos)
                    .HasForeignKey(d => d.CustomerShiptoId);
            });

            modelBuilder.Entity<CustomerApplyToValue>(entity =>
            {
                entity.HasIndex(e => e.CustomerAdjustmentId, "IX_CustomerApplyToValues_CustomerAdjustmentId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.CustomerAdjustment)
                    .WithMany(p => p.CustomerApplyToValues)
                    .HasForeignKey(d => d.CustomerAdjustmentId)
                    .HasConstraintName("FK_CustomerApplyToValues_CustomerAdjustments_CustomerAdjustmen~");
            });

            modelBuilder.Entity<CustomerAttribute>(entity =>
            {
                entity.HasIndex(e => e.CustomerSettingId, "IX_CustomerAttributes_CustomerSettingId");

                entity.HasIndex(e => e.ParentCustomerAttributeId, "IX_CustomerAttributes_ParentCustomerAttributeId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AttributeMaster)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.CustomerSetting)
                    .WithMany(p => p.CustomerAttributes)
                    .HasForeignKey(d => d.CustomerSettingId);

                entity.HasOne(d => d.ParentCustomerAttribute)
                    .WithMany(p => p.InverseParentCustomerAttribute)
                    .HasForeignKey(d => d.ParentCustomerAttributeId)
                    .HasConstraintName("FK_CustomerAttributes_CustomerAttributes_ParentCustomerAttribu~");
            });

            modelBuilder.Entity<CustomerContact>(entity =>
            {
                entity.HasIndex(e => e.CustomerInfomationId, "IX_CustomerContacts_CustomerInfomationId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ContactCode).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DisplayName).HasMaxLength(300);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(300);

                entity.Property(e => e.Hobbies).HasMaxLength(500);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.Note).HasMaxLength(1000);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.CustomerInfomation)
                    .WithMany(p => p.CustomerContacts)
                    .HasForeignKey(d => d.CustomerInfomationId);
            });

            modelBuilder.Entity<CustomerContactEmail>(entity =>
            {
                entity.HasIndex(e => e.CustomerContactId, "IX_CustomerContactEmails_CustomerContactId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Email).HasMaxLength(80);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.CustomerContact)
                    .WithMany(p => p.CustomerContactEmails)
                    .HasForeignKey(d => d.CustomerContactId);
            });

            modelBuilder.Entity<CustomerContactPhone>(entity =>
            {
                entity.HasIndex(e => e.CustomerContactId, "IX_CustomerContactPhones_CustomerContactId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.PhoneNumber).HasMaxLength(11);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.CustomerContact)
                    .WithMany(p => p.CustomerContactPhones)
                    .HasForeignKey(d => d.CustomerContactId);
            });

            modelBuilder.Entity<CustomerContract>(entity =>
            {
                entity.HasIndex(e => e.CustomerId, "IX_CustomerContracts_CustomerId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ContractId).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerContracts)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<CustomerDmsAttribute>(entity =>
            {
                entity.ToTable("CustomerDmsAttribute");

                entity.HasIndex(e => e.CustomerShiptoId, "IX_CustomerDmsAttribute_CustomerShiptoId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.CustomerAttribute)
                    .WithMany(p => p.CustomerDmsAttributes)
                    .HasForeignKey(d => d.CustomerAttributeId);

                entity.HasOne(d => d.CustomerShipto)
                    .WithMany(p => p.CustomerDmsAttributes)
                    .HasForeignKey(d => d.CustomerShiptoId);
            });

            modelBuilder.Entity<CustomerHierarchy>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.NodeId).ValueGeneratedOnAdd();

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<CustomerHierarchyMapping>(entity =>
            {
                entity.HasIndex(e => e.CustomerAttributeId, "IX_CustomerHierarchyMappings_CustomerAttributeId");

                entity.HasIndex(e => e.CustomerHierarchyId, "IX_CustomerHierarchyMappings_CustomerHierarchyId");

                entity.HasIndex(e => e.CustomerSettingHierarchyId, "IX_CustomerHierarchyMappings_CustomerSettingHierarchyId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.CustomerAttribute)
                    .WithMany(p => p.CustomerHierarchyMappings)
                    .HasForeignKey(d => d.CustomerAttributeId)
                    .HasConstraintName("FK_CustomerHierarchyMappings_CustomerAttributes_CustomerAttrib~");

                entity.HasOne(d => d.CustomerHierarchy)
                    .WithMany(p => p.CustomerHierarchyMappings)
                    .HasForeignKey(d => d.CustomerHierarchyId)
                    .HasConstraintName("FK_CustomerHierarchyMappings_CustomerHierarchies_CustomerHiera~");

                entity.HasOne(d => d.CustomerSettingHierarchy)
                    .WithMany(p => p.CustomerHierarchyMappings)
                    .HasForeignKey(d => d.CustomerSettingHierarchyId)
                    .HasConstraintName("FK_CustomerHierarchyMappings_CustomerSettingHierarchies_Custom~");
            });

            modelBuilder.Entity<CustomerInformation>(entity =>
            {
                entity.HasIndex(e => e.CustomerCode, "IX_CustomerInformations_CustomerCode")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BankAccount).HasMaxLength(80);

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.BankNumber).HasMaxLength(20);

                entity.Property(e => e.BusinessAddress).HasMaxLength(255);

                entity.Property(e => e.CodeAtDistributor).HasMaxLength(10);

                entity.Property(e => e.CodeAtVendor).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CustomerCode).HasMaxLength(11);

                entity.Property(e => e.ErpCode).HasMaxLength(10);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(300);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.LegalInformation).HasMaxLength(255);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(11);

                entity.Property(e => e.ShortName).HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<CustomerSetting>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AttributeId)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("AttributeID");

                entity.Property(e => e.AttributeName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<CustomerSettingHierarchy>(entity =>
            {
                entity.HasIndex(e => e.CustomerSettingId, "IX_CustomerSettingHierarchies_CustomerSettingId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.CustomerSetting)
                    .WithMany(p => p.CustomerSettingHierarchies)
                    .HasForeignKey(d => d.CustomerSettingId)
                    .HasConstraintName("FK_CustomerSettingHierarchies_CustomerSettings_CustomerSetting~");
            });

            modelBuilder.Entity<CustomerShipto>(entity =>
            {
                entity.HasIndex(e => e.CustomerInfomationId, "IX_CustomerShiptos_CustomerInfomationId");

                entity.HasIndex(e => new { e.ShiptoCode, e.CustomerInfomationId }, "IX_CustomerShiptos_ShiptoCode_CustomerInfomationId")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.ShiptoCode).HasMaxLength(4);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.CustomerInfomation)
                    .WithMany(p => p.CustomerShiptos)
                    .HasForeignKey(d => d.CustomerInfomationId);
            });

            modelBuilder.Entity<CustomerShiptoContact>(entity =>
            {
                entity.HasIndex(e => e.CustomerContactId, "IX_CustomerShiptoContacts_CustomerContactId");

                entity.HasIndex(e => e.CustomerShiptoId, "IX_CustomerShiptoContacts_CustomerShiptoId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.CustomerContact)
                    .WithMany(p => p.CustomerShiptoContacts)
                    .HasForeignKey(d => d.CustomerContactId);

                entity.HasOne(d => d.CustomerShipto)
                    .WithMany(p => p.CustomerShiptoContacts)
                    .HasForeignKey(d => d.CustomerShiptoId);
            });

            modelBuilder.Entity<DataLog>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.FieldName).HasMaxLength(256);

                entity.Property(e => e.NewValue).HasMaxLength(256);

                entity.Property(e => e.ObjectId).HasMaxLength(36);

                entity.Property(e => e.ObjectName).HasMaxLength(150);

                entity.Property(e => e.OldValue).HasMaxLength(256);
            });

            modelBuilder.Entity<DataProtectionKey>(entity =>
            {
                entity.HasIndex(e => e.FriendlyName, "DataProtectionKeys_FriendlyName_key")
                    .IsUnique();

                entity.Property(e => e.FriendlyName).IsRequired();

                entity.Property(e => e.Xml).IsRequired();
            });

            modelBuilder.Entity<DataTypeDefinition>(entity =>
            {
                entity.ToTable("DataTypeDefinition");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<DistCache>(entity =>
            {
                entity.ToTable("DistCache");

                entity.Property(e => e.AbsoluteExpiration).HasColumnType("timestamp with time zone");

                entity.Property(e => e.ExpiresAtTime).HasColumnType("timestamp with time zone");
            });

            modelBuilder.Entity<Distributor>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AttentionEmailValue).HasMaxLength(80);

                entity.Property(e => e.AttentionFirstName).HasMaxLength(10);

                entity.Property(e => e.AttentionFullName).HasMaxLength(200);

                entity.Property(e => e.AttentionLastName).HasMaxLength(40);

                entity.Property(e => e.AttentionMiddleName).HasMaxLength(10);

                entity.Property(e => e.AttentionPhoneValue).HasMaxLength(11);

                entity.Property(e => e.BankAccount).HasMaxLength(80);

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.BankNumber).HasMaxLength(20);

                entity.Property(e => e.BusinessAddressDept).HasMaxLength(100);

                entity.Property(e => e.BusinessAddressStreet).HasMaxLength(1000);

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.Dmscode)
                    .HasMaxLength(10)
                    .HasColumnName("DMSCode");

                entity.Property(e => e.Email).HasMaxLength(80);

                entity.Property(e => e.Fax).HasMaxLength(12);

                entity.Property(e => e.FullName).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.NumberOfAccountingNp).HasColumnName("NumberOfAccountingNP");

                entity.Property(e => e.Phone).HasMaxLength(11);

                entity.Property(e => e.PrincipalLinkedCode).HasMaxLength(10);

                entity.Property(e => e.TaxCode).HasMaxLength(14);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.Property(e => e.ValidFrom).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");

                entity.Property(e => e.Website).HasMaxLength(80);
            });

            modelBuilder.Entity<DistributorContact>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ContactCode).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DepartmentNumber).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(80);

                entity.Property(e => e.FirstName).HasMaxLength(10);

                entity.Property(e => e.FullName).HasMaxLength(200);

                entity.Property(e => e.Hobbies).HasMaxLength(500);

                entity.Property(e => e.LastName).HasMaxLength(40);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(11);

                entity.Property(e => e.Street).HasMaxLength(1000);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.Distributor)
                    .WithMany(p => p.DistributorContacts)
                    .HasForeignKey(d => d.DistributorId);
            });

            modelBuilder.Entity<DistributorContract>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BilltoCode).HasMaxLength(10);

                entity.Property(e => e.ContractCode).HasMaxLength(10);

                entity.Property(e => e.ContractDescription).HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.Distributor)
                    .WithMany(p => p.DistributorContracts)
                    .HasForeignKey(d => d.DistributorId);
            });

            modelBuilder.Entity<DistributorHierarchy>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.NodeId).ValueGeneratedOnAdd();

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<DistributorHierarchyMapping>(entity =>
            {
                entity.HasIndex(e => e.CustomerAttributeId, "IX_DistributorHierarchyMappings_CustomerAttributeId");

                entity.HasIndex(e => e.CustomerSettingHierarchyId, "IX_DistributorHierarchyMappings_CustomerSettingHierarchyId");

                entity.HasIndex(e => e.DistributorHierarchyId, "IX_DistributorHierarchyMappings_DistributorHierarchyId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.CustomerAttribute)
                    .WithMany(p => p.DistributorHierarchyMappings)
                    .HasForeignKey(d => d.CustomerAttributeId)
                    .HasConstraintName("FK_DistributorHierarchyMappings_CustomerAttributes_CustomerAtt~");

                entity.HasOne(d => d.CustomerSettingHierarchy)
                    .WithMany(p => p.DistributorHierarchyMappings)
                    .HasForeignKey(d => d.CustomerSettingHierarchyId)
                    .HasConstraintName("FK_DistributorHierarchyMappings_CustomerSettingHierarchies_Cus~");

                entity.HasOne(d => d.DistributorHierarchy)
                    .WithMany(p => p.DistributorHierarchyMappings)
                    .HasForeignKey(d => d.DistributorHierarchyId)
                    .HasConstraintName("FK_DistributorHierarchyMappings_DistributorHierarchies_Distrib~");
            });

            modelBuilder.Entity<DistributorHistorical>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BilltoCodeonErp).HasColumnName("BilltoCodeonERP");

                entity.Property(e => e.EffectiveDate).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");

                entity.HasOne(d => d.Distributor)
                    .WithMany(p => p.DistributorHistoricals)
                    .HasForeignKey(d => d.DistributorId);

                entity.HasOne(d => d.DistributorShipto)
                    .WithMany(p => p.DistributorHistoricals)
                    .HasForeignKey(d => d.DistributorShiptoId)
                    .HasConstraintName("FK_DistributorHistoricals_DistributorShiptos_DistributorShipto~");
            });

            modelBuilder.Entity<DistributorPriceApplyToOutletAttribute>(entity =>
            {
                entity.HasIndex(e => e.DistributorPriceVolumeId, "IX_DistributorPriceApplyToOutletAttributes_DistributorPriceVol~");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.DistributorPriceVolume)
                    .WithMany(p => p.DistributorPriceApplyToOutletAttributes)
                    .HasForeignKey(d => d.DistributorPriceVolumeId)
                    .HasConstraintName("FK_DistributorPriceApplyToOutletAttributes_DistributorPriceVol~");
            });

            modelBuilder.Entity<DistributorPriceItemGroup>(entity =>
            {
                entity.HasIndex(e => e.DistributorPriceVolumeId, "IX_DistributorPriceItemGroups_DistributorPriceVolumeId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.DistributorPriceVolume)
                    .WithMany(p => p.DistributorPriceItemGroups)
                    .HasForeignKey(d => d.DistributorPriceVolumeId)
                    .HasConstraintName("FK_DistributorPriceItemGroups_DistributorPriceVolumes_Distribu~");
            });

            modelBuilder.Entity<DistributorPriceVolume>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<DistributorPriceVolumeLevel>(entity =>
            {
                entity.HasIndex(e => e.DistributorPriceVolumeId, "IX_DistributorPriceVolumeLevels_DistributorPriceVolumeId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.DistributorPriceVolume)
                    .WithMany(p => p.DistributorPriceVolumeLevels)
                    .HasForeignKey(d => d.DistributorPriceVolumeId)
                    .HasConstraintName("FK_DistributorPriceVolumeLevels_DistributorPriceVolumes_Distri~");
            });

            modelBuilder.Entity<DistributorSellingArea>(entity =>
            {
                entity.HasIndex(e => e.TerritoryMappingId, "IX_DistributorSellingAreas_TerritoryMappingId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.EffectDate).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.TerritoryMapping)
                    .WithMany(p => p.DistributorSellingAreas)
                    .HasForeignKey(d => d.TerritoryMappingId);
            });

            modelBuilder.Entity<DistributorShipto>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DepartmentNumber).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.ShiptoCode).HasMaxLength(4);

                entity.Property(e => e.ShiptoCodeOnErp)
                    .HasMaxLength(14)
                    .HasColumnName("ShiptoCodeOnERP");

                entity.Property(e => e.ShiptoName).HasMaxLength(80);

                entity.Property(e => e.Street).HasMaxLength(1000);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.Distributor)
                    .WithMany(p => p.DistributorShiptos)
                    .HasForeignKey(d => d.DistributorId);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.DistrictCode).HasMaxLength(10);

                entity.Property(e => e.ShortName).HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<DropSizeRefResult>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.RecognizedDateForPsov).HasColumnName("RecognizedDateForPSOV");

                entity.Property(e => e.Sicode).HasColumnName("SICode");

                entity.Property(e => e.Sitype).HasColumnName("SIType");

                entity.Property(e => e.Sku).HasColumnName("SKU");

                entity.Property(e => e.SovinvoiceValue).HasColumnName("SOVInvoiceValue");

                entity.Property(e => e.SovnoofSku).HasColumnName("SOVNoofSKU");
            });

            modelBuilder.Entity<DsaDelivery>(entity =>
            {
                entity.ToTable("DSA_Deliveries");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<DsaDistributorSellingArea>(entity =>
            {
                entity.ToTable("DSA_DistributorSellingAreas");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<DsaGeographicalMapping>(entity =>
            {
                entity.ToTable("DSA_GeographicalMapping");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<DsaSalesTeamAssignment>(entity =>
            {
                entity.ToTable("DSA_SalesTeamAssignments");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.IsSicbase).HasColumnName("IsSICBase");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<DsadistributorShipTo>(entity =>
            {
                entity.ToTable("DSADistributorShipTos");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<Dsageographical>(entity =>
            {
                entity.ToTable("DSAGeographicals");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<DynamicFieldConfigure>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DeletedBy).HasMaxLength(256);

                entity.Property(e => e.FieldType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<EcoLocalization>(entity =>
            {
                entity.ToTable("EcoLocalization");

                entity.Property(e => e.Comment).HasMaxLength(512);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.FileName).HasMaxLength(128);

                entity.Property(e => e.LocaleId).HasMaxLength(10);

                entity.Property(e => e.PrincipalCode).HasMaxLength(5);

                entity.Property(e => e.ResourceId).HasMaxLength(1024);

                entity.Property(e => e.ResourceSet).HasMaxLength(512);

                entity.Property(e => e.Type).HasMaxLength(512);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<EmailType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.IsRdos).HasColumnName("IsRDOS");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Path).HasMaxLength(256);

                entity.Property(e => e.ServiceUrl).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<GeographicalMapping>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<GeographicalMaster>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(4);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(80);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<GeographicalStructure>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<InvAdjustmentDetail>(entity =>
            {
                entity.ToTable("INV_AdjustmentDetails");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");
            });

            modelBuilder.Entity<InvAdjustmentHeader>(entity =>
            {
                entity.ToTable("INV_AdjustmentHeaders");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<InvAllocationDetail>(entity =>
            {
                entity.ToTable("INV_AllocationDetails");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");
            });

            modelBuilder.Entity<InvInventoryTransaction>(entity =>
            {
                entity.ToTable("INV_InventoryTransactions");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<InvLotAvailable>(entity =>
            {
                entity.ToTable("INV_LotAvailables");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<InvReason>(entity =>
            {
                entity.ToTable("INV_Reasons");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<InvSellInLotByDate>(entity =>
            {
                entity.ToTable("INV_SellInLotByDates");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<InvSellOutLotByDate>(entity =>
            {
                entity.ToTable("INV_SellOutLotByDates");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<InvWhTransferDetail>(entity =>
            {
                entity.ToTable("INV_WhTransferDetails");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");
            });

            modelBuilder.Entity<InvWhTransferHeader>(entity =>
            {
                entity.ToTable("INV_WhTransferHeaders");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<InvWhTransferToEmployeeHeader>(entity =>
            {
                entity.ToTable("INV_WhTransferToEmployeeHeaders");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Emplocation).HasColumnName("EMPLocation");
            });

            modelBuilder.Entity<InventoryItem>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Avatar).HasMaxLength(25);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.DistribiutorCode).HasMaxLength(10);

                entity.Property(e => e.Erpcode)
                    .HasMaxLength(10)
                    .HasColumnName("ERPCode");

                entity.Property(e => e.GroupId).HasMaxLength(20);

                entity.Property(e => e.InventoryItemId)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ItemType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Lsnumber).HasColumnName("LSNumber");

                entity.Property(e => e.Note).HasMaxLength(1000);

                entity.Property(e => e.ReportName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Status).IsRequired();

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<InventoryKit>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.InventoryItemId)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.KitDescription)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.NonStockItem)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.StockItem)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Uom)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<InventoryResult>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");
            });

            modelBuilder.Entity<ItemAttribute>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.ItemAttributeCode)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.ItemAttributeMaster)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ItemGroup>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Avatar).HasMaxLength(25);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ItemHierarchyMapping>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.HierarchyAttribute1)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.HierarchyAttribute10).HasMaxLength(4);

                entity.Property(e => e.HierarchyAttribute2)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.HierarchyAttribute3)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.HierarchyAttribute4).HasMaxLength(4);

                entity.Property(e => e.HierarchyAttribute5).HasMaxLength(4);

                entity.Property(e => e.HierarchyAttribute6).HasMaxLength(4);

                entity.Property(e => e.HierarchyAttribute7).HasMaxLength(4);

                entity.Property(e => e.HierarchyAttribute8).HasMaxLength(4);

                entity.Property(e => e.HierarchyAttribute9).HasMaxLength(4);

                entity.Property(e => e.NodeId).ValueGeneratedOnAdd();

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ItemHierarchyMappingCompetitor>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ItemManufacture>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ManufactureId).HasColumnName("ManufactureID");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ItemSetting>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AttributeId)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.AttributeName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ItemsCompetitor>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CompetitorId).HasColumnName("CompetitorID");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ItemsFile>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.FileId)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("FileID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ItemsUomconversion>(entity =>
            {
                entity.ToTable("ItemsUOMConversions");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Dm).HasColumnName("DM");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.DefaultUserRole).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ShortName).HasMaxLength(50);
            });

            modelBuilder.Entity<JobTitleRole>(entity =>
            {
                entity.HasIndex(e => e.JobTitleId, "IX_JobTitleRoles_JobTitleId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TitleRole).HasMaxLength(50);

                entity.HasOne(d => d.JobTitle)
                    .WithMany(p => p.JobTitleRoles)
                    .HasForeignKey(d => d.JobTitleId);
            });

            modelBuilder.Entity<Kit>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Avatar).HasMaxLength(25);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.ItemKitId).HasMaxLength(10);

                entity.Property(e => e.Lsnumber).HasColumnName("LSNumber");

                entity.Property(e => e.ShortName).HasMaxLength(30);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<KitInventoryItemConversion>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.InventoryItemId)
                    .HasMaxLength(10)
                    .HasColumnName("InventoryItemID");

                entity.Property(e => e.InventoryItemIddb).HasColumnName("InventoryItemIDDb");

                entity.Property(e => e.KitId).HasColumnName("KitID");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<KitUomConversion>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Dm).HasColumnName("DM");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.KitId).HasColumnName("KitID");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<Kpi>(entity =>
            {
                entity.ToTable("KPIs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.KpisCalType).HasColumnName("KPIsCalType");

                entity.Property(e => e.KpisCode).HasColumnName("KPIsCode");

                entity.Property(e => e.KpisDescription).HasColumnName("KPIsDescription");
            });

            modelBuilder.Entity<KpiResult>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<KpiTargetComplete>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<KpisForObjectRef>(entity =>
            {
                entity.ToTable("KPIsForObjectRefs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.KpisObjectCode).HasColumnName("KPIsObjectCode");

                entity.Property(e => e.KpisObjectDescription).HasColumnName("KPIsObjectDescription");
            });

            modelBuilder.Entity<KpisObject>(entity =>
            {
                entity.ToTable("KPIsObjects");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.KpisCode).HasColumnName("KPIsCode");

                entity.Property(e => e.KpisObjectCode).HasColumnName("KPIsObjectCode");
            });

            modelBuilder.Entity<KpisSiref>(entity =>
            {
                entity.ToTable("KPIsSirefs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.KpisCode).HasColumnName("KPIsCode");
            });

            modelBuilder.Entity<KpisTarget>(entity =>
            {
                entity.ToTable("KPIsTargets");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.KpisTargetCode).HasColumnName("KPIsTargetCode");

                entity.Property(e => e.KpisTargetType).HasColumnName("KPIsTargetType");
            });

            modelBuilder.Entity<KpisTargetForObject>(entity =>
            {
                entity.ToTable("KPIsTargetForObjects");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.KpisCode).HasColumnName("KPIsCode");

                entity.Property(e => e.KpisTargetCode).HasColumnName("KPIsTargetCode");

                entity.Property(e => e.KpisTargetValue).HasColumnName("KPIsTargetValue");

                entity.Property(e => e.KpisTargetValueOriginal).HasColumnName("KPIsTargetValueOriginal");

                entity.Property(e => e.PicsalesTerritory).HasColumnName("PICSalesTerritory");

                entity.Property(e => e.Sic).HasColumnName("SIC");
            });

            modelBuilder.Entity<KpisTargetFrequency>(entity =>
            {
                entity.ToTable("KPIsTargetFrequencies");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.KpisTargetCode).HasColumnName("KPIsTargetCode");
            });

            modelBuilder.Entity<KpisTargetGroupByKpisRepeat>(entity =>
            {
                entity.ToTable("KPIsTargetGroupByKPIsRepeats");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.KpisCode).HasColumnName("KPIsCode");

                entity.Property(e => e.KpisDisplayType).HasColumnName("KPIsDisplayType");

                entity.Property(e => e.KpisRepeatTargetValue).HasColumnName("KPIsRepeatTargetValue");

                entity.Property(e => e.KpisTargetCode).HasColumnName("KPIsTargetCode");

                entity.Property(e => e.KpisType).HasColumnName("KPIsType");

                entity.Property(e => e.PicsalesTerritory).HasColumnName("PICSalesTerritory");

                entity.Property(e => e.Sic).HasColumnName("SIC");
            });

            modelBuilder.Entity<KpisTargetProductList>(entity =>
            {
                entity.ToTable("KPIsTargetProductLists");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.KpisTargetCode).HasColumnName("KPIsTargetCode");
            });

            modelBuilder.Entity<KpisTargetProductListItemCode>(entity =>
            {
                entity.ToTable("KPIsTargetProductListItemCodes");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.KpisTargetCode).HasColumnName("KPIsTargetCode");
            });

            modelBuilder.Entity<KpisTargetProductListKpi>(entity =>
            {
                entity.ToTable("KPIsTargetProductListKpis");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.KpisCode).HasColumnName("KPIsCode");

                entity.Property(e => e.KpisTargetCode).HasColumnName("KPIsTargetCode");
            });

            modelBuilder.Entity<KpiseasonCoefficient>(entity =>
            {
                entity.ToTable("KPISeasonCoefficients");

                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DeletedBy).HasMaxLength(256);

                entity.Property(e => e.KpisettingId).HasColumnName("KPISettingId");

                entity.Property(e => e.PeriodCode).HasMaxLength(20);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<Kpisetting>(entity =>
            {
                entity.ToTable("KPISetting");

                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.BasedValue).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DeletedBy).HasMaxLength(256);

                entity.Property(e => e.PicsaleTerritory)
                    .HasMaxLength(50)
                    .HasColumnName("PICSaleTerritory");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<KpivisitFrequency>(entity =>
            {
                entity.ToTable("KPIVisitFrequency");

                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.BusinessModel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DeletedBy).HasMaxLength(256);

                entity.Property(e => e.KpisettingId).HasColumnName("KPISettingId");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<Localization>(entity =>
            {
                entity.ToTable("localizations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Binfile).HasColumnName("binfile");

                entity.Property(e => e.Comment)
                    .HasMaxLength(512)
                    .HasColumnName("comment");

                entity.Property(e => e.Filename)
                    .HasMaxLength(128)
                    .HasColumnName("filename");

                entity.Property(e => e.Localeid)
                    .HasMaxLength(10)
                    .HasColumnName("localeid");

                entity.Property(e => e.Resourceid)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("resourceid");

                entity.Property(e => e.Resourceset)
                    .HasMaxLength(512)
                    .HasColumnName("resourceset");

                entity.Property(e => e.Textfile).HasColumnName("textfile");

                entity.Property(e => e.Type)
                    .HasMaxLength(512)
                    .HasColumnName("type");

                entity.Property(e => e.Updated).HasColumnName("updated");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.Property(e => e.Valuetype).HasColumnName("valuetype");
            });

            modelBuilder.Entity<LocalizationsBackup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("localizations_backup");

                entity.Property(e => e.Binfile).HasColumnName("binfile");

                entity.Property(e => e.Comment)
                    .HasMaxLength(512)
                    .HasColumnName("comment");

                entity.Property(e => e.Filename)
                    .HasMaxLength(128)
                    .HasColumnName("filename");

                entity.Property(e => e.Localeid)
                    .HasMaxLength(10)
                    .HasColumnName("localeid");

                entity.Property(e => e.Pk)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("pk");

                entity.Property(e => e.Resourceid)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("resourceid");

                entity.Property(e => e.Resourceset)
                    .HasMaxLength(512)
                    .HasColumnName("resourceset");

                entity.Property(e => e.Textfile).HasColumnName("textfile");

                entity.Property(e => e.Type)
                    .HasMaxLength(512)
                    .HasColumnName("type");

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.Property(e => e.Valuetype)
                    .HasColumnName("valuetype")
                    .HasDefaultValueSql("0");
            });

            modelBuilder.Entity<LppcRefResult>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.LppcnoofSku).HasColumnName("LPPCNoofSKU");

                entity.Property(e => e.LppcorderorInvoiceValue).HasColumnName("LPPCOrderorInvoiceValue");

                entity.Property(e => e.RecognizedDateForPc).HasColumnName("RecognizedDateForPC");

                entity.Property(e => e.ScnoofSku).HasColumnName("SCNoofSKU");

                entity.Property(e => e.ScorderValue).HasColumnName("SCOrderValue");

                entity.Property(e => e.Sicode).HasColumnName("SICode");

                entity.Property(e => e.Sitype).HasColumnName("SIType");

                entity.Property(e => e.Sku).HasColumnName("SKU");
            });

            modelBuilder.Entity<Manufacture>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AddressLine1).HasMaxLength(100);

                entity.Property(e => e.AddressLine2).HasMaxLength(1000);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirtName).HasMaxLength(10);

                entity.Property(e => e.FullName).HasMaxLength(120);

                entity.Property(e => e.GpslocationLat)
                    .HasMaxLength(25)
                    .HasColumnName("GPSLocationLat");

                entity.Property(e => e.GpslocationLng)
                    .HasMaxLength(25)
                    .HasColumnName("GPSLocationLng");

                entity.Property(e => e.LastName).HasMaxLength(10);

                entity.Property(e => e.LegalInformation).HasMaxLength(60);

                entity.Property(e => e.ManufactureCode).HasMaxLength(10);

                entity.Property(e => e.ManufactureName).HasMaxLength(60);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(60);

                entity.Property(e => e.ShortName).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.Property(e => e.TaxCode).HasMaxLength(13);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasIndex(e => e.FeatureId, "IX_Menus_FeatureId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.Icon).HasMaxLength(30);

                entity.Property(e => e.Is1Sprincipal).HasColumnName("Is1SPrincipal");

                entity.Property(e => e.IsRdos).HasColumnName("IsRDOS");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.FeatureId);
            });

            modelBuilder.Entity<MobileFeaturesPermission>(entity =>
            {
                entity.ToTable("MobileFeaturesPermission");

                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DeletedBy).HasMaxLength(256);

                entity.Property(e => e.Permission).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<MobileUser>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.PhoneCountryCode).HasMaxLength(8);

                entity.Property(e => e.PhoneCountryIso)
                    .HasMaxLength(5)
                    .HasColumnName("PhoneCountryISO");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''::character varying");
            });

            modelBuilder.Entity<MobileUserApplication>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Status).HasMaxLength(10);
            });

            modelBuilder.Entity<MobileUserDevice>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.AppVersion).HasMaxLength(10);

                entity.Property(e => e.DeviceId).HasMaxLength(255);

                entity.Property(e => e.DeviceName).HasMaxLength(50);

                entity.Property(e => e.EffectDate).HasPrecision(6);

                entity.Property(e => e.Os)
                    .HasMaxLength(10)
                    .HasColumnName("OS")
                    .HasComment(" IMEI hay Serieral của thiết bị");

                entity.Property(e => e.Osversion)
                    .HasMaxLength(10)
                    .HasColumnName("OSVersion");

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.Property(e => e.UntilDate).HasPrecision(6);
            });

            modelBuilder.Entity<MobileUserEmployee>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            });

            modelBuilder.Entity<MobileUserInfo>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.AvatarFileName).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(2);

                entity.Property(e => e.JobTitle).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<MobileUserPrinciple>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Status).HasMaxLength(10);
            });

            modelBuilder.Entity<MobileUserPrinciplesHistory>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Status).HasMaxLength(10);
            });

            modelBuilder.Entity<MobileUserSetting>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Language).HasMaxLength(10);
            });

            modelBuilder.Entity<MobileUsersLocked>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.DeviceId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.DeviceName).HasMaxLength(50);

                entity.Property(e => e.Reason).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(10);
            });

            modelBuilder.Entity<NotificationTemplate>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Body).HasMaxLength(255);

                entity.Property(e => e.Purpose).HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''::character varying");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PaginationConfig>(entity =>
            {
                entity.HasIndex(e => e.FeatureId, "IX_PaginationConfigs_FeatureId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.PaginationConfigs)
                    .HasForeignKey(d => d.FeatureId);
            });

            modelBuilder.Entity<ParameterWithSiref>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.PcconditionsNoofSku).HasColumnName("PcconditionsNoofSKU");

                entity.Property(e => e.RecognizedDateforSov).HasColumnName("RecognizedDateforSOV");
            });

            modelBuilder.Entity<ParameterWithSitype>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.PcconditionsNoofSku).HasColumnName("PcconditionsNoofSKU");

                entity.Property(e => e.RecognizedDateforSov).HasColumnName("RecognizedDateforSOV");
            });

            modelBuilder.Entity<PcrefResult>(entity =>
            {
                entity.ToTable("PCRefResults");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.PcinvoiceValue).HasColumnName("PCInvoiceValue");

                entity.Property(e => e.PcnoofSku).HasColumnName("PCNoofSKU");

                entity.Property(e => e.RecognizedDateForPc).HasColumnName("RecognizedDateForPC");

                entity.Property(e => e.Sicode).HasColumnName("SICode");

                entity.Property(e => e.Sitype).HasColumnName("SIType");

                entity.Property(e => e.SocreateBy).HasColumnName("SOCreateBy");

                entity.Property(e => e.SoowerCode).HasColumnName("SOOwerCode");
            });

            modelBuilder.Entity<PhoneType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoAllocationSetting>(entity =>
            {
                entity.ToTable("PO_AllocationSettings");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoAllocationSettingItemGroup>(entity =>
            {
                entity.ToTable("PO_AllocationSettingItemGroups");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoAverageDailySale>(entity =>
            {
                entity.ToTable("PO_AverageDailySales");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoDeliveryLeadTime>(entity =>
            {
                entity.ToTable("PO_DeliveryLeadTimes");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoGrpodetailItem>(entity =>
            {
                entity.ToTable("PO_GRPODetailItems");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Grponumber).HasColumnName("GRPONumber");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoGrpoheader>(entity =>
            {
                entity.ToTable("PO_GRPOHeaders");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Grpodate).HasColumnName("GRPODate");

                entity.Property(e => e.Grponumber).HasColumnName("GRPONumber");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoOrderDetail>(entity =>
            {
                entity.ToTable("PO_OrderDetails");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoOrderHeader>(entity =>
            {
                entity.ToTable("PO_OrderHeaders");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoPoconfirmDetailItem>(entity =>
            {
                entity.ToTable("PO_POConfirmDetailItems");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoPoconfirmHeader>(entity =>
            {
                entity.ToTable("PO_POConfirmHeaders");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoPurchaseSchedule>(entity =>
            {
                entity.ToTable("PO_PurchaseSchedules");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoPurchaseScheduleDetail>(entity =>
            {
                entity.ToTable("PO_PurchaseScheduleDetails");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoReturnDetailItem>(entity =>
            {
                entity.ToTable("PO_ReturnDetailItems");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoReturnHeader>(entity =>
            {
                entity.ToTable("PO_ReturnHeaders");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Grponumber).HasColumnName("GRPONumber");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoRpoparameter>(entity =>
            {
                entity.ToTable("PO_RPOParameters");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.IncludedPoshipping).HasColumnName("IncludedPOShipping");

                entity.Property(e => e.IncludedPotransit).HasColumnName("IncludedPOTransit");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoStockKeepingDay>(entity =>
            {
                entity.ToTable("PO_StockKeepingDays");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PoStockKeepingDayItemHierarchy>(entity =>
            {
                entity.ToTable("PO_StockKeepingDayItemHierarchies");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<Policy>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.Key).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PriceDefinitionDistributor>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PriceList>(entity =>
            {
                entity.HasIndex(e => e.PriceListTypeId, "IX_PriceLists_PriceListTypeId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ObjectApply).HasMaxLength(255);

                entity.Property(e => e.PriceListCode).HasMaxLength(50);

                entity.Property(e => e.PriceListTypeCode).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.PriceListType)
                    .WithMany(p => p.PriceLists)
                    .HasForeignKey(d => d.PriceListTypeId);
            });

            modelBuilder.Entity<PriceListDistributeSellingArea>(entity =>
            {
                entity.HasIndex(e => e.PriceListId, "IX_PriceListDistributeSellingAreas_PriceListId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.SellingAreaCode).HasMaxLength(50);

                entity.Property(e => e.SellingAreaDescription).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.PriceList)
                    .WithMany(p => p.PriceListDistributeSellingAreas)
                    .HasForeignKey(d => d.PriceListId);
            });

            modelBuilder.Entity<PriceListItemGroup>(entity =>
            {
                entity.HasIndex(e => e.PriceListId, "IX_PriceListItemGroups_PriceListId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.ItemGroupCode).HasMaxLength(50);

                entity.Property(e => e.UoM).HasMaxLength(25);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.PriceList)
                    .WithMany(p => p.PriceListItemGroups)
                    .HasForeignKey(d => d.PriceListId);
            });

            modelBuilder.Entity<PriceListOutletAttributeValue>(entity =>
            {
                entity.HasIndex(e => e.PriceListId, "IX_PriceListOutletAttributeValues_PriceListId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.OutletAttributeLevelCode).HasMaxLength(50);

                entity.Property(e => e.OutletAttributeValue).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.PriceList)
                    .WithMany(p => p.PriceListOutletAttributeValues)
                    .HasForeignKey(d => d.PriceListId);
            });

            modelBuilder.Entity<PriceListSalesTerritoryLevel>(entity =>
            {
                entity.HasIndex(e => e.PriceListId, "IX_PriceListSalesTerritoryLevels_PriceListId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.SalesTerritoryLevelCode).HasMaxLength(50);

                entity.Property(e => e.SalesTerritoryLevelDescription).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.PriceList)
                    .WithMany(p => p.PriceListSalesTerritoryLevels)
                    .HasForeignKey(d => d.PriceListId);
            });

            modelBuilder.Entity<PriceListType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BasePriceCode).HasMaxLength(25);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.PriceListTypeCode).HasMaxLength(12);

                entity.Property(e => e.PriceType).HasMaxLength(20);

                entity.Property(e => e.SaleTerritoryLevel).HasMaxLength(255);

                entity.Property(e => e.SaleTerritoryLevelDescription).HasMaxLength(255);

                entity.Property(e => e.SalesTerritoryCode).HasMaxLength(255);

                entity.Property(e => e.SalesTerritoryDescription).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PriceListTypeAttributeList>(entity =>
            {
                entity.HasIndex(e => e.PriceListTypeId, "IX_PriceListTypeAttributeLists_PriceListTypeId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.PriceListType)
                    .WithMany(p => p.PriceListTypeAttributeLists)
                    .HasForeignKey(d => d.PriceListTypeId);
            });

            modelBuilder.Entity<PriceSetting>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PriceSettingAuditLog>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<PrimarySic>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PrimarySicExcludeHierarchyDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.PrimarySic)
                    .WithMany(p => p.PrimarySicExcludeHierarchyDetails)
                    .HasForeignKey(d => d.PrimarySicId);
            });

            modelBuilder.Entity<PrimarySicExcludeItemGroupDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.PrimarySic)
                    .WithMany(p => p.PrimarySicExcludeItemGroupDetails)
                    .HasForeignKey(d => d.PrimarySicId);
            });

            modelBuilder.Entity<PrimarySicIncludeDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.HierarchyValue)
                    .WithMany(p => p.PrimarySicIncludeDetails)
                    .HasForeignKey(d => d.HierarchyValueId);

                entity.HasOne(d => d.PrimarySic)
                    .WithMany(p => p.PrimarySicIncludeDetails)
                    .HasForeignKey(d => d.PrimarySicId);
            });

            modelBuilder.Entity<Principal>(entity =>
            {
                entity.HasIndex(e => e.PackageId, "IX_Principals_PackageId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address1).HasMaxLength(200);

                entity.Property(e => e.Address2).HasMaxLength(200);

                entity.Property(e => e.Code).HasMaxLength(5);

                entity.Property(e => e.Country).HasMaxLength(30);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.InitializationStatus).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.SecretKey).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.Property(e => e.Web).HasMaxLength(200);

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.Principals)
                    .HasForeignKey(d => d.PackageId);
            });

            modelBuilder.Entity<PrincipalEmpContract>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId, "IX_PrincipalEmpContracts_EmployeeId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ContractType).HasMaxLength(100);

                entity.Property(e => e.EmployeeCode).HasMaxLength(100);

                entity.Property(e => e.FilePath).HasMaxLength(255);

                entity.Property(e => e.JobTitle).HasMaxLength(100);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.PrincipalEmpContracts)
                    .HasForeignKey(d => d.EmployeeId);
            });

            modelBuilder.Entity<PrincipalProfile>(entity =>
            {
                entity.ToTable("PrincipalProfile");

                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.AddressCity).HasMaxLength(100);

                entity.Property(e => e.AddressCountry).HasMaxLength(50);

                entity.Property(e => e.AddressDeparmentNo).HasMaxLength(100);

                entity.Property(e => e.AddressDistrict).HasMaxLength(100);

                entity.Property(e => e.AddressProvince).HasMaxLength(100);

                entity.Property(e => e.AddressRegion).HasMaxLength(100);

                entity.Property(e => e.AddressState).HasMaxLength(100);

                entity.Property(e => e.AddressStreet).HasMaxLength(100);

                entity.Property(e => e.AddressWard).HasMaxLength(100);

                entity.Property(e => e.AttentionEmail).HasMaxLength(100);

                entity.Property(e => e.AttentionEmailType).HasMaxLength(255);

                entity.Property(e => e.AttentionFirstName).HasMaxLength(50);

                entity.Property(e => e.AttentionFullName).HasMaxLength(255);

                entity.Property(e => e.AttentionLastName).HasMaxLength(50);

                entity.Property(e => e.AttentionMiddleName).HasMaxLength(50);

                entity.Property(e => e.AttentionPhoneNumber).HasMaxLength(12);

                entity.Property(e => e.AttentionPhoneType).HasMaxLength(255);

                entity.Property(e => e.AttentionPosition).HasMaxLength(50);

                entity.Property(e => e.AttentionTitle).HasMaxLength(50);

                entity.Property(e => e.BankAccount).HasMaxLength(100);

                entity.Property(e => e.BankName).HasMaxLength(255);

                entity.Property(e => e.BankNumber).HasMaxLength(20);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.ContractEmail).HasMaxLength(100);

                entity.Property(e => e.ContractEmailType).HasMaxLength(255);

                entity.Property(e => e.ContractFirstName).HasMaxLength(50);

                entity.Property(e => e.ContractLastName).HasMaxLength(50);

                entity.Property(e => e.ContractMiddleName).HasMaxLength(50);

                entity.Property(e => e.ContractPhoneNumber).HasMaxLength(12);

                entity.Property(e => e.ContractPhoneType).HasMaxLength(255);

                entity.Property(e => e.ContractPosition).HasMaxLength(50);

                entity.Property(e => e.ContractTitle).HasMaxLength(10);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.Latitude).HasMaxLength(255);

                entity.Property(e => e.Longitude).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.ShortName).HasMaxLength(100);

                entity.Property(e => e.TaxNumber).HasMaxLength(100);

                entity.Property(e => e.Website).HasMaxLength(100);
            });

            modelBuilder.Entity<PrincipalSetting>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Key).HasMaxLength(50);
            });

            modelBuilder.Entity<PrincipalWarehouse>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.AddressCity).HasMaxLength(100);

                entity.Property(e => e.AddressCountry).HasMaxLength(50);

                entity.Property(e => e.AddressDeparmentNo).HasMaxLength(100);

                entity.Property(e => e.AddressDistrict).HasMaxLength(100);

                entity.Property(e => e.AddressProvince).HasMaxLength(100);

                entity.Property(e => e.AddressRegion).HasMaxLength(100);

                entity.Property(e => e.AddressState).HasMaxLength(100);

                entity.Property(e => e.AddressStreet).HasMaxLength(100);

                entity.Property(e => e.AddressWard).HasMaxLength(100);

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Decscription).HasMaxLength(255);

                entity.Property(e => e.Latitude).HasMaxLength(255);

                entity.Property(e => e.LinkedCode).HasMaxLength(20);

                entity.Property(e => e.Longitude).HasMaxLength(255);

                entity.Property(e => e.ManagerDob).HasColumnName("ManagerDOB");

                entity.Property(e => e.ManagerEmail).HasMaxLength(100);

                entity.Property(e => e.ManagerEmailType).HasMaxLength(20);

                entity.Property(e => e.ManagerFirstName).HasMaxLength(50);

                entity.Property(e => e.ManagerFullName).HasMaxLength(255);

                entity.Property(e => e.ManagerGender).HasMaxLength(20);

                entity.Property(e => e.ManagerLastName).HasMaxLength(50);

                entity.Property(e => e.ManagerMiddleName).HasMaxLength(50);

                entity.Property(e => e.ManagerNote).HasMaxLength(255);

                entity.Property(e => e.ManagerPhoneNumber).HasMaxLength(30);

                entity.Property(e => e.ManagerPhoneType).HasMaxLength(20);

                entity.Property(e => e.ManagerTitle).HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(20);
            });

            modelBuilder.Entity<PrincipalWarehouseLocation>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Code).ValueGeneratedOnAdd();

                entity.Property(e => e.Decscription).HasMaxLength(255);
            });

            modelBuilder.Entity<PrincipalWinzardSetup>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<PrincipleEmployee>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountName).HasMaxLength(100);

                entity.Property(e => e.AccountPassword).HasMaxLength(255);

                entity.Property(e => e.AccountStatus).HasMaxLength(50);

                entity.Property(e => e.AddressCity).HasMaxLength(100);

                entity.Property(e => e.AddressCountry).HasMaxLength(50);

                entity.Property(e => e.AddressDeparmentNo).HasMaxLength(100);

                entity.Property(e => e.AddressDistrict).HasMaxLength(100);

                entity.Property(e => e.AddressProvince).HasMaxLength(100);

                entity.Property(e => e.AddressRegion).HasMaxLength(100);

                entity.Property(e => e.AddressState).HasMaxLength(100);

                entity.Property(e => e.AddressStreet).HasMaxLength(100);

                entity.Property(e => e.AddressWard).HasMaxLength(100);

                entity.Property(e => e.AvartarFilePath).HasMaxLength(255);

                entity.Property(e => e.BankAccountName).HasMaxLength(100);

                entity.Property(e => e.BankAccountNumber).HasMaxLength(100);

                entity.Property(e => e.BankBranch).HasMaxLength(255);

                entity.Property(e => e.BankName).HasMaxLength(255);

                entity.Property(e => e.DistributorCode).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.EmailType).HasMaxLength(255);

                entity.Property(e => e.EmployeeCode).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.Idcard).HasMaxLength(12);

                entity.Property(e => e.Idcard2).HasMaxLength(12);

                entity.Property(e => e.InsuranceId).HasMaxLength(100);

                entity.Property(e => e.JobTitle).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MainPhoneNumber).HasMaxLength(12);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.Position).HasMaxLength(100);

                entity.Property(e => e.PrincipalEmpCode).HasMaxLength(100);

                entity.Property(e => e.SoStructure).HasMaxLength(100);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TaxNumber).HasMaxLength(100);

                entity.Property(e => e.Territory).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(20);
            });

            modelBuilder.Entity<PriorityPriceListType>(entity =>
            {
                entity.HasIndex(e => e.PriceListTypeId, "IX_PriorityPriceListTypes_PriceListTypeId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.PriceListType)
                    .WithMany(p => p.PriorityPriceListTypes)
                    .HasForeignKey(d => d.PriceListTypeId);
            });

            modelBuilder.Entity<ProductList>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<ProductListItemCode>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.ProvinceCode).HasMaxLength(10);

                entity.Property(e => e.ShortName).HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PurchaseBasePrice>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ContractType).HasMaxLength(20);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.PriceType).HasMaxLength(20);

                entity.Property(e => e.PurchasePriceCode).HasMaxLength(12);

                entity.Property(e => e.SalesTerritoryCode).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<PurchasePriceItemGroup>(entity =>
            {
                entity.HasIndex(e => e.PurchaseBasePriceId, "IX_PurchasePriceItemGroups_PurchaseBasePriceId");

                entity.HasIndex(e => e.SalesPriceItemGroupReferenceId, "IX_PurchasePriceItemGroups_SalesPriceItemGroupReferenceId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ItemGroupCode).HasMaxLength(255);

                entity.Property(e => e.Uom).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.PurchaseBasePrice)
                    .WithMany(p => p.PurchasePriceItemGroups)
                    .HasForeignKey(d => d.PurchaseBasePriceId)
                    .HasConstraintName("FK_PurchasePriceItemGroups_PurchaseBasePrices_PurchaseBasePric~");

                entity.HasOne(d => d.SalesPriceItemGroupReference)
                    .WithMany(p => p.PurchasePriceItemGroups)
                    .HasForeignKey(d => d.SalesPriceItemGroupReferenceId)
                    .HasConstraintName("FK_PurchasePriceItemGroups_SalesPriceItemGroupReferences_Sales~");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.AppVersion).HasMaxLength(20);

                entity.Property(e => e.Os)
                    .HasMaxLength(10)
                    .HasColumnName("OS");

                entity.Property(e => e.RefreshToken1)
                    .HasMaxLength(255)
                    .HasColumnName("RefreshToken");
            });

            modelBuilder.Entity<RefreshTokenModel>(entity =>
            {
                entity.ToTable("RefreshTokenModel");

                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.AppName).HasMaxLength(20);

                entity.Property(e => e.AppVersion).HasMaxLength(20);

                entity.Property(e => e.Os)
                    .HasMaxLength(10)
                    .HasColumnName("OS");

                entity.Property(e => e.RefreshToken).HasMaxLength(255);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.RegionCode).HasMaxLength(10);

                entity.Property(e => e.ShortName).HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");

                entity.Property(e => e.DisplayName).HasMaxLength(36);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<RoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_RoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<RzBeatPlan>(entity =>
            {
                entity.ToTable("RZ_BeatPlans");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<RzBeatPlanEmployee>(entity =>
            {
                entity.ToTable("RZ_BeatPlanEmployees");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<RzBeatPlanShipto>(entity =>
            {
                entity.ToTable("RZ_BeatPlanShiptos");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<RzLocation>(entity =>
            {
                entity.ToTable("RZ_Locations");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<RzParameterLevelApply>(entity =>
            {
                entity.ToTable("RZ_ParameterLevelApplies");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<RzParameterSetting>(entity =>
            {
                entity.ToTable("RZ_ParameterSettings");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<RzParameterType>(entity =>
            {
                entity.ToTable("RZ_ParameterTypes");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<RzParameterValue>(entity =>
            {
                entity.ToTable("RZ_ParameterValues");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<RzRouteZoneInfomation>(entity =>
            {
                entity.ToTable("RZ_RouteZoneInfomations");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<RzRouteZoneParameter>(entity =>
            {
                entity.ToTable("RZ_RouteZoneParameters");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EffectiveDate).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");
            });

            modelBuilder.Entity<RzRouteZoneShipto>(entity =>
            {
                entity.ToTable("RZ_RouteZoneShiptos");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<RzRouteZoneType>(entity =>
            {
                entity.ToTable("RZ_RouteZoneTypes");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<RzVisitFrequency>(entity =>
            {
                entity.ToTable("RZ_VisitFrequency");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<SaUserWithDistributorShipto>(entity =>
            {
                entity.ToTable("SA_UserWithDistributorShiptos");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DeletedBy).HasMaxLength(256);

                entity.Property(e => e.DistributorCode).HasMaxLength(20);

                entity.Property(e => e.DistributorShiptoCode).HasMaxLength(20);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.Property(e => e.UserCode).HasMaxLength(36);
            });

            modelBuilder.Entity<SaleCalendar>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.QuarterStructure).HasMaxLength(20);
            });

            modelBuilder.Entity<SaleCalendarActionHistory>(entity =>
            {
                entity.ToTable("SaleCalendarActionHistory");

                entity.HasIndex(e => e.SaleCalendarId, "IX_SaleCalendarActionHistory_SaleCalendarId");

                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.ActionName).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.HasOne(d => d.SaleCalendar)
                    .WithMany(p => p.SaleCalendarActionHistories)
                    .HasForeignKey(d => d.SaleCalendarId);
            });

            modelBuilder.Entity<SaleCalendarGenerate>(entity =>
            {
                entity.HasIndex(e => e.SaleCalendarId, "IX_SaleCalendarGenerates_SaleCalendarId");

                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.HasOne(d => d.SaleCalendar)
                    .WithMany(p => p.SaleCalendarGenerates)
                    .HasForeignKey(d => d.SaleCalendarId);
            });

            modelBuilder.Entity<SaleCalendarHoliday>(entity =>
            {
                entity.HasIndex(e => e.SaleCalendarId, "IX_SaleCalendarHolidays_SaleCalendarId");

                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.HasOne(d => d.SaleCalendar)
                    .WithMany(p => p.SaleCalendarHolidays)
                    .HasForeignKey(d => d.SaleCalendarId);
            });

            modelBuilder.Entity<SaleGroup>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Code).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ShortName).HasMaxLength(100);
            });

            modelBuilder.Entity<SalesBasePrice>(entity =>
            {
                entity.HasIndex(e => e.PurchaseBasePriceId, "IX_SalesBasePrices_PurchaseBasePriceId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.PriceType).HasMaxLength(20);

                entity.Property(e => e.PurchaseBasePriceCode).HasMaxLength(12);

                entity.Property(e => e.SalesPriceCode).HasMaxLength(12);

                entity.Property(e => e.SalesTerritoryCode).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.PurchaseBasePrice)
                    .WithMany(p => p.SalesBasePrices)
                    .HasForeignKey(d => d.PurchaseBasePriceId);
            });

            modelBuilder.Entity<SalesIndicatorRef>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LevelSitype).HasColumnName("LevelSIType");
            });

            modelBuilder.Entity<SalesIndicatorType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<SalesOganization>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.TerritoryStructureCode).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<SalesPriceItemGroup>(entity =>
            {
                entity.HasIndex(e => e.SalesBasePriceId, "IX_SalesPriceItemGroups_SalesBasePriceId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ItemGroupCode).HasMaxLength(255);

                entity.Property(e => e.Uom).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.SalesBasePrice)
                    .WithMany(p => p.SalesPriceItemGroups)
                    .HasForeignKey(d => d.SalesBasePriceId);
            });

            modelBuilder.Entity<SalesPriceItemGroupReference>(entity =>
            {
                entity.HasIndex(e => e.SalesBasePriceId, "IX_SalesPriceItemGroupReferences_SalesBasePriceId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.SalesBasePrice)
                    .WithMany(p => p.SalesPriceItemGroupReferences)
                    .HasForeignKey(d => d.SalesBasePriceId)
                    .HasConstraintName("FK_SalesPriceItemGroupReferences_SalesBasePrices_SalesBasePric~");
            });

            modelBuilder.Entity<ScAuditlogReconcile>(entity =>
            {
                entity.ToTable("SC_AuditlogReconciles");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ScSalesOrganizationStructure>(entity =>
            {
                entity.ToTable("SC_SalesOrganizationStructures");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.IsSpecificSic).HasColumnName("IsSpecificSIC");

                entity.Property(e => e.Sic).HasColumnName("SIC");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ScSalesTeamAssignment>(entity =>
            {
                entity.ToTable("SC_SalesTeamAssignments");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.IsSicbase).HasColumnName("IsSICBase");

                entity.Property(e => e.SostructureCode).HasColumnName("SOStructureCode");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ScTerritoryLevel>(entity =>
            {
                entity.ToTable("SC_TerritoryLevels");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ScTerritoryMapping>(entity =>
            {
                entity.ToTable("SC_TerritoryMappings");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.EffectiveDate).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ScTerritoryStructure>(entity =>
            {
                entity.ToTable("SC_TerritoryStructures");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ScTerritoryStructureDetail>(entity =>
            {
                entity.ToTable("SC_TerritoryStructureDetails");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.TerritoryStructureCode).IsRequired();

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ScTerritoryStructureGeographicalMapping>(entity =>
            {
                entity.ToTable("SC_TerritoryStructureGeographicalMappings");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ScTerritoryValue>(entity =>
            {
                entity.ToTable("SC_TerritoryValues");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(4);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<ScrefResult>(entity =>
            {
                entity.ToTable("SCRefResults");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.Sicode).HasColumnName("SICode");

                entity.Property(e => e.Sitype).HasColumnName("SIType");

                entity.Property(e => e.SocreateBy).HasColumnName("SOCreateBy");

                entity.Property(e => e.SoowerCode).HasColumnName("SOOwerCode");
            });

            modelBuilder.Entity<SdoResult>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.SdopercentConverage).HasColumnName("SDOPercentConverage");

                entity.Property(e => e.SdopercentGrow).HasColumnName("SDOPercentGrow");

                entity.Property(e => e.Sdoresult1).HasColumnName("SDOResult");
            });

            modelBuilder.Entity<Sdoconfig>(entity =>
            {
                entity.ToTable("SDOConfigs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IsMainProductGainSdocoverage).HasColumnName("IsMainProductGainSDOCoverage");

                entity.Property(e => e.IsMainProductGainSdogrowth).HasColumnName("IsMainProductGainSDOGrowth");

                entity.Property(e => e.IsMaintainSdo).HasColumnName("IsMaintainSDO");

                entity.Property(e => e.MinimumNumberSdo).HasColumnName("MinimumNumberSDO");

                entity.Property(e => e.NumberSdotoDo).HasColumnName("NumberSDOToDo");

                entity.Property(e => e.ProductGainSdocoverageType).HasColumnName("ProductGainSDOCoverageType");

                entity.Property(e => e.ProductGainSdocoverageValue).HasColumnName("ProductGainSDOCoverageValue");

                entity.Property(e => e.ProductGainSdogrowthType).HasColumnName("ProductGainSDOGrowthType");

                entity.Property(e => e.ProductGainSdogrowthValue).HasColumnName("ProductGainSDOGrowthValue");

                entity.Property(e => e.Sdocode).HasColumnName("SDOCode");

                entity.Property(e => e.Sdodescription).HasColumnName("SDODescription");
            });

            modelBuilder.Entity<SdoconfigSalesOrder>(entity =>
            {
                entity.ToTable("SDOConfigSalesOrders");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.SalesUoMcode).HasColumnName("SalesUoMCode");

                entity.Property(e => e.Sdocode).HasColumnName("SDOCode");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Apikind).HasColumnName("APIKind");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Ecrurl)
                    .HasMaxLength(256)
                    .HasColumnName("ECRURL");

                entity.Property(e => e.Ecrversion)
                    .HasMaxLength(10)
                    .HasColumnName("ECRVersion");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.Property(e => e.Url)
                    .HasMaxLength(350)
                    .HasColumnName("URL");

                entity.Property(e => e.Versions).HasMaxLength(350);
            });

            modelBuilder.Entity<ServiceDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(150);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.Property(e => e.Url)
                    .HasMaxLength(350)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<ShiptoContact>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ShiptoContacts)
                    .HasForeignKey(d => d.ContactId);

                entity.HasOne(d => d.Shipto)
                    .WithMany(p => p.ShiptoContacts)
                    .HasForeignKey(d => d.ShiptoId);
            });

            modelBuilder.Entity<ShiptoHistorical>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ShiptoCodeonErp).HasColumnName("ShiptoCodeonERP");
            });

            modelBuilder.Entity<SivRefResult>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.PcinvoiceValue).HasColumnName("PCInvoiceValue");

                entity.Property(e => e.PcnoofSku).HasColumnName("PCNoofSKU");

                entity.Property(e => e.RecognizedDateForPc).HasColumnName("RecognizedDateForPC");

                entity.Property(e => e.Sicode).HasColumnName("SICode");

                entity.Property(e => e.Sitype).HasColumnName("SIType");

                entity.Property(e => e.SocreateBy).HasColumnName("SOCreateBy");

                entity.Property(e => e.SoowerCode).HasColumnName("SOOwerCode");
            });

            modelBuilder.Entity<SkurefResult>(entity =>
            {
                entity.ToTable("SKURefResults");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.Sicode).HasColumnName("SICode");

                entity.Property(e => e.Sitype).HasColumnName("SIType");

                entity.Property(e => e.SocreateBy).HasColumnName("SOCreateBy");

                entity.Property(e => e.SoowerCode).HasColumnName("SOOwerCode");
            });

            modelBuilder.Entity<SoFirstTimeCustomer>(entity =>
            {
                entity.ToTable("SO_FirstTimeCustomers");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DistributorShiptoId).HasColumnName("DistributorShiptoID");
            });

            modelBuilder.Entity<SoOrderInformation>(entity =>
            {
                entity.ToTable("SO_OrderInformations");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AreaManagerId).HasColumnName("Area_Manager_ID");

                entity.Property(e => e.BranchManagerId).HasColumnName("Branch_Manager_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_By");

                entity.Property(e => e.CreatedBy1).HasColumnName("CreatedBy");

                entity.Property(e => e.CustomerShiptoId).HasColumnName("CustomerShiptoID");

                entity.Property(e => e.DistyBilltoId).HasColumnName("Disty_billtoID");

                entity.Property(e => e.DsaManagerId).HasColumnName("DSA_Manager_ID");

                entity.Property(e => e.Dsaid).HasColumnName("DSAID");

                entity.Property(e => e.ExternalOrdNbr).HasColumnName("External_OrdNBR");

                entity.Property(e => e.IsReturn).HasColumnName("isReturn");

                entity.Property(e => e.NsdId).HasColumnName("NSD_ID");

                entity.Property(e => e.OrdAmt).HasColumnName("Ord_Amt");

                entity.Property(e => e.OrdDiscAmt).HasColumnName("Ord_Disc_Amt");

                entity.Property(e => e.OrdExtendAmt).HasColumnName("Ord_Extend_Amt");

                entity.Property(e => e.OrdQty).HasColumnName("Ord_Qty");

                entity.Property(e => e.OrdSkus).HasColumnName("Ord_SKUs");

                entity.Property(e => e.OrdlineDiscAmt).HasColumnName("Ordline_Disc_Amt");

                entity.Property(e => e.OrigOrdAmt).HasColumnName("Orig_Ord_Amt");

                entity.Property(e => e.OrigOrdDiscAmt).HasColumnName("Orig_Ord_Disc_Amt");

                entity.Property(e => e.OrigOrdExtendAmt).HasColumnName("Orig_Ord_Extend_Amt");

                entity.Property(e => e.OrigOrdQty).HasColumnName("Orig_Ord_Qty");

                entity.Property(e => e.OrigOrdSkus).HasColumnName("Orig_Ord_SKUs");

                entity.Property(e => e.OrigOrdlineDiscAmt).HasColumnName("Orig_Ordline_Disc_Amt");

                entity.Property(e => e.OrigPromotionQty).HasColumnName("Orig_Promotion_Qty");

                entity.Property(e => e.OwnerId).HasColumnName("Owner_ID");

                entity.Property(e => e.PeriodId).HasColumnName("PeriodID");

                entity.Property(e => e.PrincipalId).HasColumnName("PrincipalID");

                entity.Property(e => e.PromotionAmt).HasColumnName("Promotion_Amt");

                entity.Property(e => e.PromotionQty).HasColumnName("Promotion_Qty");

                entity.Property(e => e.RegionManagerId).HasColumnName("Region_Manager_ID");

                entity.Property(e => e.RouteZoneId).HasColumnName("RouteZoneID");

                entity.Property(e => e.RouteZoneType).HasColumnName("RouteZOneType");

                entity.Property(e => e.RzSuppervisorId).HasColumnName("RZ_Suppervisor_ID");

                entity.Property(e => e.SalesOrgId).HasColumnName("SalesOrgID");

                entity.Property(e => e.SalesRepId).HasColumnName("SalesRepID");

                entity.Property(e => e.ShippedAmt).HasColumnName("Shipped_Amt");

                entity.Property(e => e.ShippedDiscAmt).HasColumnName("Shipped_Disc_Amt");

                entity.Property(e => e.ShippedExtendAmt).HasColumnName("Shipped_Extend_Amt");

                entity.Property(e => e.ShippedLineDiscAmt).HasColumnName("Shipped_line_Disc_Amt");

                entity.Property(e => e.ShippedPromotionQty).HasColumnName("Shipped_Promotion_Qty");

                entity.Property(e => e.ShippedQty).HasColumnName("Shipped_Qty");

                entity.Property(e => e.ShippedSkus).HasColumnName("Shipped_SKUs");

                entity.Property(e => e.ShiptoAttribute1).HasColumnName("Shipto_Attribute1");

                entity.Property(e => e.ShiptoAttribute10).HasColumnName("Shipto_Attribute10");

                entity.Property(e => e.ShiptoAttribute2).HasColumnName("Shipto_Attribute2");

                entity.Property(e => e.ShiptoAttribute3).HasColumnName("Shipto_Attribute3");

                entity.Property(e => e.ShiptoAttribute4).HasColumnName("Shipto_Attribute4");

                entity.Property(e => e.ShiptoAttribute5).HasColumnName("Shipto_Attribute5");

                entity.Property(e => e.ShiptoAttribute6).HasColumnName("Shipto_Attribute6");

                entity.Property(e => e.ShiptoAttribute7).HasColumnName("Shipto_Attribute7");

                entity.Property(e => e.ShiptoAttribute8).HasColumnName("Shipto_Attribute8");

                entity.Property(e => e.ShiptoAttribute9).HasColumnName("Shipto_Attribute9");

                entity.Property(e => e.SicId).HasColumnName("SIC_ID");

                entity.Property(e => e.SubAreaManagerId).HasColumnName("Sub_Area_Manager_ID");

                entity.Property(e => e.SubRegionManagerId).HasColumnName("Sub_Region_Manager_ID");

                entity.Property(e => e.TerritoryStrId).HasColumnName("TerritoryStrID");

                entity.Property(e => e.TotalVat).HasColumnName("TotalVAT");

                entity.Property(e => e.VisitId).HasColumnName("VisitID");

                entity.Property(e => e.WareHouseId).HasColumnName("WareHouseID");
            });

            modelBuilder.Entity<SoOrderItem>(entity =>
            {
                entity.ToTable("SO_OrderItems");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DiscountDealId).HasColumnName("DiscountDealID");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");

                entity.Property(e => e.DiscountSchemeId).HasColumnName("DiscountSchemeID");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.OrdLineAmt).HasColumnName("Ord_Line_Amt");

                entity.Property(e => e.OrdLineDiscAmt).HasColumnName("Ord_line_Disc_Amt");

                entity.Property(e => e.OrdLineExtendAmt).HasColumnName("Ord_Line_Extend_Amt");

                entity.Property(e => e.OrigOrdLineAmt).HasColumnName("Orig_Ord_Line_Amt");

                entity.Property(e => e.OrigOrdLineDiscAmt).HasColumnName("Orig_Ord_line_Disc_Amt");

                entity.Property(e => e.OrigOrdLineExtendAmt).HasColumnName("Orig_Ord_Line_Extend_Amt");

                entity.Property(e => e.ShippedLineAmt).HasColumnName("Shipped_Line_Amt");

                entity.Property(e => e.ShippedLineDiscAmt).HasColumnName("Shipped_line_Disc_Amt");

                entity.Property(e => e.ShippedLineExtendAmt).HasColumnName("Shipped_Line_Extend_Amt");

                entity.Property(e => e.Uom).HasColumnName("UOM");

                entity.Property(e => e.Vat).HasColumnName("VAT");

                entity.Property(e => e.Vatcode).HasColumnName("VATCode");
            });

            modelBuilder.Entity<SoReason>(entity =>
            {
                entity.ToTable("SO_Reasons");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<SoSumPickingListDetail>(entity =>
            {
                entity.ToTable("SO_SumPickingListDetails");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<SoSumPickingListHeader>(entity =>
            {
                entity.ToTable("SO_SumPickingListHeaders");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DistributorShiptoId).HasColumnName("DistributorShiptoID");

                entity.Property(e => e.WareHouseId).HasColumnName("WareHouseID");
            });

            modelBuilder.Entity<SovRefResult>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.RecognizedDateForPsov).HasColumnName("RecognizedDateForPSOV");

                entity.Property(e => e.Sicode).HasColumnName("SICode");

                entity.Property(e => e.Sitype).HasColumnName("SIType");

                entity.Property(e => e.Sku).HasColumnName("SKU");

                entity.Property(e => e.SovinvoiceValue).HasColumnName("SOVInvoiceValue");

                entity.Property(e => e.SovnoofSku).HasColumnName("SOVNoofSKU");
            });

            modelBuilder.Entity<Standard>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<StandardItem>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.ShortName).HasMaxLength(40);

                entity.Property(e => e.StateCode).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<SystemLog>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ObjectName).HasMaxLength(256);
            });

            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedDate)
                    .HasPrecision(6)
                    .HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");

                entity.Property(e => e.DeletedDate).HasPrecision(6);

                entity.Property(e => e.Description).HasDefaultValueSql("''::character varying");

                entity.Property(e => e.SettingKey).HasDefaultValueSql("''::character varying");

                entity.Property(e => e.SettingType).HasDefaultValueSql("''::character varying");

                entity.Property(e => e.UpdatedDate).HasPrecision(6);
            });

            modelBuilder.Entity<TempBaselineDataBusinessModel>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempBaselineDataConditionExclude>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempBaselineDataOrder>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.DsamanagerCode).HasColumnName("DSAManagerCode");

                entity.Property(e => e.Nsdcode).HasColumnName("NSDCode");

                entity.Property(e => e.RzsuppervisorCode).HasColumnName("RZSuppervisorCode");

                entity.Property(e => e.Siccode).HasColumnName("SICCode");

                entity.Property(e => e.SocreateBy).HasColumnName("SOCreateBy");

                entity.Property(e => e.SoownerCode).HasColumnName("SOOwnerCode");

                entity.Property(e => e.SoshippedAmt).HasColumnName("SOShippedAmt");

                entity.Property(e => e.SoshippedExtendAmt).HasColumnName("SOShippedExtendAmt");

                entity.Property(e => e.VisitDate).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");
            });

            modelBuilder.Entity<TempBaselineDataOrderDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempBaselineDataPurchaseOrder>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.DsamanagerCode).HasColumnName("DSAManagerCode");

                entity.Property(e => e.Nsdcode).HasColumnName("NSDCode");

                entity.Property(e => e.RzsuppervisorCode).HasColumnName("RZSuppervisorCode");

                entity.Property(e => e.Siccode).HasColumnName("SICCode");

                entity.Property(e => e.SocreateBy).HasColumnName("SOCreateBy");

                entity.Property(e => e.SoownerCode).HasColumnName("SOOwnerCode");

                entity.Property(e => e.SoshippedAmt).HasColumnName("SOShippedAmt");

                entity.Property(e => e.SoshippedExtendAmt).HasColumnName("SOShippedExtendAmt");
            });

            modelBuilder.Entity<TempBaselineDataPurchaseOrderDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempBaselineDataVisit>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.DsamanagerCode).HasColumnName("DSAManagerCode");

                entity.Property(e => e.Nsdcode).HasColumnName("NSDCode");

                entity.Property(e => e.Siccode).HasColumnName("SICCode");
            });

            modelBuilder.Entity<TempBaselineDataVisitStepResult>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempBaselineDetailRequestPo>(entity =>
            {
                entity.ToTable("TempBaselineDetailRequestPOs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Grpocode).HasColumnName("GRPOCode");

                entity.Property(e => e.Rpocode).HasColumnName("RPOCode");

                entity.Property(e => e.Skucode).HasColumnName("SKUCode");
            });

            modelBuilder.Entity<TempBaselineHeaderRequestPo>(entity =>
            {
                entity.ToTable("TempBaselineHeaderRequestPOs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Grpocode).HasColumnName("GRPOCode");

                entity.Property(e => e.Rpocode).HasColumnName("RPOCode");
            });

            modelBuilder.Entity<TempBeatPlan>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TempBeatPlanDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempCheckInventoryVisit>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempEvaluationPhotoVisit>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempInventoryItemInfor>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            });

            modelBuilder.Entity<TempInvreport>(entity =>
            {
                entity.ToTable("Temp_INVReports");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempInvreportLot>(entity =>
            {
                entity.ToTable("Temp_INVReportLots");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempKpidistributor>(entity =>
            {
                entity.ToTable("Temp_KPIDistributors");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Dsatarget).HasColumnName("DSATarget");

                entity.Property(e => e.Kpicode).HasColumnName("KPICode");

                entity.Property(e => e.SuggestKpi).HasColumnName("SuggestKPI");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TempKpiemployee>(entity =>
            {
                entity.ToTable("Temp_KPIEmployees");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Dsatarget).HasColumnName("DSATarget");

                entity.Property(e => e.Kpicode).HasColumnName("KPICode");

                entity.Property(e => e.SuggestKpi).HasColumnName("SuggestKPI");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TempParameterWithSitype>(entity =>
            {
                entity.ToTable("Temp_ParameterWithSitypes");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.PcconditionsNoofSku).HasColumnName("PcconditionsNoofSKU");

                entity.Property(e => e.RecognizedDateforSov).HasColumnName("RecognizedDateforSOV");
            });

            modelBuilder.Entity<TempPoKpi>(entity =>
            {
                entity.ToTable("Temp_PoKPIs");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TempProgram>(entity =>
            {
                entity.ToTable("Temp_Programs");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempProgramCustomer>(entity =>
            {
                entity.ToTable("Temp_ProgramCustomers");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EffectiveDate).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");
            });

            modelBuilder.Entity<TempProgramCustomerDetailsItem>(entity =>
            {
                entity.ToTable("Temp_ProgramCustomerDetailsItems");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Uomcode).HasColumnName("UOMCode");

                entity.Property(e => e.Vatcode).HasColumnName("VATCode");
            });

            modelBuilder.Entity<TempProgramCustomerItemsGroup>(entity =>
            {
                entity.ToTable("Temp_ProgramCustomerItemsGroup");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Uomcode).HasColumnName("UOMCode");
            });

            modelBuilder.Entity<TempProgramCustomersDetail>(entity =>
            {
                entity.ToTable("Temp_ProgramCustomersDetails");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempProgramDetailReward>(entity =>
            {
                entity.ToTable("Temp_ProgramDetailReward");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Uomcode).HasColumnName("UOMCode");
            });

            modelBuilder.Entity<TempProgramDetailsItemsGroup>(entity =>
            {
                entity.ToTable("Temp_ProgramDetailsItemsGroup");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Uomcode).HasColumnName("UOMCode");
            });

            modelBuilder.Entity<TempProgramsDetail>(entity =>
            {
                entity.ToTable("Temp_ProgramsDetails");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempPromotionOrderRefNumber>(entity =>
            {
                entity.ToTable("Temp_PromotionOrderRefNumber");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempRoundingRule>(entity =>
            {
                entity.ToTable("Temp_RoundingRules");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempRouteZone>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.Dsaid).HasColumnName("DSAId");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TempSalesIndicatorType>(entity =>
            {
                entity.ToTable("Temp_SalesIndicatorTypes");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LevelSitype).HasColumnName("LevelSIType");
            });

            modelBuilder.Entity<TempTpOrderDetail>(entity =>
            {
                entity.ToTable("Temp_TpOrderDetails");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DiscountId)
                    .HasMaxLength(10)
                    .HasColumnName("DiscountID");

                entity.Property(e => e.DiscountName).HasMaxLength(255);

                entity.Property(e => e.DiscountSchemeId)
                    .HasMaxLength(10)
                    .HasColumnName("DiscountSchemeID");

                entity.Property(e => e.DiscountType).HasMaxLength(10);

                entity.Property(e => e.InventoryId)
                    .HasMaxLength(10)
                    .HasColumnName("InventoryID");

                entity.Property(e => e.OrdNbr)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PromotionLevel).HasMaxLength(10);

                entity.Property(e => e.PromotionLevelName).HasMaxLength(255);

                entity.Property(e => e.Uom)
                    .HasMaxLength(100)
                    .HasColumnName("UOM");

                entity.Property(e => e.Uomname)
                    .HasMaxLength(255)
                    .HasColumnName("UOMName");
            });

            modelBuilder.Entity<TempTpOrderHeader>(entity =>
            {
                entity.ToTable("Temp_TpOrderHeaders");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AreaCode)
                    .HasMaxLength(10)
                    .HasColumnName("Area_Code");

                entity.Property(e => e.AreaManagerCode)
                    .HasMaxLength(10)
                    .HasColumnName("Area_Manager_Code");

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(10)
                    .HasColumnName("Branch_Code");

                entity.Property(e => e.BranchManagerCode)
                    .HasMaxLength(10)
                    .HasColumnName("Branch_Manager_Code");

                entity.Property(e => e.CustomerAttribute0).HasMaxLength(100);

                entity.Property(e => e.CustomerAttribute1).HasMaxLength(100);

                entity.Property(e => e.CustomerAttribute2).HasMaxLength(100);

                entity.Property(e => e.CustomerAttribute3).HasMaxLength(100);

                entity.Property(e => e.CustomerAttribute4).HasMaxLength(100);

                entity.Property(e => e.CustomerAttribute5).HasMaxLength(100);

                entity.Property(e => e.CustomerAttribute6).HasMaxLength(100);

                entity.Property(e => e.CustomerAttribute7).HasMaxLength(100);

                entity.Property(e => e.CustomerAttribute8).HasMaxLength(100);

                entity.Property(e => e.CustomerAttribute9).HasMaxLength(100);

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(10)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.CustomerName).HasMaxLength(255);

                entity.Property(e => e.DiscountCode).HasMaxLength(10);

                entity.Property(e => e.DistyBilltoCode)
                    .HasMaxLength(10)
                    .HasColumnName("Disty_BilltoCode");

                entity.Property(e => e.DistyBilltoName).HasColumnName("Disty_BilltoName");

                entity.Property(e => e.DsaCode)
                    .HasMaxLength(10)
                    .HasColumnName("DSA_Code");

                entity.Property(e => e.DsaManagerCode)
                    .HasMaxLength(10)
                    .HasColumnName("DSA_Manager_Code");

                entity.Property(e => e.NsdCode)
                    .HasMaxLength(10)
                    .HasColumnName("NSD_Code");

                entity.Property(e => e.OrdNbr)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PeriodCode).HasMaxLength(10);

                entity.Property(e => e.PrincipalId)
                    .HasMaxLength(10)
                    .HasColumnName("PrincipalID");

                entity.Property(e => e.RecallOrderCode).HasMaxLength(10);

                entity.Property(e => e.ReferenceLink).HasMaxLength(255);

                entity.Property(e => e.RegionCode)
                    .HasMaxLength(10)
                    .HasColumnName("Region_Code");

                entity.Property(e => e.RegionManagerCode)
                    .HasMaxLength(10)
                    .HasColumnName("Region_Manager_Code");

                entity.Property(e => e.RouteZoneId)
                    .HasMaxLength(10)
                    .HasColumnName("RouteZoneID");

                entity.Property(e => e.RouteZoneName).HasMaxLength(255);

                entity.Property(e => e.RzSuppervisorCode)
                    .HasMaxLength(10)
                    .HasColumnName("RZ_Suppervisor_Code");

                entity.Property(e => e.SalesOrgCode)
                    .HasMaxLength(10)
                    .HasColumnName("SalesOrg_Code");

                entity.Property(e => e.SalesRepCode).HasMaxLength(10);

                entity.Property(e => e.ShiptoId)
                    .HasMaxLength(10)
                    .HasColumnName("ShiptoID");

                entity.Property(e => e.ShiptoName).HasMaxLength(255);

                entity.Property(e => e.SicCode)
                    .HasMaxLength(10)
                    .HasColumnName("SIC_Code");

                entity.Property(e => e.SoShippedDiscAmt).HasColumnName("SO_Shipped_Disc_Amt");

                entity.Property(e => e.Status).HasMaxLength(100);

                entity.Property(e => e.SubAreaCode)
                    .HasMaxLength(10)
                    .HasColumnName("SubArea_Code");

                entity.Property(e => e.SubAreaManagerCode)
                    .HasMaxLength(10)
                    .HasColumnName("Sub_Area_Manager_Code");

                entity.Property(e => e.SubRegionCode)
                    .HasMaxLength(10)
                    .HasColumnName("SubRegion_Code");

                entity.Property(e => e.SubRegionManagerCode)
                    .HasMaxLength(10)
                    .HasColumnName("Sub_Region_Manager_Code");
            });

            modelBuilder.Entity<TempVisitStep>(entity =>
            {
                entity.ToTable("Temp_VisitSteps");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempVisitStepsDefaultResult>(entity =>
            {
                entity.ToTable("Temp_VisitStepsDefaultResults");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TempVisitStepsReasonResult>(entity =>
            {
                entity.ToTable("Temp_VisitStepsReasonResults");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TemporarySic>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status).IsRequired();

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TemporarySicItemGroupDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.TemporarySic)
                    .WithMany(p => p.TemporarySicItemGroupDetails)
                    .HasForeignKey(d => d.TemporarySicId);
            });

            modelBuilder.Entity<TemporarySicKitDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.TemporarySic)
                    .WithMany(p => p.TemporarySicKitDetails)
                    .HasForeignKey(d => d.TemporarySicId);
            });

            modelBuilder.Entity<TerritoryMapping>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.TerritoryStructureCode).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TerritoryStructure>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.UntilDate).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TerritoryStructureDetail>(entity =>
            {
                entity.HasIndex(e => e.TerritoryStructureId, "IX_TerritoryStructureDetails_TerritoryStructureId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.Source).HasMaxLength(20);

                entity.Property(e => e.TerritoryStructureCode).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.TerritoryStructure)
                    .WithMany(p => p.TerritoryStructureDetails)
                    .HasForeignKey(d => d.TerritoryStructureId)
                    .HasConstraintName("FK_TerritoryStructureDetails_TerritoryStructures_TerritoryStru~");
            });

            modelBuilder.Entity<TerritoryValue>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.Source).HasMaxLength(20);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpBudget>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BudgetAllocationForm)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.BudgetAllocationLevel).HasMaxLength(10);

                entity.Property(e => e.BudgetType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.IO)
                    .IsRequired()
                    .HasColumnName("IO");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SaleOrg)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpBudgetAdjustment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Account).HasMaxLength(100);

                entity.Property(e => e.BudgetCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Reason).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpBudgetAllotment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BudgetCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.SalesTerritoryValueCode).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpBudgetAllotmentAdjustment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BudgetCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpBudgetDefine>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BudgetCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.ItemHierarchyLevel).HasMaxLength(100);

                entity.Property(e => e.ItemHierarchyValue).HasMaxLength(100);

                entity.Property(e => e.PackSize).HasMaxLength(100);

                entity.Property(e => e.PromotionProductCode).HasMaxLength(10);

                entity.Property(e => e.PromotionProductType).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpDiscount>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DiscountFrequency)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SaleOrg)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Scheme).HasMaxLength(255);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpDiscountStructure>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DiscountCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.SicCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpDiscountStructureDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DiscountCode).HasMaxLength(10);

                entity.Property(e => e.NameDiscountLevel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SicCode).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpObjectDiscount>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DiscountCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpObjectDiscountDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CustomerShipToCode).HasMaxLength(10);

                entity.Property(e => e.DiscountCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ObjectSalesAttributeDiscountCode).HasMaxLength(10);

                entity.Property(e => e.ObjectType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpObjectSalesAttributeDiscount>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.SalesAttributeCode).HasMaxLength(10);

                entity.Property(e => e.SalesAttributeValueCode).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpPromotion>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ApplicableObjectType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.FrequencyPromotion).HasMaxLength(10);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PromotionType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.SaleOrg)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Scheme)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ScopeType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SicCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpPromotionDefinitionProductForGift>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.LevelCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Packing)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PromotionCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpPromotionDefinitionProductForSale>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.LevelCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Packing)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PromotionCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpPromotionDefinitionStructure>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BudgetForDonation).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.LevelCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.LevelName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ProductTypeForGift).HasMaxLength(10);

                entity.Property(e => e.ProductTypeForSale)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PromotionCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpPromotionObjectCustomerAttributeLevel>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CustomerAttributerLevel)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PromotionCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpPromotionObjectCustomerAttributeValue>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CustomerAttributerLevel)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CustomerAttributerValue)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PromotionCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpPromotionObjectCustomerShipto>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CustomerCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CustomerShiptoCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PromotionCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpPromotionScopeDsa>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.PromotionCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ScopeDsaValue)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpPromotionScopeTerritory>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.PromotionCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.SalesTerritoryValue)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpScopeDiscount>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DiscountCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.SalesTerritoryLevelCode).HasMaxLength(10);

                entity.Property(e => e.ScopeType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpScopeDiscountDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DiscountCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.SalesTerritoryLevelCode).HasMaxLength(10);

                entity.Property(e => e.SalesTerritoryValueCode).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpSettlement>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ProgramType)
                    .IsRequired()
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpSettlementDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DistributorCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.OrdNbr).HasMaxLength(10);

                entity.Property(e => e.ProgramType).IsRequired();

                entity.Property(e => e.PromotionDiscountCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.SettlementCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.ShiptoId).HasColumnName("ShiptoID");

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TpSettlementObject>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.ProgramType).IsRequired();

                entity.Property(e => e.PromotionDiscountCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.SettlementCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<TradePromotion>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<Uom>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.UomId).HasMaxLength(10);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(150);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.LastUpdatedUtc)
                    .HasColumnName("LastUpdatedUTC")
                    .HasDefaultValueSql("'0001-01-01 00:00:00'::timestamp without time zone");

                entity.Property(e => e.LockoutEnd).HasColumnType("timestamp with time zone");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.Property(e => e.UserCode).HasMaxLength(36);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_UserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_UserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserLoginLog>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Agent).HasMaxLength(250);

                entity.Property(e => e.Ip)
                    .HasMaxLength(30)
                    .HasColumnName("IP");

                entity.Property(e => e.Message).HasMaxLength(250);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<UserPolicy>(entity =>
            {
                entity.ToTable("UserPolicy");

                entity.HasIndex(e => e.PolicyId, "IX_UserPolicy_PolicyId");

                entity.HasIndex(e => e.UserId, "IX_UserPolicy_UserId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.UserPolicies)
                    .HasForeignKey(d => d.PolicyId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPolicies)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_UserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(36);
            });

            modelBuilder.Entity<Vat>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.Property(e => e.VatId).HasMaxLength(5);
            });

            modelBuilder.Entity<Version>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.Description).HasMaxLength(150);
            });

            modelBuilder.Entity<VisitStep>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<VpoRefResult>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.Sicode).HasColumnName("SICode");

                entity.Property(e => e.Sitype).HasColumnName("SIType");

                entity.Property(e => e.Sku).HasColumnName("SKU");

                entity.Property(e => e.SovinvoiceValue).HasColumnName("SOVInvoiceValue");

                entity.Property(e => e.SovnoofSku).HasColumnName("SOVNoofSKU");
            });

            modelBuilder.Entity<VvrefResult>(entity =>
            {
                entity.ToTable("VVRefResults");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dsacode).HasColumnName("DSACode");

                entity.Property(e => e.Sicode).HasColumnName("SICode");

                entity.Property(e => e.Sitype).HasColumnName("SIType");
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.ShortName).HasMaxLength(40);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.Property(e => e.WardCode).HasMaxLength(10);
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(80);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<WebNotiMessagese>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<WinzardFeature>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.DetailFormPath).HasMaxLength(255);

                entity.Property(e => e.FeatureCode).HasMaxLength(100);

                entity.Property(e => e.FeatureName).HasMaxLength(255);

                entity.Property(e => e.Icon).HasMaxLength(100);

                entity.Property(e => e.ListFormPath).HasMaxLength(255);

                entity.Property(e => e.NewFormPath).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(100);
            });

            modelBuilder.Entity<WinzardSetting>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DisplayText).HasMaxLength(255);

                entity.Property(e => e.RoutePath).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
