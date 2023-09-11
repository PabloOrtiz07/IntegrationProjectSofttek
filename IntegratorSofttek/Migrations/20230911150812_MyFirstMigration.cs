using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegratorSofttek.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    HourlyRate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_firstName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    user_lastName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    user_dni = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    user_type = table.Column<int>(type: "int", nullable: false),
                    user_password = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    user_email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    user_isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    user_deletedTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    project = table.Column<int>(type: "int", nullable: false),
                    service = table.Column<int>(type: "int", nullable: false),
                    HoursQuantity = table.Column<int>(type: "int", nullable: false),
                    HourlyRate = table.Column<double>(type: "float", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "projects",
                columns: new[] { "Id", "Adress", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "123 Main St", "Project 1", 1 },
                    { 2, "456 Elm St", "Project 2", 2 },
                    { 3, "789 Oak St", "Project 3", 1 }
                });

            migrationBuilder.InsertData(
                table: "services",
                columns: new[] { "Id", "Description", "HourlyRate", "Status" },
                values: new object[,]
                {
                    { 1, "Service 1", 25.0, true },
                    { 2, "Service 2", 30.0, true },
                    { 3, "Service 3", 20.0, false }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "user_deletedTimeUtc", "user_dni", "user_email", "user_firstName", "user_isDeleted", "user_lastName", "user_password", "user_type" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1001010", "pablo@example.com", "Pablo", false, "Ortiz", "123", 1 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "213", "alice@example.com", "Alice", false, "Johnson", "456", 2 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "214", "bob@example.com", "Bob", true, "Smith", "789", 1 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "315", "eva@example.com", "Eva", false, "Lee", "567", 2 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "416", "john@example.com", "John", true, "Doe", "901", 1 }
                });

            migrationBuilder.InsertData(
                table: "works",
                columns: new[] { "Id", "Cost", "Date", "HourlyRate", "HoursQuantity", "project", "service" },
                values: new object[,]
                {
                    { 1, 1000.0, new DateTime(2023, 9, 11, 12, 8, 12, 728, DateTimeKind.Local).AddTicks(2287), 25.0, 40, 0, 0 },
                    { 2, 900.0, new DateTime(2023, 9, 10, 12, 8, 12, 728, DateTimeKind.Local).AddTicks(2300), 30.0, 30, 0, 0 },
                    { 3, 1000.0, new DateTime(2023, 9, 9, 12, 8, 12, 728, DateTimeKind.Local).AddTicks(2304), 20.0, 50, 0, 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "works");
        }
    }
}
