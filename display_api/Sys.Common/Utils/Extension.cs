using Sys.Common.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Sys.Common.Utils
{
    public static class Extension
    {
        public static string GetUser(this IEnumerable<Claim> claims)
        {
            string result = null;
            if (claims.IsNotEmpty())
            {
                result = claims.FirstOrDefault(t => t.Type.Equals(ClaimTypes.Name))?.Value;
            }
            return result;
        }
    }
}
