using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gimpies_Blazor1.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesandPolicies3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Roles",
                newName: "fk_UserRoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fk_UserRoleID",
                table: "Roles",
                newName: "RoleId");
        }
    }
}
