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
                    project_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    project_name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    project_address = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    project_status = table.Column<int>(type: "int", nullable: false),
                    project_isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    project_deletedTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.project_id);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    service_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    service_description = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    service_isActive = table.Column<bool>(type: "bit", nullable: false),
                    service_hourlyRate = table.Column<double>(type: "float", nullable: false),
                    service_isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    service_deletedTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.service_id);
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
                    user_deletedTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "works",
                columns: table => new
                {
                    work_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    work_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Project = table.Column<int>(type: "int", nullable: false),
                    Service = table.Column<int>(type: "int", nullable: false),
                    work_hoursQuantity = table.Column<int>(type: "int", nullable: false),
                    work_hourlyRate = table.Column<double>(type: "float", nullable: false),
                    work_cost = table.Column<double>(type: "float", nullable: false),
                    work_isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    work_deletedTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works", x => x.work_id);
                });

            migrationBuilder.InsertData(
                table: "projects",
                columns: new[] { "project_id", "project_address", "project_deletedTimeUtc", "project_isDeleted", "project_name", "project_status" },
                values: new object[,]
                {
                    { 1, "123 Main St", null, false, "Project 1", 0 },
                    { 2, "456 Elm St", null, false, "Project 2", 1 },
                    { 3, "789 Oak St", null, false, "Project 3", 0 }
                });

            migrationBuilder.InsertData(
                table: "services",
                columns: new[] { "service_id", "service_deletedTimeUtc", "service_description", "service_hourlyRate", "service_isActive", "service_isDeleted" },
                values: new object[,]
                {
                    { 1, null, "Service 1", 25.0, true, false },
                    { 2, null, "Service 2", 30.0, true, false },
                    { 3, null, "Service 3", 20.0, false, false }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "user_id", "user_deletedTimeUtc", "user_dni", "user_email", "user_firstName", "user_isDeleted", "user_lastName", "user_password", "user_type" },
                values: new object[,]
                {
                    { 1, null, "1001010", "pablo@example.com", "Pablo", false, "Ortiz", "123", 1 },
                    { 2, null, "213", "alice@example.com", "Alice", false, "Johnson", "456", 1 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "214", "bob@example.com", "Bob", true, "Smith", "789", 0 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "315", "eva@example.com", "Eva", false, "Lee", "567", 0 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "416", "john@example.com", "John", true, "Doe", "901", 0 }
                });

            migrationBuilder.InsertData(
                table: "works",
                columns: new[] { "work_id", "work_cost", "work_date", "work_deletedTimeUtc", "work_hourlyRate", "work_hoursQuantity", "work_isDeleted", "Project", "Service" },
                values: new object[,]
                {
                    { 1, 1000.0, new DateTime(2023, 9, 12, 16, 5, 41, 306, DateTimeKind.Local).AddTicks(9912), null, 25.0, 40, false, 1, 1 },
                    { 2, 900.0, new DateTime(2023, 9, 11, 16, 5, 41, 306, DateTimeKind.Local).AddTicks(9922), null, 30.0, 30, false, 2, 2 },
                    { 3, 1000.0, new DateTime(2023, 9, 10, 16, 5, 41, 306, DateTimeKind.Local).AddTicks(9927), null, 20.0, 50, false, 1, 3 },
                    { 4, 980.0, new DateTime(2023, 9, 9, 16, 5, 41, 306, DateTimeKind.Local).AddTicks(9928), null, 28.0, 35, false, 2, 1 },
                    { 5, 990.0, new DateTime(2023, 9, 8, 16, 5, 41, 306, DateTimeKind.Local).AddTicks(9929), null, 22.0, 45, false, 3, 2 }
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
