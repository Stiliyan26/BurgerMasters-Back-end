using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerMasters.Infrastructure.Migrations
{
    public partial class newww : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ReviewMessages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "453a4524-0cd1-46e6-abde-3219df401504",
                column: "ConcurrencyStamp",
                value: "c3d48268-59f2-4f53-9f1f-48e6964eff0f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a439eb91-8c15-4e7a-abef-7f4ebc004826",
                column: "ConcurrencyStamp",
                value: "2ffb5e2c-db6e-4b20-a051-c497e23f5ee7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a0407939-a95d-40a2-8db6-020d349bd2bb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f764931c-9e79-4c9b-9d69-4e16030d5152", "AQAAAAEAACcQAAAAECH5Ewk+Tn1lsRPSqQKVMlPRM0e9nh6FUb/Ri9TL6OLSjTj51RuWmkT5JMOD+579WQ==", "62d334b9-b5ef-408b-8b6a-a1e10aea58c2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c30d2c49-d677-42b3-9295-a0b1dae91806",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91bac091-17e1-490c-9914-347c1d38104d", "AQAAAAEAACcQAAAAENg4tolh1W5nZwT93Z0bx1vs7QHcF2UvGukyvg0vyEaloxXjuWbNf6YhYnTTrU62aA==", "3a18c9e0-7052-4f5e-97ea-dd4f63e8220d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e130798b-a521-45ad-85df-b232eaaadc09",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f11d722-4ffa-4d1c-b63f-19e5ad623d00", "AQAAAAEAACcQAAAAEL/wjM13RAVYxYOJvu/rMe3PaQFKoEnnjLlYxs/zGg0zcRCDC3FxuVqaS5ubdRlgew==", "5e98ed31-9242-46a4-9e03-450f2cd358b8" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewMessages_ApplicationUserId",
                table: "ReviewMessages",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewMessages_AspNetUsers_ApplicationUserId",
                table: "ReviewMessages",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewMessages_AspNetUsers_ApplicationUserId",
                table: "ReviewMessages");

            migrationBuilder.DropIndex(
                name: "IX_ReviewMessages_ApplicationUserId",
                table: "ReviewMessages");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ReviewMessages");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "453a4524-0cd1-46e6-abde-3219df401504",
                column: "ConcurrencyStamp",
                value: "464ad430-046d-4f49-83da-156dd73ecad7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a439eb91-8c15-4e7a-abef-7f4ebc004826",
                column: "ConcurrencyStamp",
                value: "f85f4942-cd03-417e-a339-b37f57ca1ffb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a0407939-a95d-40a2-8db6-020d349bd2bb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d109b1b-b26d-463a-9eff-ad09ecbc2edc", "AQAAAAEAACcQAAAAEBkaIZo45EoYhDjCEzfnH+jDVfkDtC73oJCWXDv4ysv3hMF+xEtFpbcAJplaZlgvrA==", "4acf8046-767d-42d1-9b9b-228ed6d2c518" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c30d2c49-d677-42b3-9295-a0b1dae91806",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0275abe0-d58b-4e00-b5a9-d00b255c8bb2", "AQAAAAEAACcQAAAAENbLCFzm/jizFzzHukJV6wnSGGtNSi0xyL0ceOVCNzlf2X3AuuTH8HhzKn5T6Wujrg==", "0ba98721-64cc-4b4f-9c26-bdfa66ba366d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e130798b-a521-45ad-85df-b232eaaadc09",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97efb406-7937-46ec-80f0-1298704f0008", "AQAAAAEAACcQAAAAEOHhRaGNJhvP/aO2hnjX5yPNzRDpSTpKDd1S2af5utVgf+5Lbc4F5Tl3bLum7d2q8Q==", "47a9e045-6516-477e-8c43-4c509569c425" });
        }
    }
}
