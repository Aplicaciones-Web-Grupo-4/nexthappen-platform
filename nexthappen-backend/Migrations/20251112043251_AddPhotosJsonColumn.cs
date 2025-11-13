using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nexthappen_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotosJsonColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photos",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "PhotosJson",
                table: "Events",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotosJson",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "Photos",
                table: "Events",
                type: "text",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
