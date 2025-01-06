using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gimpies_Blazor1.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesandPolicies1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Userid",
                keyValue: 1,
                column: "fk_UserRoleID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Userid",
                keyValue: 2,
                column: "fk_UserRoleID",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Userid",
                keyValue: 3,
                column: "fk_UserRoleID",
                value: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Userid",
                keyValue: 1,
                column: "fk_UserRoleID",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Userid",
                keyValue: 2,
                column: "fk_UserRoleID",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Userid",
                keyValue: 3,
                column: "fk_UserRoleID",
                value: 0);
        }
    }
}
