using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Novel.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentModerationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeleteReason",
                table: "comments",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_unicode_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "comments",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "comments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteReason",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "comments");
        }
    }
}
