using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AviaTM.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoTransfers_Cargos_IdCargo",
                table: "InfoTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_InfoTransfers_RouteMaps_IdRoute",
                table: "InfoTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_InfoTransfers_Transports_IdTransport",
                table: "InfoTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDats_AspNetUsers_IdUser",
                table: "OrderDats");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDats_InfoTransfers_IdInfoTransfer",
                table: "OrderDats");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDats_OrderMains_IdOrder",
                table: "OrderDats");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDats_TypeUsers_IdTypeUser",
                table: "OrderDats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDats",
                table: "OrderDats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InfoTransfers",
                table: "InfoTransfers");

            migrationBuilder.RenameTable(
                name: "OrderDats",
                newName: "OrderData");

            migrationBuilder.RenameTable(
                name: "InfoTransfers",
                newName: "InfoTransfer");

            migrationBuilder.RenameColumn(
                name: "CapacityWeight",
                table: "Transports",
                newName: "IdRouteMap");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDats_IdUser",
                table: "OrderData",
                newName: "IX_OrderData_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDats_IdTypeUser",
                table: "OrderData",
                newName: "IX_OrderData_IdTypeUser");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDats_IdOrder",
                table: "OrderData",
                newName: "IX_OrderData_IdOrder");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDats_IdInfoTransfer",
                table: "OrderData",
                newName: "IX_OrderData_IdInfoTransfer");

            migrationBuilder.RenameIndex(
                name: "IX_InfoTransfers_IdTransport",
                table: "InfoTransfer",
                newName: "IX_InfoTransfer_IdTransport");

            migrationBuilder.RenameIndex(
                name: "IX_InfoTransfers_IdRoute",
                table: "InfoTransfer",
                newName: "IX_InfoTransfer_IdRoute");

            migrationBuilder.RenameIndex(
                name: "IX_InfoTransfers_IdCargo",
                table: "InfoTransfer",
                newName: "IX_InfoTransfer_IdCargo");

            migrationBuilder.AddColumn<int>(
                name: "AxlePressure",
                table: "Transports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Transports",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "FuelConsumption",
                table: "Transports",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "IdTypeFuel",
                table: "Transports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Transports",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "IdRouteMap",
                table: "Cargos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderData",
                table: "OrderData",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InfoTransfer",
                table: "InfoTransfer",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TransportLoadCapacity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    minValue = table.Column<int>(type: "integer", nullable: false),
                    maxValue = table.Column<int>(type: "integer", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportLoadCapacity", x => x.Id);
                });

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
                name: "IX_Transports_IdRouteMap",
                table: "Transports",
                column: "IdRouteMap");

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_IdRouteMap",
                table: "Cargos",
                column: "IdRouteMap");

            migrationBuilder.CreateIndex(
                name: "IX_TransportTransportLoadCapacity_TransportsId",
                table: "TransportTransportLoadCapacity",
                column: "TransportsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_RouteMaps_IdRouteMap",
                table: "Cargos",
                column: "IdRouteMap",
                principalTable: "RouteMaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoTransfer_Cargos_IdCargo",
                table: "InfoTransfer",
                column: "IdCargo",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoTransfer_RouteMaps_IdRoute",
                table: "InfoTransfer",
                column: "IdRoute",
                principalTable: "RouteMaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoTransfer_Transports_IdTransport",
                table: "InfoTransfer",
                column: "IdTransport",
                principalTable: "Transports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderData_AspNetUsers_IdUser",
                table: "OrderData",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderData_InfoTransfer_IdInfoTransfer",
                table: "OrderData",
                column: "IdInfoTransfer",
                principalTable: "InfoTransfer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderData_OrderMains_IdOrder",
                table: "OrderData",
                column: "IdOrder",
                principalTable: "OrderMains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderData_TypeUsers_IdTypeUser",
                table: "OrderData",
                column: "IdTypeUser",
                principalTable: "TypeUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_RouteMaps_IdRouteMap",
                table: "Transports",
                column: "IdRouteMap",
                principalTable: "RouteMaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_RouteMaps_IdRouteMap",
                table: "Cargos");

            migrationBuilder.DropForeignKey(
                name: "FK_InfoTransfer_Cargos_IdCargo",
                table: "InfoTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_InfoTransfer_RouteMaps_IdRoute",
                table: "InfoTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_InfoTransfer_Transports_IdTransport",
                table: "InfoTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderData_AspNetUsers_IdUser",
                table: "OrderData");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderData_InfoTransfer_IdInfoTransfer",
                table: "OrderData");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderData_OrderMains_IdOrder",
                table: "OrderData");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderData_TypeUsers_IdTypeUser",
                table: "OrderData");

            migrationBuilder.DropForeignKey(
                name: "FK_Transports_RouteMaps_IdRouteMap",
                table: "Transports");

            migrationBuilder.DropTable(
                name: "TransportTransportLoadCapacity");

            migrationBuilder.DropTable(
                name: "TransportLoadCapacity");

            migrationBuilder.DropIndex(
                name: "IX_Transports_IdRouteMap",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Cargos_IdRouteMap",
                table: "Cargos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderData",
                table: "OrderData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InfoTransfer",
                table: "InfoTransfer");

            migrationBuilder.DropColumn(
                name: "AxlePressure",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "FuelConsumption",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "IdTypeFuel",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "IdRouteMap",
                table: "Cargos");

            migrationBuilder.RenameTable(
                name: "OrderData",
                newName: "OrderDats");

            migrationBuilder.RenameTable(
                name: "InfoTransfer",
                newName: "InfoTransfers");

            migrationBuilder.RenameColumn(
                name: "IdRouteMap",
                table: "Transports",
                newName: "CapacityWeight");

            migrationBuilder.RenameIndex(
                name: "IX_OrderData_IdUser",
                table: "OrderDats",
                newName: "IX_OrderDats_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_OrderData_IdTypeUser",
                table: "OrderDats",
                newName: "IX_OrderDats_IdTypeUser");

            migrationBuilder.RenameIndex(
                name: "IX_OrderData_IdOrder",
                table: "OrderDats",
                newName: "IX_OrderDats_IdOrder");

            migrationBuilder.RenameIndex(
                name: "IX_OrderData_IdInfoTransfer",
                table: "OrderDats",
                newName: "IX_OrderDats_IdInfoTransfer");

            migrationBuilder.RenameIndex(
                name: "IX_InfoTransfer_IdTransport",
                table: "InfoTransfers",
                newName: "IX_InfoTransfers_IdTransport");

            migrationBuilder.RenameIndex(
                name: "IX_InfoTransfer_IdRoute",
                table: "InfoTransfers",
                newName: "IX_InfoTransfers_IdRoute");

            migrationBuilder.RenameIndex(
                name: "IX_InfoTransfer_IdCargo",
                table: "InfoTransfers",
                newName: "IX_InfoTransfers_IdCargo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDats",
                table: "OrderDats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InfoTransfers",
                table: "InfoTransfers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InfoTransfers_Cargos_IdCargo",
                table: "InfoTransfers",
                column: "IdCargo",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoTransfers_RouteMaps_IdRoute",
                table: "InfoTransfers",
                column: "IdRoute",
                principalTable: "RouteMaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoTransfers_Transports_IdTransport",
                table: "InfoTransfers",
                column: "IdTransport",
                principalTable: "Transports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDats_AspNetUsers_IdUser",
                table: "OrderDats",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDats_InfoTransfers_IdInfoTransfer",
                table: "OrderDats",
                column: "IdInfoTransfer",
                principalTable: "InfoTransfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDats_OrderMains_IdOrder",
                table: "OrderDats",
                column: "IdOrder",
                principalTable: "OrderMains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDats_TypeUsers_IdTypeUser",
                table: "OrderDats",
                column: "IdTypeUser",
                principalTable: "TypeUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
