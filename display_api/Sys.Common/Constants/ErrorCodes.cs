namespace Sys.Common.Constants
{
    public static class ErrorCodes
    {
        public const string CreateSuccess = "CreateSuccess";
        public const string CreateFailed = "CreateFailed";
        public const string UpdateSuccess = "UpdateSuccess";
        public const string UpdateFailed = "UpdateFailed";
        public const string DeleteSuccess = "DeleteSuccess";
        public const string DeleteFailed = "DeleteFailed";
        public const string GetDataFailed = "GetDataFailed";
        
        public const string DuplicateCode = "DuplicateCode";
        public const string IdIsRequired = "IdIsRequired";
        public const string EntityNotFound = "EntityNotFound";
        public static class General
        {
            public const string InvalidParam = "GEN_InvalidParam";
            public const string PhoneNotFound = "GEN_PhoneNotFound";
            public const string WrongUsername = "ACC_WrongPhoneNumberOrPassword";
            public const string TokenExpire = "GEN_TokenExpire";
            public const string InvalidToken = "GEN_InvalidToken";
            public const string InvaildUserId = "GEN_InvaildUserId";
            public const string PleaseUpdatePageIndexOrPageSize = "GEN_PleaseUpdatePageIndexOrPageSize";
            public const string InvalidFile = "GEN_InvalidFile";
            public const string CannotGetData = "GEN_DataNotFound";
        }
        public static class S3
        {
            public const string NotExistFile = "S3_NotExistFile";
        }
        public static class Account
        {
            public const string PhoneExist = "ACC_PhoneNumberExist";
            public const string PhoneNotExist = "ACC_PhoneNumberNotExist";
            public const string PhoneNumberLimitDistanceTimeSendSMS = "ACC_PhoneNumberLimitDistanceTimeSendSMS";
            public const string AccountLockedSendSms = "ACC_LockedSendSms";
            public const string AccountLocked = "ACC_Locked";
            public const string AccountTmpLocked = "ACC_TemporaryLock";
            public const string AccountCountryCodeTooLong = "ACC_AccountCountryCodeTooLong";
            public const string AccountCountryIsoTooLong = "ACC_AccountCountryIsoeTooLong";
            public const string AccountReLogin = "ACC_ReLogin";
        }

        public static class Application
        {
            public const string AppNotFound = "APPLICATION_AppNotFound";
            public const string LanguagePackNotFound = "APPLICATION_LanguagePackNotFound";
        }

        public static class ApplicationVersions
        {
            public const string ApplicationVersionsNotFound = "APPLICATION_ApplicationVersionsNotFound";
            public const string ApplicationVersionsExisted = "APPLICATION_ApplicationVersionsExisted";
            public const string ApplicationVersionsExistedInLanguagePack = "APPLICATION_ApplicationVersionsExistedInLanguagePack";
        }

        public static class Principal
        {
            public const string PrincipalNotFound = "PRINCIPAL_PrincipalNotFound";
            public const string OldPrincipalNotFound = "PRINCIPAL_OldPrincipalNotFound";
            public const string PrincipalInActive = "PRINCIPAL_PrincipalInActive";
            public const string InvalidSecretKey = "PRINCIPAL_InvalidSecretKey";
        }

        public static class AppInviteCode
        {
            public const string InviteCodeNotFound = "APPINVITE_InviteCodeNotFound";
            public const string InviteCodeExpired = "APPINVITE_InviteCodeExpired";
            public const string InviteCodeActived = "APPINVITE_InviteCodeActived";
            public const string ChangeStatusPrincipalFailed = "APPINVITE_ChangeStatusPrincipalFailed";
            public const string InsertPrincipalFailed = "APPINVITE_InsertPrincipalFailed";
            public const string InvalidInviteCodeUrl = "APPINVITE_InvalidPrincipalUrl";
            public const string AddFailedAtBase = "APPINVITE_AddFailedAtBase";
            public const string DeleteEmployeeInOldPrincipalFail = "APPINVITE_DeleteEmployeeInOldPrincipalFail";
        }

        public static class MobileUser
        {
            public const string UserNotFound = "USER_UserNotFound";
            public const string UserInActive = "USER_UserInActive";
            public const string UserRegistered = "USER_UserRegistered";
            public const string UserLocked = "USER_UserLocked";
            public const string UserNotLocked = "USER_UserNotLocked";
        }

        public static class MobileUserPrincipal
        {
            public const string AddFailed = "USERPRINCIPAL_AddFailed";
            public const string UserExistedInBase = "USERPRINCIPAL_UserExisted";
            public const string UserExistedInOthersMess = "USERPRINCIPAL_UserExistedInOthers_{0}";
            public const string UserExistedInOldPrincipalMess = "USERPRINCIPAL_UserExistedInOldPrincipal_{0}_{1}";
            public const string UserExistedInPrincipal = "USERPRINCIPAL_UserExistedPrincipal";
            public const string UserNotExistedInPrincipal = "USERPRINCIPAL_UserNotExistedInPrincipal";
        }

        public static class MobileUserPrincipalHis
        {
            public const string AddFailed = "USERPRINCIPALHIS_AddFailed";
        }

        public static class MobileUserEmployee
        {
            public const string AddFailed = "USEREMPLOYEE_AddFailed";
            public const string NotFound = "USEREMPLOYEE_NotFound";
            public const string DisablePrincipalFailedAtBase = "USEREMPLOYEE_DisablePrincipalFailedAtBase";
        }

        public static class MobileUserApp
        {
            public const string UserNotFoundInApp = "USERAPP_UserNotFoundInApp";
        }

        public static class MobileUserInfo
        {
            public const string UserNotFound = "USERINFO_UserNotFound";
        }

        public static class MobileUserDevice
        {
            public const string DeviceNotFound = "USERDEVICE_DeviceNotFound";
            public const string PhoneNumberNotAccept = "USERDEVICE_PhoneNumberNotAccept";
        }

        public static class MobileUserSetting
        {
            public const string SettingNotFound = "USERSETTING_SettingNotFound";
        }

        public static class RefreshToken
        {
            public const string UserNotFound = "REFRESHTOKEN_SettingNotFound";
        }

        public static class PrincipleEmployee
        {
            public const string EmployeeNotFound = "PRINCIPALEMPLOYEE_EmployeeNotFound";
        }

        public static class SystemSetting
        {
            public const string TERM_NOTFOUND = "ERR_NotFoundTerm";
            public const string InvalidExpirationSeconds = "ERR_InvalidExpirationSeconds";
            public const string AuthRdosNotFound = "ERR_AuthRdosNotFound";
            public const string MobileActiveLinkNotFound = "ERR_MobileActiveLinkNotFound";
            public const string SOME_CONFIG_NOTFOUND = "ERR_SomeConfigNotFound";
        }

        public static class Device
        {
            public const string ExistInAnotherDevice = "ACC_ExistInAnotherDevice";
            public const string NotFoundInfo = "DeviceNotFound";
            public const string AppNotExist = "Application_NotExist";
            public const string BlockOldDevice = "Block_OldDevice";
        }

        public static class ActivePrincipal
        {
            public const string GetFailed = "ACTIVEPRINCIPAL_Failed";
            public const string InvalidPrincipalUrl = "ACTIVEPRINCIPAL_InvalidPrincipalUrl";
        }

        public static class RDosInfo
        {
            public const string InvalidURL = "RDOS_InvalidURL";
        }

        public static class MobileUserLock
        {
            public const string DeviceLockedNotFound = "MOBILEUSERLOCK_DeviceLockedNotFound";
        }

        public static class ApplicationNotiDeviceToken
        {
            public const string TemplateNotificationNotFound = "APPNOTIDEVICETOKEN_TemplateNotificationNotFound";
            public const string DeviceTokenNotFound = "APPNOTIDEVICETOKEN_DeviceTokenNotFound";
        }

        public static class LanguagePack
        {
            public const string LanguagePackByAppIdNotFound = "LANGUAGEPACKGE_LanguagePackByAppIdNotFound";
            public const string LanguagePackNotFound = "LANGUAGEPACKGE_LanguagePackNotFound";
            public const string LanguagePackCannotUpdateStatusPublished = "LANGUAGEPACKGE_LanguagePackCannotUpdateStatusPublished";
        }

        public static class AppTheme
        {
            public const string AppThemeElementByAppIdNotFound = "APPTHEME_AppThemeElementByAppIdNotFound";
            public const string AppThemeSugesstionByAppIdNotFound = "APPTHEME_AppThemeSugesstionByAppIdNotFound";
            public const string AppThemeConfigByAppIdNotFound = "APPTHEME_AppThemeConfigByAppIdNotFound";
            public const string AppThemeConfigNotFound = "APPTHEME_AppThemeConfigNotFound";
            public const string AppThemeSettingNotFound = "APPTHEME_AppThemeSettingNotFound";
            public const string AppThemeHasOneApplied = "APPTHEME_AppThemeHasOneApplied";
        }

        public enum ELogLevel
        {
            Infomation = 1,
            Warning = 2,
            Exception = 3
        }
        public enum ErrorCode
        {
            ApplicationInviteCode = 84000,
            PrincipleEmployee = 83000,
            MobileUserEmployee = 82000,
            AppNotiMessage = 88000,

            AppTheme = 81000,
            AppVersion = 80000,
            InviteCode = 79000,
            LanguagePack = 78000,
            Localization = 77000,
            MobileTheme = 76000,

            TpPromotion = 20000,
            SettlementCode = 57000,
        }

        #region TP
        public enum PromotionError
        {
            ListPromotionDiscountFailed = 80001,
            PromotionCodeIsExist = 80002,
            CreatePromotionFailed = 80003,
            GetListPromotionPopupFailed = 80004,
            GetGeneralPromotionByCodeFailed = 80005,
            GetDetailPromotionByCodeFailed = 80006,
            UpdatePromotionFailed = 80007,
            PromotionCodeIsNotExist = 80008,
            DeletePromotionByCodeFailed = 80009,
            GetListProductForGiftByPromotionCodeFailed = 80010,
            GetListBudgetByPromotionCodeFailed = 80011,
            GetSettlementPromotionByPromotionCodeFailed = 80012,
            GetListSchemePromotionBySchemeFailed = 80013
        }

        public enum BudgetError
        {
            ListBudgetSuccess = 100000,
            ListBudgetFailed = 100001,
            GetBudgetFailed = 100002,
            CreateBudgetFailed = 100003,
            CreateBudgetFailedCodeExist = 100004,
            CreateBudgetSuccess = 100005,
            UpdateBudgetFailed = 100006,
            UpdateBudgetFailedBudgetNotExist = 100007,
            UpdateBudgetFailedCodeExist = 100008,
            UpdateBudgetSuccess = 100009,
            DeleteBudgetFailed = 100010,
            DeleteBudgetFailedBudgetNotExist = 100011,

            ValidateBudgetFailed = 100012,
            ValidateBudgetSuccess = 100013,
            NoDataBudget = 100014
        }

        public enum BudgetAdjustmentError
        {
            CreateBudgetAdjustmentFailed = 100021,
            CreateBudgetAdjustmentFailedBudgetCodeNotExist = 100022,
            GetListBudgetAdjustmentFailed = 100023,
            GetBudgetAdjustmentFailed = 100024
        }

        public enum TpDiscountError
        {
            ListTpDiscountSuccess = 32000,
            CreateTpDiscountFailed = 32001,
            CreateTpDiscountFailedTpDiscountCodeExist = 32018,
            CreateTpDiscountSuccess = 32002,
            UpdateTpDiscountFailed = 32003,
            UpdateTpDiscountFailedTpDiscountNotExist = 32019,
            UpdateTpDiscountSuccess = 32004,
            GetTpDiscountFailed = 32005,
            ListTpDiscountFailed = 32006,
            DeleteTpDiscountFailed = 32007,
            DeleteTpDiscountFailedTpDiscountNotExist = 32008,

            ValidateTpDiscountFailed = 32009,
            ValidateTpDiscountSuccess = 32010,
            NoDataTpDiscount = 32011,
            ImportExcelTpDiscountFailed = 32012,
            ImportExcelTpDiscountValidateStructureFileFailed = 32013,
            ImportExcelTpDiscountDataValidationFailed = 32014,
            ImportExcelTpDiscountSuccess = 32015,
            InsertUpdateTpDiscountFromExcelSuccess = 32016,
            InsertUpdateTpDiscountFromExcelFailed = 32017,
            GetTpDiscountForSettlementByDiscountCodeFailed = 32018
        }

        public enum TpSettlementError
        {
            ListSettlementFailed = 570001,
            ListSchemaFailed = 570002,
            SuggestionCodeFailed = 570003,
            ListSalePeriodFailed = 570004,
            DeleteSettlementFailedSettlementNotExist = 570005,
            DeleteSettlementFailed = 570006,
            SettlementCodeIsExist = 570007,
            CreateSettlementFailed = 570008,
            SettlementCodeDoesNotExist = 570009,
            UpdateSettlementFailed = 570010,
            DeleteBudgetByCodeFailed = 570011,
            ListSettlementConfirmFailed = 570012,
            GetSettlementByPromotionCodeBySaleCalendarFailed = 570013,
            GetSettlementByDiscountCodeBySaleCalendarFailed = 570014
        }

        public enum ExternalError
        {
            GetListCalendarBySalePeriodFailed = 60000,
            GetCalendarGenerateByCodeFailed = 60001,
            GetListDistributorByUserCodeFailed = 60002,
            GetCalendarByTypeByDateFailed = 60003
        }

        public enum SalesOrgError
        {
            GetListSalesOrgFailed = 90001,
            GetListSalesTerritoryLevelFailed = 90002,
            GetListSalesTerritoryValueFailed = 90003,
            GetListSalesDsaValueFailed = 90004,
        }

        public enum TempOrderError
        {
            GetOrderDistributorInfoByPromotionFailed = 91001,
            GetOrderDistributorInfoByListPromotionFailed = 91002,
            GetOrderDistributorInfoByDiscountFailed = 91003,
        }
        
        public enum CustomerError
        {
            GetListCustomerAttributeByCustomerSettingIdFailed = 92001,
        }

        public enum DisplayError
        {
            ListDisplayFailed = 100001,
            GetDisplayFailed = 100002,
            UpdateDisplayFailedCodeExist = 100003,
            UpdateDisplayFailed = 100006,
            GetListDisplayScopeByDisplayCodeFailed = 100007,
            GetListDisplayCustomerAttributeByDisplayCodeFailed = 100008,
            GetListCustomerShiptoByScopeFailed = 100009,
            ConfirmDisplayFailed = 100010
        }

        public enum DisPayRewardError
        {
            ListPayRewardFailed = 110001,
            GetPayRewardFailed = 110002,
            UpdatePayRewardFailedCodeExist = 110003,
        }

        public enum DisSettlementError
        {
            ListSettlementFailed = 570001,
            SuggestionCodeFailed = 570003,
            ListSalePeriodFailed = 570004,
            UpdateSettlementFailed = 570010,
            DeleteSettlementFailed = 570006,
            ListSettlementConfirmFailed = 570012,
            GetListSettlementConfirmByDistributorFailed = 100002,
        }
        #endregion
    }
}