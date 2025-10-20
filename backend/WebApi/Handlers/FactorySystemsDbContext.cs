using Microsoft.EntityFrameworkCore;
using Models;

namespace WebApi.Handlers
{
    public class FactorySystemsDbContext : DbContext
    {
        public FactorySystemsDbContext(DbContextOptions<FactorySystemsDbContext> options) : base(options)
        {
        }
            
        public DbSet<SystemsDTO> SystemsDbSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AddStaticData(modelBuilder);
        }

        private void AddStaticData(ModelBuilder modelBuilder)
        {
            SystemsDTO systemsData = new()
            {
                Id = Guid.NewGuid(),
                ApplicationName = "CustomerSystem",
                ApplicationCode = "123ABC",
                CostCenter = "ABC112",
                EmailSupport = [
                    "customer1@company.com",
                    "customer2@company.com"
                    ],
                Status = "Ativo",
                Database = "SQL Server",
                InstallationLocation = "Main Server"
            };

            modelBuilder.Entity<SystemsDTO>().HasData(systemsData);
        }
    }
}
