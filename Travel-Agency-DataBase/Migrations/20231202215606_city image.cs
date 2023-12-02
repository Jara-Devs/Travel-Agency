using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Agency_DataBase.Migrations
{
    /// <inheritdoc />
    public partial class cityimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Cities",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ImageId",
                table: "Cities",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Images_ImageId",
                table: "Cities",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Images_ImageId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_ImageId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Cities");
        }
    }
}
