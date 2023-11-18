using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Agency_DataBase.Migrations
{
    /// <inheritdoc />
    public partial class entitieswithimages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "TouristPlaces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "TouristActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Hotels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Excursions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TouristPlaces_ImageId",
                table: "TouristPlaces",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_TouristActivities_ImageId",
                table: "TouristActivities",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ImageId",
                table: "Offers",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_ImageId",
                table: "Hotels",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_ImageId",
                table: "Excursions",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_Images_ImageId",
                table: "Excursions",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Images_ImageId",
                table: "Hotels",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Images_ImageId",
                table: "Offers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristActivities_Images_ImageId",
                table: "TouristActivities",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TouristPlaces_Images_ImageId",
                table: "TouristPlaces",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_Images_ImageId",
                table: "Excursions");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Images_ImageId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Images_ImageId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristActivities_Images_ImageId",
                table: "TouristActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_TouristPlaces_Images_ImageId",
                table: "TouristPlaces");

            migrationBuilder.DropIndex(
                name: "IX_TouristPlaces_ImageId",
                table: "TouristPlaces");

            migrationBuilder.DropIndex(
                name: "IX_TouristActivities_ImageId",
                table: "TouristActivities");

            migrationBuilder.DropIndex(
                name: "IX_Offers_ImageId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_ImageId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Excursions_ImageId",
                table: "Excursions");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "TouristPlaces");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "TouristActivities");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Excursions");
        }
    }
}
