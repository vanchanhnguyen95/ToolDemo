using Sys.Common.Helper;
using System.Collections.Generic;
using System.Linq;

namespace Sys.Common.Extensions
{
    public static class JwtExtension
    {
        public static string GetPhoneNumber(this IEnumerable<System.Security.Claims.Claim> claims)
        {
            var result = string.Empty;
            if (claims.IsNotEmpty())
            {
                result = claims.FirstOrDefault(t => t.Type.Equals("phonenumber"))?.Value;
            }
            return result;
        }

        public static string GetUserName(this IEnumerable<System.Security.Claims.Claim> claims)
        {
            var result = string.Empty;
            if (claims.IsNotEmpty())
            {
                result = claims.FirstOrDefault(t => t.Type.Equals("username"))?.Value;
            }
            return result;
        }

        public static string GetUserId(this IEnumerable<System.Security.Claims.Claim> claims)
        {
            var result = string.Empty;
            if (claims.IsNotEmpty())
            {
                result = claims.FirstOrDefault(t => t.Type.Equals("UserID"))?.Value;
            }
            return result;
        }

        public static string GetSecretKey(this IEnumerable<System.Security.Claims.Claim> claims)
        {
            var result = string.Empty;
            if (claims.IsNotEmpty())
            {
                result = claims.FirstOrDefault(t => t.Type.Equals("SecretKey"))?.Value;
            }
            return result;
        }

        public static string GetEmpId(this IEnumerable<System.Security.Claims.Claim> claims)
        {
            var result = string.Empty;
            if (claims.IsNotEmpty())
            {
                result = claims.FirstOrDefault(t => t.Type.Equals("EmpId"))?.Value;
            }
            return result;
        }
    }
}