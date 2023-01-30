using DemoRedis.Configurations;
using DemoRedis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace DemoRedis.Attributes
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;

        public CacheAttribute(int timeToLiveSeconds = 1000)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheConfiguration = context.HttpContext.RequestServices.GetRequiredService<RedisConfiguration>();

            if(!cacheConfiguration.Enable)
            {
                await next();
                return;
            }

            // Xem cache có hay chưa
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await cacheService.GetCacheResponseAsync(cacheKey);

            // nếu nó có dữ liệu thì respon nó ra
            if(!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result= contentResult;
                return;
            }

            // nếu chưa có dữ liệu thì đưa vào cache
            var excutedContext = await next();
            if (excutedContext.Result is OkObjectResult objectResult)
#pragma warning disable CS8604 // Possible null reference argument.
                await cacheService.SetCacheResponseAsync(cacheKey, response: objectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        private static string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");
            foreach(var(key,value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
