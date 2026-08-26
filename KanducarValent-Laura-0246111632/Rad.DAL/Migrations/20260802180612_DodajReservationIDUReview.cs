using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rad.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DodajReservationIDUReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationID",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReservationID",
                table: "Reviews",
                column: "ReservationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Reservations_ReservationID",
                table: "Reviews",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Reservations_ReservationID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReservationID",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReservationID",
                table: "Reviews");
        }
    }
}
