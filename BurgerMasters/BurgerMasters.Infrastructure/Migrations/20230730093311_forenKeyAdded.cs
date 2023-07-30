using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerMasters.Infrastructure.Migrations
{
    public partial class forenKeyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_ApplicationUserId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_ApplicationUserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Notification");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Notification",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_UserId",
                table: "Notification",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_UserId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_UserId",
                table: "Notification");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Notification",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Notification",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ApplicationUserId",
                table: "Notification",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_ApplicationUserId",
                table: "Notification",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
