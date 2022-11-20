using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Common.JWT
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var obj = jwtUtils.ValidateJwtToken(token);
            if (obj != null)
            {
                context.Items["obj"] = obj;
            }

            await _next(context);
        }
    }
}