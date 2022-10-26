using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAPManagementSystem.Migrations
{
    public partial class newmigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "ExpectedHours", "IsEvent", "Name", "URL" },
                values: new object[,]
                {
                    { 4, "Lorem ipsum dolor sit amet", 17, false, "Postman API Testing", "www.loremipsum.dolor" },
                    { 5, "Lorem ipsum dolor sit amet", 12, false, ".NET Core API | Jumpstart", "www.loremipsum.dolor" },
                    { 6, "Lorem ipsum dolor sit amet", 12, true, "Project task no.1", "www.loremipsum.org" },
                    { 7, "Lorem ipsum dolor sit amet", 13, false, "HTML5 & CSS3 with Animations", "www.loremipsum.dolor" },
                    { 8, "Lorem ipsum dolor sit amet", 14, false, "Complete Javascript Bootcamp | ES6", "www.loremipsum.dolor" },
                    { 9, "Lorem ipsum dolor sit amet", 3, true, "Task refactor", "" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
