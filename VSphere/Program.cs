using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VSphere;
using VSphere.Context;
using VSphere.Models.Identity;

namespace VCenterVMWare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationIdentityUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                //var context = serviceProvider.GetRequiredService<VSphereContext>();
                DataInitializer.SeedData(userManager, roleManager);
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build());
    }
}
