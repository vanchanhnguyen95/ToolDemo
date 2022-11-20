using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis.Report;
using RDOS.TMK_DisplayAPI.Models.SalesOrg;
using RDOS.TMK_DisplayAPI.Services.Base;
using Sys.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis.Report
{
    public class DisplayPeriodTrackingReportService : IDisplayPeriodTrackingReportService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DisplayService> _logger;
        private readonly IBaseRepository<DisPayReward> _repository;
        private readonly IBaseRepository<DisPayRewardDetail> _repositoryDetail;

        public DisplayPeriodTrackingReportService(IMapper mapper,
            ILogger<DisplayService> logger,
            IBaseRepository<DisPayReward> repository,
            IBaseRepository<DisPayRewardDetail> repositoryDetail,
            IBaseRepository<DisDisplay> repositoryDisPlay)
        {
            _mapper = mapper;
            _logger = logger;
            _repository = repository;
            _repositoryDetail = repositoryDetail;
        }

        public async Task<IQueryable<DisplayPeriodTrackingReportListModel>> GetListDisplayPeriodTrackingReportAsync(string DisplayCode)
        {
            var dataDisPayReward = await _repository.GetAllQueryable(x => x.DisplayCode == DisplayCode).AsNoTracking().ToListAsync();
            var dataDisPayRewardDetail = await _repositoryDetail.GetAllQueryable().AsNoTracking().ToListAsync();
            var result = (from payReward in dataDisPayReward
                          join detail in dataDisPayRewardDetail on payReward.Code equals detail.DisPayRewardCode into emptypayRewardDetail
                          from detail in emptypayRewardDetail.DefaultIfEmpty()
                          select new DisplayPeriodTrackingReportListModel()
                          {
                              DisplayCode = DisplayCode,
                              Code = payReward.Code,
                              Name = payReward.Name,
                              StartDate = payReward.StartDate,
                              EndDate = payReward.EndDate,
                              ProductCode = detail?.ProductCode,
                              ProductDescription = "",
                              Packing = "",
                              PackingDescription = "",
                              Quantity = detail != null && detail.Quantity.HasValue ? detail.Quantity.Value : 0,
                              Amount = detail != null && detail.Amount.HasValue ? detail.Amount.Value : 0,
                          }).AsQueryable();
            return await Task.FromResult(result);
        }
    }
}
