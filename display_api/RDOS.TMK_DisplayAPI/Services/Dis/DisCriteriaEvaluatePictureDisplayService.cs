using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Services.Base;
using Sys.Common.Constants;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Services.Dis
{
    public class DisCriteriaEvaluatePictureDisplayService : IDisCriteriaEvaluatePictureDisplayService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<SystemSetting> _systemSettingRepository;
        private readonly ILogger<DisCriteriaEvaluatePictureDisplayService> _logger;
        private readonly IBaseRepository<DisCriteriaEvaluatePictureDisplay> _repository;

        public DisCriteriaEvaluatePictureDisplayService(IMapper mapper, IBaseRepository<DisCriteriaEvaluatePictureDisplay> repository, ILogger<DisCriteriaEvaluatePictureDisplayService> logger, IBaseRepository<SystemSetting> systemSettingRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _systemSettingRepository = systemSettingRepository;
        }

        private IQueryable<DisCriteriaEvaluatePictureDisplay> CriteriaEvaluatePictureDisplays => _repository.GetAllQueryable(x => x.DeleteFlag == 0);

        private IQueryable<SystemSetting> SystemSettings => _systemSettingRepository.GetAllQueryable();

        public Task<bool> CreateAsync(DisCriteriaEvaluatePictureDisplayRequest request)
        {
            _logger.LogInformation("info: {request}", request);
            try
            {
                //Fix later when has authorize
                var entity = _mapper.Map<DisCriteriaEvaluatePictureDisplay>(request)
                                    .InitInsert("");

                var result = _repository.Insert(entity);

                return Task.FromResult(result != null);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Was an error occrred while create dis criteria evaluate picture display");
                throw new ArgumentException(ex.Message);
            }
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                return Task.FromResult(_repository.Delete(id) != null);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Was an error occurred while delete critera evaluate picture display");
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<bool> ExistsByCodeAsync(string code, Guid? id = default)
            => await CriteriaEvaluatePictureDisplays.AnyAsync(x => (id == null || x.Id != id) && x.Code == code);

        public async Task<DisCriteriaEvaluatePictureDisplay> FindByIdAsync(Guid id)
            => await CriteriaEvaluatePictureDisplays.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<DisCriteriaEvaluatePictureDisplayModel> GetCriteriaEvaluatePictureDisplayByCodeAsync(string code)
        {
            return await GetListCriteriaEvaluatePictureDisplay().Where(x => x.Code == code).FirstOrDefaultAsync();
        }

        public IQueryable<DisCriteriaEvaluatePictureDisplayModel> GetListCriteriaEvaluatePictureDisplay()
        {
            const string TypeStatus = CommonData.SystemSetting.DisEvaluationCriteriaStatus;
            const string TypeResult = CommonData.SystemSetting.DisEvaluationCriteriaResult;
            return CriteriaEvaluatePictureDisplays
                .Join(SystemSettings.Where(x => x.SettingType == TypeStatus), a => a.Status, b => b.SettingKey,
                (a, b) => new
                {
                    Criteria = a,
                    Status = b.SettingValue
                })
                .Join(SystemSettings.Where(x => x.SettingType == TypeResult), c => c.Criteria.Result, d => d.SettingKey,
                (c, d) => new
                {
                    c.Criteria,
                    c.Status,
                    Result = d.SettingValue
                }).Select(x => new DisCriteriaEvaluatePictureDisplayModel
                {
                    Id = x.Criteria.Id,
                    Code = x.Criteria.Code,
                    Result = x.Criteria.Result,
                    Status = x.Criteria.Status,
                    CriteriaDescription = x.Criteria.CriteriaDescription,
                    StatusDisplay = x.Status,
                    ResultDisplay = x.Result,
                }).AsNoTracking();
        }

        public Task<DisCriteriaEvaluatePictureDisplayModel> UpdateAsync(DisCriteriaEvaluatePictureDisplay request)
        {
            _logger.LogInformation("info: {request}", request);
            try
            {
                var result = _repository.Update(request.InitUpdate(""));
                return Task.FromResult(_mapper.Map<DisCriteriaEvaluatePictureDisplayModel>(result));
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Was an error occrred while update dis criteria evaluate picture display");
                throw new ArgumentException(ex.Message);
            }
        }
    }
}