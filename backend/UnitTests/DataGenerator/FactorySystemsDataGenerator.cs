using WebApi.Models;

namespace UnitTests.DataGenerator
{
    internal static class FactorySystemsDataGenerator
    {
        public static SystemsDTO CreateSystem(
            Guid id, string appName = "PaymentSystem", string appCode = "AA01", string costCenter = "ABC112", string status = "Ativo", string database = "SQL Server", string location = "Main Server")
        {
            return new SystemsDTO()
            {
                Id = id,
                ApplicationName = appName,
                ApplicationCode = appCode,
                CostCenter = costCenter,
                EmailSupport = ["email@company.com"],
                Status = status,
                Database = database,
                InstallationLocation = location
            };
        }
    }
}
