using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class SeatsVenue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VenueNumber",
                table: "Seat",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seat_VenueNumber",
                table: "Seat",
                column: "VenueNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Venues_VenueNumber",
                table: "Seat",
                column: "VenueNumber",
                principalTable: "Venues",
                principalColumn: "VenueNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Venues_VenueNumber",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Seat_VenueNumber",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "VenueNumber",
                table: "Seat");
        }
    }
}
