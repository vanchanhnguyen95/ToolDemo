using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Services.Base;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Dis.PayReward
{
    public class PayRewardDetailService : IPayRewardDetailService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PayRewardDetailService> _logger;
        private readonly IBaseRepository<DisPayRewardDetail> _repository;

        public PayRewardDetailService(IMapper mapper, ILogger<PayRewardDetailService> logger, IBaseRepository<DisPayRewardDetail> repository)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
        }

        public IQueryable<DisPayRewardDetail> PayRewardDetails
            => _repository.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking();

        public IQueryable<DisPayRewardDetail> GetPayRewardDetailsByPayRewardCode(string payRewardCode)
        {
            var payResultDetails = PayRewardDetails.Where(x => x.DisPayRewardCode == payRewardCode);
            return payResultDetails;
        }
    }
}
