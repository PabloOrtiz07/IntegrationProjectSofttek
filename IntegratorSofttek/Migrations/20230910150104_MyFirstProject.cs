using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegratorSofttek.Migrations
{
    public partial class MyFirstProject : Migration
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dni = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoursQuantity = table.Column<int>(type: "int", nullable: false),
                    HourlyRate = table.Column<double>(type: "float", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_works_projects_Id",
                        column: x => x.Id,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_works_services_Id",
                        column: x => x.Id,
                        principalTable: "services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                columns: new[] { "Id", "Dni", "FirstName", "LastName", "Password", "Type" },
                values: new object[,]
                {
                    { 1, 1001010, "Pablo", "Ortiz", "123", 1 },
                    { 2, 213, "Alice", "Johnson", "456", 2 },
                    { 3, 214, "Bob", "Smith", "789", 1 }
                });

            migrationBuilder.InsertData(
                table: "works",
                columns: new[] { "Id", "Cost", "Date", "HourlyRate", "HoursQuantity" },
                values: new object[] { 1, 1000.0, new DateTime(2023, 9, 10, 12, 1, 4, 715, DateTimeKind.Local).AddTicks(480), 25.0, 40 });

            migrationBuilder.InsertData(
                table: "works",
                columns: new[] { "Id", "Cost", "Date", "HourlyRate", "HoursQuantity" },
                values: new object[] { 2, 900.0, new DateTime(2023, 9, 9, 12, 1, 4, 715, DateTimeKind.Local).AddTicks(492), 30.0, 30 });

            migrationBuilder.InsertData(
                table: "works",
                columns: new[] { "Id", "Cost", "Date", "HourlyRate", "HoursQuantity" },
                values: new object[] { 3, 1000.0, new DateTime(2023, 9, 8, 12, 1, 4, 715, DateTimeKind.Local).AddTicks(496), 20.0, 50 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "works");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "services");
        }
    }
}
