using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePropForInstructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Departments_DepartmentDID",
                table: "Instructor");

            migrationBuilder.DropIndex(
                name: "IX_Instructor_DepartmentDID",
                table: "Instructor");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentDID",
                table: "Instructor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Instructor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_DID",
                table: "Instructor",
                column: "DID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Departments_DID",
                table: "Instructor",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Departments_DID",
                table: "Instructor");

            migrationBuilder.DropIndex(
                name: "IX_Instructor_DID",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Instructor");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentDID",
                table: "Instructor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_DepartmentDID",
                table: "Instructor",
                column: "DepartmentDID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Departments_DepartmentDID",
                table: "Instructor",
                column: "DepartmentDID",
                principalTable: "Departments",
                principalColumn: "DID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
