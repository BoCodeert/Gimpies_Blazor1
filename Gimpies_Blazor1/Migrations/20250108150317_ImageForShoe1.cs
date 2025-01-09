using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gimpies_Blazor1.Migrations
{
    /// <inheritdoc />
    public partial class ImageForShoe1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Shoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Shoes");
        }
    }
}
