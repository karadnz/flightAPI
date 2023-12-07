using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flightAPI.Migrations
{
    public partial class Route5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Airports_DestinationAirportId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Airports_OriginAirportId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Routes");

            migrationBuilder.RenameColumn(
                name: "OriginAirportId",
                table: "Routes",
                newName: "DepartureAirportId");

            migrationBuilder.RenameColumn(
                name: "DestinationAirportId",
                table: "Routes",
                newName: "ArrivalAirportId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_OriginAirportId",
                table: "Routes",
                newName: "IX_Routes_DepartureAirportId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_DestinationAirportId",
                table: "Routes",
                newName: "IX_Routes_ArrivalAirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Airports_ArrivalAirportId",
                table: "Routes",
                column: "ArrivalAirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Airports_DepartureAirportId",
                table: "Routes",
                column: "DepartureAirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Airports_ArrivalAirportId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Airports_DepartureAirportId",
                table: "Routes");

            migrationBuilder.RenameColumn(
                name: "DepartureAirportId",
                table: "Routes",
                newName: "OriginAirportId");

            migrationBuilder.RenameColumn(
                name: "ArrivalAirportId",
                table: "Routes",
                newName: "DestinationAirportId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_DepartureAirportId",
                table: "Routes",
                newName: "IX_Routes_OriginAirportId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_ArrivalAirportId",
                table: "Routes",
                newName: "IX_Routes_DestinationAirportId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Routes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Airports_DestinationAirportId",
                table: "Routes",
                column: "DestinationAirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Airports_OriginAirportId",
                table: "Routes",
                column: "OriginAirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
