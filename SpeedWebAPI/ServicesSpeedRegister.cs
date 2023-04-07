using Microsoft.Extensions.DependencyInjection;
using SpeedWebAPI.Services;

namespace SpeedWebAPI
{
    public static class ServicesSpeedRegister
    {
        public static void RegisterSpeedServices(this IServiceCollection services)
        {
            services.AddScoped<ISpeedLimitService, SpeedLimitService>();
            services.AddScoped<ISpeedProviderFileService, SpeedProviderFileService>();
            services.AddScoped<ISpeedLimitPQAService, SpeedLimitPQAService>();
        }
    }
}
