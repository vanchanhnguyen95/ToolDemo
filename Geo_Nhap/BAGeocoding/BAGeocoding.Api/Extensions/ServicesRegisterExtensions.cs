﻿using BAGeocoding.Api.Interfaces;
using BAGeocoding.Api.Services;

namespace BAGeocoding.Api.Extensions
{
    public static class ServicesRegisterExtensions
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IGeoService, GeoService>();
            services.AddScoped(typeof(IVietNamShapeService), typeof(VietNameShapeService));
            services.AddScoped(typeof(IRoadNameService), typeof(RoadNameService));
            services.AddScoped(typeof(IRegionService), typeof(RegionService));
            services.AddScoped(typeof(IProvinceService), typeof(ProvinceService));
            services.AddScoped(typeof(IRoadElasticService), typeof(RoadElasticService));
        }
    }
}
