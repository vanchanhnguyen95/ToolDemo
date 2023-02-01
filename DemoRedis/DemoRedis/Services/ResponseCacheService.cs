using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System.Text;

namespace DemoRedis.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabaseAsync _databaseAsync;

        public ResponseCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _databaseAsync = connectionMultiplexer.GetDatabase();
        }

        public async Task<string> GetCacheResponseAsync(string cacheKey)
        {
            var cacheResponse = await _databaseAsync.StringGetAsync(cacheKey);
            return cacheResponse;
        }

        public async Task RemoveCacheResponseAsync(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException("Value cannot be null or white space");

            await foreach (var key in GetkeyAsync(pattern + "*"))
            {
                //await _distributedCache.RemoveAsync(key);
                bool _isKeyExist = await _databaseAsync.KeyExistsAsync(key);
                if (_isKeyExist == true)
                    await _databaseAsync.KeyDeleteAsync(key);
            }
        }

        private async IAsyncEnumerable<string> GetkeyAsync(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException("Value cannot be null or white space");

            foreach(var endPoint in _connectionMultiplexer.GetEndPoints())
            {
                var server = _connectionMultiplexer.GetServer(endPoint);
                foreach(var key in server.Keys(pattern: pattern))
                {
                    yield return key.ToString();
                }
            }
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

            //var redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
            //var options = new DistributedCacheEntryOptions()
            //.SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
            //.SetSlidingExpiration(TimeSpan.FromMinutes(2));
            //await _distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            await _databaseAsync.StringSetAsync(cacheKey, Encoding.UTF8.GetBytes(serializedCustomerList), timeOut);
        }
    }
}
