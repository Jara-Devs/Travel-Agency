using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Agency_DataBase.Migrations
{
    /// <inheritdoc />
    public partial class isSingleOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_Payments_ReserveTourist_PaymentId",
                table: "Reserves");

            migrationBuilder.DropIndex(
                name: "IX_Reserves_ReserveTourist_PaymentId",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "ReserveTourist_PaymentId",
                table: "Reserves");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentId",
                table: "Reserves",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<bool>(
                name: "IsSingleOffer",
                table: "Packages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSingleOffer",
                table: "Packages");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentId",
                table: "Reserves",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ReserveTourist_PaymentId",
                table: "Reserves",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_ReserveTourist_PaymentId",
                table: "Reserves",
                column: "ReserveTourist_PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_Payments_ReserveTourist_PaymentId",
                table: "Reserves",
                column: "ReserveTourist_PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
