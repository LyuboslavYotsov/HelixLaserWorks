using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelixLaserWorks.Infrastructure.Migrations
{
    public partial class OfferAcceptedOnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptedOn",
                table: "Offers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb7a13fd-35ff-4924-a06c-52edcb74e608",
                column: "ConcurrencyStamp",
                value: "cbb13e38-36bb-4aee-80ea-5bca6081b303");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c2e7178-6349-4801-88fb-426de93ab2c7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9065acc8-15df-460f-8973-1a137415144d", "AQAAAAEAACcQAAAAEHhm86nQfu5/8BcoUSjAs+uypisuInDsDmv9qJy+i67Zg3PajvWo9Xf3A4CoxvC3ZQ==", "e3d707da-5fdf-470c-b25f-4de653cf6910" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "534e5524-cfeb-4561-b7fb-db4ded672702",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c227cde-d8ad-4dad-a6a2-ba71f27eb40a", "AQAAAAEAACcQAAAAEH7165/fR8hvOh9ylXV61/hoVMTuIhe6IZLttFetZ7LZtT/5XFr158wRrOgacTHVxg==", "a4b8fb86-a40f-49e8-ba17-8587cd9165a9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedOn",
                table: "Offers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb7a13fd-35ff-4924-a06c-52edcb74e608",
                column: "ConcurrencyStamp",
                value: "8232c3a6-4379-4093-9649-5807679bb030");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c2e7178-6349-4801-88fb-426de93ab2c7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb3051f3-fa48-4ae3-8e3b-ffde11d6caf6", "AQAAAAEAACcQAAAAEP2YdJnVL2917ABgKBCYwX8z4VdHddoIfBXYqrx/8g/oi5ba3B/dVDOcJUFoTI9M9g==", "1256de34-67f8-4e94-8034-c04ca8905a1f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "534e5524-cfeb-4561-b7fb-db4ded672702",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f516a5c7-8a8f-4687-8109-4d7f49f20377", "AQAAAAEAACcQAAAAECNnLnYRCj+XvcHg11h/mmQtwC1DMQe8/7SIv+gXyCXHS5jidaFPnwDXrnRLbIryiA==", "6ab1ec85-b643-494e-8f19-dab019075b78" });
        }
    }
}
