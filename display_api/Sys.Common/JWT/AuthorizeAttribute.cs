using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sys.Common.Helper;
using Sys.Common.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace Sys.Common.JWT
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var obj = (JWTResponse)context.HttpContext.Items["obj"];
            if (obj == null)
            {
                context.Result = new JsonResult(null) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                if (obj.UserName.IsNullOrWriteSpace())
                {
                    context.Result = new JsonResult(null) { StatusCode = StatusCodes.Status401Unauthorized };
                }
                else if (obj.ErrorCode == null)
                {
                    var claims = new ClaimsIdentity(new[] { new Claim("UserID", obj.UserId), new Claim("username", obj.UserName), new Claim("EmpId", obj.EmpId) });
                    
                    context.HttpContext.User.AddIdentity(claims);
                }
                else if (obj.ErrorCode != null)
                {
                    Result<string> result = new Result<string>();
                    result.Messages.Add(obj.ErrorCode);
                    context.Result = new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
                }
            }
        }
    }
}