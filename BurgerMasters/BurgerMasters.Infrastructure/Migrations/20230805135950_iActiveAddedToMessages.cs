using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerMasters.Infrastructure.Migrations
{
    public partial class iActiveAddedToMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ReviewMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "453a4524-0cd1-46e6-abde-3219df401504",
                column: "ConcurrencyStamp",
                value: "7e5d7a1b-2f14-4846-9266-b4ffeb480591");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a439eb91-8c15-4e7a-abef-7f4ebc004826",
                column: "ConcurrencyStamp",
                value: "c9a47558-9a05-4552-bb25-8be4a31a5edd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a0407939-a95d-40a2-8db6-020d349bd2bb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "abe82106-80d3-4ac6-a56d-5d5db5cf2299", "AQAAAAEAACcQAAAAEDId6CXdNFpUsEnXXkLCA4e+Mu7EzqYyfRY8dBRagFgSPRexE82r2L6BVaUZRKMhQQ==", "9e56a742-4de4-42b8-b2d1-e27f09834687" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c30d2c49-d677-42b3-9295-a0b1dae91806",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50ae9502-00f3-4254-be0f-130aeff06b7c", "AQAAAAEAACcQAAAAEHlkyobB5KliPtY6Y+nh0be3zAbWJlxXWjl1166DPFsJcX3iDN596otkrAoZSt0nmA==", "7ca35226-d8b1-4d08-b996-4151f6f2fdda" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e130798b-a521-45ad-85df-b232eaaadc09",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e23fb32-8980-43c9-9b56-4c4ce961a13c", "AQAAAAEAACcQAAAAELEpV+YJassvbXN8EFByuoiWj55a3JPvzzjl0w0qktT/uq06H3dMOvWmLaAS1FFvnQ==", "b8129ca5-5b46-40f6-ba23-f9f9bbab5877" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ReviewMessages");

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
    }
}
