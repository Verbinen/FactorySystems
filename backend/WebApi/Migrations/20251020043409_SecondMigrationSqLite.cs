using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigrationSqLite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemsDbSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ApplicationName = table.Column<string>(type: "TEXT", nullable: false),
                    ApplicationCode = table.Column<string>(type: "TEXT", nullable: false),
                    CostCenter = table.Column<string>(type: "TEXT", nullable: false),
                    EmailSupport = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Database = table.Column<string>(type: "TEXT", nullable: false),
                    InstallationLocation = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemsDbSet", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SystemsDbSet",
                columns: new[] { "Id", "ApplicationCode", "ApplicationName", "CostCenter", "Database", "EmailSupport", "InstallationLocation", "Status" },
                values: new object[] { new Guid("9affc4f0-4994-4781-980d-4edce4e47dc9"), "123ABC", "CustomerSystem", "ABC112", "SQL Server", "[\"customer1@company.com\",\"customer2@company.com\"]", "Main Server", "Ativo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemsDbSet");
        }
    }
}
