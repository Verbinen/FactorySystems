using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemsDbSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostCenter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSupport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Database = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstallationLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemsDbSet", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SystemsDbSet",
                columns: new[] { "Id", "ApplicationCode", "ApplicationName", "CostCenter", "Database", "EmailSupport", "InstallationLocation", "Status" },
                values: new object[] { new Guid("72739e44-748b-4f9b-8bd0-5243c3578b3f"), "123ABC", "CustomerSystem", "ABC112", "SQL Server", "[\"customer1@company.com\",\"customer2@company.com\"]", "Main Server", "Ativo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemsDbSet");
        }
    }
}
