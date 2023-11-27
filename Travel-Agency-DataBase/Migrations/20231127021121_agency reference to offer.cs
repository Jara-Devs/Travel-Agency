using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Agency_DataBase.Migrations
{
    /// <inheritdoc />
    public partial class agencyreferencetooffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Offers_AgencyId",
                table: "Offers",
                column: "AgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Agencies_AgencyId",
                table: "Offers",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Agencies_AgencyId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_AgencyId",
                table: "Offers");
        }
    }
}
