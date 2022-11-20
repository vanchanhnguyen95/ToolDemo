using Microsoft.Extensions.DependencyInjection;

namespace Sys.Common.Cors
{
    public class CorsConfig
    {
        public void Setup(IServiceCollection services)
        {
            string origins = "";
            if (SystemConfig.CORS == null || SystemConfig.CORS.Count <= 0)
                origins = "*";
            else
                SystemConfig.CORS.ForEach(x => { origins = origins + x.Trim(); });

            if (origins.Contains("*"))
            {
                services.AddCors(x =>
                {
                    x.AddPolicy(SystemConfig.CorsName, builder =>
                    {
                        builder.AllowAnyMethod()
                               .AllowAnyHeader()
                               .SetIsOriginAllowed(origin => true)
                               .AllowCredentials()
                               .WithExposedHeaders("Content-Disposition");
                    });
                });
            }
            else
            {
                services.AddCors(x =>
                {
                    x.AddPolicy(SystemConfig.CorsName, builder =>
                    {
                        builder.AllowAnyMethod()
                               .AllowAnyHeader()
                               .WithOrigins(origins)
                               .AllowCredentials()
                               .WithExposedHeaders("Content-Disposition");
                    });
                });
            }
        }
    }
}