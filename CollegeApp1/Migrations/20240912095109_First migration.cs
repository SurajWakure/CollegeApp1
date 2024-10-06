using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Collegeapp1.Migrations
{
    /// <inheritdoc />
    public partial class Firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    marks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "Address", "DOB", "Email", "Name", "marks" },
                values: new object[,]
                {
                    { 1, "sawtamalii nagar murud", new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "surajwakure007@gmail.com", "suraj", 0 },
                    { 2, "sawtamalii nagar murud", new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "ajitwakure007@gmail.com", "ajit", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "students");
        }
    }
}
