using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace school.Migrations
{
    /// <inheritdoc />
    public partial class UserRolesJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_roles_RolesId",
                table: "RoleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_users_UsersId",
                table: "RoleUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleUser",
                table: "RoleUser");

            migrationBuilder.RenameTable(
                name: "RoleUser",
                newName: "user_roles");

            migrationBuilder.RenameIndex(
                name: "IX_RoleUser_UsersId",
                table: "user_roles",
                newName: "IX_user_roles_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles",
                columns: new[] { "RolesId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_roles_RolesId",
                table: "user_roles",
                column: "RolesId",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_users_UsersId",
                table: "user_roles",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_roles_RolesId",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_users_UsersId",
                table: "user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles");

            migrationBuilder.RenameTable(
                name: "user_roles",
                newName: "RoleUser");

            migrationBuilder.RenameIndex(
                name: "IX_user_roles_UsersId",
                table: "RoleUser",
                newName: "IX_RoleUser_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleUser",
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_roles_RolesId",
                table: "RoleUser",
                column: "RolesId",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_users_UsersId",
                table: "RoleUser",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
