using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Common.Constants
{
    public static class CommonData
    {
        public static class SystemSetting
        {
            // Promotion
            public const string PromotionType = "PROMOTION_TYPE";
            public const string ApplicableObject = "TMK_APPLICABLE_OBJECT";
            public const string MktScope = "TMK_SCOPE";
            public const string Frequency = "PROMOTION_SALECALENDAR";
            public const string Object = "TMK_APPLICABLE_OBJECT";
            public const string TpType = "TPTYPE";
            public const string SlsFreequencySettlement = "SETTLEMENT_FREQUENCY";
            public const string PromotionStatus = "PROMOTION_STATUS";
            public const string TmkApplicable = "TMKAPPLICABLE";


            public const string TpStatus = "TPSTATUS";
            public const string BudgetType = "BUDGET_TYPE";
            public const string SlStatus = "SLSTATUS";
            public const string BudgetAllocationForm = "BUDGET_ALLOCATION_FORM";
            public const string ScopeType = "TMK_SCOPE";
            public const string ApplicationObject = "TMK_APPLICABLE_OBJECT";
            public const string SettlementPromotionStatus = "SETTLEMENT_PROMOTION_STATUS";
            public const string DistributorConfirmSettlement = "DISTRIBUTOR_CONFIRM_SETTLEMENT";

            //Display
            public const string DisplayStatus = "DISSTATUS";
            public const string DisFrequency = "FREQUENCY";
            public const string ConfirmResultStatus = "CONFIRM_RESULT_STATUS";
            public const string DisplayMaintenanceStatus = "DISSTATUS";
            public const string SettlementStatus = "SETTLEMENT_STATUS";

            //display evaluation criteria
            public const string DisEvaluationCriteriaStatus = "DIS_EVALUATION_CRITERIA_STATUS";
            public const string DisEvaluationCriteriaResult = "DIS_EVALUATION_CRITERIA_RESULT";
        }

        public static class Status
        {
            public const string Active = "01";
            public const string InActive = "02";
        }

        public static class PromotionSetting
        {
            //ProgramType
            public const string PromotionProgram = "01";
            public const string DiscountProgram = "02";

            // Status Budget for Promotion
            public const string StatusDefining = "01";
            public const string StatusCanLinkPromotion = "02";
            public const string StatusLinkedPromotion = "03";

            // Scope
            public const string ScopeNationwide = "01";
            public const string ScopeSalesTerritoryLevel = "02";
            public const string ScopeDSA = "03";

            // Applicable Object
            public const string ObjectAllSalePoint = "01";
            public const string ObjectCustomerAttributes = "02";
            public const string ObjectCustomerShipto = "03";

            //PromotionProductType
            public const string SKU = "01";
            public const string ItemGroup = "02";
            public const string ItemHierarchyValue = "03";

            // status Promotion
            public const string Inprogress = "01";
            public const string WaitConfirm = "02";
            public const string Confirmed = "03";
            public const string Refuse = "04";

            public const int AccordingToTheProgram = 1;
            public const int SaleCalendar = 2;
        }

        public static class SettlementDisplay
        {
            // status Settlement
            public const string Inprogress = "01";
            public const string WaitConfirm = "02";
            public const string confirmed = "03";

            public const string Create = "01";
            public const string Confirm = "02";
        }

        public static class DisplaySetting
        {
            // TMK Type
            public const string Promotion = "01";
            public const string Display = "02";

            // Status Display
            public const string Inprogress = "01";
            public const string Confirmed = "02";
            public const string Register = "03";
            public const string Implementation = "04";
            public const string Closed = "05";

            // Scope
            public const string ScopeNationwide = "01";
            public const string ScopeSalesTerritoryLevel = "02";
            public const string ScopeDSA = "03";

            // Object
            public const string ObjectAllSalePoint = "01";
            public const string ObjectCustomerAttributes = "02";
            public const string ObjectCustomerShipto = "03";

            // Type DisplayBudget
            public const int TypeBudgetNow = 1;
            public const int TypeBudgetAdjustment = 2;
            public const int TypeBudgetAllotmentAdjustment = 3;

            // Product type
            public const string ProductForDisplay = "1";
            public const string ProductForReward = "2";
            public const string ProductForSalesOut = "3";
            public const string ProductForRewardReview = "4";

            //Display ProductType
            public const string SKU = "01";
            public const string ItemGroup = "02";
            public const string ItemHierarchyValue = "03";

            // TempOrder
            public const string StatusActive = "01";
            public const string StatusInActive = "02";

            // Sale Calendar Type
            public const string WEEK = "WEEK";
            public const string MONTH = "MONTH";
            public const string QUARTER = "QUARTER";
            public const string YEAR = "YEAR";

            public const string WEEK_VALUE = "01";
            public const string MONTH_VALUE = "02";
            public const string QUARTER_VALUE = "03";
            public const string YEAR_VALUE = "04";
        }

        public static class SettlementSetting
        {
            public const string Defining = "01";
            public const string WaitConfirm = "02";
            public const string Confirmed = "03";
        }

        public static class TmktypeSetting
        {
            public const string Promotion = "01";
            public const string Display = "02";
            public const string Posm = "03";
        }

        public static class TerritoryLevelSetting
        {
            public const string Branch = "TL01";
            public const string Region = "TL02";
            public const string SubRegion = "TL03";
            public const string Area = "TL04";
            public const string SubArea = "TL05";
        }

        public static class DisConfirmResult
        {
            public const string StatusDefining = "01";
            public const string StatusConfirm = "02";


        }

        public static class PayRewardType
        {
            public const int PRODUCT = 0;
            public const int DONATE = 1;
            public const int DONATEPERCENT = 2;
        }
        public static class PayRewardTypeByStructure
        {
            public const string DisplayReward = "01";
            public const string SalesOutputReward = "02";
            public const string WeightReward = "03";
        }
        public static class PayRewardSetting
        {
            public const string Defining = "01";
            public const string Confirm = "02";
        }

        public class ItemSetting
        {
            public const string IT01 = "IT01";
            public const string IT02 = "IT02";
            public const string IT03 = "IT03";
            public const string IT04 = "IT04";
            public const string IT05 = "IT05";
            public const string IT06 = "IT06";
            public const string IT07 = "IT07";
            public const string IT08 = "IT08";
            public const string IT09 = "IT09";
            public const string IT10 = "IT10";
        }
    }
}
