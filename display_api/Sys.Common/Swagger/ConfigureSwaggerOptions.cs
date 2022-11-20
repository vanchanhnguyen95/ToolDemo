using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

namespace Sys.Common.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;
        private string WEBSITE = "https://ones.rdos.vn/login";

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
          this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                  description.GroupName,
                    new OpenApiInfo()
                    {
                        Title = SystemConfig.SwaggerName + $" - APIs Version {description.GroupName}",
                        Version = description.ApiVersion.ToString(),
                        Extensions = new Dictionary<string, IOpenApiExtension>
                          {
                                {
                                    "x-logo", new OpenApiObject
                                    {
                                        {"url", new OpenApiString("https://afc.unit.vn/assets/images/vinbus_logo_with_slogan.png")},
                                        {"altText", new OpenApiString("The Logo")}
                                    }
                                }
                          },
                        Contact = new OpenApiContact()
                        {
                            Url = new Uri(WEBSITE),
                            Name = SystemConfig.SwaggerName
                        }
                    });
            }
        }
    }
}