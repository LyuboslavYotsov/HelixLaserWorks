using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelixLaserWorks.Infrastructure.Migrations
{
    public partial class OfferIsCustomerContactedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCustomerContacted",
                table: "Offers",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustomerContacted",
                table: "Offers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb7a13fd-35ff-4924-a06c-52edcb74e608",
                column: "ConcurrencyStamp",
                value: "cfdc6ab1-b4e9-4c10-82c2-3672047f502d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c2e7178-6349-4801-88fb-426de93ab2c7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "868d2c01-9fd8-45dd-b272-85f97513cde2", "AQAAAAEAACcQAAAAEAjC83MyoXkNPO93SgtUWKlwabya1bey2NghskLi9/qAh7zhVudVZz6vSwtc1AUkrQ==", "f9815b7e-18b3-4905-8d78-c0e28d8a139a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "534e5524-cfeb-4561-b7fb-db4ded672702",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20e81072-abd8-4145-adb8-d07a6433af39", "AQAAAAEAACcQAAAAEBO/HIdChEsheSdmGJKoKXqu9XMgSdztz5j18Usk1aj4iIwEU9O+lbEzzBKYt0GaAQ==", "b7692e12-fc71-486e-a30a-81f9b295bbf9" });
        }
    }
}
