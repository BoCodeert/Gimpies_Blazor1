using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gimpies_Blazor1.Migrations
{
    /// <inheritdoc />
    public partial class LoginModel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "fk_UserRoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserPolicies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<int>(type: "int", nullable: false),
                    PolicyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPolicies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserPolicies",
                columns: new[] { "Id", "IsEnabled", "PolicyName", "Userid" },
                values: new object[,]
                {
                    { 1, true, "View_Shoes", 1 },
                    { 2, true, "Add_Shoes", 1 },
                    { 3, true, "Edit_Shoes", 1 },
                    { 4, true, "Delete_Shoes", 1 },
                    { 5, false, "Sell_Shoes", 1 },
                    { 6, false, "Buy_Shoes", 1 },
                    { 7, true, "View_Shoes", 2 },
                    { 8, false, "Add_Shoes", 2 },
                    { 9, false, "Edit_Shoes", 2 },
                    { 10, false, "Delete_Shoes", 2 },
                    { 11, false, "Sell_Shoes", 2 },
                    { 12, true, "Buy_Shoes", 2 },
                    { 13, true, "View_Shoes", 3 },
                    { 14, false, "Add_Shoes", 3 },
                    { 15, false, "Edit_Shoes", 3 },
                    { 16, false, "Delete_Shoes", 3 },
                    { 17, false, "Sell_Shoes", 3 },
                    { 18, true, "Buy_Shoes", 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Userid", "PasswordHashed", "Username", "fk_UserRoleId" },
                values: new object[,]
                {
                    { 1, "1", "admin", 0 },
                    { 2, "1", "buyer", 0 },
                    { 3, "1", "seller", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPolicies");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Userid",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Userid",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Userid",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "fk_UserRoleId",
                table: "Users");
        }
    }
}
