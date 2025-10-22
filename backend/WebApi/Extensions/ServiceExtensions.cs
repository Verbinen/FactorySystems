using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
            return builder.Services.AddDbContext<FactorySystemsDbContext>(options => options.UseSqlite("Data Source=factory_systems.db"));
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
                
            return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()) ;
        }

        public static IServiceProvider ApplyDatabaseMigration(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<FactorySystemsDbContext>();
            db.Database.Migrate();

            return serviceProvider;
        }

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(setup =>
            {
                setup.AddPolicy("Cors", policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });

            return services;
        }
    }
}
