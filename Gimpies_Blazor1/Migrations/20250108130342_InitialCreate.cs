using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gimpies_Blazor1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Colours",
                columns: table => new
                {
                    ColourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColourName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colours", x => x.ColourId);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    ModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.ModelId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    SizeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SizeValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.SizeId);
                });

            migrationBuilder.CreateTable(
                name: "UserPolicies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_UserRoleID = table.Column<int>(type: "int", nullable: false),
                    PolicyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPolicies_Roles_fk_UserRoleID",
                        column: x => x.fk_UserRoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Userid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHashed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fk_UserRoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Userid);
                    table.ForeignKey(
                        name: "FK_Users_Roles_fk_UserRoleID",
                        column: x => x.fk_UserRoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shoes",
                columns: table => new
                {
                    ShoeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    ColourId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoes", x => x.ShoeId);
                    table.ForeignKey(
                        name: "FK_Shoes_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shoes_Colours_ColourId",
                        column: x => x.ColourId,
                        principalTable: "Colours",
                        principalColumn: "ColourId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shoes_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "ModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shoes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Buyer" },
                    { 3, "Seller" }
                });

            migrationBuilder.InsertData(
                table: "UserPolicies",
                columns: new[] { "Id", "IsEnabled", "PolicyName", "fk_UserRoleID" },
                values: new object[,]
                {
                    { 1, true, "View_Shoes", 1 },
                    { 2, true, "Add_Shoes", 1 },
                    { 3, true, "Edit_Shoes", 1 },
                    { 4, true, "Delete_Shoes", 1 },
                    { 5, true, "View_Users", 1 },
                    { 6, true, "Add_Users", 1 },
                    { 7, true, "Edit_Users", 1 },
                    { 8, true, "Delete_Users", 1 },
                    { 9, true, "View_Shoes", 2 },
                    { 10, true, "Buy_Shoes", 2 },
                    { 11, true, "View_Shoes", 3 },
                    { 12, true, "Sell_Shoes", 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Userid", "PasswordHashed", "Username", "fk_UserRoleID" },
                values: new object[,]
                {
                    { 1, "1", "admin", 1 },
                    { 2, "1", "buyer", 2 },
                    { 3, "1", "seller", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_BrandId",
                table: "Shoes",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_ColourId",
                table: "Shoes",
                column: "ColourId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_ModelId",
                table: "Shoes",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_SizeId",
                table: "Shoes",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPolicies_fk_UserRoleID",
                table: "UserPolicies",
                column: "fk_UserRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_fk_UserRoleID",
                table: "Users",
                column: "fk_UserRoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shoes");

            migrationBuilder.DropTable(
                name: "UserPolicies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Colours");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
