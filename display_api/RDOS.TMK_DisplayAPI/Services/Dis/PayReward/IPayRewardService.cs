using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis.PayReward;
using RDOS.TMK_DisplayAPI.Models.Dis.Report;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis.PayReward
{
    public interface IPayRewardService
    {
        public Task<IQueryable<DisPayReward>> GetListPayReward();
        public Task<DisPayRewardModel> GetPayRewardByCode(string code);
        public Task<DisPayRewardModel> FindByCodeAndDisplayCodeAsync(string code, string displayCode);
        public Task<DisPayRewardModel> CreateOrUpdate(RequestPayRewardModel request);
        public BaseResultModel SoftDelete(string PayRewardCode);
        public BaseResultModel Delete(string PayRewardCode);
        public List<DisPayRewardDetailModel> GetListPayRewardDetail(RequestPayRewardDetailModel request);
		  Task<IQueryable<PayRewardReportResponse>> GetPayRewardReportByLevelAsync(string levelCode, string comfirmResultCode);
		  Task<IQueryable<PayRewardReportLevelGrouped>> GetPayRewardLevelGroupedAsync(string comfirmResultCode);
		  Task<IQueryable<PayRewardReportResponse>> GetPayRewardReportHeaderAsync(string comfirmResultCode);

        Task<IQueryable<DisplayPeriodTrackingReportListModel>> GetListDisplayPeriodTrackingReportAsync(string DisplayCode);
        Task<IQueryable<DisFollowRewardProgressQuantityCustomerModel>> GetListQuantityCustomerAsync(string DisplayCode);
    }
}
