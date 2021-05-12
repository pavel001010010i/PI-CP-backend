using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AviaTM.Migrations
{
    public partial class AddRequestAndOrderEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderData");

            migrationBuilder.DropTable(
                name: "Registers");

            migrationBuilder.DropTable(
                name: "RequestDeliveries");

            migrationBuilder.DropTable(
                name: "InfoTransfer");

            migrationBuilder.DropTable(
                name: "OrderMains");

            migrationBuilder.DropTable(
                name: "TypeUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InfoTransfer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdCargo = table.Column<int>(type: "integer", nullable: false),
                    IdRoute = table.Column<int>(type: "integer", nullable: false),
                    IdTransport = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoTransfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoTransfer_Cargos_IdCargo",
                        column: x => x.IdCargo,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoTransfer_RouteMaps_IdRoute",
                        column: x => x.IdRoute,
                        principalTable: "RouteMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoTransfer_Transports_IdTransport",
                        column: x => x.IdTransport,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderMains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Registers",
                columns: table => new
                {
                    Login = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PasswordConfirm = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registers", x => x.Login);
                });

            migrationBuilder.CreateTable(
                name: "RequestDeliveries",
                columns: table => new
                {
                    IdRequest = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CargoId = table.Column<int>(type: "integer", nullable: false),
                    CastDelivery = table.Column<double>(type: "double precision", nullable: false),
                    CountryIdFrom = table.Column<string>(type: "text", nullable: false),
                    CountryIdTo = table.Column<string>(type: "text", nullable: true),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    DateDelivery = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateDeparture = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProviderId = table.Column<int>(type: "integer", nullable: false),
                    StatusRequest = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestDeliveries", x => x.IdRequest);
                    table.ForeignKey(
                        name: "FK_RequestDeliveries_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IdInfoTransfer = table.Column<int>(type: "integer", nullable: false),
                    IdOrder = table.Column<int>(type: "integer", nullable: false),
                    IdTypeUser = table.Column<int>(type: "integer", nullable: false),
                    IdUser = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderData_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderData_InfoTransfer_IdInfoTransfer",
                        column: x => x.IdInfoTransfer,
                        principalTable: "InfoTransfer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderData_OrderMains_IdOrder",
                        column: x => x.IdOrder,
                        principalTable: "OrderMains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderData_TypeUsers_IdTypeUser",
                        column: x => x.IdTypeUser,
                        principalTable: "TypeUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfoTransfer_IdCargo",
                table: "InfoTransfer",
                column: "IdCargo");

            migrationBuilder.CreateIndex(
                name: "IX_InfoTransfer_IdRoute",
                table: "InfoTransfer",
                column: "IdRoute");

            migrationBuilder.CreateIndex(
                name: "IX_InfoTransfer_IdTransport",
                table: "InfoTransfer",
                column: "IdTransport");

            migrationBuilder.CreateIndex(
                name: "IX_OrderData_IdInfoTransfer",
                table: "OrderData",
                column: "IdInfoTransfer");

            migrationBuilder.CreateIndex(
                name: "IX_OrderData_IdOrder",
                table: "OrderData",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_OrderData_IdTypeUser",
                table: "OrderData",
                column: "IdTypeUser");

            migrationBuilder.CreateIndex(
                name: "IX_OrderData_IdUser",
                table: "OrderData",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDeliveries_CargoId",
                table: "RequestDeliveries",
                column: "CargoId");
        }
    }
}
