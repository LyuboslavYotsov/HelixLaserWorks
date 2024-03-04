using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelixLaserWorks.Infrastructure.Migrations
{
    public partial class CooperAndChipwoodPriceFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 4,
                column: "PricePerSquareMeter",
                value: 42.00m);

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 5,
                column: "PricePerSquareMeter",
                value: 6.00m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 4,
                column: "PricePerSquareMeter",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 5,
                column: "PricePerSquareMeter",
                value: 0m);
        }
    }
}
