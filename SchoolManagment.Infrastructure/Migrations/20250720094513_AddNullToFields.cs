using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNullToFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstructorSubject_Instructor_InstructorInsId",
                table: "InstructorSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructorSubject_Subjects_SubjectSubID",
                table: "InstructorSubject");

            migrationBuilder.AddColumn<decimal>(
                name: "Grade",
                table: "StudentSubjects",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectSubID",
                table: "InstructorSubject",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "InstructorInsId",
                table: "InstructorSubject",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SuperVisorId",
                table: "Instructor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorSubject_Instructor_InstructorInsId",
                table: "InstructorSubject",
                column: "InstructorInsId",
                principalTable: "Instructor",
                principalColumn: "InsId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorSubject_Subjects_SubjectSubID",
                table: "InstructorSubject",
                column: "SubjectSubID",
                principalTable: "Subjects",
                principalColumn: "SubID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstructorSubject_Instructor_InstructorInsId",
                table: "InstructorSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructorSubject_Subjects_SubjectSubID",
                table: "InstructorSubject");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "StudentSubjects");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectSubID",
                table: "InstructorSubject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstructorInsId",
                table: "InstructorSubject",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SuperVisorId",
                table: "Instructor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorSubject_Instructor_InstructorInsId",
                table: "InstructorSubject",
                column: "InstructorInsId",
                principalTable: "Instructor",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorSubject_Subjects_SubjectSubID",
                table: "InstructorSubject",
                column: "SubjectSubID",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
