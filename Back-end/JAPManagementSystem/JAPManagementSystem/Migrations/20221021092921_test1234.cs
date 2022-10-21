using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAPManagementSystem.Migrations
{
    public partial class test1234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LectureId",
                table: "Lectures",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProgramId",
                table: "JapPrograms",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Lectures",
                newName: "LectureId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "JapPrograms",
                newName: "ProgramId");
        }
    }
}
