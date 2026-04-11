using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Novel.Migrations
{
    /// <inheritdoc />
    public partial class AddChapterSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "cointransactions_ibfk_novel",
                table: "cointransactions");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "chapters",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "cointransactions_ibfk_novel",
                table: "cointransactions",
                column: "NovelId",
                principalTable: "novels",
                principalColumn: "NovelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "cointransactions_ibfk_novel",
                table: "cointransactions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "chapters");

            migrationBuilder.AddForeignKey(
                name: "cointransactions_ibfk_novel",
                table: "cointransactions",
                column: "NovelId",
                principalTable: "novels",
                principalColumn: "NovelId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
