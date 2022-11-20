using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Paging;
using RDOS.TMK_DisplayAPI.Services.Dis;
using Sys.Common.Constants;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace RDOS.TMK_DisplayAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DisCriteriaEvaluatePictureDisplayController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IDisCriteriaEvaluatePictureDisplayService _disCriteriaEvaluatePictureDisplayService;
        public DisCriteriaEvaluatePictureDisplayController(IDisCriteriaEvaluatePictureDisplayService disCriteriaEvaluatePictureDisplayService, IMapper mapper)
        {
            _disCriteriaEvaluatePictureDisplayService = disCriteriaEvaluatePictureDisplayService;
            _mapper = mapper;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetListCriteriaEvaluatePictureDisplay")]
        public async Task<IActionResult> GetListCriteriaEvaluatePictureDisplay([FromBody] EcoParameters parameters)
        {
            var disCriterias = _disCriteriaEvaluatePictureDisplayService.GetListCriteriaEvaluatePictureDisplay()
                                                                        .Where(await parameters
                                                                        .Predicate<DisCriteriaEvaluatePictureDisplayModel>());

            var result = await (parameters.HasOrderBy
                ? disCriterias.OrderBy(parameters.OrderBy)
                : disCriterias.OrderBy(x => x.Code)
                .ThenBy(x => x.Result))
                .ToPaginatedAsync(parameters.PageNumber, parameters.PageSize, parameters.IsDropdown);

            return Ok(BaseResultModel.Success(new CriteriaEvaluatePictureDisplayListModel(result)));
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetCriteriaEvaluatePictureDisplayByCode/{code}")]
        public async Task<IActionResult> GetCriteriaEvaluatePictureDisplayByCode([FromRoute] string code)
        {
            var disCriteria = await _disCriteriaEvaluatePictureDisplayService.GetCriteriaEvaluatePictureDisplayByCodeAsync(code);
            return Ok(BaseResultModel.Success(disCriteria));
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("Detail/{id}")]
        public async Task<IActionResult> Detail([FromRoute] Guid? id)
        {
            if (id == null)
            {
                return Ok(BaseResultModel.Fail(ErrorCodes.EntityNotFound));
            }

            var result = await _disCriteriaEvaluatePictureDisplayService.GetListCriteriaEvaluatePictureDisplay()
                                                                        .Where(x => x.Id == id.Value)
                                                                        .FirstOrDefaultAsync();
            return Ok(BaseResultModel.Success(result));
        }

        [HttpPost]
        [Route("Create")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Create([FromBody] DisCriteriaEvaluatePictureDisplayRequest request)
        {
            try
            {
                var exists = await _disCriteriaEvaluatePictureDisplayService.ExistsByCodeAsync(request.Code);
                if (exists)
                {
                    return Ok(BaseResultModel.Fail(ErrorCodes.DuplicateCode));
                }

                await _disCriteriaEvaluatePictureDisplayService.CreateAsync(request);
                return Ok(BaseResultModel.Success(ErrorCodes.CreateSuccess));
            }
            catch
            {
                return Ok(BaseResultModel.Fail(ErrorCodes.CreateFailed));
            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Update([FromRoute] Guid? id, [FromBody] DisCriteriaEvaluatePictureDisplayRequest request)
        {
            try
            {
                if (id == null)
                {
                    return Ok(BaseResultModel.Fail(ErrorCodes.IdIsRequired));
                }

                var disCriteria = await _disCriteriaEvaluatePictureDisplayService.FindByIdAsync(id.Value);
                if (disCriteria == null)
                {
                    return Ok(BaseResultModel.Fail(ErrorCodes.EntityNotFound));
                }

                var exists = await _disCriteriaEvaluatePictureDisplayService.ExistsByCodeAsync(request.Code, id);
                if (exists)
                {
                    return Ok(BaseResultModel.Fail(ErrorCodes.DuplicateCode));
                }

                await _disCriteriaEvaluatePictureDisplayService.UpdateAsync(_mapper.Map(request, disCriteria));
                return Ok(BaseResultModel.Success(ErrorCodes.UpdateSuccess));
            }
            catch
            {
                return Ok(BaseResultModel.Fail(ErrorCodes.UpdateFailed));
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Delete([FromRoute] Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return Ok(BaseResultModel.Fail(ErrorCodes.IdIsRequired));
                }

                var disCriteria = await _disCriteriaEvaluatePictureDisplayService.FindByIdAsync(id.Value);
                if (disCriteria == null)
                {
                    return Ok(BaseResultModel.Fail(ErrorCodes.EntityNotFound));
                }

                disCriteria.DeleteFlag = 1;
                await _disCriteriaEvaluatePictureDisplayService.UpdateAsync(disCriteria);
                return Ok(BaseResultModel.Success(ErrorCodes.DeleteSuccess));
            }
            catch
            {
                return Ok(BaseResultModel.Fail(ErrorCodes.DeleteFailed));
            }
        }
    }
}