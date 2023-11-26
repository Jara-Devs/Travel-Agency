using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Agency_DataBase.Migrations
{
    /// <inheritdoc />
    public partial class reactionstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "Liked",
                table: "Reactions");

            migrationBuilder.AddColumn<int>(
                name: "ReactionState",
                table: "Reactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReactionState",
                table: "Reactions");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Reactions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "Liked",
                table: "Reactions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
