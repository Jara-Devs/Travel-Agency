using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Agency_DataBase.Migrations
{
    /// <inheritdoc />
    public partial class offertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_Excursions_ExcursionId",
                table: "Offer");

            migrationBuilder.DropForeignKey(
                name: "FK_Offer_Flights_FlightId",
                table: "Offer");

            migrationBuilder.DropForeignKey(
                name: "FK_Offer_Hotels_HotelId",
                table: "Offer");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferPackage_Offer_OffersId",
                table: "OfferPackage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offer",
                table: "Offer");

            migrationBuilder.RenameTable(
                name: "Offer",
                newName: "Offers");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_Id",
                table: "Offers",
                newName: "IX_Offers_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_HotelId",
                table: "Offers",
                newName: "IX_Offers_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_FlightId",
                table: "Offers",
                newName: "IX_Offers_FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_ExcursionId",
                table: "Offers",
                newName: "IX_Offers_ExcursionId");

            migrationBuilder.AddColumn<int>(
                name: "Availability",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "EndDate",
                table: "Offers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Offers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "StartDate",
                table: "Offers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferPackage_Offers_OffersId",
                table: "OfferPackage",
                column: "OffersId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Excursions_ExcursionId",
                table: "Offers",
                column: "ExcursionId",
                principalTable: "Excursions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Flights_FlightId",
                table: "Offers",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Hotels_HotelId",
                table: "Offers",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferPackage_Offers_OffersId",
                table: "OfferPackage");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Excursions_ExcursionId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Flights_FlightId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Hotels_HotelId",
                table: "Offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Offers");

            migrationBuilder.RenameTable(
                name: "Offers",
                newName: "Offer");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_Id",
                table: "Offer",
                newName: "IX_Offer_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_HotelId",
                table: "Offer",
                newName: "IX_Offer_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_FlightId",
                table: "Offer",
                newName: "IX_Offer_FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_ExcursionId",
                table: "Offer",
                newName: "IX_Offer_ExcursionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offer",
                table: "Offer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_Excursions_ExcursionId",
                table: "Offer",
                column: "ExcursionId",
                principalTable: "Excursions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_Flights_FlightId",
                table: "Offer",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_Hotels_HotelId",
                table: "Offer",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferPackage_Offer_OffersId",
                table: "OfferPackage",
                column: "OffersId",
                principalTable: "Offer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
