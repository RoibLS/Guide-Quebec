using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuebecAdventures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddJournalWishlistFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CoverImageContent",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "CoverImageMimeType",
                table: "Activities",
                newName: "WishlistNote");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Reviews",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Activities",
                type: "text",
                nullable: true);

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

            migrationBuilder.AddColumn<bool>(
                name: "IsVisited",
                table: "Activities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Seasons",
                table: "Activities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "VisitedAt",
                table: "Activities",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "IsVisited",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Seasons",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "VisitedAt",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "WishlistNote",
                table: "Activities",
                newName: "CoverImageMimeType");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Reviews",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Reviews",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "CoverImageContent",
                table: "Activities",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Activities",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<List<string>>(
                name: "Season",
                table: "Activities",
                type: "text[]",
                nullable: false);
        }
    }
}
