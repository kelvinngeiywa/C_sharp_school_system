using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace school.Migrations
{
    /// <inheritdoc />
    public partial class renameToRegNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "regNumber",
                table: "students",
                newName: "RegNumber");

            migrationBuilder.CreateIndex(
                name: "IX_guardians_Email",
                table: "guardians",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_guardians_PhoneNumber",
                table: "guardians",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_guardians_Email",
                table: "guardians");

            migrationBuilder.DropIndex(
                name: "IX_guardians_PhoneNumber",
                table: "guardians");

            migrationBuilder.RenameColumn(
                name: "RegNumber",
                table: "students",
                newName: "regNumber");
        }
    }
}
