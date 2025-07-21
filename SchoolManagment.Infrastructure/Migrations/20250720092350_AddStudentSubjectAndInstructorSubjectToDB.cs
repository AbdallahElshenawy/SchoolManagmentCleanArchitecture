using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentSubjectAndInstructorSubjectToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentSubjects_SubID",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentSubjects_DID",
                table: "DepartmentSubjects");

            migrationBuilder.DropColumn(
                name: "StudSubID",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "DeptSubID",
                table: "DepartmentSubjects");

            migrationBuilder.AddColumn<int>(
                name: "InsManagerId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                columns: new[] { "SubID", "StudID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects",
                columns: new[] { "DID", "SubID" });

            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuperVisorId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DID = table.Column<int>(type: "int", nullable: false),
                    DepartmentDID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.InsId);
                    table.ForeignKey(
                        name: "FK_Instructor_Departments_DepartmentDID",
                        column: x => x.DepartmentDID,
                        principalTable: "Departments",
                        principalColumn: "DID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructor_Instructor_SuperVisorId",
                        column: x => x.SuperVisorId,
                        principalTable: "Instructor",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstructorSubject",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false),
                    SubId = table.Column<int>(type: "int", nullable: false),
                    InstructorInsId = table.Column<int>(type: "int", nullable: false),
                    SubjectSubID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorSubject", x => new { x.InsId, x.SubId });
                    table.ForeignKey(
                        name: "FK_InstructorSubject_Instructor_InstructorInsId",
                        column: x => x.InstructorInsId,
                        principalTable: "Instructor",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorSubject_Subjects_SubjectSubID",
                        column: x => x.SubjectSubID,
                        principalTable: "Subjects",
                        principalColumn: "SubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InsManagerId",
                table: "Departments",
                column: "InsManagerId",
                unique: true,
                filter: "[InsManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_DepartmentDID",
                table: "Instructor",
                column: "DepartmentDID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_SuperVisorId",
                table: "Instructor",
                column: "SuperVisorId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSubject_InstructorInsId",
                table: "InstructorSubject",
                column: "InstructorInsId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSubject_SubjectSubID",
                table: "InstructorSubject",
                column: "SubjectSubID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructor_InsManagerId",
                table: "Departments",
                column: "InsManagerId",
                principalTable: "Instructor",
                principalColumn: "InsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructor_InsManagerId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "InstructorSubject");

            migrationBuilder.DropTable(
                name: "Instructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Departments_InsManagerId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "InsManagerId",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "StudSubID",
                table: "StudentSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "DeptSubID",
                table: "DepartmentSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSubjects",
                table: "StudentSubjects",
                column: "StudSubID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentSubjects",
                table: "DepartmentSubjects",
                column: "DeptSubID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjects_SubID",
                table: "StudentSubjects",
                column: "SubID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSubjects_DID",
                table: "DepartmentSubjects",
                column: "DID");
        }
    }
}
