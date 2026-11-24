using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace p48_lessons_asp.Migrations
{
    /// <inheritdoc />
    public partial class SetUpAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "Id", "Completed", "CreatedAt", "DeadlineDate", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("00040993-0902-445c-95d8-6a950012dcfd"), false, new DateTime(2026, 11, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Local), "An uncompleted task.", "Uncompleted Task" },
                    { new Guid("31bbe80d-b560-4e1b-82e7-313aa6b08a5f"), false, new DateTime(2026, 11, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 11, 23, 0, 0, 0, 0, DateTimeKind.Local), "An expired task.", "Expired Task" },
                    { new Guid("376bafb4-0956-41fb-a8dc-7ccc0ce77482"), true, new DateTime(2026, 11, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Local), "A completed task", "Completed Task" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("00040993-0902-445c-95d8-6a950012dcfd"));

            migrationBuilder.DeleteData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("31bbe80d-b560-4e1b-82e7-313aa6b08a5f"));

            migrationBuilder.DeleteData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: new Guid("376bafb4-0956-41fb-a8dc-7ccc0ce77482"));
        }
    }
}
