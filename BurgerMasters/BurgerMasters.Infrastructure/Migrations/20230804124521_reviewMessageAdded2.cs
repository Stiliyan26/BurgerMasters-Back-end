using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerMasters.Infrastructure.Migrations
{
    public partial class reviewMessageAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "453a4524-0cd1-46e6-abde-3219df401504",
                column: "ConcurrencyStamp",
                value: "c9c66967-66f1-4cbd-973a-6b50fa59575b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a439eb91-8c15-4e7a-abef-7f4ebc004826",
                column: "ConcurrencyStamp",
                value: "95fa28db-7537-462e-b4a8-de3ac0ca9410");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a0407939-a95d-40a2-8db6-020d349bd2bb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff1b6470-58f0-4b40-8636-1a13bf3b36c0", "AQAAAAEAACcQAAAAEN5Ra/CSOcELDJ+nquvFANrWeQcIQZgb5Bzjtt7bnkf6r9O5c97sCDDXKKkLKfeSyQ==", "f0e94a29-e4bb-48a8-a610-386635148669" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c30d2c49-d677-42b3-9295-a0b1dae91806",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69bd63b0-d64a-4fe3-bbe9-d6e580a2a2b1", "AQAAAAEAACcQAAAAENm08TwJ0xl2qc51cwMNYFEuaYMS3fgJF+KrFoTOOBGThJ/NQ8NMwS9W/HRJ6ncmoA==", "46de9ee2-90d1-4d7a-aa5d-c42beac8f71c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e130798b-a521-45ad-85df-b232eaaadc09",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c504f687-2bef-4c10-a8de-ec9efbbecb39", "AQAAAAEAACcQAAAAEG3rWcT3azIHsQXImOB+x5hvzM3gQAbN8nKwWFOwF/GQgQxKm6BgOtFMNPMGgbreVw==", "c25202f8-975f-4494-a7e2-b8588bb528cb" });
        }
    }
}
