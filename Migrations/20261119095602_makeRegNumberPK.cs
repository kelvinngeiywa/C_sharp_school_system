using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace school.Migrations
{
    /// <inheritdoc />
    public partial class makeRegNumberPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_student_guardian_students_StudentId",
                table: "student_guardian");

            migrationBuilder.DropPrimaryKey(
                name: "PK_students",
                table: "students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_student_guardian",
                table: "student_guardian");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "student_guardian");

            migrationBuilder.AlterColumn<string>(
                name: "RegNumber",
                table: "students",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Residence",
                table: "students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentRegNumber",
                table: "student_guardian",
                type: "character varying(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_students",
                table: "students",
                column: "RegNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_student_guardian",
                table: "student_guardian",
                columns: new[] { "StudentRegNumber", "GuardianId" });

            migrationBuilder.AddForeignKey(
                name: "FK_student_guardian_students_StudentRegNumber",
                table: "student_guardian",
                column: "StudentRegNumber",
                principalTable: "students",
                principalColumn: "RegNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_student_guardian_students_StudentRegNumber",
                table: "student_guardian");

            migrationBuilder.DropPrimaryKey(
                name: "PK_students",
                table: "students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_student_guardian",
                table: "student_guardian");

            migrationBuilder.DropColumn(
                name: "Residence",
                table: "students");

            migrationBuilder.DropColumn(
                name: "StudentRegNumber",
                table: "student_guardian");

            migrationBuilder.AlterColumn<string>(
                name: "RegNumber",
                table: "students",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "student_guardian",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_students",
                table: "students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_student_guardian",
                table: "student_guardian",
                columns: new[] { "StudentId", "GuardianId" });

            migrationBuilder.AddForeignKey(
                name: "FK_student_guardian_students_StudentId",
                table: "student_guardian",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
