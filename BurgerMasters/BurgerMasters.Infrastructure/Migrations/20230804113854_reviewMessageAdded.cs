using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerMasters.Infrastructure.Migrations
{
    public partial class reviewMessageAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReviewMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewMessages", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewMessages");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "453a4524-0cd1-46e6-abde-3219df401504",
                column: "ConcurrencyStamp",
                value: "25aa1085-862e-4c99-9398-a8f230c6b8ee");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a439eb91-8c15-4e7a-abef-7f4ebc004826",
                column: "ConcurrencyStamp",
                value: "f19d8c5f-c61d-4d28-a73f-e9d787610669");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a0407939-a95d-40a2-8db6-020d349bd2bb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a540fd21-2377-4838-b31f-a0e90fd4816c", "AQAAAAEAACcQAAAAECDFSQxqW/OSlfd9WU32Jvc7oEILCDkShe0iFvmdy8SjI/u0Zi9kqQ2blgD59kuvIA==", "7d808fe1-e73d-483a-9a8b-f639c3181fb9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c30d2c49-d677-42b3-9295-a0b1dae91806",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3de7d9dd-8802-4f45-a61d-5f9866c3522b", "AQAAAAEAACcQAAAAENLORiX5XekSTMSEOEWBvuD3QLCOhX7f024U4rqRyo7BgRs798gyvS104apzR29fEA==", "21b0348a-ac3e-495c-9304-a1cfccefadd4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e130798b-a521-45ad-85df-b232eaaadc09",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a00062f-22e3-409d-a38f-c638597cee0c", "AQAAAAEAACcQAAAAEC3eIFLDrCaadlsoWw7RgpdeGQ1vOKC+46Lg3hxDB307XnUusQC7s+QqVmRXfopvfA==", "1ba0bfc1-420a-4533-b5fd-46ee22d0d45b" });
        }
    }
}
