using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Agency_DataBase.Migrations
{
    /// <inheritdoc />
    public partial class Offers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelOffer_Hotels_HotelId",
                table: "HotelOffer");

            migrationBuilder.DropTable(
                name: "ExcursionOffer");

            migrationBuilder.DropTable(
                name: "FlightOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelOffer",
                table: "HotelOffer");

            migrationBuilder.RenameTable(
                name: "HotelOffer",
                newName: "Offer");

            migrationBuilder.RenameIndex(
                name: "IX_HotelOffer_HotelId",
                table: "Offer",
                newName: "IX_Offer_HotelId");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Offer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Offer",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ExcursionId",
                table: "Offer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "Offer",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offer",
                table: "Offer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_ExcursionId",
                table: "Offer",
                column: "ExcursionId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_FlightId",
                table: "Offer",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_Id",
                table: "Offer",
                column: "Id",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offer",
                table: "Offer");

            migrationBuilder.DropIndex(
                name: "IX_Offer_ExcursionId",
                table: "Offer");

            migrationBuilder.DropIndex(
                name: "IX_Offer_FlightId",
                table: "Offer");

            migrationBuilder.DropIndex(
                name: "IX_Offer_Id",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "ExcursionId",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "Offer");

            migrationBuilder.RenameTable(
                name: "Offer",
                newName: "HotelOffer");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_HotelId",
                table: "HotelOffer",
                newName: "IX_HotelOffer_HotelId");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "HotelOffer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelOffer",
                table: "HotelOffer",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ExcursionOffer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExcursionId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcursionOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcursionOffer_Excursions_ExcursionId",
                        column: x => x.ExcursionId,
                        principalTable: "Excursions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FlightOffer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightOffer_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionOffer_ExcursionId",
                table: "ExcursionOffer",
                column: "ExcursionId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightOffer_FlightId",
                table: "FlightOffer",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelOffer_Hotels_HotelId",
                table: "HotelOffer",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
