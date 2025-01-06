using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gimpies_Blazor1.Migrations
{
    /// <inheritdoc />
    public partial class UserPolicies1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PolicyName", "fk_UserRoleID" },
                values: new object[] { "View_Users", 1 });

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PolicyName", "fk_UserRoleID" },
                values: new object[] { "Add_Users", 1 });

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "PolicyName", "fk_UserRoleID" },
                values: new object[] { "Edit_Users", 1 });

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "PolicyName", "fk_UserRoleID" },
                values: new object[] { "Delete_Users", 1 });

            migrationBuilder.InsertData(
                table: "UserPolicies",
                columns: new[] { "Id", "IsEnabled", "PolicyName", "fk_UserRoleID" },
                values: new object[,]
                {
                    { 9, true, "View_Shoes", 2 },
                    { 10, true, "Buy_Shoes", 2 },
                    { 11, true, "View_Shoes", 3 },
                    { 12, true, "Sell_Shoes", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PolicyName", "fk_UserRoleID" },
                values: new object[] { "View_Shoes", 2 });

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PolicyName", "fk_UserRoleID" },
                values: new object[] { "Buy_Shoes", 2 });

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "PolicyName", "fk_UserRoleID" },
                values: new object[] { "View_Shoes", 3 });

            migrationBuilder.UpdateData(
                table: "UserPolicies",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "PolicyName", "fk_UserRoleID" },
                values: new object[] { "Sell_Shoes", 3 });
        }
    }
}
