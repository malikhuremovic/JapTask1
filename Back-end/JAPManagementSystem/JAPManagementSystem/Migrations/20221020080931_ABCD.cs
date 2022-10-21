using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAPManagementSystem.Migrations
{
    public partial class ABCD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectedHours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JapProgramLecture",
                columns: table => new
                {
                    LectureId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JapProgramLecture", x => new { x.LectureId, x.ProgramId });
                    table.ForeignKey(
                        name: "FK_JapProgramLecture_JapPrograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "JapPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JapProgramLecture_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JapProgramLecture_ProgramId",
                table: "JapProgramLecture",
                column: "ProgramId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JapProgramLecture");

            migrationBuilder.DropTable(
                name: "Lectures");
        }
    }
}
