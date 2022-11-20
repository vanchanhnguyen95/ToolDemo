using AutoMapper;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Models.External;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable;
using RDOS.TMK_DisplayAPI.Models.Dis.Temp;
using static RDOS.TMK_DisplayAPI.Models.Dis.ConfirmResultDetailListModel;
using RDOS.TMK_DisplayAPI.Models.Dis.PayReward;

namespace RDOS.TMK_DisplayAPI.Models
{
	 public class AutoMapping : Profile
	 {
		  public AutoMapping()
		  {
				CreateMap<SaleCalendarGenerate, SaleCalendarByTyeModel>();

				CreateMap<TempDisPosmForCustomerShipto, TempDisPosmForCustomerShiptoModel>().ReverseMap();
				CreateMap<TempDisCustomerShiptoSaleOrQuantity, TempDisCustomerShiptoSaleOrQuantityModel>().ReverseMap();
				CreateMap<TempDisCustomerShiptoNotHave, TempDisCustomerShiptoNotHaveModel>().ReverseMap();

				CreateMap<DisCriteriaEvaluatePictureDisplay, DisCriteriaEvaluatePictureDisplayModel>().ReverseMap();
				CreateMap<DisCriteriaEvaluatePictureDisplayRequest, DisCriteriaEvaluatePictureDisplay>().ReverseMap();
				CreateMap<DisBudgetModel, DisBudget>().ReverseMap();
				CreateMap<DisBudgetForScopeTerritoryModel, DisBudgetForScopeTerritory>().ReverseMap();
				CreateMap<DisBudgetForScopeDsaModel, DisBudgetForScopeDsa>().ReverseMap();
				CreateMap<DisBudgetForCusAttributeModel, DisBudgetForCusAttribute>().ReverseMap();
				CreateMap<DisCustomerShiptoModel, DisCustomerShipto>().ReverseMap();
				CreateMap<DisCustomerShiptoDetailModel, DisCustomerShiptoDetail>().ReverseMap();
				CreateMap<DisDisplayModel, DisDisplay>().ReverseMap();
				CreateMap<DisConfirmResult, DisConfirmResultDisplayModel>().ReverseMap();
				CreateMap<DisConfirmResultDetail, DisConfirmResultDetailDisplayModel>().ReverseMap();
				CreateMap<TempDisConfirmResultDetail, TempDisConfirmResultDetailModel>().ReverseMap();
				CreateMap<DisSettlement, DisSettlementModel>().ReverseMap();
				CreateMap<DisSettlementDetail, DisSettlementDetailModel>().ReverseMap();
				CreateMap<TempDisOrderHeader, TempDisOrderHeaderModel>().ReverseMap();
				CreateMap<TempDisOrderDetail, TempDisOrderDetailModel>().ReverseMap();
				CreateMap<DisApproveRegistrationCustomerRequest, DisApproveRegistrationCustomer>().ReverseMap();
				CreateMap<DisApproveRegistrationCustomerDetailRequest, DisApproveRegistrationCustomerDetail>().ReverseMap();
				CreateMap<DisDisplayModel, DisDisplay>().ReverseMap();
				CreateMap<DisDefinitionStructureDataModel, DisDefinitionStructure>().ReverseMap();
				CreateMap<DisDefinitionProductTypeDetailModel, DisDefinitionProductTypeDetail>().ReverseMap();
				CreateMap<DisDefinitionCriteriaEvaluateModel, DisDefinitionCriteriaEvaluate>().ReverseMap();
				CreateMap<DisWeightGetExtraRewardsDetailModel, DisWeightGetExtraRewardsDetail>().ReverseMap();
				CreateMap<DisDefinitionGuideImageModel, DisDefinitionGuideImage>().ReverseMap();
				CreateMap<DisplayHeaderReportModel, DisDisplay>().ReverseMap();
				CreateMap<DisConfirmResultDetailValueModel, TempDisConfirmResultDetail>().ReverseMap();
				CreateMap<DisConfirmResultsModel, DisConfirmResult>().ReverseMap();
				CreateMap<DisConfirmResultDetailValueModel, DisConfirmResultDetail>().ReverseMap();
				CreateMap<ConfirmResultDetailJoiningResponse, ConfirmResultDetailJoinReport>()
					 .ForMember(x => x.Customer, v => v.MapFrom(i => i.CustomerCode))
					 .ForMember(x => x.CustomerShipToName, v => v.MapFrom(i => i.Address))
					 .ForMember(x => x.CustomerShipToCode, v => v.MapFrom(i => i.CustomerShiptoCode))
					 .ReverseMap();
			//CreateMap<DisBudgetReportModel, DisBudget>().ReverseMap();
			CreateMap<DisPayReward, DisPayRewardModel>().ReverseMap();
			CreateMap<DisPayRewardDetail, DisPayRewardDetailModel>().ReverseMap();
		}
	 }
}
