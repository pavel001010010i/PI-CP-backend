using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AviaTM.Migrations
{
    public partial class init9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportTransportLoadCapacity");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Transports");

            migrationBuilder.AddColumn<int>(
                name: "IdTransLodaCapacity",
                table: "Transports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transports_IdTransLodaCapacity",
                table: "Transports",
                column: "IdTransLodaCapacity");

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_TransportLoadCapacity_IdTransLodaCapacity",
                table: "Transports",
                column: "IdTransLodaCapacity",
                principalTable: "TransportLoadCapacity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transports_TransportLoadCapacity_IdTransLodaCapacity",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Transports_IdTransLodaCapacity",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "IdTransLodaCapacity",
                table: "Transports");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Transports",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Transports",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "TransportTransportLoadCapacity",
                columns: table => new
                {
                    TransportLoadCapacitysId = table.Column<int>(type: "integer", nullable: false),
                    TransportsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportTransportLoadCapacity", x => new { x.TransportLoadCapacitysId, x.TransportsId });
                    table.ForeignKey(
                        name: "FK_TransportTransportLoadCapacity_TransportLoadCapacity_Transp~",
                        column: x => x.TransportLoadCapacitysId,
                        principalTable: "TransportLoadCapacity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportTransportLoadCapacity_Transports_TransportsId",
                        column: x => x.TransportsId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportTransportLoadCapacity_TransportsId",
                table: "TransportTransportLoadCapacity",
                column: "TransportsId");
        }
    }
}
