using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Microsoft.AspNetCore.Builder;

namespace Web.Extensions
{
    public static class SwaggerServicesExtensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddSwaggerGen(sw =>
            {
                sw.SwaggerDoc("v1",
                                new OpenApiInfo
                                {
                                    Title = "Ecommerce API",
                                    Version = "v1"
                                });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }   
                };
                sw.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement { {securitySchema , new string[] {"Bearer"}}};
                sw.AddSecurityRequirement(securityRequirement);
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocuments(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(sw =>
            {
                sw.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API v1");
            });
            return app;
        }

    }
}