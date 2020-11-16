using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Core.Entities.Identity;

namespace Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host =CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try 
                {
                  var context = services.GetRequiredService<StoreContext>();
                  await context.Database.MigrateAsync();  
                  await StoreContextSeedData.SeedAsync(context, loggerFactory);    

                  var userManager = services.GetRequiredService<UserManager<AppUser>>();
                   var identityContext =  services.GetRequiredService<AppIdentityDbContext>();
                  await identityContext.Database.MigrateAsync();
                  await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
                }
                catch (Exception ex)
                {
                  var logger = loggerFactory.CreateLogger<Program>();
                  logger.LogError(ex, "An error occurred during migration");      
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => 
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
