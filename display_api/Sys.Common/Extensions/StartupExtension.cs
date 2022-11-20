using Microsoft.Extensions.DependencyInjection;
using Sys.Common.Helper;

namespace Sys.Common.Extensions
{
    public static class StartupExtension
    {
        public static void AddCommonService(this IServiceCollection services)
        {
            services.AddScoped<ApiRequestHelper>();
            services.AddSingleton<IFirebaseHelper, FirebaseHelper>();
        }
    }
}