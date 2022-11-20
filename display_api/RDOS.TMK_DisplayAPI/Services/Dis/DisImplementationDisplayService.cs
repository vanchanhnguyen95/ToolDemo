using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Services.Base;
using Sys.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public class DisImplementationDisplayService : IDisImplementationDisplayService
    {
        #region Property
        private readonly ILogger<DisImplementationDisplayService> _logger;
        private readonly IBaseRepository<DisDisplay> _serviceDisplay;
        private readonly IBaseRepository<SystemSetting> _systemSettingService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public DisImplementationDisplayService(ILogger<DisImplementationDisplayService> logger,
            IBaseRepository<DisDisplay> serviceDisplay,
            IBaseRepository<SystemSetting> systemSettingService,
            IMapper mapper)
        {
            _logger = logger;
            _serviceDisplay = serviceDisplay;
            _systemSettingService = systemSettingService;
            _mapper = mapper;
        }
        #endregion

        #region Method

        public DisDisplayModel GetDisplayByCode(string code)
        {
            var systemSettings = _systemSettingService.GetAllQueryable(x => x.IsActive).AsQueryable();
            return (from d in _serviceDisplay.GetAllQueryable(x => x.DeleteFlag == 0 && x.Code == code).AsNoTracking()
                    join statusObj in systemSettings.Where(x => x.SettingType == CommonData.SystemSetting.DisplayMaintenanceStatus)
                    .AsNoTracking() on d.Status equals statusObj.SettingKey
                    select new DisDisplayModel
                    {
                        Id = d.Id,
                        Code = d.Code,
                        FullName = d.FullName,
                        Name = d.FullName,
                        Status = d.Status,
                        StatusName = statusObj.Description,
                        RegistrationStartDate = d.RegistrationStartDate,
                        RegistrationEndDate = d.RegistrationEndDate,
                        IsOverbudget = d.IsOverbudget,
                        ImplementationStartDate = d.ImplementationStartDate,
                        ImplementationEndDate = d.ImplementationEndDate,
                        ProgramCloseDate = d.ProgramCloseDate,
                        ReasonCloseProgram = d.ReasonCloseProgram,
                        FilePathReasonCloseProgram = d.FilePathReasonCloseProgram,
                        FileNameReasonCloseProgram = d.FileNameReasonCloseProgram
                    }).FirstOrDefault();
        }

        public IQueryable<DisDisplayModel> GetListDisplay()
        {
            var systemSettings = _systemSettingService.GetAllQueryable(x => x.IsActive).AsQueryable();

            var listResult =
                (from d in _serviceDisplay.GetAllQueryable(x => x.DeleteFlag == 0
                 && x.Status != CommonData.DisplaySetting.Inprogress
                 && x.Status != CommonData.DisplaySetting.Closed).AsNoTracking()
                 join statusObj in systemSettings.Where(x => x.SettingType == CommonData.SystemSetting.DisplayMaintenanceStatus)
                 .AsNoTracking() on d.Status equals statusObj.SettingKey

                 select new DisDisplayModel
                 {
                     Id = d.Id,
                     Code = d.Code,
                     FullName = d.FullName,
                     Name = d.FullName,
                     Status = d.Status,
                     StatusName = statusObj.Description,
                     RegistrationStartDate = d.RegistrationStartDate,
                     RegistrationEndDate = d.RegistrationEndDate,
                     IsOverbudget = d.IsOverbudget,
                     ImplementationStartDate = d.ImplementationStartDate,
                     ImplementationEndDate = d.ImplementationEndDate,
                     ProgramCloseDate = d.ProgramCloseDate,
                     ReasonCloseProgram = d.ReasonCloseProgram,
                     FilePathReasonCloseProgram = d.FilePathReasonCloseProgram,
                     FileNameReasonCloseProgram = d.FileNameReasonCloseProgram
                 }).AsQueryable();
            return listResult;
        }

        public IQueryable<DisDisplayModel> GetListDisplayCode()
        {
            var systemSettings = _systemSettingService.GetAllQueryable(x => x.IsActive).AsQueryable();

            var listResult =
                (from d in _serviceDisplay.GetAllQueryable(x => x.DeleteFlag == 0
                 && (x.Status == CommonData.DisplaySetting.Implementation || x.Status == CommonData.DisplaySetting.Closed)).AsNoTracking()
                 join statusObj in systemSettings.Where(x => x.SettingType == CommonData.SystemSetting.DisplayMaintenanceStatus)
                 .AsNoTracking() on d.Status equals statusObj.SettingKey

                 select new DisDisplayModel
                 {
                     Id = d.Id,
                     Code = d.Code,
                     FullName = d.FullName,
                     Name = d.FullName,
                     Status = d.Status,
                     StatusName = statusObj.Description,
                     RegistrationStartDate = d.RegistrationStartDate,
                     RegistrationEndDate = d.RegistrationEndDate,
                     IsOverbudget = d.IsOverbudget,
                     ImplementationStartDate = d.ImplementationStartDate,
                     ImplementationEndDate = d.ImplementationEndDate,
                     ProgramCloseDate = d.ProgramCloseDate,
                     ReasonCloseProgram = d.ReasonCloseProgram,
                     FilePathReasonCloseProgram = d.FilePathReasonCloseProgram,
                     FileNameReasonCloseProgram = d.FileNameReasonCloseProgram
                 }).AsQueryable();
            return listResult;
        }

        public bool UpdateDataDisplay(DisDisplayUpdModel input)
        {
            string userlogin = string.Empty;
            var display = _serviceDisplay.FirstOrDefault(d => d.Code == input.Code && d.DeleteFlag == 0);
            if (display != null)
            {
                if (input.Status == CommonData.DisplaySetting.Register)
                {
                    display.RegistrationStartDate = input.RegistrationStartDate;
                    display.RegistrationEndDate = input.RegistrationEndDate;
                    display.IsOverbudget = input.IsOverbudget;
                }
                else if (input.Status == CommonData.DisplaySetting.Implementation)
                {
                    display.ImplementationStartDate = input.ImplementationStartDate;
                    display.ImplementationEndDate = input.ImplementationEndDate;
                }
                else if (input.Status == CommonData.DisplaySetting.Closed)
                {
                    display.ProgramCloseDate = input.ProgramCloseDate;
                    display.ReasonCloseProgram = input.ReasonCloseProgram;
                    display.FilePathReasonCloseProgram = input.FilePathReasonCloseProgram;
                    display.FileNameReasonCloseProgram = input.FileNameReasonCloseProgram;
                }
                display.Status = input.Status;
                display.UpdatedBy = userlogin;
                display.UpdatedDate = DateTime.Now;
                _serviceDisplay.Update(display);
                _serviceDisplay.Save();
                return true;
            }

            return false;
        }

        #endregion
    }
}
