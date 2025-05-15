using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityEducationApi.Migrations
{
    /// <inheritdoc />
    public partial class initialTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EstimatedTime",
                table: "Chapters",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EstimatedTime",
                table: "Answers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedTime",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "EstimatedTime",
                table: "Answers");
        }
    }
}
