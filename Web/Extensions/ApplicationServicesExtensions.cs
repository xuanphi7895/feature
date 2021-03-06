using System.Linq;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Web.Errors;
using Infrastructure.Services;

namespace Web.Extensions {
    public static class ApplicationServicesExtensions {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services) {

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductRepository, ProductRepository> ();
            services.AddScoped<IBasketRepository, BasketRepository> ();
            services.AddScoped (typeof (IGenericRepository<>), typeof (GenericRepository<>));
            services.Configure<ApiBehaviorOptions> (options => options.InvalidModelStateResponseFactory = actionContext => {
                var errors = actionContext.ModelState
                    .Where (e => e.Value.Errors.Count > 0)
                    .SelectMany (x => x.Value.Errors)
                    .Select (x => x.ErrorMessage).ToArray ();
                var errorResponse = new ApiValidationErrorResponse {
                    Errors = errors
                };
                return new BadRequestObjectResult (errorResponse);
            });
            return services;
        }

    }
}