using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gimpies_Blazor1.Migrations
{
    /// <inheritdoc />
    public partial class isActiveShoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Shoes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Shoes");
        }
    }
}
