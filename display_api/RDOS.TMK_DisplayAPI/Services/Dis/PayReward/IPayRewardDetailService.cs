using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Dis.PayReward
{
    public interface IPayRewardDetailService
    {
        IQueryable<DisPayRewardDetail> PayRewardDetails { get; }
        IQueryable<DisPayRewardDetail> GetPayRewardDetailsByPayRewardCode(string payRewardCode);
    }
}
