using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class MoviesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    ProductionYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venue",
                columns: table => new
                {
                    VenueNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowsNumber = table.Column<int>(type: "int", nullable: false),
                    ColsNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue", x => x.VenueNumber);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    ColNumber = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsHandicapped = table.Column<bool>(type: "bit", nullable: false),
                    VenueNumber = table.Column<int>(type: "int", nullable: true),
                    VenueNumber1 = table.Column<int>(type: "int", nullable: true),
                    VenueNumber2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seat_Venue_VenueNumber",
                        column: x => x.VenueNumber,
                        principalTable: "Venue",
                        principalColumn: "VenueNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Seat_Venue_VenueNumber1",
                        column: x => x.VenueNumber1,
                        principalTable: "Venue",
                        principalColumn: "VenueNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Seat_Venue_VenueNumber2",
                        column: x => x.VenueNumber2,
                        principalTable: "Venue",
                        principalColumn: "VenueNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShowTime",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VenueNumber = table.Column<int>(type: "int", nullable: true),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShowTime_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShowTime_Venue_VenueNumber",
                        column: x => x.VenueNumber,
                        principalTable: "Venue",
                        principalColumn: "VenueNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seat_VenueNumber",
                table: "Seat",
                column: "VenueNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_VenueNumber1",
                table: "Seat",
                column: "VenueNumber1");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_VenueNumber2",
                table: "Seat",
                column: "VenueNumber2");

            migrationBuilder.CreateIndex(
                name: "IX_ShowTime_MovieId",
                table: "ShowTime",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowTime_VenueNumber",
                table: "ShowTime",
                column: "VenueNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "ShowTime");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Venue");
        }
    }
}
