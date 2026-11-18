using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace school.Migrations
{
    /// <inheritdoc />
    public partial class manyTomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roles_users_UserId",
                table: "roles");

            migrationBuilder.DropIndex(
                name: "IX_roles_UserId",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "roles");

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "roles",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("01933b4a-1000-7000-8000-000000000001"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("01933b4a-2000-7000-8000-000000000002"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("01933b4a-3000-7000-8000-000000000003"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("01933b4a-4000-7000-8000-000000000004"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("01933b4a-5000-7000-8000-000000000005"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "Id",
                keyValue: new Guid("01933b4a-6000-7000-8000-000000000006"),
                column: "UserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_roles_UserId",
                table: "roles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_roles_users_UserId",
                table: "roles",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
