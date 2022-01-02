using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class seatsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Reservation_ReservationId",
                table: "Seat");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Venues_VenueNumber",
                table: "Seat");

            migrationBuilder.DropForeignKey(
                name: "FK_SeatPackage_Seat_SeatId",
                table: "SeatPackage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seat",
                table: "Seat");

            migrationBuilder.RenameTable(
                name: "Seat",
                newName: "Seats");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_VenueNumber",
                table: "Seats",
                newName: "IX_Seats_VenueNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_ReservationId",
                table: "Seats",
                newName: "IX_Seats_ReservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatPackage_Seats_SeatId",
                table: "SeatPackage",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Reservation_ReservationId",
                table: "Seats",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_Venues_VenueNumber",
                table: "Seats",
                column: "VenueNumber",
                principalTable: "Venues",
                principalColumn: "VenueNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatPackage_Seats_SeatId",
                table: "SeatPackage");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Reservation_ReservationId",
                table: "Seats");

            migrationBuilder.DropForeignKey(
                name: "FK_Seats_Venues_VenueNumber",
                table: "Seats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            migrationBuilder.RenameTable(
                name: "Seats",
                newName: "Seat");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_VenueNumber",
                table: "Seat",
                newName: "IX_Seat_VenueNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Seats_ReservationId",
                table: "Seat",
                newName: "IX_Seat_ReservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seat",
                table: "Seat",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Reservation_ReservationId",
                table: "Seat",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Venues_VenueNumber",
                table: "Seat",
                column: "VenueNumber",
                principalTable: "Venues",
                principalColumn: "VenueNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatPackage_Seat_SeatId",
                table: "SeatPackage",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
