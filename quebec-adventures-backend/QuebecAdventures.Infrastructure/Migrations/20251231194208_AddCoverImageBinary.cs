using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuebecAdventures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCoverImageBinary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Activities");

            migrationBuilder.AddColumn<byte[]>(
                name: "CoverImageContent",
                table: "Activities",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverImageMimeType",
                table: "Activities",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageContent",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CoverImageMimeType",
                table: "Activities");

            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "Activities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Activities",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
