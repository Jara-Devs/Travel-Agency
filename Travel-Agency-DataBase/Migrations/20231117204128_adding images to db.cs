using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Agency_DataBase.Migrations
{
    /// <inheritdoc />
    public partial class addingimagestodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_TouristPlaces_Place1Id",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_TouristPlaces_Place2Id",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_Users_ReserveTourist_UserId",
                table: "Reserves");

            migrationBuilder.DropIndex(
                name: "IX_Reserves_ReserveTourist_UserId",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Reserves_UserIdentities");

            migrationBuilder.DropColumn(
                name: "ReserveTourist_UserId",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "UserIdentity_Address",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "Place2Id",
                table: "Flights",
                newName: "OriginId");

            migrationBuilder.RenameColumn(
                name: "Place1Id",
                table: "Flights",
                newName: "DestinationId");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_Place2Id",
                table: "Flights",
                newName: "IX_Flights_OriginId");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_Place1Id",
                table: "Flights",
                newName: "IX_Flights_DestinationId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Reserves",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Id",
                table: "Images",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_Url",
                table: "Images",
                column: "Url",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_TouristPlaces_DestinationId",
                table: "Flights",
                column: "DestinationId",
                principalTable: "TouristPlaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_TouristPlaces_OriginId",
                table: "Flights",
                column: "OriginId",
                principalTable: "TouristPlaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_TouristPlaces_DestinationId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_TouristPlaces_OriginId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.RenameColumn(
                name: "OriginId",
                table: "Flights",
                newName: "Place2Id");

            migrationBuilder.RenameColumn(
                name: "DestinationId",
                table: "Flights",
                newName: "Place1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_OriginId",
                table: "Flights",
                newName: "IX_Flights_Place2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_DestinationId",
                table: "Flights",
                newName: "IX_Flights_Place1Id");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Reserves_UserIdentities",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Reserves",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ReserveTourist_UserId",
                table: "Reserves",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdentity_Address",
                table: "Payments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_ReserveTourist_UserId",
                table: "Reserves",
                column: "ReserveTourist_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_TouristPlaces_Place1Id",
                table: "Flights",
                column: "Place1Id",
                principalTable: "TouristPlaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_TouristPlaces_Place2Id",
                table: "Flights",
                column: "Place2Id",
                principalTable: "TouristPlaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_Users_ReserveTourist_UserId",
                table: "Reserves",
                column: "ReserveTourist_UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
