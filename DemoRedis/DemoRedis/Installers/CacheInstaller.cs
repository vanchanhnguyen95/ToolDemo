using DemoRedis.Configurations;
using DemoRedis.Services;
using StackExchange.Redis;

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

            var multiplexer = ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
