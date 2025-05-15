using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityEducationApi.Migrations
{
    /// <inheritdoc />
    public partial class initialTime2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedTime",
                table: "Answers");

            migrationBuilder.AddColumn<string>(
                name: "EstimatedTime",
                table: "Episodes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedTime",
                table: "Episodes");

            migrationBuilder.AddColumn<string>(
                name: "EstimatedTime",
                table: "Answers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
