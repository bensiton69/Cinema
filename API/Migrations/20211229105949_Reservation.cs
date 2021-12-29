using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Reservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "Seat",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ShowTimeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CostumerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_CostumerUserId",
                        column: x => x.CostumerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_ShowTime_ShowTimeId",
                        column: x => x.ShowTimeId,
                        principalTable: "ShowTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seat_ReservationId",
                table: "Seat",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CostumerUserId",
                table: "Reservations",
                column: "CostumerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ShowTimeId",
                table: "Reservations",
                column: "ShowTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Reservations_ReservationId",
                table: "Seat",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Reservations_ReservationId",
                table: "Seat");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Seat_ReservationId",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Seat");
        }
    }
}
