using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelixLaserWorks.Infrastructure.Migrations
{
    public partial class UsersAndRoleSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cb7a13fd-35ff-4924-a06c-52edcb74e608", "cfdc6ab1-b4e9-4c10-82c2-3672047f502d", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2c2e7178-6349-4801-88fb-426de93ab2c7", 0, "868d2c01-9fd8-45dd-b272-85f97513cde2", "customer@mail.com", false, false, null, "CUSTOMER@MAIL.COM", "CUSTOMER@MAIL.COM", "AQAAAAEAACcQAAAAEAjC83MyoXkNPO93SgtUWKlwabya1bey2NghskLi9/qAh7zhVudVZz6vSwtc1AUkrQ==", null, false, "f9815b7e-18b3-4905-8d78-c0e28d8a139a", false, "customer@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "534e5524-cfeb-4561-b7fb-db4ded672702", 0, "20e81072-abd8-4145-adb8-d07a6433af39", "admin@mail.com", false, false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAEBO/HIdChEsheSdmGJKoKXqu9XMgSdztz5j18Usk1aj4iIwEU9O+lbEzzBKYt0GaAQ==", null, false, "b7692e12-fc71-486e-a30a-81f9b295bbf9", false, "admin@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cb7a13fd-35ff-4924-a06c-52edcb74e608", "534e5524-cfeb-4561-b7fb-db4ded672702" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cb7a13fd-35ff-4924-a06c-52edcb74e608", "534e5524-cfeb-4561-b7fb-db4ded672702" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c2e7178-6349-4801-88fb-426de93ab2c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb7a13fd-35ff-4924-a06c-52edcb74e608");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "534e5524-cfeb-4561-b7fb-db4ded672702");
        }
    }
}
