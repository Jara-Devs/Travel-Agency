using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Agency_DataBase.Migrations
{
    /// <inheritdoc />
    public partial class offersreserves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfferReserve",
                columns: table => new
                {
                    OffersId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ReservesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferReserve", x => new { x.OffersId, x.ReservesId });
                    table.ForeignKey(
                        name: "FK_OfferReserve_Offers_OffersId",
                        column: x => x.OffersId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferReserve_Reserves_ReservesId",
                        column: x => x.ReservesId,
                        principalTable: "Reserves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OfferReserve_ReservesId",
                table: "OfferReserve",
                column: "ReservesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferReserve");
        }
    }
}
