using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAPManagementSystem.Migrations
{
    public partial class MigrationWithAddedAdmin2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "stringstring");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0b5260a0-94c9-4681-8468-945a4aa4373f", 0, "0af37133-6d9e-4e43-aa0a-e88240493840", "Admin", "malikhuremovic2001@hotmail.com", false, "Malik", "Huremovic", false, null, "MALIKHUREMOVIC2001@HOTMAIL.COM", "MALIKHUREM", "AQAAAAEAACcQAAAAEAT9mk3FWTVJa/q7eobHLC7r4P8wMbs9fcfttAYtUF/7eGFX+sOtz9gosH5zWhNXiQ==", null, false, 0, "B4WFEMAOZ47PNHKJF642V6QWHWK2JHPN", false, "malikhurem" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b5260a0-94c9-4681-8468-945a4aa4373f");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "stringstring", 0, "9b4d5152-3afc-4f86-a11f-72902fb02302", "Admin", "malikhuremovic01@gmail.com", false, "Malik", "Huremovic", false, null, null, null, "AQAAAAEAACcQAAAAENcrT5CE4+xUrK/b9+hH8bExDdQ9mhyst1LsUEzP8/Qp5npNrCLGRS2feRIorsxs6A==", null, false, 0, "592a4f2b-e965-4024-9051-14f948776eb9", false, "malikhuremovic" });
        }
    }
}
