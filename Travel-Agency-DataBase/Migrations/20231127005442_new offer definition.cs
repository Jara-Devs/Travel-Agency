using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Agency_DataBase.Migrations
{
    /// <inheritdoc />
    public partial class newofferdefinition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferPackage");

            migrationBuilder.CreateTable(
                name: "ExcursionOfferPackage",
                columns: table => new
                {
                    ExcursionOffersId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PackagesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcursionOfferPackage", x => new { x.ExcursionOffersId, x.PackagesId });
                    table.ForeignKey(
                        name: "FK_ExcursionOfferPackage_Offers_ExcursionOffersId",
                        column: x => x.ExcursionOffersId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExcursionOfferPackage_Packages_PackagesId",
                        column: x => x.PackagesId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FlightOfferPackage",
                columns: table => new
                {
                    FlightOffersId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PackagesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightOfferPackage", x => new { x.FlightOffersId, x.PackagesId });
                    table.ForeignKey(
                        name: "FK_FlightOfferPackage_Offers_FlightOffersId",
                        column: x => x.FlightOffersId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightOfferPackage_Packages_PackagesId",
                        column: x => x.PackagesId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HotelOfferPackage",
                columns: table => new
                {
                    HotelOffersId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PackagesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelOfferPackage", x => new { x.HotelOffersId, x.PackagesId });
                    table.ForeignKey(
                        name: "FK_HotelOfferPackage_Offers_HotelOffersId",
                        column: x => x.HotelOffersId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelOfferPackage_Packages_PackagesId",
                        column: x => x.PackagesId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionOfferPackage_PackagesId",
                table: "ExcursionOfferPackage",
                column: "PackagesId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightOfferPackage_PackagesId",
                table: "FlightOfferPackage",
                column: "PackagesId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelOfferPackage_PackagesId",
                table: "HotelOfferPackage",
                column: "PackagesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcursionOfferPackage");

            migrationBuilder.DropTable(
                name: "FlightOfferPackage");

            migrationBuilder.DropTable(
                name: "HotelOfferPackage");

            migrationBuilder.CreateTable(
                name: "OfferPackage",
                columns: table => new
                {
                    OffersId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PackagesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferPackage", x => new { x.OffersId, x.PackagesId });
                    table.ForeignKey(
                        name: "FK_OfferPackage_Offers_OffersId",
                        column: x => x.OffersId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferPackage_Packages_PackagesId",
                        column: x => x.PackagesId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OfferPackage_PackagesId",
                table: "OfferPackage",
                column: "PackagesId");
        }
    }
}
