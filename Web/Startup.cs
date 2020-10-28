using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Web.Controllers;
using Web.Extensions;
using Web.Helpers;
using Web.Middleware;

namespace Web {
    public class Startup {
        private readonly IConfiguration _config;
        public Startup (IConfiguration configuration) {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddControllers ();
            services.AddAutoMapper (typeof (MappingProfiles));
            //services.AddDbContext<StoreContext> (x => x.UseSqlite (_config.GetConnectionString ("DefaultString")));
            services.AddDbContext<StoreContext> (sql => sql.UseSqlServer (_config.GetConnectionString ("SQLDefaultString")));
            services.AddSingleton<IConnectionMultiplexer> (c => {
                var configuration = ConfigurationOptions.Parse (_config.GetConnectionString ("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            services.AddSwaggerServices ();
            // services.AddCors (opt => {
            //     opt.AddPolicy ("CorsPolicy", policy => {
            //         policy.AllowAnyHeader ().AllowAnyMethod ().WithOrigins ("https://localhost:5001");
            //     });
            // });
            services.AddCors (options => {
                options.AddPolicy ("CorsPolicy",
                    builder => {
                        builder.WithOrigins ("http://localhost:4200")
                            .AllowAnyHeader ()
                            .AllowAnyMethod ();
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseMiddleware<ExceptionMiddleware> ();
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            app.UseSwaggerDocuments ();
            app.UseStatusCodePagesWithReExecute ("/errors/{0}");
            app.UseHttpsRedirection ();
            app.UseStaticFiles ();
            app.UseCors ("CorsPolicy");
            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}