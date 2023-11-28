using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Agency_DataBase.Migrations
{
    /// <inheritdoc />
    public partial class manyhotelsovernightexcursion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_Hotels_HotelId",
                table: "Excursions");

            migrationBuilder.DropIndex(
                name: "IX_Excursions_HotelId",
                table: "Excursions");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Excursions");

            migrationBuilder.DropColumn(
                name: "IsOverNight",
                table: "Excursions");

            migrationBuilder.CreateTable(
                name: "ExcursionHotel",
                columns: table => new
                {
                    HotelsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OverNightExcursionsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcursionHotel", x => new { x.HotelsId, x.OverNightExcursionsId });
                    table.ForeignKey(
                        name: "FK_ExcursionHotel_Excursions_OverNightExcursionsId",
                        column: x => x.OverNightExcursionsId,
                        principalTable: "Excursions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExcursionHotel_Hotels_HotelsId",
                        column: x => x.HotelsId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionHotel_OverNightExcursionsId",
                table: "ExcursionHotel",
                column: "OverNightExcursionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcursionHotel");

            migrationBuilder.AddColumn<Guid>(
                name: "HotelId",
                table: "Excursions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<bool>(
                name: "IsOverNight",
                table: "Excursions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_HotelId",
                table: "Excursions",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_Hotels_HotelId",
                table: "Excursions",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");
        }
    }
}
