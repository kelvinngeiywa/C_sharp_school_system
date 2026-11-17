using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace school.Migrations
{
    /// <inheritdoc />
    public partial class newMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Fullname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    DOB = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roles_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("01933b4a-1000-7000-8000-000000000001"), new DateOnly(2026, 11, 17), "Super Admin role", "SUPER_ADMIN", null },
                    { new Guid("01933b4a-2000-7000-8000-000000000002"), new DateOnly(2026, 11, 17), "Admin role", "ADMIN", null },
                    { new Guid("01933b4a-3000-7000-8000-000000000003"), new DateOnly(2026, 11, 17), "Principal role", "PRINCIPAL", null },
                    { new Guid("01933b4a-4000-7000-8000-000000000004"), new DateOnly(2026, 11, 17), "Teacher role", "TEACHER", null },
                    { new Guid("01933b4a-5000-7000-8000-000000000005"), new DateOnly(2026, 11, 17), "Parent role", "PARENT", null },
                    { new Guid("01933b4a-6000-7000-8000-000000000006"), new DateOnly(2026, 11, 17), "Support staff role", "STAFF", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_roles_UserId",
                table: "roles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
