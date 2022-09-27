using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAPManagementSystem.Migrations
{
    public partial class Finalmigrationwithseededdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "JapPrograms",
                columns: new[] { "Id", "Content", "Name" },
                values: new object[] { 1, ".NET & React.js", "JAP DEV" });

            migrationBuilder.InsertData(
                table: "JapPrograms",
                columns: new[] { "Id", "Content", "Name" },
                values: new object[] { 2, "Selenium & Unit & Integration Testing", "JAP QA" });

            migrationBuilder.InsertData(
                table: "JapPrograms",
                columns: new[] { "Id", "Content", "Name" },
                values: new object[] { 3, "Linux & Docker", "JAP DevOps" });

            migrationBuilder.InsertData(
                table: "Selections",
                columns: new[] { "Id", "DateEnd", "DateStart", "JapProgramId", "Name", "Status" },
                values: new object[] { 1, new DateTime(2022, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Dev Jap September", 1 });

            migrationBuilder.InsertData(
                table: "Selections",
                columns: new[] { "Id", "DateEnd", "DateStart", "JapProgramId", "Name", "Status" },
                values: new object[] { 2, new DateTime(2022, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Dev QA June", 2 });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "SelectionId", "Status" },
                values: new object[,]
                {
                    { 1, "johndoe@mail.com", "John", "Doe", 1, 1 },
                    { 2, "janendoe@mail.com", "Jane", "Doe", 1, 2 },
                    { 3, "johndoe@mail.com", "John", "Doe", 1, 1 },
                    { 4, "malikhuremovic01@mail.com", "Malik", "Huremović", 1, 1 },
                    { 5, "isakIsabegovic@mail.com", "Ishak", "Isabegović", 1, 1 },
                    { 6, "emirbajric@mail.com", "Emir", "Bajrić", 2, 2 },
                    { 7, "zlatansprečo@mail.com", "Zlatan", "Sprečo", 2, 2 },
                    { 8, "sanelh@mail.com", "Sanel", "Hodžić", 2, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JapPrograms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Selections",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Selections",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JapPrograms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JapPrograms",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
