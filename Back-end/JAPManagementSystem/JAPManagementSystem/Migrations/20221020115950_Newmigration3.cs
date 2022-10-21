using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAPManagementSystem.Migrations
{
    public partial class Newmigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isEvent",
                table: "Lectures",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isEvent",
                table: "Lectures");
        }
    }
}
