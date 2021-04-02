using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoC.Calendar.Data.Migrations
{
    public partial class AddDefaultData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "End", "Start", "Text" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 3, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 3, 31, 0, 0, 0, 0, DateTimeKind.Local), "Birthday" },
                    { 2, new DateTime(2021, 3, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 3, 22, 0, 0, 0, 0, DateTimeKind.Local), "Day off" },
                    { 3, new DateTime(2021, 3, 25, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 3, 23, 0, 0, 0, 0, DateTimeKind.Local), "Work from home" },
                    { 4, new DateTime(2021, 4, 2, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 4, 2, 10, 0, 0, 0, DateTimeKind.Local), "Online meeting" },
                    { 5, new DateTime(2021, 4, 2, 13, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 4, 2, 10, 0, 0, 0, DateTimeKind.Local), "Skype call" },
                    { 6, new DateTime(2021, 4, 2, 14, 30, 0, 0, DateTimeKind.Local), new DateTime(2021, 4, 2, 14, 0, 0, 0, DateTimeKind.Local), "Dentist appointment" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
