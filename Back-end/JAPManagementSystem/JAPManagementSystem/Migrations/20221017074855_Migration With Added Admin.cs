using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAPManagementSystem.Migrations
{
    public partial class MigrationWithAddedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "stringstring", 0, "9b4d5152-3afc-4f86-a11f-72902fb02302", "Admin", "malikhuremovic01@gmail.com", false, "Malik", "Huremovic", false, null, null, null, "AQAAAAEAACcQAAAAENcrT5CE4+xUrK/b9+hH8bExDdQ9mhyst1LsUEzP8/Qp5npNrCLGRS2feRIorsxs6A==", null, false, 0, "592a4f2b-e965-4024-9051-14f948776eb9", false, "malikhuremovic" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "stringstring");
        }
    }
}
