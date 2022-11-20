using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RDOS.TMK_DisplayAPI.Infrastructure.Dis;
using RDOS.TMK_DisplayAPI.Models;
using RDOS.TMK_DisplayAPI.Models.Dis;
using RDOS.TMK_DisplayAPI.Models.Paging;
using RDOS.TMK_DisplayAPI.Services.Dis;
using RDOS.TMK_DisplayAPI.Services.TempDis;
using Sys.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Transactions;

namespace RDOS.TMK_DisplayAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DisApproveRegistrationCustomerController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ITempDisApproveRegistrationCustomerService _tempDisApproveRegistrationCustomerService;
        private readonly IDisApproveRegistrationCustomerService _disApproveRegistrationCustomerService;
        private readonly IDisApproveRegistrationCustomerDetailService _disApproveRegistrationCustomerDetailService;
        private readonly IDisplayService _displayService;
        public DisApproveRegistrationCustomerController(ITempDisApproveRegistrationCustomerService tempDisApproveRegistrationCustomerService, IDisApproveRegistrationCustomerService disApproveRegistrationCustomerService, IMapper mapper, IDisApproveRegistrationCustomerDetailService disApproveRegistrationCustomerDetailService, IDisplayService displayService)
        {
            _mapper = mapper;
            _tempDisApproveRegistrationCustomerService = tempDisApproveRegistrationCustomerService;
            _disApproveRegistrationCustomerService = disApproveRegistrationCustomerService;
            _disApproveRegistrationCustomerDetailService = disApproveRegistrationCustomerDetailService;
            _displayService = displayService;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetDisApproveRegistrationCustomer")]
        public async Task<IActionResult> GetDisApproveRegistrationCustomer([FromBody] EcoParameters parameters)
        {
            var result = await _tempDisApproveRegistrationCustomerService
                  .GetDisApproveRegistrationCustomer()
                  .Where(parameters.Filter ?? "true")
                  .ToPaginatedAsync(parameters.PageNumber,
                  parameters.PageSize);

            return Ok(BaseResultModel.Success(result));
        }
        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetDisApproveRegistrationCustomerForReport")]
        public async Task<IActionResult> GetDisApproveRegistrationCustomerForReport([FromBody] EcoParameters parameters)
        {
            var result = await _tempDisApproveRegistrationCustomerService
                  .GetDisApproveRegistrationCustomer()
                  .Where(parameters.Filter ?? "true")
                  .ToDynamicListAsync();

            return Ok(BaseResultModel.Success(result));
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetDisApproveRegistrationCustomerDetail")]
        public async Task<IActionResult> GetDisApproveRegistrationCustomerDetail([FromBody] EcoParameters parameters)
        {
            var result = await _disApproveRegistrationCustomerDetailService
                  .GetDisApproveRegistrationCustomerDetail()
                  .Where(parameters.Filter ?? "true")
                  .ToPaginatedAsync(parameters.PageNumber,
                  parameters.PageSize);

            return Ok(BaseResultModel.Success(result));
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetDisApproveRegistrationCustomerDetailsByCustomerCodes")]
        public async Task<IActionResult> GetDisApproveRegistrationCustomerDetailsByCustomerCodes([FromBody] IList<DisApproveRegistrationCustomerCodeRequest> request)
        {
            var codes = request.Select(x => (x.CustomerCode, x.DisplayLevel)).ToList();
            var customerCodes = await _disApproveRegistrationCustomerDetailService.GetCustomerCodesAsync(codes);
            return Ok(BaseResultModel.Success(customerCodes));
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("getbydisplaycode/{0}")]
        public async Task<IActionResult> GetByDisplayCode([FromRoute] string displayCode)
        {
            var data = await _disApproveRegistrationCustomerService.FindByDisplayCodeAsync(displayCode);
            return Ok(BaseResultModel.Success(data));
        }

        [HttpPost]
        [Route("create")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Create([FromBody] DisApproveRegistrationCustomerRequest request)
        {
            using TransactionScope transaction = new(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var display = await _displayService.GetDisplayByCodeAsync(request.DisplayCode);
                if (display == null)
                {
                    return Ok(BaseResultModel.Fail("DisplayNotFound"));
                }

                var isBeforeImplementationTime = display.ImplementationStartDate > DateTime.Now && display.Status == CommonData.DisplaySetting.Implementation;
                if ((!display.IsImplementation && request.IsAdditionalRegistration) && !isBeforeImplementationTime)
                {
                    return Ok(BaseResultModel.Fail("ConditionNotSatisfied"));
                }

                DisApproveRegistrationCustomer entity = new() { DisplayCode = request.DisplayCode };
                if (request.IsAdditionalRegistration)
                {
                    entity = _mapper.Map<DisApproveRegistrationCustomer>(request);
                }

                var disApproveCustomerDetails = await RemoveExistCustomerCodesAsync(request.Details);

                var result = await _disApproveRegistrationCustomerService.CreateAsync(entity);
                if (result != null && disApproveCustomerDetails.Count > 0)
                {
                    var items = _mapper.Map<List<DisApproveRegistrationCustomerDetail>>(disApproveCustomerDetails);
                    _disApproveRegistrationCustomerDetailService.BulkInsert(items);

                    transaction.Complete();
                    return Ok(BaseResultModel.Success(ErrorCodes.CreateSuccess));
                }

                return Ok(BaseResultModel.Fail("ExistListRegistrationCustomer"));
            }
            catch (Exception)
            {
                return Ok(BaseResultModel.Fail("ConfirmFailed"));
            }
        }

        private async Task<List<DisApproveRegistrationCustomerDetailRequest>> RemoveExistCustomerCodesAsync(List<DisApproveRegistrationCustomerDetailRequest> disApproveCustomerDetails)
        {
            if (disApproveCustomerDetails != null)
            {
                var items = disApproveCustomerDetails.Select(x => (x.CustomerCode, x.DisplayLevel)).ToList();
                var customerCodes = await _disApproveRegistrationCustomerDetailService.GetCustomerCodesAsync(items);
                if (customerCodes?.Count > 0)
                {
                    disApproveCustomerDetails.RemoveAll(x => customerCodes.Any(code => code == x.CustomerCode));
                }

                return disApproveCustomerDetails;
            }

            return new();
        }
    }
}