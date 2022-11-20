using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure.DisTempTable;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RDOS.TMK_DisplayAPI.Models.Dis.ConfirmResultDetailListModel;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public class TempDisConfirmResultDetailService : ITempDisConfirmResultDetailService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TempDisConfirmResultDetailService> _logger;
        private readonly IBaseRepository<TempDisConfirmResultDetail> _repository;

        public TempDisConfirmResultDetailService(IMapper mapper, IBaseRepository<TempDisConfirmResultDetail> repository, ILogger<TempDisConfirmResultDetailService> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<DisConfirmResultDetailValueModel>> GetListTempDisConfirmResult(string DisplayCode, string PeriodCode)
        {
            var listresult = _mapper.Map<List<DisConfirmResultDetailValueModel>>(await _repository.GetAllQueryable(x => x.DisplayCode == DisplayCode && x.PeriodCode == PeriodCode).OrderBy(x=> x.CustomerCode).ThenBy(y=>y.CustomerShiptoCode).ToListAsync());
            return listresult;
        }

        public Task<IQueryable<TempDisConfirmResultDetail>> GetListTempDisConfirmResultDetailAsync()
        {
            var listTempResultDetail = _repository.GetAllQueryable().AsNoTracking();
            return Task.FromResult(listTempResultDetail);
        }

    }
}