using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAPManagementSystem.Migrations
{
    public partial class test23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JapProgramLecture");

            migrationBuilder.CreateTable(
                name: "ProgramLectures",
                columns: table => new
                {
                    LectureId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramLectures", x => new { x.LectureId, x.ProgramId });
                    table.ForeignKey(
                        name: "FK_ProgramLectures_JapPrograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "JapPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramLectures_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramLectures_ProgramId",
                table: "ProgramLectures",
                column: "ProgramId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramLectures");

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
    }
}
