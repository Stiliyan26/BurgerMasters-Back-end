using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerMasters.Infrastructure.Migrations
{
    public partial class biggerSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "ReviewMessages",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "453a4524-0cd1-46e6-abde-3219df401504",
                column: "ConcurrencyStamp",
                value: "18d0d44e-eb23-4794-9620-88e582f7df3a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a439eb91-8c15-4e7a-abef-7f4ebc004826",
                column: "ConcurrencyStamp",
                value: "bd94dafa-ca5c-4f9a-9902-6635fc0205a8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a0407939-a95d-40a2-8db6-020d349bd2bb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31909149-a73d-4dd2-9642-8ac7ece2dc94", "AQAAAAEAACcQAAAAELS4Ur7gUtbtcUM6TntyJhW5ZNMzdWMCfmGXTJzoB5y1yxkIZHDLZPm7M5pGJFvDOw==", "9b5d8586-03c4-4843-92f6-aa4482bdc4ea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c30d2c49-d677-42b3-9295-a0b1dae91806",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4338361f-3546-43b0-9bda-7d6dbc97267b", "AQAAAAEAACcQAAAAEJ/t46DWHUFL64f1Nf0CTWkNL+rjBwyTPiKirieRzVAnOpckSwAlaahpgm7Hm4VfyQ==", "32a6ba49-8ef4-41f4-b381-7dd2ca13df47" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e130798b-a521-45ad-85df-b232eaaadc09",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f4a5be35-ac7a-4afe-8115-8eba16525a14", "AQAAAAEAACcQAAAAEDhGuFL7idYhG5zj6uus3323JUmbaCZjlaYMKIZ3OB5hBUBMJSaMimqQsK96PwjKaw==", "8a15fe35-c119-4511-b5f5-94dd583769e6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "ReviewMessages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

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
        }
    }
}
