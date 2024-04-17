using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelixLaserWorks.Infrastructure.Migrations
{
    public partial class OrderAdminFeedbackMaxLengthAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdminFeedback",
                table: "Orders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb7a13fd-35ff-4924-a06c-52edcb74e608",
                column: "ConcurrencyStamp",
                value: "7dbc5a86-21cc-4b48-bb2e-89fa2bd332db");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c2e7178-6349-4801-88fb-426de93ab2c7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6979741e-a65a-4bf4-8c85-1796afc48371", "AQAAAAEAACcQAAAAEHCmiXnAdjMRE01//vx8XHZH0QDU2OGCAAtKnNGqJZuwokcNauEw8821fg6W+LAREQ==", "c71859af-61de-4896-9511-8307030601d0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "534e5524-cfeb-4561-b7fb-db4ded672702",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b7c9b851-ee1d-438f-b062-d2347337a29e", "AQAAAAEAACcQAAAAEJWs4UUAjshFR3GgCHjExTmNIGTEpwyXITx9DdX4cK75OCHbzEkl7a+KkncXYlV+Qg==", "b20aeaaf-a1d0-4e0f-82b8-66a9797c2df7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdminFeedback",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

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
    }
}
