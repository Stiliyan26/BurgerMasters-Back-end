using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerMasters.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsPending = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewMessage_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ItemTypeId = table.Column<int>(type: "int", nullable: false),
                    PortionSize = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItem_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MenuItem_ItemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "ItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserMenuItems",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    ItemQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserMenuItems", x => new { x.ApplicationUserId, x.MenuItemId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserMenuItems_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserMenuItems_MenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_MenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "453a4524-0cd1-46e6-abde-3219df401504", "03371baa-e06a-4375-a068-3f7f2dfadc73", "Admin", "ADMIN" },
                    { "a439eb91-8c15-4e7a-abef-7f4ebc004826", "83a6843f-9abf-42f9-b136-27bf16df7c00", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Birthdate", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a0407939-a95d-40a2-8db6-020d349bd2bb", 0, "Street: Vitosha Boulevard, Number: 10, Block: A", new DateTime(1998, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "abf28eb2-5663-42be-a223-3727f3caaaed", "stiliyan@gmail.com", false, false, null, "STILIYAN@GMAIL.COM", "STILIYAN", "AQAAAAEAACcQAAAAEH1h6LMEvLntQPMhHohoiYVAZRzMVL9dH03U4M+zjQYvmkO/lXFy1vnp6wv0WjDpkA==", null, false, "d944fa10-3779-4ba1-aa23-05e195ce1061", false, "Stiliyan26" },
                    { "c30d2c49-d677-42b3-9295-a0b1dae91806", 0, "Street: Shipchenski Prohod Street, Number: 20, Block: B", new DateTime(1998, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "9b34c0ae-b2e6-478d-8152-512d4669bb3b", "peter@gmail.com", false, false, null, "PETER@GMAIL.COM", "PETER12", "AQAAAAEAACcQAAAAEJTl3dpbPBWKt2HCnjp/hE0aS6OrxZP0L/Ar4oJ9rbaFDd1HFlmEpsq6n0xC6lzxjg==", null, false, "be1f7113-5ce9-4898-8635-c65a2b98aa97", false, "Peter12" },
                    { "d27076cc-efe7-4b1e-9730-e9630be4d3a6", 0, " Street: Tsarigradsko Shose Boulevard, Number: 40, Block: D", new DateTime(2002, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "7e6aacdb-e3e9-4657-b605-e19c560bd293", "pavlin@gmail.com", false, false, null, "PAVLIN@GMAIL.COM", "PAVLIN14", "AQAAAAEAACcQAAAAEFuM7E0+LrOY59Isq7Uk+zaiG4Gjx7DPkEjZg5LNNBShm0Rmt4otWirhQ7ONMpAivA==", null, false, "29c0b0a2-7664-448c-9964-4261cb50210e", false, "Pavlin14" },
                    { "e130798b-a521-45ad-85df-b232eaaadc09", 0, "Street: Alexander Malinov Boulevard, Number: 30, Block: C", new DateTime(2003, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "85a0d36a-2b5e-4666-9884-4348746fe65e", "bogdan@gmail.com", false, false, null, "BOGDAN@GMAIL.COM", "BOGDAN16", "AQAAAAEAACcQAAAAEBB1YAgtP+dR5wt03TZKC7e8MNCpifObMWP8D2bvNUyCqSv5bYJ/wgt+ZL7Opql4dQ==", null, false, "db666779-38c5-4a82-8fc5-70bcabc06bfc", false, "Bogdan16" }
                });

            migrationBuilder.InsertData(
                table: "ItemTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Burger" },
                    { 2, "Drink" },
                    { 3, "Fries" },
                    { 4, "Hotdog" },
                    { 5, "Grill" },
                    { 6, "Salad" },
                    { 7, "Sandwich" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "453a4524-0cd1-46e6-abde-3219df401504", "a0407939-a95d-40a2-8db6-020d349bd2bb" },
                    { "453a4524-0cd1-46e6-abde-3219df401504", "c30d2c49-d677-42b3-9295-a0b1dae91806" },
                    { "a439eb91-8c15-4e7a-abef-7f4ebc004826", "d27076cc-efe7-4b1e-9730-e9630be4d3a6" },
                    { "a439eb91-8c15-4e7a-abef-7f4ebc004826", "e130798b-a521-45ad-85df-b232eaaadc09" }
                });

            migrationBuilder.InsertData(
                table: "MenuItem",
                columns: new[] { "Id", "ApplicationUserId", "CreatorId", "Description", "ImageUrl", "IsActive", "ItemTypeId", "Name", "PortionSize", "Price" },
                values: new object[,]
                {
                    { 42, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade Brioche Bread, Burger sauce, Black Angus ground beef mixed with cheddar, Melted Irish red cheddar, Crispy bacon, Caramelized onions", "JuicyLucy.webp", true, 1, "THE JUICY LUCY", 350, 17.40m },
                    { 43, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade Brioche Bread, Pineapple butter with rum, 200g Black Angus ground beef, Grilled pineapple, Crispy bacon with brown sugar", "PineappleBaconRun.webp", true, 1, "Pineapple Bacon Run", 380, 19.40m },
                    { 44, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, Homemade pickle, Caramelized onions", "RustySavage.webp", true, 1, "RUSTY SAVAGE", 630, 27.49m },
                    { 45, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade Brioche Bread, Grilled onions, Thunder spicy sauce (tomato sauce, jalapeño, pickle, sweet apricot), Black Angus ground beef, Homemade cheddar sauce, Homemade pickle", "Tunder.webp", true, 1, "THUNDER", 380, 17.49m },
                    { 46, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade Brioche Bread, Burger sauce, Iceberg, Black Angus ground beef, Crispy bacon, American cheese, Tomato, Pickled red onion", "American.webp", true, 1, "American Cheese Burger", 320, 14.99m },
                    { 47, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Homemade Brioche Bread, Black Angus ground beef, Aioli sauce, Shrimp, Iceberg", "SurfNTurff.webp", true, 1, "Surf'n'Turf", 370, 19.49m },
                    { 48, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Black Angus ground beef, American cheese, Cheddar, Jalapeno", "BurgerPie.webp", true, 1, "Burger Pie", 380, 19.49m },
                    { 49, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Black Angus ground beef, American cheese, Cheddar, Jalapeno", "BurgerPie.webp", true, 1, "Burger Pie", 380, 19.49m },
                    { 50, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Homemade Brioche Bread, Burger sauce, 200g Black Angus ground beef patties with brown sugar and whiskey, Smokey BBQ, Mushrooms with olive oil, onion and garlic, Crispy bacon, Very cheddar, Fresh tomato", "SmokeyWhiskeyCheeseburger.webp", true, 1, "Smokey Whiskey Cheeseburger", 480, 23.49m },
                    { 51, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Homemade Brioche Bread, BBQ sauce with bourbon, Our pickle, Black Angus ground beef with blue cheese, mozzarella and lots of cheddar, wrapped in bacon, Grilled onions", "TrippleCheese.webp", true, 1, "Triple Cheeseburger", 550, 23.49m },
                    { 52, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Homemade Brioche Bread, Homemade bourbon BBQ sauce, Crispy iceberg, Black Angus ground beef patties, Very cheddar, Bacon Apricot Marmalade", "BaconJam.webp", true, 1, "Bacon Jam Burger", 450, 19.49m },
                    { 53, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Homemade Brioche Bread, Colsloe, Slow Roasted Pork Shoulder, Caramelized onions, Melted cheddar, Homemade bourbon BBQ sauce", "BBQPulledPork.webp", true, 1, "BBQ Pulled Pork", 350, 14.40m },
                    { 54, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade potatoes, Vegetable Oil, Salt", "RegularFries.webp", true, 3, "Regular Fries", 170, 4.99m },
                    { 55, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade potatoes, Vegetable Oil, Salt, Homemade hot sauce", "SpicyFries.webp", true, 3, "Spicy Fries", 240, 6.99m },
                    { 56, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade potatoes, Vegetable Oil, Salt, Pepper, House fried sauce and jalapenos", "CheddarFries.webp", true, 3, "Cheddar Fries", 300, 7.99m },
                    { 57, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Onions, Flour, Bread crumbs, Salt and pepper", "OnionRings.webp", true, 3, "Breaded onion rings", 250, 6.99m },
                    { 58, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Mozzarella cheese, Blueberry jam, All purpose flour", "MozzarellaSticks.webp", true, 3, "Mozzarella sticks", 170, 9.99m },
                    { 59, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Carbonated Water, Phosphoric Acid, Sweeteners (Aspartame, Acesulfame Potassium), Natural Flavors (including Caffeine)", "CocaColaNoSugar.webp", true, 2, "Coca-Cola no sugar", 330, 2.50m },
                    { 60, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Carbonated Water, High Fructose Corn Syrup, Caramel Color, Phosphoric Acid, Natural Flavors", "Classic.webp", true, 2, "Coca-Cola Original", 330, 2.50m },
                    { 61, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Carbonated Water, High Fructose Corn Syrup, Caramel Color, Phosphoric Acid, Potassium Benzoate", "CherryVanilla.webp", true, 2, "Coca-cola cherry", 355, 4.90m },
                    { 62, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Carbonated Water, High Fructose Corn Syrup, Caramel Color, Phosphoric Acid, Caffeine, Sodium Benzoat", "OrangeVanilla.webp", true, 2, "Coca-cola orange vanilla", 355, 4.90m },
                    { 63, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Carbonated Water, High Fructose Corn Syrup, Caramel Color, Phosphoric Acid, Natural Flavors (including Vanilla), Caffeine", "Vanilla.webp", true, 2, "Coca-cola vanila", 355, 4.90m },
                    { 64, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Carbonated Water, High Fructose Corn Syrup, Natural and Artificial Flavors, Citric Acid, Sodium Citrate, Red 40 (Color)", "FantaStrawberry.webp", true, 2, "Fanta strawberry", 355, 4.90m },
                    { 65, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Carbonated Water, High Fructose Corn Syrup, Citric Acid, Natural and Artificial Flavors, Red 40 (Color), Blue 1 (Color)", "FantaBerry.webp", true, 2, "Fanta berry", 355, 4.90m },
                    { 66, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Carbonated Water, High Fructose Corn Syrup, Natural and Artificial Flavors, Citric Acid, Sodium Benzoate, Red 40 (Color), Blue 1 (Color)", "FantaGrape.webp", true, 2, "Fanta Grape", 355, 4.90m },
                    { 67, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Carbonated Water, High Fructose Corn Syrup, Natural Flavors, Natural Flavors, Sodium Benzoate, Yellow 6 (Color), Red 40 (Color)", "FantaPeach.webp", true, 2, "Fanta Peach", 355, 4.90m },
                    { 68, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Carbonated Water, High Fructose Corn Syrup, Natural Flavors, Sodium Benzoat, Potassium Sorbate, Yellow 5 (Color), Yellow 5 (Color)", "FantaPineapple.webp", true, 2, "Fanta Pineapple", 355, 4.90m },
                    { 69, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade bread, BBQ sauce, Grilled sausage with cheddar and bacon, Mustard, Onion, Oklahoma Mince, Homemade cheddar sauce", "Oklahoma.webp", true, 4, "Old Style Oklahoma", 200, 12.49m },
                    { 70, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade Brioche Bread, Grilled beef sausage, Smoked bacon, Mustard, Pasta with American cheese", "MacNcheese.webp", true, 4, "Old Style Mac'n'cheese", 200, 11.49m },
                    { 71, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Homemade brioche bread, Smoked pork sausage, Smoked bacon, Coleslaw salad, BBQ sauce, Mustard", "Mineapolis.webp", true, 4, "Old Style Mineapolis", 200, 11.49m },
                    { 72, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Beef sausage breaded with cornmeal, egg, mustard and honey, Coleslaw salad, Mustard", "CornDog.webp", true, 4, "CornDog", 250, 11.49m },
                    { 73, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "160 g of Black Angus ground beef, Spicy beans with bacon, molasses and spices, Coleslaw", "BlackAngus.webp", true, 5, "Black Angus Grill", 460, 18.90m },
                    { 74, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "2 smoked pork sausages, Spicy beans with bacon, molasses and spices, Coleslaw", "PorkSausage.webp", true, 5, "Smoked pork sausage", 400, 19.10m },
                    { 75, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "2 patties of 160g each. from Black Angus ground beef, Spicy beans with bacon, molasses and spices, Coleslaw", "BlackAngus2.webp", true, 5, "Double Black Angus Gril", 640, 23.90m },
                    { 76, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Red cabbage,White Cabbage,Carrot,Mayonnaise dressing", "Coleslaw.webp", true, 6, "Salad Coleslaw", 250, 5.99m },
                    { 77, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Lettuce,Tomato,Blue cheese,Avocado,Chicken fillet,Bacon,Quail eggs,Red onion,Dressing", "Cobb.webp", true, 6, "Salad Cobb", 400, 14.90m },
                    { 78, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade American bread toasted with butter, Breaded sirloin (pork), Mustard, White onion, Colsloe, Tomato", "FiredPork.webp", true, 7, "Fried Pork Priviledge", 390, 13.90m },
                    { 79, null, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade Brioche Bread, American potato salad with mustard and bacon, Ground beef with Sloppy Joe sauce", "SloppyJoe.webp", true, 7, "Sloppy Joe", 320, 11.49m }
                });

            migrationBuilder.InsertData(
                table: "MenuItem",
                columns: new[] { "Id", "ApplicationUserId", "CreatorId", "Description", "ImageUrl", "IsActive", "ItemTypeId", "Name", "PortionSize", "Price" },
                values: new object[,]
                {
                    { 80, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Homemade Brioche Bread, Coleslaw, Crispy bacon, BBQ sauce with bourbon, Caramelized onions", "RustyBacon.webp", true, 7, "Rusty Bacon", 380, 9.99m },
                    { 81, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Toast slices with smoked mayonnaise, pan-fried, Lettuce, Tomato", "ScrambledEggs.webp", true, 7, "Scrambled eggs with bacon", 270, 10.99m },
                    { 82, null, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Toast slices with smoked mayonnaise, pan-fried, Cheddar, Mozzarella", "GrillnCheese.webp", true, 7, "American Grill and Cheese", 200, 10.09m }
                });

            migrationBuilder.InsertData(
                table: "ReviewMessage",
                columns: new[] { "Id", "IsActive", "Message", "SentDate", "UserId" },
                values: new object[,]
                {
                    { 1, true, "The quality of the food is very good and the price matches the quality!", new DateTime(2023, 8, 2, 15, 15, 22, 0, DateTimeKind.Unspecified), "e130798b-a521-45ad-85df-b232eaaadc09" },
                    { 2, true, "Absolutely loved dining at this restaurant!", new DateTime(2023, 7, 3, 16, 11, 23, 0, DateTimeKind.Unspecified), "d27076cc-efe7-4b1e-9730-e9630be4d3a6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserMenuItems_MenuItemId",
                table: "ApplicationUserMenuItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_ApplicationUserId",
                table: "MenuItem",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_ItemTypeId",
                table: "MenuItem",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_MenuItemId",
                table: "OrderDetails",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewMessage_UserId",
                table: "ReviewMessage",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserMenuItems");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ReviewMessage");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MenuItem");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
