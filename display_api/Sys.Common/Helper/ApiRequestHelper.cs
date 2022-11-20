using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sys.Common.Extensions;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Common.Helper
{
    public class ApiRequestHelper
    {
        private const int API_TIMEOUT = 90;
        private const string SECRECT_KEY = "SaleRepABCXYZ";

        private readonly ILogger _logger;

        public ApiRequestHelper(ILogger<ApiRequestHelper> logger)
        {
            _logger = logger;
        }

        #region GET

        public async Task<T> GetAsync<T>(
                            string url,
                            Dictionary<string, string> headers = null,
                            object requestParams = null,
                            string accessToken = "",
                            int timeout = API_TIMEOUT,
                            bool isThrowException = true,
                            bool isInternal = true)
        {
            var result = await ProcessGetAsync<T>(url, headers, requestParams, accessToken, timeout, isThrowException, isInternal);
            return result;
        }

        public async Task GetAsync(
                        string url,
                        Dictionary<string, string> headers = null,
                        object requestParams = null,
                        string accessToken = "",
                        int timeout = API_TIMEOUT,
                        bool isThrowException = true,
                        bool isInternal = true)
        {
            await ProcessGetAsync<object>(url, headers, requestParams, accessToken, timeout, isThrowException, isInternal);
        }

        private async Task<T> ProcessGetAsync<T>(
                            string url,
                            Dictionary<string, string> headers = null,
                            object requestParams = null,
                            string accessToken = "",
                            int timeout = API_TIMEOUT,
                            bool isThrowException = true,
                            bool isInternal = true)
        {
            var result = new RestApiResponse<T>();
            try
            {
                var _flurlRequest = GenerateFlurlRequest(url, headers, accessToken, isInternal);
                var response = requestParams != null ? await _flurlRequest.SetQueryParams(requestParams).WithTimeout(timeout).GetAsync() : await _flurlRequest.WithTimeout(timeout).GetAsync();
                if (response.ResponseMessage.IsSuccessStatusCode)
                {
                    var stringResponse = await response.ResponseMessage.Content.ReadAsStringAsync();
                    result.Result = JsonConvert.DeserializeObject<T>(stringResponse);
                    result.IsSuccess = true;
                }
            }
            catch (FlurlHttpTimeoutException timeoutex)
            {
                result = CatchTimeout(timeoutex, result);
            }
            catch (FlurlHttpException ex)
            {
                result = await CatchFlurlHttpException(ex, result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
            }

            WriteLog(url, result, requestParams, isThrowException);

            return result.Result;
        }

        #endregion GET

        #region POST

        public async Task<T> PostAsync<T>(
                            string url,
                            Dictionary<string, string> headers = null,
                            object requestParams = null,
                            object requestBody = null,
                            string accessToken = "",
                            int timeout = API_TIMEOUT,
                            bool isThrowException = true,
                            bool isInternal = true)
        {
            var result = await ProcessPostAsync<T>(url, headers, requestParams, requestBody, accessToken, timeout, isThrowException, isInternal);
            return result;
        }

        public async Task PostAsync(
                        string url,
                        Dictionary<string, string> headers = null,
                        object requestParams = null,
                        object requestBody = null,
                        string accessToken = "",
                        int timeout = API_TIMEOUT,
                        bool isThrowException = true,
                        bool isInternal = false)
        {
            await ProcessPostAsync<object>(url, headers, requestParams, requestBody, accessToken, timeout, isThrowException, isInternal);
        }

        public async Task<T> ProcessPostAsync<T>(
                            string url,
                            Dictionary<string, string> headers = null,
                            object requestParams = null,
                            object requestBody = null,
                            string accessToken = "",
                            int timeout = API_TIMEOUT,
                            bool isThrowException = true,
                            bool isInternal = false)
        {
            var result = new RestApiResponse<T>();

            try
            {
                var _flurlRequest = GenerateFlurlRequest(url, headers, accessToken, isInternal);
                if (requestParams != null)
                    _flurlRequest = _flurlRequest.SetQueryParams(requestParams);

                var response = await _flurlRequest.WithTimeout(timeout).PostJsonAsync(requestBody);

                if (response.ResponseMessage.IsSuccessStatusCode)
                {
                    var stringResponse = await response.ResponseMessage.Content.ReadAsStringAsync();
                    result.Result = JsonConvert.DeserializeObject<T>(stringResponse);
                    result.IsSuccess = true;
                }
            }
            catch (FlurlHttpTimeoutException timeoutex)
            {
                result = CatchTimeout(timeoutex, result);
            }
            catch (FlurlHttpException ex)
            {
                result = await CatchFlurlHttpException(ex, result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
            }

            WriteLog(url, result, requestParams, requestBody, isThrowException);

            return result.Result;
        }

        #endregion POST

        #region Handle base request

        private async Task<RestApiResponse<T>> CatchFlurlHttpException<T>(FlurlHttpException ex, RestApiResponse<T> result)
        {
            var errorResponse = await ex.GetResponseStringAsync();

            result.Exception = ex;
            result.ExceptionMessage = ex.Message;
            result.IsSuccess = false;
            //Try parse response to Custom Exception
            try
            {
                if (errorResponse is string) result.ExceptionMessage += $" - {errorResponse}";
                else
                {
                    var customException = JsonConvert.DeserializeObject<CustomExceptionResponse>(errorResponse);
                    if (customException != null)
                    {
                        result.IsCustomException = true;
                        result.CustomException = customException;
                        result.IsSuccess = false;
                    }
                }
            }
            catch
            {
                throw ex;
            }

            return result;
        }

        private RestApiResponse<T> CatchTimeout<T>(FlurlHttpTimeoutException timeoutex, RestApiResponse<T> result)
        {
            result.IsSuccess = false;
            result.ExceptionMessage = "Time out";
            result.Exception = timeoutex;

            return result;
        }

        private IFlurlRequest GenerateFlurlRequest(string url, Dictionary<string, string> headers = null, string accessToken = "", bool isInternal = false)
        {
            var _request = new Url(url);
            var _flurlRequest = _request.WithHeader("Content-Type", "application/json");

            if (!string.IsNullOrEmpty(accessToken))
                _flurlRequest = _flurlRequest.WithHeader("Authorization", accessToken);

            if (headers != null && headers.Count > 0)
            {
                foreach (var item in headers)
                {
                    _flurlRequest = _flurlRequest.WithHeader(item.Key, item.Value);
                }
            }

            if (isInternal)
            {
                var res = GenerateHashToken();
                _flurlRequest = _flurlRequest.WithHeader("hashToken", res.HashToken);
                _flurlRequest = _flurlRequest.WithHeader("reqDate", res.RequestDate);
            }

            return _flurlRequest;
        }

        private (string HashToken, string RequestDate) GenerateHashToken()
        {
            var reqDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var str = SECRECT_KEY + reqDate;

            var hashToken = str.HashMD5();

            return (hashToken, reqDate);
        }

        private void WriteLog<T>(string url, RestApiResponse<T> result, object requestParams = null, object requestBody = null, bool isThrowException = true)
        {
            if (result.IsSuccess) return;

            var message = result.IsCustomException ? result.CustomException.Message
                                                   : !string.IsNullOrEmpty(result.ExceptionMessage) ? result.ExceptionMessage : result.Exception.Message;
            var stackTrace = result.IsCustomException ? string.Empty : result.Exception.StackTrace;
            var param = requestParams != null ? JsonConvert.SerializeObject(requestParams) : "";
            var body = requestBody != null ? JsonConvert.SerializeObject(requestBody) : "";

            _logger.LogError($"[{DateTime.Now}] [ERR] Request API: {url}\r\nRequest params: {param}\r\nRequest body: {body}\r\nError message: {message}\r\n{stackTrace}");

            var code = result.IsCustomException ? result.CustomException.Code : "UN_HANDLING_ERROR";

            if (isThrowException) throw new CustomExceptionExtension(code, message);
        }

        #endregion Handle base request
    }
}