using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class MigrationEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemsDbSet",
                keyColumn: "Id",
                keyValue: new Guid("9affc4f0-4994-4781-980d-4edce4e47dc9"));

            migrationBuilder.InsertData(
                table: "SystemsDbSet",
                columns: new[] { "Id", "ApplicationCode", "ApplicationName", "CostCenter", "Database", "EmailSupport", "InstallationLocation", "Status" },
                values: new object[] { new Guid("57f33d84-5f82-4449-b486-151d39c9a280"), "123ABC", "CustomerSystem", "ABC112", "SQLServer", "[\"customer1@company.com\",\"customer2@company.com\"]", "Main Server", "Ativo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemsDbSet",
                keyColumn: "Id",
                keyValue: new Guid("57f33d84-5f82-4449-b486-151d39c9a280"));

            migrationBuilder.InsertData(
                table: "SystemsDbSet",
                columns: new[] { "Id", "ApplicationCode", "ApplicationName", "CostCenter", "Database", "EmailSupport", "InstallationLocation", "Status" },
                values: new object[] { new Guid("9affc4f0-4994-4781-980d-4edce4e47dc9"), "123ABC", "CustomerSystem", "ABC112", "SQL Server", "[\"customer1@company.com\",\"customer2@company.com\"]", "Main Server", "Ativo" });
        }
    }
}
