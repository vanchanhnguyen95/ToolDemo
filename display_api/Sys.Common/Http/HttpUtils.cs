using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Sys.Common.Http
{
    public sealed class HttpUtils
    {
        private static ILogger<HttpUtils> _logger;
        private static IHttpContextAccessor _accessor;

        public HttpUtils(IHttpContextAccessor accessor, ILogger<HttpUtils> logger)
        {
            _logger = logger;
            _accessor = accessor;
        }

        public static IEnumerable<string> GetRequestHeaderValues(HttpRequestMessage request, string headerName)
        {
            if (request == null || string.IsNullOrWhiteSpace(headerName))
            {
                return null;
            }

            IEnumerable<string> headerValues = null;

            if (request.Headers.Contains(headerName))
            {
                request.Headers.TryGetValues(headerName, out headerValues);
            }

            return headerValues;
        }

        public static string GetRequestHeaderValue(HttpRequestMessage request, string headerName)
        {
            if (request == null || string.IsNullOrWhiteSpace(headerName))
            {
                return string.Empty;
            }

            IEnumerable<string> headerValues = GetRequestHeaderValues(request, headerName);
            return headerValues != null ? headerValues.FirstOrDefault() : string.Empty;
        }

        public static IEnumerable<string> GetRequestHeaderValues(HttpRequest request, string headerName)
        {
            if (request == null || string.IsNullOrWhiteSpace(headerName))
            {
                return null;
            }
            IEnumerable<string> headerValues = null;
            if (request.Headers.Keys.Contains(headerName, StringComparer.OrdinalIgnoreCase))
            {
                var values = request.Headers.Where(x => x.Key == headerName).Select(k => k.Value.ToString()).ToList();
                headerValues = new List<string>(values);
            }
            return headerValues;
        }

        public static string GetRequestHeaderValue(HttpRequest request, string headerName)
        {
            if (request == null || string.IsNullOrWhiteSpace(headerName))
            {
                return string.Empty;
            }

            IEnumerable<string> headerValues = GetRequestHeaderValues(request, headerName);
            return headerValues != null ? headerValues.FirstOrDefault() : string.Empty;
        }

        public static string GetClientIpAddress()
        {
            string clientIp = "Swagger or Postman";
            try
            {
                clientIp = GetIPAddress();
                if (_accessor != null && string.IsNullOrEmpty(clientIp))
                    clientIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return clientIp;
        }

        public static string GetIPAddress()
        {
            string hostname = Environment.MachineName;
            IPHostEntry Host = Dns.GetHostEntry(hostname);
            string result = "Cannot detect IP";
            if (Host.AddressList.Length < 10)
            {
                foreach (IPAddress IP in Host.AddressList)
                {
                    if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        result = Convert.ToString(IP);
                    }
                }
            }
            return result;
        }
    }
}