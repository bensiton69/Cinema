using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class CostumerReservationRealitaionship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_CostumerUserId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CostumerUserId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CostumerUserId",
                table: "Reservations");

            migrationBuilder.AddColumn<Guid>(
                name: "CostumerId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CostumerId",
                table: "Reservations",
                column: "CostumerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_CostumerId",
                table: "Reservations",
                column: "CostumerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_CostumerId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CostumerId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CostumerId",
                table: "Reservations");

            migrationBuilder.AddColumn<Guid>(
                name: "CostumerUserId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CostumerUserId",
                table: "Reservations",
                column: "CostumerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_CostumerUserId",
                table: "Reservations",
                column: "CostumerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
