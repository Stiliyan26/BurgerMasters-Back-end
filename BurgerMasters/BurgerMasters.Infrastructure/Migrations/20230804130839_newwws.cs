using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerMasters.Infrastructure.Migrations
{
    public partial class newwws : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ReviewMessages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "453a4524-0cd1-46e6-abde-3219df401504",
                column: "ConcurrencyStamp",
                value: "936ee4ec-68c3-4824-b249-5988e0ccbc3f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a439eb91-8c15-4e7a-abef-7f4ebc004826",
                column: "ConcurrencyStamp",
                value: "143b209c-287a-4eda-bbc6-6d8745bb5012");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a0407939-a95d-40a2-8db6-020d349bd2bb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7bdcc602-1806-443f-9caa-62531d75257b", "AQAAAAEAACcQAAAAEArYEuh3JCEhsAe8FEHRnJ48Xy1wNEFuQMpPkvQQuIgCuTfTLBaLtZmStip5wLRQNg==", "59b1d3b6-d4c6-426f-ae61-78ef9efd3ac8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c30d2c49-d677-42b3-9295-a0b1dae91806",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a87cffa7-7c11-47f8-9ce6-749185aee22e", "AQAAAAEAACcQAAAAEN9JzoviNFkX1ATWjeNTvMtNrVKlAGG/GQSiXKkYFWhJsUDXPytoMvtuM0uIm3rXcQ==", "7d8e9462-7002-49de-9e14-44fbc513b8dc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e130798b-a521-45ad-85df-b232eaaadc09",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4557e9ab-74a6-4031-81ee-9bb4b9644dbf", "AQAAAAEAACcQAAAAEE7ChQFGNebk73g91SLo1WI0Zu3vt+K3F19ea4ETGpZQ/mWU53FTHYXLSgyOz5ab6A==", "86818601-44b5-4ad0-b1c1-2e342cd80bc6" });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewMessages_UserId",
                table: "ReviewMessages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewMessages_AspNetUsers_UserId",
                table: "ReviewMessages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewMessages_AspNetUsers_UserId",
                table: "ReviewMessages");

            migrationBuilder.DropIndex(
                name: "IX_ReviewMessages_UserId",
                table: "ReviewMessages");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ReviewMessages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
