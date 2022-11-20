using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RDOS.TMK_DisplayAPI.Services.Dis;
using RDOS.TMK_DisplayAPI.Infrastructure;
using RDOS.TMK_DisplayAPI.Services.Base;
using RDOS.TMK_DisplayAPI.Services.Common;
using Sys.Common;
using Sys.Common.Helper;
using Sys.Common.Models;
using System;
using RDOS.TMK_DisplayAPI.Services.Dis.PayReward;
using RDOS.TMK_DisplayAPI.Services.Dis.Report;
using RDOS.TMK_DisplayAPI.Services.TempDis;

namespace RDOS.TMK_DisplayAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            #region AutoMapper
            services.AddAutoMapper(typeof(Startup).Assembly);
            #endregion
            //var connectStrings = Environment.GetEnvironmentVariable("CONNECTION");
            var connectStrings = Configuration.GetConnectionString("DefaultConnection");
            CoreDependency.InjectDependencies(services, connectStrings);
            services.AddHealthChecks();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<IFirebaseHelper, FirebaseHelper>();
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connectStrings));
            services.AddControllers();


            #region local service
            // MKT Display
            services.AddScoped<IDisBudgetService, DisBudgetService>();
            services.AddScoped<IDisCriteriaEvaluatePictureDisplayService, DisCriteriaEvaluatePictureDisplayService>();
            services.AddScoped<IDisApproveRegistrationCustomerService, DisApproveRegistrationCustomerService>();
            services.AddScoped<IDisApproveRegistrationCustomerDetailService, DisApproveRegistrationCustomerDetailService>();
            services.AddScoped<IDisImplementationDisplayService, DisImplementationDisplayService>();
            services.AddScoped<IDisConfirmResultService, DisConfirmResultService>();
            services.AddScoped<IDisConfirmResultDetailService, DisConfirmResultDetailService>();
            services.AddScoped<IDisplayService, DisplayService>();
            services.AddScoped<IDisCustomerShiptoService, DisCustomerShiptoService>();
            services.AddScoped<ITempDisApproveRegistrationCustomerService, TempDisApproveRegistrationCustomerService>();
            services.AddScoped<ITempDisConfirmResultDetailService, TempDisConfirmResultDetailService>();
            services.AddScoped<IDisDefinitionStructureService, DisDefinitionStructureService>();
            services.AddScoped<IPayRewardService, PayRewardService>();
            services.AddScoped<IPayRewardDetailService, PayRewardDetailService>();
            services.AddScoped<IDisSettlementService, DisSettlementService>();
            services.AddScoped<ITempDisCusShiptoSaleOrQuantityService, TempDisCusShiptoSaleOrQuantityService>();
            services.AddScoped<ITempDisCusShiptoNotHaveService, TempDisCusShiptoNotHaveService>();
            services.AddScoped<ITempDisPosmForCusShiptoService, TempDisPosmForCusShiptoService>();
            services.AddScoped<ITempDisDisplaySupportToolService, TempDisDisplaySupportToolService>();
            services.AddScoped<IDisplayProgressReportService, DisplayProgressReportService>();
            services.AddScoped<IDisplayDetailReportService, DisplayDetailReportService>();
            services.AddScoped<IDisplayListCustomerReportService, DisplayListCustomerReportService>();
            services.AddScoped<ITempDisOrderDetailService, TempDisOrderDetailService>();
            services.AddScoped<IDisSettlementDetailService, DisSettlementDetailService>();
            services.AddScoped<IExternalService, ExternalService>();
            services.AddScoped<IDisplayPeriodTrackingReportService, DisplayPeriodTrackingReportService>();
            services.AddScoped<IDisplaySyntheticReportSettlementService, DisplaySyntheticReportSettlementService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ApplicationDbContext _context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RDOS.TMK_DisplayAPI v1"));
            }
            ListServices services = new ListServices()
            {
                App = app,
                Env = env,
                Provider = provider
            };
            _context.Database.Migrate();
            CoreDependency.Configure(services);

            app.UseRouting();
            app.UseHealthChecks("/ping");
            app.UseAuthorization();
            app.UseAllElasticApm(Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
