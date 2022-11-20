using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Customer;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis.Temp;
using RDOS.TMK_DisplayAPI.Services.Base;
using Sys.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RDOS.TMK_DisplayAPI.Models.Dis.ConfirmResultDetailListModel;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public class DisConfirmResultService : IDisConfirmResultService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DisConfirmResultService> _logger;
        private readonly IBaseRepository<DisConfirmResult> _confirmResult;
        private readonly IBaseRepository<DisConfirmResultDetail> _confirmDetail;
        private readonly IBaseRepository<SystemSetting> _systemSettingService;
        private readonly IBaseRepository<DisDisplay> _displayService;
        private readonly IBaseRepository<DisDefinitionStructure> _displayStructureService;
        private readonly IBaseRepository<CustomerInformation> _customerInfoService;
        private readonly IBaseRepository<CustomerShipto> _customerShiptoService;

        public DisConfirmResultService(IMapper mapper
            , IBaseRepository<DisConfirmResult> serviceConfirmResult
            , IBaseRepository<DisConfirmResultDetail> confirmDetail
            , ILogger<DisConfirmResultService> logger
            , IBaseRepository<SystemSetting> systemSettingService
            , IBaseRepository<DisDisplay> displayService
            , IBaseRepository<DisDefinitionStructure> displayStructureService
            , IBaseRepository<CustomerInformation> customerInfoService
            , IBaseRepository<CustomerShipto> customerShiptoService
            )
        {
            _mapper = mapper;
            _confirmResult = serviceConfirmResult;
            _confirmDetail = confirmDetail;
            _logger = logger;
            _systemSettingService = systemSettingService;
            _displayService = displayService;
            _displayStructureService = displayStructureService;
            _customerInfoService = customerInfoService;
            _customerShiptoService = customerShiptoService;
        }

        public Task<IQueryable<DisConfirmResult>> GetListConfirmResultAsync()
        {
            var listConfirmResult = _confirmResult.GetAllQueryable().AsNoTracking();
            return Task.FromResult(listConfirmResult);
        }

        public Task<IQueryable<DisConfirmResultsModel>> GetListConfirmResultViewAsync()
        {
            var systemSettings = _systemSettingService.GetAllQueryable(x => x.IsActive && x.SettingType == CommonData.SystemSetting.ConfirmResultStatus).AsQueryable();
            var listResult = (
                from d in _confirmResult.GetAllQueryable(x => x.DeleteFlag == 0).AsNoTracking()
                join s in systemSettings on d.Status equals s.SettingKey
                select new DisConfirmResultsModel
                {
                    Id = d.Id,
                    Code = d.Code,
                    Status = d.Status,
                    StatusDes = s.Description,
                    Description = d.Description,
                    DisplayCode = d.DisplayCode,
                    SalesCalendarCode = d.SalesCalendarCode,
                    IsNumberVisits = d.IsNumberVisits,
                    NumberVisitsType = d.NumberVisitsType.Value,
                    PercentPass = d.PercentPass,
                    StatusName = s.Description
                }).AsQueryable();
            return Task.FromResult(listResult); 
        }

        public async Task<DisConfirmResultsModel> GetConfirmResultByCodeAsync(string code)
        {
            DisConfirmResultsModel result = new DisConfirmResultsModel();
            var disConfirmResult = _confirmResult.FirstOrDefault(x => x.Code == code && x.DeleteFlag == 0);

            if (disConfirmResult != null)
            {
                var display = _displayService.FirstOrDefault(x => x.Code == disConfirmResult.DisplayCode && x.DeleteFlag == 0);
                var displayStructure = _displayStructureService.GetAllQueryable(x => x.DeleteFlag == 0 && x.DisplayCode == disConfirmResult.DisplayCode);

                var customerShiptos = (from cs in _customerShiptoService.GetAllQueryable()
                                       join ci in _customerInfoService.GetAllQueryable()
                                       on cs.CustomerInfomationId equals ci.Id
                                       select new TempCustomerShiptoModel()
                                       {
                                           CustomerCode = ci.CustomerCode,
                                           CustomerName = ci.FullName,
                                           CustomerShiptoCode = cs.ShiptoCode,
                                           CustomerShiptoAddress = cs.Address
                                       }).AsQueryable();


                var disConfirmResultDetails = await (from dcrd in _confirmDetail.GetAllQueryable(x => x.DeleteFlag == 0 && x.DisConfirmResultCode == code)
                                               join cs in customerShiptos
                                               on new { dcrd.CustomerCode, dcrd.CustomerShiptoCode } equals new { cs.CustomerCode, cs.CustomerShiptoCode }
                                               join dstruc in displayStructure
                                               on dcrd.DisplayLevelCode equals dstruc.LevelCode
                                               select new DisConfirmResultDetailValueModel()
                                               {
                                                   Id = dcrd.Id,
                                                   DisConfirmResultCode = dcrd.DisConfirmResultCode,
                                                   DisplayCode = dcrd.DisplayCode,
                                                   DisplayName = display.FullName,
                                                   DisplayLevelCode = dcrd.DisplayLevelCode,
                                                   DisplayLevelName = dstruc.LevelName,
                                                   IsCheckSalesOutput = display.IsCheckSalesOutput,
                                                   SalesOutput = display.SalesOutput,
                                                   IndependentDisplay = display.IndependentDisplay,
                                                   SalesRegistered = dstruc.SalesToBeAchieved.HasValue ? dstruc.SalesToBeAchieved.Value : 0,
                                                   OutputRegistered = dstruc.OutputToBeAchieved.HasValue ? dstruc.OutputToBeAchieved.Value : 0,
                                                   PeriodCode = disConfirmResult.SalesCalendarCode,
                                                   CustomerCode = dcrd.CustomerCode,
                                                   CustomerName = cs.CustomerName,
                                                   CustomerShiptoCode = dcrd.CustomerShiptoCode,
                                                   CustomerAddress = cs.CustomerShiptoAddress,
                                                   NumberMustRating = dcrd.NumberMustRating,
                                                   NumberHasEvaluate = dcrd.NumberHasEvaluate,
                                                   NumberPassed = dcrd.NumberPassed,
                                                   DisplayImageResult = dcrd.DisplayImageResult,
                                                   DisplayImageResultDes = dcrd.DisplayImageResultDes,
                                                   DisplaySalesResult = dcrd.DisplaySalesResult,
                                                   DisplaySalesResultDes = dcrd.DisplaySalesResultDes,
                                                   AssessmentPeriodResult = dcrd.AssessmentPeriodResult,
                                                   AssessmentPeriodResultDes = dcrd.AssessmentPeriodResultDes
                                               }).ToListAsync() ;

                result = _mapper.Map<DisConfirmResultsModel>(disConfirmResult);
                result.DisConfirmResultDetail = disConfirmResultDetails;
            }
            
            return result;
        }
        public async Task<List<DisConfirmResultDisplayModel>> GetConfirmResultByDisplayCodeAsync(string DisCode)
        {
            var disConfirmResult = await _confirmResult.GetAllQueryable(x => x.DisplayCode == DisCode 
            && x.DeleteFlag == 0 
            && x.Status == CommonData.DisConfirmResult.StatusConfirm).AsNoTracking().ToListAsync();

            var lstResult = new List<DisConfirmResultDisplayModel>();

            foreach (var item in disConfirmResult)
            {
                var tmp = _mapper.Map<DisConfirmResultDisplayModel>(item);
                lstResult.Add(tmp);
            }
            return lstResult;
        }

        public DisConfirmResultDisplayModel GetConfirmResultByCode(string code)
        {
            var returnValue = _mapper.Map<DisConfirmResultDisplayModel>(_confirmResult.GetAllQueryable().AsNoTracking()
               .Where(m => m.Code == code && m.DeleteFlag == 0).FirstOrDefault());
            return returnValue;
        }

        public async Task<DisConfirmResultDisplayModel> GetConfirmResultByDisplayCodeSaleCalendar(TempDisplayConfirmRequestModel parameters)
        {
            var disConfirmResult = await _confirmResult.GetAllQueryable(
                x => x.DisplayCode == parameters.DisplayCode && x.DeleteFlag == 0
            && x.SalesCalendarCode == parameters.SalesCalendarCode).AsNoTracking().FirstOrDefaultAsync();
            return _mapper.Map<DisConfirmResultDisplayModel>(disConfirmResult);
        }

        public void DeleteConfirmResult(DisConfirmResultDisplayModel input, string userlogin)
        {
            // Update ConfirmResult
            var confirmResult = _mapper.Map<DisConfirmResult>(input);
            confirmResult.DeleteFlag = 1;
            _confirmResult.Update(confirmResult);

            // Update ConfirmResultDetail
            List<DisConfirmResultDetail> lstUpdate = _confirmDetail.GetAllQueryable(x => x.DisConfirmResultCode == confirmResult.Code).ToList();
            lstUpdate.ForEach(x => { x.DeleteFlag = 1; });
            _confirmDetail.UpdateRange(lstUpdate);
        }

        public void CreateDisConfirmResult(DisConfirmResultsModel input, string userlogin)
        {
            // Create ConfirmResult
            var confirmResult = _mapper.Map<DisConfirmResult>(input);

            confirmResult.Id = Guid.NewGuid();
            confirmResult.CreatedBy = userlogin;
            confirmResult.CreatedDate = DateTime.Now;
            confirmResult.DeleteFlag = 0;
            _confirmResult.Insert(confirmResult);

            // Create ConfirmResultDetail
            var lstConfirmDetail = _mapper.Map<List<DisConfirmResultDetail>>(input.DisConfirmResultDetail);
            lstConfirmDetail.ForEach(x => x.Id = Guid.NewGuid());
            _confirmDetail.InsertRange(lstConfirmDetail);
        }

        public void UpdateDisConfirmResult(DisConfirmResultsModel input, string userlogin)
        {
            var confirmResult = _confirmResult.FirstOrDefault(x => x.Code.ToLower().Equals(input.Code.ToLower()) && x.DeleteFlag == 0);
            if (confirmResult != null)
            {
                var id = confirmResult.Id;
                confirmResult = _mapper.Map<DisConfirmResult>(input);
                confirmResult.UpdatedBy = userlogin;
                confirmResult.UpdatedDate = DateTime.Now;
                confirmResult.Id = id;
                _confirmResult.Update(confirmResult);
            }
        }
    }
}