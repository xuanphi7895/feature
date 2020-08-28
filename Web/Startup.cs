using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Repository;
using Infrastructure.Data;
using Core.Interfaces;
using Web.Controllers;
using Web.Helpers;
using Web.Middleware;
using AutoMapper;
using Web.Errors;
using Microsoft.OpenApi.Models;

namespace Web
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultString")));
            services.Configure<ApiBehaviorOptions>(options => options.InvalidModelStateResponseFactory = actionContext => 
            {
                var errors = actionContext.ModelState
                            .Where(e => e.Value.Errors.Count > 0)
                            .SelectMany(x => x.Value.Errors)
                            .Select(x => x.ErrorMessage).ToArray();
                var errorResponse = new  ApiValidationErrorResponse {
                    Errors = errors
                };
                return new BadRequestObjectResult(errorResponse);           
            });
            services.AddSwaggerGen(sw => {
                sw.SwaggerDoc("v1", 
                                new OpenApiInfo{
                                    Title = "Ecommerce API", Version= "v2"
                                });
            });
            //services.Add
            // services.AddSingleton<IMyService>((container) =>
            // {
            //     var logger = container.GetRequiredService<ILogger<ProductsController>>();
            //     return new ProductsController() { Logger = logger };
            // });
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
             app.UseSwagger();
            app.UseSwaggerUI(sw => {
                sw.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API v1");
                });
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
