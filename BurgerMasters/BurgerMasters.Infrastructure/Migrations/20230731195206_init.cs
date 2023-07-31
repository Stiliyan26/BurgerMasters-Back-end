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
                name: "MenuItems",
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
                    CreatorId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_ItemTypes_ItemTypeId",
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
                        name: "FK_ApplicationUserMenuItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
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
                        name: "FK_OrderDetails_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
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
                    { "453a4524-0cd1-46e6-abde-3219df401504", "25aa1085-862e-4c99-9398-a8f230c6b8ee", "Admin", "ADMIN" },
                    { "a439eb91-8c15-4e7a-abef-7f4ebc004826", "f19d8c5f-c61d-4d28-a73f-e9d787610669", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Birthdate", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a0407939-a95d-40a2-8db6-020d349bd2bb", 0, "Street: 17, bul. Cherni vrah", new DateTime(1998, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "a540fd21-2377-4838-b31f-a0e90fd4816c", "stiliyan@gmail.com", false, false, null, "STILIYAN@GMAIL.COM", "STILIYAN", "AQAAAAEAACcQAAAAECDFSQxqW/OSlfd9WU32Jvc7oEILCDkShe0iFvmdy8SjI/u0Zi9kqQ2blgD59kuvIA==", null, false, "7d808fe1-e73d-483a-9a8b-f639c3181fb9", false, "Stiliyan26" },
                    { "c30d2c49-d677-42b3-9295-a0b1dae91806", 0, "Street: 17, bul. Cherni vrah", new DateTime(1998, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "3de7d9dd-8802-4f45-a61d-5f9866c3522b", "peter@gmail.com", false, false, null, "PETER@GMAIL.COM", "PETER12", "AQAAAAEAACcQAAAAENLORiX5XekSTMSEOEWBvuD3QLCOhX7f024U4rqRyo7BgRs798gyvS104apzR29fEA==", null, false, "21b0348a-ac3e-495c-9304-a1cfccefadd4", false, "Peter12" },
                    { "e130798b-a521-45ad-85df-b232eaaadc09", 0, "Street: 17, bul. Cherni vrah", new DateTime(2003, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "6a00062f-22e3-409d-a38f-c638597cee0c", "bogdan@gmail.com", false, false, null, "BOGDAN@GMAIL.COM", "BOGDAN16", "AQAAAAEAACcQAAAAEC3eIFLDrCaadlsoWw7RgpdeGQ1vOKC+46Lg3hxDB307XnUusQC7s+QqVmRXfopvfA==", null, false, "1ba0bfc1-420a-4533-b5fd-46ee22d0d45b", false, "Bogdan16" }
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
                    { "a439eb91-8c15-4e7a-abef-7f4ebc004826", "e130798b-a521-45ad-85df-b232eaaadc09" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "CreatorId", "Description", "ImageUrl", "IsActive", "ItemTypeId", "Name", "PortionSize", "Price" },
                values: new object[,]
                {
                    { 42, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade Brioche Bread, Burger sauce, Black Angus ground beef mixed with cheddar, Melted Irish red cheddar, Crispy bacon, Caramelized onions", "JuicyLucy.webp", true, 1, "THE JUICY LUCY", 350, 17.40m },
                    { 43, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade Brioche Bread, Pineapple butter with rum, 200g Black Angus ground beef, Grilled pineapple, Crispy bacon with brown sugar", "PineappleBaconRun.webp", true, 1, "Pineapple Bacon Run", 380, 19.40m },
                    { 44, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, Homemade pickle, Caramelized onions", "RustySavage.webp", true, 1, "RUSTY SAVAGE", 630, 27.49m },
                    { 45, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade Brioche Bread, Grilled onions, Thunder spicy sauce (tomato sauce, jalapeño, pickle, sweet apricot), Black Angus ground beef, Homemade cheddar sauce, Homemade pickle", "Tunder.webp", true, 1, "THUNDER", 380, 17.49m },
                    { 46, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade Brioche Bread, Burger sauce, Iceberg, Black Angus ground beef, Crispy bacon, American cheese, Tomato, Pickled red onion", "American.webp", true, 1, "American Cheese Burger", 320, 14.99m },
                    { 47, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Homemade Brioche Bread, Black Angus ground beef, Aioli sauce, Shrimp, Iceberg", "SurfNTurff.webp", true, 1, "Surf'n'Turf", 370, 19.49m },
                    { 48, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Black Angus ground beef, American cheese, Cheddar, Jalapeno", "BurgerPie.webp", true, 1, "Burger Pie", 380, 19.49m },
                    { 49, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Black Angus ground beef, American cheese, Cheddar, Jalapeno", "BurgerPie.webp", true, 1, "Burger Pie", 380, 19.49m },
                    { 50, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Homemade Brioche Bread, Burger sauce, 200g Black Angus ground beef patties with brown sugar and whiskey, Smokey BBQ, Mushrooms with olive oil, onion and garlic, Crispy bacon, Very cheddar, Fresh tomato", "SmokeyWhiskeyCheeseburger.webp", true, 1, "Smokey Whiskey Cheeseburger", 480, 23.49m },
                    { 51, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Homemade Brioche Bread, BBQ sauce with bourbon, Our pickle, Black Angus ground beef with blue cheese, mozzarella and lots of cheddar, wrapped in bacon, Grilled onions", "TrippleCheese.webp", true, 1, "Triple Cheeseburger", 550, 23.49m },
                    { 52, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Homemade Brioche Bread, Homemade bourbon BBQ sauce, Crispy iceberg, Black Angus ground beef patties, Very cheddar, Bacon Apricot Marmalade", "BaconJam.webp", true, 1, "Bacon Jam Burger", 450, 19.49m },
                    { 53, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Homemade Brioche Bread, Colsloe, Slow Roasted Pork Shoulder, Caramelized onions, Melted cheddar, Homemade bourbon BBQ sauce", "BBQPulledPork.webp", true, 1, "BBQ Pulled Pork", 350, 14.40m },
                    { 54, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade potatoes, Vegetable Oil, Salt", "RegularFries.webp", true, 3, "Regular Fries", 170, 4.99m },
                    { 55, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade potatoes, Vegetable Oil, Salt, Homemade hot sauce", "SpicyFries.webp", true, 3, "Spicy Fries", 240, 6.99m },
                    { 56, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade potatoes, Vegetable Oil, Salt, Pepper, House fried sauce and jalapenos", "CheddarFries.webp", true, 3, "Cheddar Fries", 300, 7.99m },
                    { 57, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Onions, Flour, Bread crumbs, Salt and pepper", "OnionRings.webp", true, 3, "Breaded onion rings", 250, 6.99m },
                    { 58, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Mozzarella cheese, Blueberry jam, All purpose flour", "MozzarellaSticks.webp", true, 3, "Mozzarella sticks", 170, 9.99m },
                    { 59, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Carbonated Water, Phosphoric Acid, Sweeteners (Aspartame, Acesulfame Potassium), Natural Flavors (including Caffeine)", "CocaColaNoSugar.webp", true, 2, "Coca-Cola no sugar", 330, 2.50m },
                    { 60, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Carbonated Water, High Fructose Corn Syrup, Caramel Color, Phosphoric Acid, Natural Flavors", "Classic.webp", true, 2, "Coca-Cola Original", 330, 2.50m },
                    { 61, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Carbonated Water, High Fructose Corn Syrup, Caramel Color, Phosphoric Acid, Potassium Benzoate", "CherryVanilla.webp", true, 2, "Coca-cola cherry", 355, 4.90m },
                    { 62, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Carbonated Water, High Fructose Corn Syrup, Caramel Color, Phosphoric Acid, Caffeine, Sodium Benzoat", "OrangeVanilla.webp", true, 2, "Coca-cola orange vanilla", 355, 4.90m },
                    { 63, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Carbonated Water, High Fructose Corn Syrup, Caramel Color, Phosphoric Acid, Natural Flavors (including Vanilla), Caffeine", "Vanilla.webp", true, 2, "Coca-cola vanila", 355, 4.90m },
                    { 64, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Carbonated Water, High Fructose Corn Syrup, Natural and Artificial Flavors, Citric Acid, Sodium Citrate, Red 40 (Color)", "FantaStrawberry.webp", true, 2, "Fanta strawberry", 355, 4.90m },
                    { 65, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Carbonated Water, High Fructose Corn Syrup, Citric Acid, Natural and Artificial Flavors, Red 40 (Color), Blue 1 (Color)", "FantaBerry.webp", true, 2, "Fanta berry", 355, 4.90m },
                    { 66, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Carbonated Water, High Fructose Corn Syrup, Natural and Artificial Flavors, Citric Acid, Sodium Benzoate, Red 40 (Color), Blue 1 (Color)", "FantaGrape.webp", true, 2, "Fanta Grape", 355, 4.90m },
                    { 67, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Carbonated Water, High Fructose Corn Syrup, Natural Flavors, Natural Flavors, Sodium Benzoate, Yellow 6 (Color), Red 40 (Color)", "FantaPeach.webp", true, 2, "Fanta Peach", 355, 4.90m },
                    { 68, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Carbonated Water, High Fructose Corn Syrup, Natural Flavors, Sodium Benzoat, Potassium Sorbate, Yellow 5 (Color), Yellow 5 (Color)", "FantaPineapple.webp", true, 2, "Fanta Pineapple", 355, 4.90m },
                    { 69, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade bread, BBQ sauce, Grilled sausage with cheddar and bacon, Mustard, Onion, Oklahoma Mince, Homemade cheddar sauce", "Oklahoma.webp", true, 4, "Old Style Oklahoma", 200, 12.49m },
                    { 70, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade Brioche Bread, Grilled beef sausage, Smoked bacon, Mustard, Pasta with American cheese", "MacNcheese.webp", true, 4, "Old Style Mac'n'cheese", 200, 11.49m },
                    { 71, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Homemade brioche bread, Smoked pork sausage, Smoked bacon, Coleslaw salad, BBQ sauce, Mustard", "Mineapolis.webp", true, 4, "Old Style Mineapolis", 200, 11.49m },
                    { 72, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Beef sausage breaded with cornmeal, egg, mustard and honey, Coleslaw salad, Mustard", "CornDog.webp", true, 4, "CornDog", 250, 11.49m },
                    { 73, "a0407939-a95d-40a2-8db6-020d349bd2bb", "160 g of Black Angus ground beef, Spicy beans with bacon, molasses and spices, Coleslaw", "BlackAngus.webp", true, 5, "Black Angus Grill", 460, 18.90m },
                    { 74, "a0407939-a95d-40a2-8db6-020d349bd2bb", "2 smoked pork sausages, Spicy beans with bacon, molasses and spices, Coleslaw", "PorkSausage.webp", true, 5, "Smoked pork sausage", 400, 19.10m },
                    { 75, "c30d2c49-d677-42b3-9295-a0b1dae91806", "2 patties of 160g each. from Black Angus ground beef, Spicy beans with bacon, molasses and spices, Coleslaw", "BlackAngus2.webp", true, 5, "Double Black Angus Gril", 640, 23.90m },
                    { 76, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Red cabbage,White Cabbage,Carrot,Mayonnaise dressing", "Coleslaw.webp", true, 6, "Salad Coleslaw", 250, 5.99m },
                    { 77, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Lettuce,Tomato,Blue cheese,Avocado,Chicken fillet,Bacon,Quail eggs,Red onion,Dressing", "Cobb.webp", true, 6, "Salad Cobb", 400, 14.90m },
                    { 78, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade American bread toasted with butter, Breaded sirloin (pork), Mustard, White onion, Colsloe, Tomato", "FiredPork.webp", true, 7, "Fried Pork Priviledge", 390, 13.90m },
                    { 79, "a0407939-a95d-40a2-8db6-020d349bd2bb", "Homemade Brioche Bread, American potato salad with mustard and bacon, Ground beef with Sloppy Joe sauce", "SloppyJoe.webp", true, 7, "Sloppy Joe", 320, 11.49m },
                    { 80, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Homemade Brioche Bread, Coleslaw, Crispy bacon, BBQ sauce with bourbon, Caramelized onions", "RustyBacon.webp", true, 7, "Rusty Bacon", 380, 9.99m }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "CreatorId", "Description", "ImageUrl", "IsActive", "ItemTypeId", "Name", "PortionSize", "Price" },
                values: new object[] { 81, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Toast slices with smoked mayonnaise, pan-fried, Lettuce, Tomato", "ScrambledEggs.webp", true, 7, "Scrambled eggs with bacon", 270, 10.99m });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "CreatorId", "Description", "ImageUrl", "IsActive", "ItemTypeId", "Name", "PortionSize", "Price" },
                values: new object[] { 82, "c30d2c49-d677-42b3-9295-a0b1dae91806", "Toast slices with smoked mayonnaise, pan-fried, Cheddar, Mozzarella", "GrillnCheese.webp", true, 7, "American Grill and Cheese", 200, 10.09m });

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
                name: "IX_MenuItems_ItemTypeId",
                table: "MenuItems",
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
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
