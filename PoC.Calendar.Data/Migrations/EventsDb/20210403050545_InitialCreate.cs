using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PoC.Calendar.Data.Migrations.EventsDb
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalendarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CalendarId", "End", "Start", "Text" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Local), "Birthday" },
                    { 2, 1, new DateTime(2021, 3, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 3, 23, 0, 0, 0, 0, DateTimeKind.Local), "Day off" },
                    { 3, 1, new DateTime(2021, 3, 26, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 3, 24, 0, 0, 0, 0, DateTimeKind.Local), "Work from home" },
                    { 4, 1, new DateTime(2021, 4, 3, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 4, 3, 10, 0, 0, 0, DateTimeKind.Local), "Online meeting" },
                    { 5, 1, new DateTime(2021, 4, 3, 13, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 4, 3, 10, 0, 0, 0, DateTimeKind.Local), "Skype call" },
                    { 6, 1, new DateTime(2021, 4, 3, 14, 30, 0, 0, DateTimeKind.Local), new DateTime(2021, 4, 3, 14, 0, 0, 0, DateTimeKind.Local), "Dentist appointment" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
