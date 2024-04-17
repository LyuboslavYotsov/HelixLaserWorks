using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelixLaserWorks.Infrastructure.Migrations
{
    public partial class NewMaterialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "CorrosionResistance", "Density", "Description", "ImageUrl", "MaterialTypeId", "Name", "PricePerSquareMeter", "Specification" },
                values: new object[] { 5, true, 0.63, "Chipboard, also known as particleboard, is an engineered wood product made from wood chips, shavings, and resin that are compressed and bonded together under heat and pressure. It’s commonly used in furniture, shelving, and construction applications as an affordable alternative to solid wood, offering good stability but with a slightly coarser texture. One of laser cut Chipboard’s greatest assets is that it’s completely green. Made entirely from recycled pasteboard, our chipboard is 100% recyclable.", "https://media.wickes.co.uk/is/image/wickes/normal/Chipboard-Flooring-Wickes-P5-T-G-Chipboard-Flooring-18mm-x-600mm-x-2-4m~N0705_164516_00?$ratio43$&fit=crop&extend=-50,-250,-50,0", 3, "ChipWood", 0m, null });

            migrationBuilder.InsertData(
                table: "MaterialsThicknesses",
                columns: new[] { "MaterialId", "ThicknessId" },
                values: new object[,]
                {
                    { 5, 1 },
                    { 5, 2 },
                    { 5, 3 },
                    { 5, 4 },
                    { 5, 5 },
                    { 5, 6 },
                    { 5, 7 },
                    { 5, 8 },
                    { 5, 9 },
                    { 5, 10 },
                    { 5, 11 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MaterialsThicknesses",
                keyColumns: new[] { "MaterialId", "ThicknessId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "MaterialsThicknesses",
                keyColumns: new[] { "MaterialId", "ThicknessId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "MaterialsThicknesses",
                keyColumns: new[] { "MaterialId", "ThicknessId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "MaterialsThicknesses",
                keyColumns: new[] { "MaterialId", "ThicknessId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "MaterialsThicknesses",
                keyColumns: new[] { "MaterialId", "ThicknessId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "MaterialsThicknesses",
                keyColumns: new[] { "MaterialId", "ThicknessId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "MaterialsThicknesses",
                keyColumns: new[] { "MaterialId", "ThicknessId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "MaterialsThicknesses",
                keyColumns: new[] { "MaterialId", "ThicknessId" },
                keyValues: new object[] { 5, 8 });

            migrationBuilder.DeleteData(
                table: "MaterialsThicknesses",
                keyColumns: new[] { "MaterialId", "ThicknessId" },
                keyValues: new object[] { 5, 9 });

            migrationBuilder.DeleteData(
                table: "MaterialsThicknesses",
                keyColumns: new[] { "MaterialId", "ThicknessId" },
                keyValues: new object[] { 5, 10 });

            migrationBuilder.DeleteData(
                table: "MaterialsThicknesses",
                keyColumns: new[] { "MaterialId", "ThicknessId" },
                keyValues: new object[] { 5, 11 });

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
