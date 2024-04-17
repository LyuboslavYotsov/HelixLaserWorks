using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelixLaserWorks.Infrastructure.Migrations
{
    public partial class MaterialTypeDbSetAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_MaterialType_MaterialTypeId",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialType",
                table: "MaterialType");

            migrationBuilder.RenameTable(
                name: "MaterialType",
                newName: "MaterialTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialTypes",
                table: "MaterialTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_MaterialTypes_MaterialTypeId",
                table: "Materials",
                column: "MaterialTypeId",
                principalTable: "MaterialTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_MaterialTypes_MaterialTypeId",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialTypes",
                table: "MaterialTypes");

            migrationBuilder.RenameTable(
                name: "MaterialTypes",
                newName: "MaterialType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialType",
                table: "MaterialType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_MaterialType_MaterialTypeId",
                table: "Materials",
                column: "MaterialTypeId",
                principalTable: "MaterialType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
