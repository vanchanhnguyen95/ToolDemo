using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System.Text;

namespace DemoRedis.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabaseAsync _databaseAsync;

        public ResponseCacheService(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = distributedCache;
            _connectionMultiplexer = connectionMultiplexer;
            _databaseAsync = connectionMultiplexer.GetDatabase();

        }

        public async Task<string> GetCacheResponseAsync(string cacheKey)
        {
           var cacheResponse = await _distributedCache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cacheResponse) ? null : cacheResponse;
        }

        public async Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut)
        {
            if (response == null)
                return;

            string serializedCustomerList = JsonConvert.SerializeObject(response);

            var serializerResponse = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);

            var options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
            .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            await _distributedCache.SetAsync(cacheKey, redisCustomerList, options);

            // Set up thời gian vào cache
            //await _distributedCache.SetStringAsync(cacheKey, serializerResponse, new DistributedCacheEntryOptions
            //{
            //    AbsoluteExpirationRelativeToNow= timeOut
            //});
            //await _distributedCache.SetStringAsync(cacheKey, serializerResponse, new DistributedCacheEntryOptions
            //{
            //    AbsoluteExpirationRelativeToNow = timeOut
            //});

            //var options = new DistributedCacheEntryOptions()
            //.SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
            //.SetSlidingExpiration(TimeSpan.FromMinutes(2));
            //await _distributedCache.SetAsync(cacheKey, serializerResponse, options);
        }
    }
}
