using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityEducationApi.Migrations
{
    /// <inheritdoc />
    public partial class initialAltText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageAltText",
                table: "ReadingMaterials",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageAltText",
                table: "Episodes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageAltText",
                table: "Chapters",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageAltText",
                table: "ReadingMaterials");

            migrationBuilder.DropColumn(
                name: "ImageAltText",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "ImageAltText",
                table: "Chapters");
        }
    }
}
