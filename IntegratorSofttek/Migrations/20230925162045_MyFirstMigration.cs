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
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    role_deletedTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.role_id);
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
                    user_password = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    user_email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    user_isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    user_deletedTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_users_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "works",
                columns: table => new
                {
                    work_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    work_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    service_id = table.Column<int>(type: "int", nullable: false),
                    project_id = table.Column<int>(type: "int", nullable: false),
                    work_hoursQuantity = table.Column<int>(type: "int", nullable: false),
                    work_hourlyRate = table.Column<double>(type: "float", nullable: false),
                    work_cost = table.Column<double>(type: "float", nullable: false),
                    work_isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    work_deletedTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_works", x => x.work_id);
                    table.ForeignKey(
                        name: "FK_works_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_works_services_service_id",
                        column: x => x.service_id,
                        principalTable: "services",
                        principalColumn: "service_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "projects",
                columns: new[] { "project_id", "project_address", "project_deletedTimeUtc", "project_isDeleted", "project_name", "project_status" },
                values: new object[,]
                {
                    { 1, "123 Main St", null, false, "Project 1", 0 },
                    { 2, "456 Elm St", null, false, "Project 2", 1 },
                    { 3, "789 Oak St", null, false, "Project 3", 0 },
                    { 4, "101 Pine St", null, false, "Project 4", 1 },
                    { 5, "222 Cedar St", null, false, "Project 5", 0 },
                    { 6, "333 Oak St", null, false, "Project 6", 2 },
                    { 7, "444 Elm St", null, false, "Project 7", 0 },
                    { 8, "555 Maple St", null, false, "Project 8", 1 },
                    { 9, "666 Birch St", null, false, "Project 9", 0 },
                    { 10, "777 Walnut St", null, false, "Project 10", 0 }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "role_id", "role_deletedTimeUtc", "role_description", "role_isDeleted", "role_name" },
                values: new object[,]
                {
                    { 1, null, "Administrator", false, "Administrator" },
                    { 2, null, "Consultant", false, "Consultant" }
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
                columns: new[] { "user_id", "user_deletedTimeUtc", "user_dni", "user_email", "user_firstName", "user_isDeleted", "user_lastName", "user_password", "role_id" },
                values: new object[,]
                {
                    { 1, null, "1001010", "adm@gmail.com", "Pablo", false, "Ortiz", "9f3d321cd0a1ccafa899226d5190f74618cb23b789aa998e1d7f741956132434", 1 },
                    { 2, null, "213", "noadm@gmail.com", "Kevin", false, "Johnson", "a10ad3a74bccd29b56cb5ec5a213d1a27b293b6bb88797418a31f09c2a707bf4", 2 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "214", "bob@example.com", "Bob", true, "Smith", "a10ad3a74bccd29b56cb5ec5a213d1a27b293b6bb88797418a31f09c2a707bf4", 1 },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "315", "eva@example.com", "Eva", false, "Lee", "ff192a780cb98e260d87c38683c2e155dfe48897e454e47390063fd76755651f", 2 },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "416", "john@example.com", "John", true, "Doe", "fbafa90f00f6416a6d1e8535234f9603aaf07258d7a98424ec011a5f7aa634ff", 2 }
                });

            migrationBuilder.InsertData(
                table: "works",
                columns: new[] { "work_id", "work_cost", "work_date", "work_deletedTimeUtc", "work_hourlyRate", "work_hoursQuantity", "work_isDeleted", "project_id", "service_id" },
                values: new object[,]
                {
                    { 1, 1000.0, new DateTime(2023, 9, 25, 13, 20, 45, 40, DateTimeKind.Local).AddTicks(6443), null, 25.0, 40, false, 1, 1 },
                    { 2, 900.0, new DateTime(2023, 9, 24, 13, 20, 45, 40, DateTimeKind.Local).AddTicks(6458), null, 30.0, 30, false, 2, 2 },
                    { 3, 1000.0, new DateTime(2023, 9, 23, 13, 20, 45, 40, DateTimeKind.Local).AddTicks(6463), null, 20.0, 50, false, 1, 3 },
                    { 4, 980.0, new DateTime(2023, 9, 22, 13, 20, 45, 40, DateTimeKind.Local).AddTicks(6464), null, 28.0, 35, false, 2, 1 },
                    { 5, 990.0, new DateTime(2023, 9, 21, 13, 20, 45, 40, DateTimeKind.Local).AddTicks(6465), null, 22.0, 45, false, 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_user_email",
                table: "users",
                column: "user_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_works_project_id",
                table: "works",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_works_service_id",
                table: "works",
                column: "service_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "works");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "services");
        }
    }
}
