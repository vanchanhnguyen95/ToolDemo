using DemoRedis.Configurations;
using DemoRedis.Services;
using StackExchange.Redis;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using StackExchange.Redis;

namespace DemoRedis.Installers
{
    public class CacheInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisConfiguration = new RedisConfiguration();
            configuration.GetSection("RedisConfiguration").Bind(redisConfiguration);
            services.AddSingleton(redisConfiguration);

            if (!redisConfiguration.Enable)
                return;

            var muxer = ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString);
            services.AddSingleton<IConnectionMultiplexer>(muxer);
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = "localhost:4455";
            //});
            //services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString));
            //services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(new ConfigurationOptions
            //{
            //    EndPoints = { $"{redisConfiguration.GetValue<string>("RedisCache:Host")}:{redisConfiguration.GetValue<int>("RedisCache:Port")}" },
            //    Ssl = true,
            //    AbortOnConnectFail = false,
            //}));
            //services.AddStackExchangeRedisCache(option => option.Configuration = redisConfiguration.ConnectionString);
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
