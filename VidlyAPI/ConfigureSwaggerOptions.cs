using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace VidlyAPI
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
            => this.provider = provider;
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var Desc in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(Desc.GroupName, new OpenApiInfo()
                {
                    Title = $"Vidly {Desc.ApiVersion}",
                    Version = Desc.ApiVersion.ToString()
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Jwt authorization header using bearer scheme /r/n/r/n " +
                "Enter 'Bearer' [space] and then your token in the text input below " +
                "/r/n/r/n Example:\"Bearer 123abcdef\"",
                    Name = "Authanication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                    new List<string>()
                    }
                });
            }
        }
    }
}
