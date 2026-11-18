using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace school.Migrations
{
    /// <inheritdoc />
    public partial class teacherSubjectMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subjects_teachers_TeacherId",
                table: "subjects");

            migrationBuilder.DropIndex(
                name: "IX_subjects_TeacherId",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "subjects");

            migrationBuilder.CreateTable(
                name: "teacher_subject",
                columns: table => new
                {
                    SubjectsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeachersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_subject", x => new { x.SubjectsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_teacher_subject_subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_teacher_subject_teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_teachers_Email",
                table: "teachers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_teachers_PhoneNumber",
                table: "teachers",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subjects_SubjectCode",
                table: "subjects",
                column: "SubjectCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subjects_SubjectName",
                table: "subjects",
                column: "SubjectName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_teacher_subject_TeachersId",
                table: "teacher_subject",
                column: "TeachersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "teacher_subject");

            migrationBuilder.DropIndex(
                name: "IX_teachers_Email",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_teachers_PhoneNumber",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_subjects_SubjectCode",
                table: "subjects");

            migrationBuilder.DropIndex(
                name: "IX_subjects_SubjectName",
                table: "subjects");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "subjects",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_subjects_TeacherId",
                table: "subjects",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_subjects_teachers_TeacherId",
                table: "subjects",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id");
        }
    }
}
