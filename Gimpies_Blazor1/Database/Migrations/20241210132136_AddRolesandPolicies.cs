using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gimpies_Blazor1.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesandPolicies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.RenameColumn(
                name: "fk_UserRoleId",
                table: "Users",
                newName: "fk_UserRoleID");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "UserPolicies",
                newName: "fk_UserRoleID");

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

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Buyer" },
                    { 3, "Seller" }
                });

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IsEnabled", "PolicyName", "fk_UserRoleID" },
                values: new object[] { true, "View_Shoes", 2 });

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IsEnabled", "fk_UserRoleID" },
                values: new object[] { true, 2 });

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 7,
                column: "fk_UserRoleID",
                value: 3);

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "IsEnabled", "PolicyName", "fk_UserRoleID" },
                values: new object[] { true, "Sell_Shoes", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Users_fk_UserRoleID",
                table: "Users",
                column: "fk_UserRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPolicies_fk_UserRoleID",
                table: "UserPolicies",
                column: "fk_UserRoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPolicies_Roles_fk_UserRoleID",
                table: "UserPolicies",
                column: "fk_UserRoleID",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_fk_UserRoleID",
                table: "Users",
                column: "fk_UserRoleID",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPolicies_Roles_fk_UserRoleID",
                table: "UserPolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_fk_UserRoleID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_fk_UserRoleID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserPolicies_fk_UserRoleID",
                table: "UserPolicies");

            migrationBuilder.RenameColumn(
                name: "fk_UserRoleID",
                table: "Users",
                newName: "fk_UserRoleId");

            migrationBuilder.RenameColumn(
                name: "fk_UserRoleID",
                table: "UserPolicies",
                newName: "Userid");

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IsEnabled", "PolicyName", "Userid" },
                values: new object[] { false, "Sell_Shoes", 1 });

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IsEnabled", "Userid" },
                values: new object[] { false, 1 });

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 7,
                column: "Userid",
                value: 2);

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "IsEnabled", "PolicyName", "Userid" },
                values: new object[] { false, "Add_Shoes", 2 });

            migrationBuilder.InsertData(
                table: "UserPolicies",
                columns: new[] { "Id", "IsEnabled", "PolicyName", "Userid" },
                values: new object[,]
                {
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
        }
    }
}
