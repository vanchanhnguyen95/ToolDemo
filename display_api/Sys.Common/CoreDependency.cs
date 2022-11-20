using Community.Microsoft.Extensions.Caching.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Sys.Common.Cors;
using Sys.Common.Exceptions;
using Sys.Common.JWT;
using Sys.Common.Models;
using Sys.Common.Swagger;

namespace Sys.Common
{
    public static class CoreDependency
    {
        public static void InjectDependencies(IServiceCollection services, string connectionString)
        {
            #region CORS

            CorsConfig cors = new CorsConfig();
            cors.Setup(services);

            #endregion CORS

            #region API Version

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddRouting(options => options.LowercaseUrls = true);

            #endregion API Version

            #region Swagger

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            SwaggerConfig.AddConfig(services);

            #endregion Swagger

            #region configure GZIP response

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();

            #endregion configure GZIP response

            #region cache

            services.AddDistributedPostgreSqlCache(x =>
            {
                x.ConnectionString = connectionString;
                x.SchemaName = "public";
                x.TableName = "DistCache";
                x.CreateInfrastructure = true;
                x.DisableRemoveExpired = true;
            });

            #endregion cache

            #region JWT

            services.AddScoped<IJwtUtils, JwtUtils>();

            #endregion JWT
         
        }

        public static void Configure(ListServices services)
        {
            #region Swagger

            services.App.UseSwagger();
            SwaggerConfig.AddUI(services.App, services.Provider);

            #endregion Swagger

            #region Base

            services.App.UseResponseCompression();
            services.App.UseHttpsRedirection();
            services.App.UseRouting();

            services.App.UseCors(SystemConfig.CorsName);
            services.App.UseAuthentication();
            services.App.UseAuthorization();

            #endregion Base

            #region Middleware

            services.App.UseMiddleware<ErrorHandlerMiddleware>();
            services.App.UseMiddleware<JwtMiddleware>();

            #endregion Middleware
        }
    }
}