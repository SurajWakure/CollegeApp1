using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Collegeapp1.Migrations
{
    /// <inheritdoc />
    public partial class marksadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "Id",
                keyValue: 1,
                column: "marks",
                value: 90);

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "Id",
                keyValue: 2,
                column: "marks",
                value: 85);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "Id",
                keyValue: 1,
                column: "marks",
                value: 0);

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "Id",
                keyValue: 2,
                column: "marks",
                value: 0);
        }
    }
}
