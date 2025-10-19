using Microsoft.EntityFrameworkCore;
using WebApi.Handlers;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IConfigurationRoot ConfigureAppSettings(this WebApplicationBuilder builder)
        {
            return builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static IServiceCollection ConfigureDatabase(this WebApplicationBuilder builder)
        {
            return builder.Services.AddDbContext<FactorySystemsDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));
        }
    }
}
